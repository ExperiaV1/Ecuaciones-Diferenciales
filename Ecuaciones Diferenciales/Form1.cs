using ScottPlot;
using ScottPlot.WinForms;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace Ecuaciones_Diferenciales
{
    public partial class Form1 : Form
    {
        private List<IteracionEuler> _ultimaSimulacion = new();
        private DataGridView _tablaEuler = null!; 

        public Form1()
        {
            InitializeComponent();
            InicializarValoresYTablas();
        }

        private void InicializarValoresYTablas()
        {
            //this.Text = "Simulación Numérica - Método de Euler";

            // 1. Cargar valores sugeridos por defecto para el circuito RC
            CargarValoresCircuitoRC();

            // 2. Configurar el ComboBox con las 3 opciones
            cmbModelo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbModelo.Items.Clear();
            cmbModelo.Items.Add("Ecuación Teórica");
            cmbModelo.Items.Add("Circuito Serie");
            cmbModelo.Items.Add("ejericio ");
            cmbModelo.SelectedIndex = 0;

            cmbModelo.SelectedIndexChanged += (s, e) =>
            {
                if (cmbModelo.SelectedIndex == 2)
                {
                    txtVc0.Text = "1";   // y(1) = 1 -> y0 = 1
                    txtT0.Text = "1";    // x0 = 1
                    txtTf.Text = "1.5";  // Evaluar hasta x = 1.5
                    txtH.Text = "0.1";   // Primer paso h = 0.1
                }
                else
                {
                    CargarValoresCircuitoRC();
                }
            };

            // 3. Configurar la tabla de iteraciones (dgvEule) de forma segura
            _tablaEuler = ObtenerOCrearTabla(dgvEule);
            _tablaEuler.AutoGenerateColumns = true;
            _tablaEuler.ReadOnly = true;
            _tablaEuler.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // 4. Configurar la tabla experimental (dgvExperimental)
            dgvExperimental.Columns.Clear();
            dgvExperimental.Columns.Add("Tiempo", "Tiempo (s)");
            dgvExperimental.Columns.Add("Euler", "Euler (V)");
            dgvExperimental.Columns.Add("Experimental", "Experimental (V)");
            dgvExperimental.Columns.Add("Error", "Error (%)");


            dgvExperimental.Columns["Euler"].ReadOnly = true;
            dgvExperimental.Columns["Error"].ReadOnly = true;
            dgvExperimental.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Datos experimentales medidos en el circuito para la presentación final
            double[,] datosOsciloscopio = {
                { 0.00, 0.000 },
                { 0.01, 0.048 },
                { 0.02, 0.095 },
                { 0.03, 0.141 },
                { 0.04, 0.185 },
                { 0.05, 0.230 },
                { 0.06, 0.273 },
                { 0.10, 0.432 },
                { 0.50, 1.550 },
                { 1.00, 2.450 },
                { 2.00, 3.600 },
                { 5.00, 4.300 }
            };

            for (int i = 0; i < datosOsciloscopio.GetLength(0); i++)
            {
                string t = datosOsciloscopio[i, 0].ToString("0.00", CultureInfo.InvariantCulture);
                string vExp = datosOsciloscopio[i, 1].ToString("0.000", CultureInfo.InvariantCulture);
                dgvExperimental.Rows.Add(t, "", vExp, "");
            }

            dgvExperimental.CellEndEdit += (s, e) => ActualizarTablaExperimental();
        }

        private void CargarValoresCircuitoRC()
        {
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
        }

        // Detecta si dgvEule es un DataGridView o una pestaña (TabPage) y evita errores
        private DataGridView ObtenerOCrearTabla(Control control)
        {
            if (control is DataGridView dgvExistente)
                return dgvExistente;

            var nuevaTabla = new DataGridView { Dock = DockStyle.Fill };
            control.Controls.Add(nuevaTabla);
            return nuevaTabla;
        }

        // Cálculo de la derivada f(t, Vc) o f(x, y) según la opción elegida
        private double CalcularDerivada(double t, double vc, double vs, double rOhms, double cFaradios, double iS, double n, double vt, int indiceModelo)
        {
            if (indiceModelo == 0)
            {
                // Opción 0: Ecuación Teórica del PDF
                double iR = (vs - vc) / rOhms;
                double exponente = Math.Min(vc / (n * vt), 50.0);
                double iD = iS * (Math.Exp(exponente) - 1.0);
                return (1.0 / cFaradios) * (iR - iD);
            }
            else if (indiceModelo == 1)
            {
                // Opción 1: Modelo serie real (Vs -> R -> D -> C)
                if (vs <= vc) return 0;
                double vdEstimado = Math.Min(0.7, vs - vc);
                return (vs - vdEstimado - vc) / (rOhms * cFaradios);
            }
            else
            {
                // Opción 2: Ejercicio matemático y' = 0.2 * x * y (donde t = x, vc = y)
                return 0.2 * t * vc;
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

            int indiceModelo = cmbModelo.SelectedIndex;

            var lista = new List<IteracionEuler>();
            int iteracion = 0;
            double t = t0;

            while (t <= tf + (pasoH / 2.0))
            {
                double f = CalcularDerivada(t, vc, vs, rOhms, cFaradios, iS, n, vt, indiceModelo);
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

        // BOTÓN SIMULAR
        private void btnSimular_Click(object sender, EventArgs e)
        {
            try
            {
                double h = LeerNumero(txtH.Text);
                _ultimaSimulacion = EjecutarEuler(h);

                // 1. Llenar la tabla de iteraciones
                _tablaEuler.DataSource = null;
                _tablaEuler.DataSource = _ultimaSimulacion;

                // 2. Graficar en formsPlot1
                formsPlot1.Plot.Clear();
                double[] xs = _ultimaSimulacion.Select(x => x.t_n).ToArray();
                double[] ys = _ultimaSimulacion.Select(x => x.Vc_n).ToArray();

                var curva = formsPlot1.Plot.Add.Scatter(xs, ys);
                curva.LegendText = $"Euler (h = {h})";
                curva.LineWidth = 2;

                if (cmbModelo.SelectedIndex == 2)
                {
                    formsPlot1.Plot.Title("Solución Numérica de y' = 0.2xy con y(1) = 1");
                    formsPlot1.Plot.XLabel("Variable independiente x");
                    formsPlot1.Plot.YLabel("Variable dependiente y(x)");
                }
                else
                {
                    formsPlot1.Plot.Title("Evolución Temporal del Voltaje en el Capacitor Vc(t)");
                    formsPlot1.Plot.XLabel("Tiempo t (s)");
                    formsPlot1.Plot.YLabel("Voltaje Vc (V)");

                    // Solo superpone los puntos del osciloscopio cuando estamos en los modelos del circuito
                    ActualizarTablaExperimental();
                }

                formsPlot1.Plot.ShowLegend();
                formsPlot1.Plot.Axes.AutoScale();
                formsPlot1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Verifica que todos los campos numéricos sean válidos.\nDetalle: " + ex.Message,
                                "Error de entrada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // BOTÓN COMPARAR PASOS h
        private void btnCompararH_Click(object sender, EventArgs e)
        {
            try
            {
                // Si está elegido el ejercicio y' = 0.2xy, compara h = 0.1 y h = 0.05 como pide el enunciado
                double[] pasos = cmbModelo.SelectedIndex == 2
                    ? new double[] { 0.1, 0.05  }
                    : new double[] { 0.1, 0.01, 0.001 };

                formsPlot1.Plot.Clear();

                foreach (double h in pasos)
                {
                    var datos = EjecutarEuler(h);
                    double[] xs = datos.Select(x => x.t_n).ToArray();
                    double[] ys = datos.Select(x => x.Vc_n).ToArray();

                    var scatter = formsPlot1.Plot.Add.Scatter(xs, ys);
                    scatter.LegendText = $"Paso h = {h}";
                    scatter.MarkerSize = 5;
                    scatter.LineWidth = 2;
                }

                if (cmbModelo.SelectedIndex == 2)
                {
                    formsPlot1.Plot.Title("Comparación de y' = 0.2xy para h = 0.1 y h = 0.05");
                    formsPlot1.Plot.XLabel("x");
                    formsPlot1.Plot.YLabel("y(x)");
                }
                else
                {
                    formsPlot1.Plot.Title("Comparación del Método de Euler con distintos pasos h");
                    formsPlot1.Plot.XLabel("Tiempo t (s)");
                    formsPlot1.Plot.YLabel("Voltaje Vc (V)");
                }

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
                        else if (vExp == 0 && puntoEuler.Vc_n == 0)
                        {
                            fila.Cells["Error"].Value = "0.00 %";
                        }

                        tiemposExp.Add(tBuscado);
                        voltajesExp.Add(vExp);
                    }
                }
            }

            if (tiemposExp.Count > 0 && cmbModelo.SelectedIndex != 2)
            {
                var puntos = formsPlot1.Plot.Add.Scatter(tiemposExp.ToArray(), voltajesExp.ToArray());
                puntos.LegendText = "Puntos Experimentales (DAQ)";
                puntos.LineWidth = 0;
                puntos.MarkerSize = 8;
                formsPlot1.Plot.ShowLegend();
                formsPlot1.Plot.Axes.AutoScale();
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