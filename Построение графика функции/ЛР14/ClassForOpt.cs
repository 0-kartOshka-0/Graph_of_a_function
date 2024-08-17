using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace ЛР14
{
    internal class ClassForOpt
    {
        static ClassForOpt()
        {
            Legend = true;
            Title = true;
            Width = 2;
            ChartType = SeriesChartType.Spline;
            LineColor = Color.Aqua;
            BackgroundColor = Color.White;
        }
        public static  bool Legend;
        public static  bool Title;
        public static  int Width;
        public static  SeriesChartType ChartType;
        public static  Color LineColor;
        public static  Color BackgroundColor;
    }
}
