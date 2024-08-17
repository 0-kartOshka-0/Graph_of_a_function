using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ЛР14
{
    public partial class Form1 : Form
    {
        public double[] _xValue;
        public double[] _yValue;
        public Form1()
        {
            InitializeComponent();
        }
        private static void Function(Func<double, double> F, double Xn, double Xk, double dX, out double[] xValue, out double[] yValue)
        {
            var count = (int)Math.Ceiling((Xk - Xn) / dX) + 1;
            xValue = new double[count];
            yValue = new double[count];

            for (int i = 0; i < count; i++)
            {
                xValue[i] = Xn;
                yValue[i] = F(Xn);
                Xn += dX;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            var xn = double.Parse(textBox1.Text);
            var xk = double.Parse(textBox2.Text);
            var dx = double.Parse(textBox3.Text);
            var a = double.Parse(textBox4.Text);
            var selectedItem = comboBox1.SelectedItem.ToString();
            Func<double, double> func = x => Math.Sin(x) + Math.Cos(x);
            if (selectedItem == "y = a*e^x")
                func = x => a * Math.Exp(x);
            if (selectedItem == "y = a*e^2x")
                func = x => a * Math.Exp(2 * x);
            if (selectedItem == "y = -a*e^x")
                func = x => -a * Math.Exp(x);

            Function(func, radioButton1.Checked ? xn / 180.0 * Math.PI : xn,
                radioButton1.Checked ? xk / 180.0 * Math.PI : xk,
                radioButton1.Checked ? dx / 180.0 * Math.PI : dx,
                out _xValue, out _yValue);
            if (radioButton1.Checked)                       //функция считает в радианах, переведем их в градусы для удобства
            {
                for (int i = 0; i < _xValue.Length; i++)
                {
                    _xValue[i] = _xValue[i] / Math.PI * 180;
                }
            }
            FillDataGridFromArr(_xValue, 0, "X");
            FillDataGridFromArr(_yValue, 1, "Y");
            var graph = Schedule.FormFabric(_xValue, _yValue, selectedItem);
            graph.Show();
        }
        private void FillDataGridFromArr(double[] arr, int columnIndex, string name)
        {
            dataGridView1.RowCount = arr.Length;
            dataGridView1.Columns[columnIndex].Name = name;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {

                dataGridView1.Rows[i].Cells[columnIndex].Value = Math.Round(arr[i], 4);
            }
        }
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Back)
                return;
            if (e.KeyChar == 44)
            {
                var textbox = sender as TextBox;
                if (textbox.Text.Contains(','))
                    e.Handled = true;
                return;
            }
            if (char.IsDigit(e.KeyChar) || e.KeyChar == 8)
                return;
            e.Handled = true;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text.Length == 0 || textBox1.Text.Length == 0 || textBox2.Text.Length == 0 || textBox3.Text.Length == 0)
                button1.Enabled = false;
            else button1.Enabled = true;
        }
    }
}
