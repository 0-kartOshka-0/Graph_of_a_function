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
    public partial class Options : Form
    {
        public Options()
        {
            InitializeComponent();
            comboBox1.Items.Add("Точечный");
            comboBox1.Items.Add("Гладкая");
            switch (ClassForOpt.ChartType)
            {
                case SeriesChartType.Spline:
                    comboBox1.SelectedIndex = 1;
                    break;
                case SeriesChartType.Point:
                    comboBox1.SelectedIndex = 0;
                    break;
            }
            checkBox1.Checked = ClassForOpt.Legend;
            checkBox2.Checked = ClassForOpt.Title;
            numericUpDown1.Value = ClassForOpt.Width;
            button2.BackColor = ClassForOpt.LineColor;
            button3.BackColor = ClassForOpt.BackgroundColor;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
                button2.BackColor = colorDialog1.Color;
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            if (colorDialog2.ShowDialog() == DialogResult.OK)
                button3.BackColor = colorDialog2.Color;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            ClassForOpt.Legend = checkBox1.Checked;
            ClassForOpt.Title = checkBox2.Checked;
            ClassForOpt.Width = (int)numericUpDown1.Value;
            ClassForOpt.LineColor = button2.BackColor;
            ClassForOpt.BackgroundColor = button3.BackColor;
            switch (comboBox1.SelectedItem.ToString())
            {
                case "Гладкая":
                    ClassForOpt.ChartType = SeriesChartType.Spline;
                    break;
                case "Точечный":
                    ClassForOpt.ChartType = SeriesChartType.Point;
                    break;
            }
            Close();
        }
    }
}
