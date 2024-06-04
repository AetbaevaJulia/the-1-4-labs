using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Media;
using System.Windows.Shapes;

namespace wpf
{
    static class PointExtensions
    {
        public static Point ToMathCoordinates(this Point point, Canvas canvas, double size)
        {
            return new Point(
                (double)((point.X - canvas.ActualWidth / 2) / size),
                (double)((canvas.ActualHeight / 2 - point.Y) / size));
        }
        public static Point ToUiCoordinates(this Point point, Canvas canvas, double size)
        {
            return new Point(
                (double)(point.X * size + canvas.ActualWidth / 2),
                (double)(canvas.ActualHeight / 2 - point.Y * size));
        }
    }

    class CanvasDrawer
    {
        private Canvas Canvas;

        private readonly Point AxisXStart, AxisXEnd, AxisYStart, AxisYEnd;
        private readonly double XStart, XEnd, Step, Size;

        private readonly double LenghtPart = 6;

        public CanvasDrawer(Canvas canvas,List<Point> points,double start, double end, double step, double size)
        {
            Canvas = canvas;
            XStart = start;
            XEnd = end;
            Step = step;
            Size = size;

            AxisXStart = new Point(0, (double)(Canvas.ActualHeight / 2));
            AxisXEnd = new Point((double)(Canvas.ActualWidth), (double)(Canvas.ActualHeight / 2));
            AxisYStart = new Point((double)(Canvas.ActualWidth / 2), (double)(Canvas.ActualHeight));
            AxisYEnd = new Point((double)(Canvas.ActualWidth / 2), 0);

            DrawAxes(AxisXStart, AxisXEnd, AxisYStart, AxisYEnd);
            DrawFunc(points);
        }

        private void DrawLine (Point start, Point end, Brush color, int thickness)
        {
            Line line = new Line();

            line.StrokeThickness = thickness;
            line.Stroke = color;

            line.X1 = start.X;
            line.Y1 = start.Y;

            line.X2 = end.X;
            line.Y2 = end.Y;
            
            Canvas.Children.Add(line);
        }
        private void DrawAxesPart()
        {
            //По оси Х
            for (double x = XStart; x <= XEnd; x += Step)
            {
                if (x == 0)
                {
                    continue;
                }
                double pointX = new Point(x, 0).ToUiCoordinates(Canvas, Size).X;
                
                DrawLine(new Point(pointX, Canvas.ActualHeight / 2 - LenghtPart / 2), 
                    new Point(pointX, Canvas.ActualHeight / 2 + LenghtPart / 2), Brushes.Black, 2);
            }
            
            //По оси У
            for (double y = XStart; y <= XEnd; y += Step)
            {
                if (y == 0)
                {
                    continue;
                }
                double pointY = new Point(0, y).ToUiCoordinates(Canvas, Size).Y;
                
                DrawLine(new Point(Canvas.ActualWidth / 2 - LenghtPart / 2, pointY),
                    new Point(Canvas.ActualWidth / 2 + LenghtPart / 2, pointY), Brushes.Black, 2);
            }
        }

        private void DrawAxes(Point startX, Point endX, Point startY, Point endY)
        {
            DrawLine(startX, endX, Brushes.Black, 2); //Вывод оси Х
            DrawLine(startY, endY, Brushes.Black, 2); //Вывод оси У
            DrawAxesPart(); //Вывод делений на осях
        }

        private void DrawPoint(Point center, int radius, Brush color)
        {
            double x = center.X - radius;
            double y = center.Y - radius;
            Ellipse ellipse = new Ellipse();

            ellipse.Width = radius * 2;
            ellipse.Height = radius * 2;
            ellipse.Fill = color;
            
            Canvas.SetLeft(ellipse, x);
            Canvas.SetTop(ellipse, y);

            Canvas.Children.Add(ellipse);
        }

        private void DrawFunc(List<Point> points)
        {
            for (int i = 0; i <= points.Count - 2; i++)
            {
                DrawLine(points[i].ToUiCoordinates(Canvas, Size), 
                    points[i + 1].ToUiCoordinates(Canvas, Size), Brushes.IndianRed, 2);
            }

            for (int i = 0; i < points.Count - 1; i++)
            {
                int radius = 3;
                Point center = new Point(points[i].ToUiCoordinates(Canvas, Size));
                DrawPoint(center, radius, Brushes.Red);
            }
        }
    }

}
