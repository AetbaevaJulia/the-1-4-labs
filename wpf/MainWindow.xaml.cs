using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using RPN_logic;

namespace wpf
{
    class Point
    {
        public readonly double X;
        public readonly double Y;
        public Point (double x, double y)
        {
            X = x;
            Y = y;
        }
        public Point (Point _point)
        {
            X = _point.X;
            Y = _point.Y;
        }
    }

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtForCalculate(object sender, RoutedEventArgs e)
        {
            canvasForFunc.Children.Clear();
            DrawFunc();
        }

        private void DrawFunc()
        {
            string userText = tbForInputExpression.Text;
            int start = Convert.ToInt32(tbForInputStart.Text);
            int end = Convert.ToInt32(tbForInputEnd.Text);
            double step = Convert.ToDouble(tbForInputStep.Text);
            int size = Convert.ToInt32(tbForInputSize.Text);

            List<Point> points = new List<Point>();

            for (double x = start; x <= end; x += step)
            {
                var calculater = new RpnCalc(userText, x);
                double y = calculater.Value;
                points.Add(new Point(x, y));
            }

            var canvas = new CanvasDrawer(canvasForFunc, points, start, end, step, size);
        }

        private void TbForUsersText(object sender, TextChangedEventArgs e)
        {

        }
    }
}
