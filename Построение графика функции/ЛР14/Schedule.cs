using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ЛР14
{
    public partial class Schedule : Form
    {
        public List<double> _xValue;
        public List<double> _yValue;
        private string _graphName;
        private static Schedule singleton;

        public static Schedule FormFabric(double[] xValues, double[] yValues, string graphName) //создаем "фабрику" элементов, чтобы форма не закрывалась при изменении значений (возвращалась та же форма)
        {
            if (singleton == null)
            {
                singleton = new Schedule(xValues, yValues, graphName);
            }
            else
            {
                singleton._xValue = xValues.ToList<double>();
                singleton._yValue = yValues.ToList<double>();
                singleton._graphName = graphName;
                singleton.LoadChart();
            }
            return singleton;
        }
        private Schedule(double[] xValues, double[] yValues, string graphName)
        {
            InitializeComponent();
            _xValue = xValues.ToList<double>();
            _yValue = yValues.ToList<double>();
            _graphName = graphName;
            LoadChart();
        }

        private void LoadChart()
        {
            chart1.Series.Clear();
            chart1.Titles.Clear();
            chart1.Series.Add(new Series(_graphName)
            {
                ChartType = ClassForOpt.ChartType,
                BorderWidth = ClassForOpt.Width,
                Color = ClassForOpt.LineColor,
                IsVisibleInLegend = ClassForOpt.Legend
            });
            chart1.ChartAreas[0].AxisX.Maximum = _xValue.Max(); //Изменяем размер масштаб графика, для красоты отображения
            chart1.ChartAreas[0].AxisY.Maximum = _yValue.Max();
            chart1.ChartAreas[0].AxisX.Minimum = _xValue.Min();
            chart1.ChartAreas[0].AxisY.Minimum = _yValue.Min();
            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "{0.000}";
            chart1.ChartAreas[0].AxisY.LabelStyle.Format = "{0.000}";
            if (ClassForOpt.Title)
                chart1.Titles.Add(_graphName);
            chart1.BackColor = ClassForOpt.BackgroundColor;
            for (int i = 0; i < _xValue.Count; i++)
            {
                chart1.Series[_graphName].Points.AddXY(_xValue[i], _yValue[i]);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "png files|*.png";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                chart1.SaveImage(saveFileDialog1.FileName, ChartImageFormat.Png);
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            var options = new Options();
            options.ShowDialog();
            LoadChart();
        }

        private void Schedule_FormClosed(object sender, FormClosedEventArgs e)
        {
            singleton = null;
        }
    }
}
