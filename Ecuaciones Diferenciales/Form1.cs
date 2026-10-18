using ScottPlot;
using ScottPlot.WinForms;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Ecuaciones_Diferenciales
{
    public partial class Form1 : Form
    {
        private List<IteracionEuler> _ultimaSimulacion = new();

        public Form1()
        {
            InitializeComponent();
            InicializarValoresYTablas();
        }
        private void InicializarValoresYTablas()
        {
            this.Text = "Simulación de Circuito RC con Diodo - Método de Euler";

            // Cargar valores sugeridos 
            txtVs.Text = "5";       // 5 V
            txtR.Text = "1000";     // 1 kΩ = 1000 Ω
            txtC.Text = "1000";     // 1000 µF
            txtIs.Text = "1E-12";   // 10^-12 A
            txtN.Text = "1.5";      // Factor de idealidad (1 a 2)
            txtVt.Text = "0.02585"; // Voltaje térmico (V)
            txtVc0.Text = "0";      // Condición inicial Vc(0) = 0 V
            txtT0.Text = "0";       // Tiempo inicial = 0 s
            txtTf.Text = "5";       // Tiempo final = 5 s
            txtH.Text = "0.01";     // Paso h = 0.01 s

            // 2. Configurar el ComboBox 
            cmbModelo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbModelo.Items.Clear();
            cmbModelo.Items.Add("Ecuación Teorica");
            cmbModelo.Items.Add("Circuito Serie ");
            cmbModelo.SelectedIndex = 0;

            // 3. Configurar la tabla de iteraciones (dgvEule)
            dgvEule.AutoGenerateColumns = true;
            dgvEule.ReadOnly = true;
            dgvEule.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // 4. Configurar la tabla experimental (dgvExperimental)
            dgvExperimental.Columns.Clear();
            dgvExperimental.Columns.Add("Tiempo", "Tiempo (s)");
            dgvExperimental.Columns.Add("Euler", "Euler (V)");
            dgvExperimental.Columns.Add("Experimental", "Experimental (V)");
            dgvExperimental.Columns.Add("Error", "Error (%)");

            dgvExperimental.Columns["Euler"].ReadOnly = true;
            dgvExperimental.Columns["Error"].ReadOnly = true;
            dgvExperimental.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Tiempos de muestra para comparar con tu circuito real
            double[] tiemposEjemplo = { 0.0, 0.1, 0.2, 0.3, 0.5, 1.0, 2.0, 3.0, 4.0, 5.0 };
            foreach (double t in tiemposEjemplo)
            {
                dgvExperimental.Rows.Add(t.ToString("0.0", CultureInfo.InvariantCulture), "", "", "");
            }

            dgvExperimental.CellEndEdit += (s, e) => ActualizarTablaExperimental();
        }

        private double CalcularDerivada(double vc, double vs, double rOhms, double cFaradios, double iS, double n, double vt, bool usarModeloReal)
        {
            if (!usarModeloReal)
            {
                // Ecuación 
                double iR = (vs - vc) / rOhms;
                double exponente = Math.Min(vc / (n * vt), 50.0);
                double iD = iS * (Math.Exp(exponente) - 1.0);
                return (1.0 / cFaradios) * (iR - iD);
            }
            else
            {
                // Modelo serie real (Vs -> R -> D -> C)
                if (vs <= vc) return 0;
                double vdEstimado = Math.Min(0.7, vs - vc);
                return (vs - vdEstimado - vc) / (rOhms * cFaradios);
            }
        }

        private List<IteracionEuler> EjecutarEuler(double pasoH)
        {
            double vs = LeerNumero(txtVs.Text);
            double rOhms = LeerNumero(txtR.Text);
            double cFaradios = LeerNumero(txtC.Text) * 1e-6; // Convierte de µF a Faradios

            double iS = LeerNumero(txtIs.Text);
            double n = LeerNumero(txtN.Text);
            double vt = LeerNumero(txtVt.Text);

            double vc = LeerNumero(txtVc0.Text);
            double t0 = LeerNumero(txtT0.Text);
            double tf = LeerNumero(txtTf.Text);

            bool usarModeloReal = cmbModelo.SelectedIndex == 1;

            var lista = new List<IteracionEuler>();
            int iteracion = 0;
            double t = t0;

            while (t <= tf + (pasoH / 2.0))
            {
                double f = CalcularDerivada(vc, vs, rOhms, cFaradios, iS, n, vt, usarModeloReal);
                double vcSiguiente = vc + pasoH * f;

                lista.Add(new IteracionEuler
                {
                    n = iteracion,
                    t_n = Math.Round(t, 5),
                    Vc_n = Math.Round(vc, 6),
                    f_tn_Vcn = Math.Round(f, 6),
                    Vc_n1 = Math.Round(vcSiguiente, 6)
                });

                vc = vcSiguiente;
                t += pasoH;
                iteracion++;
            }

            return lista;
        }

        private void btnSimular_Click(object sender, EventArgs e)
        {
            try
            {
                double h = LeerNumero(txtH.Text);
                _ultimaSimulacion = EjecutarEuler(h);

                // 1. Llenar la tabla de iteraciones
                dgvEule.DataSource = null;
                dgvEule.DataSource = _ultimaSimulacion;

                // 2. Graficar en formsPlot1
                formsPlot1.Plot.Clear();
                double[] xs = _ultimaSimulacion.Select(x => x.t_n).ToArray();
                double[] ys = _ultimaSimulacion.Select(x => x.Vc_n).ToArray();

                var curva = formsPlot1.Plot.Add.Scatter(xs, ys);
                curva.LegendText = $"Euler (h = {h} s)";
                curva.LineWidth = 2;

                formsPlot1.Plot.Title("Evolución Temporal del Voltaje en el Capacitor Vc(t)");
                formsPlot1.Plot.XLabel("Tiempo t (s)");
                formsPlot1.Plot.YLabel("Voltaje Vc (V)");
                formsPlot1.Plot.ShowLegend();
                formsPlot1.Plot.Axes.AutoScale();
                formsPlot1.Refresh();

                // 3. Actualizar la tabla experimental
                ActualizarTablaExperimental();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Verifica que todos los campos numéricos sean válidos.\nDetalle: " + ex.Message,
                                "Error de entrada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCompararH_Click(object sender, EventArgs e)
        {
            try
            {
                double[] pasos = { 0.1, 0.01, 0.001 };
                formsPlot1.Plot.Clear();

                foreach (double h in pasos)
                {
                    var datos = EjecutarEuler(h);
                    double[] xs = datos.Select(x => x.t_n).ToArray();
                    double[] ys = datos.Select(x => x.Vc_n).ToArray();

                    var scatter = formsPlot1.Plot.Add.Scatter(xs, ys);
                    scatter.LegendText = $"Paso h = {h} s";
                    scatter.MarkerSize = 0;
                    scatter.LineWidth = 2;
                }

                formsPlot1.Plot.Title("Comparación del Método de Euler con distintos pasos h");
                formsPlot1.Plot.XLabel("Tiempo t (s)");
                formsPlot1.Plot.YLabel("Voltaje Vc (V)");
                formsPlot1.Plot.ShowLegend();
                formsPlot1.Plot.Axes.AutoScale();
                formsPlot1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al comparar pasos: " + ex.Message);
            }
        }
        private void ActualizarTablaExperimental()
        {
            if (_ultimaSimulacion.Count == 0) return;

            var tiemposExp = new List<double>();
            var voltajesExp = new List<double>();

            foreach (DataGridViewRow fila in dgvExperimental.Rows)
            {
                if (fila.IsNewRow) continue;

                string? strT = Convert.ToString(fila.Cells["Tiempo"].Value);
                if (!string.IsNullOrWhiteSpace(strT) &&
                    double.TryParse(strT.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out double tBuscado))
                {
                    var puntoEuler = _ultimaSimulacion.OrderBy(x => Math.Abs(x.t_n - tBuscado)).First();
                    fila.Cells["Euler"].Value = puntoEuler.Vc_n.ToString("F4");

                    string? strVExp = Convert.ToString(fila.Cells["Experimental"].Value);
                    if (!string.IsNullOrWhiteSpace(strVExp) &&
                        double.TryParse(strVExp.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out double vExp))
                    {
                        if (vExp != 0)
                        {
                            double errorPorcentual = Math.Abs((vExp - puntoEuler.Vc_n) / vExp) * 100.0;
                            fila.Cells["Error"].Value = errorPorcentual.ToString("F2") + " %";
                        }
                        tiemposExp.Add(tBuscado);
                        voltajesExp.Add(vExp);
                    }
                }
            }

            if (tiemposExp.Count > 0)
            {
                var puntos = formsPlot1.Plot.Add.Scatter(tiemposExp.ToArray(), voltajesExp.ToArray());
                puntos.LegendText = "Experimental";
                puntos.LineWidth = 0;
                puntos.MarkerSize = 8;
                formsPlot1.Plot.ShowLegend();
                formsPlot1.Refresh();
            }
        }
        private double LeerNumero(string texto)
        {
            return double.Parse(texto.Trim().Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture);
        }
    }
    public class IteracionEuler
    {
        public int n { get; set; }
        public double t_n { get; set; }
        public double Vc_n { get; set; }
        public double f_tn_Vcn { get; set; }
        public double Vc_n1 { get; set; }
    }
}
