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
        public static Point ToUiCoordinates(this Point point, Canvas canvas, double size)
        {
            return new Point(
                (double)(point.X * size + canvas.ActualWidth / 2),
                (double)(canvas.ActualHeight / 2 - point.Y * size));
        }
    }

    class CanvasDrawer
    {
        private readonly Canvas _canvas;

        private readonly Point _axisXStart, _axisXEnd, _axisYStart, _axisYEnd;
        private readonly double _xStart, _xEnd, _step, _size;

        private readonly double _lenghtPart = 6;

        public CanvasDrawer(Canvas canvas,List<Point> points,double start, double end, double step, double size)
        {
            _canvas = canvas;
            _xStart = start;
            _xEnd = end;
            _step = step;
            _size = size;

            _axisXStart = new Point(0, (double)(_canvas.ActualHeight / 2));
            _axisXEnd = new Point((double)(_canvas.ActualWidth), (double)(_canvas.ActualHeight / 2));
            _axisYStart = new Point((double)(_canvas.ActualWidth / 2), (double)(_canvas.ActualHeight));
            _axisYEnd = new Point((double)(_canvas.ActualWidth / 2), 0);

            DrawAxes(_axisXStart, _axisXEnd, _axisYStart, _axisYEnd);
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
            
            _canvas.Children.Add(line);
        }
        private void DrawAxesPart()
        {
            //По оси Х
            for (double x = _xStart; x <= _xEnd; x += _step)
            {
                if (x == 0)
                {
                    continue;
                }
                double pointX = new Point(x, 0).ToUiCoordinates(_canvas, _size).X;
                
                DrawLine(new Point(pointX, _canvas.ActualHeight / 2 - _lenghtPart / 2), 
                    new Point(pointX, _canvas.ActualHeight / 2 + _lenghtPart / 2), Brushes.Black, 2);
            }
            
            //По оси У
            for (double y = _xStart; y <= _xEnd; y += _step)
            {
                if (y == 0)
                {
                    continue;
                }
                double pointY = new Point(0, y).ToUiCoordinates(_canvas, _size).Y;
                
                DrawLine(new Point(_canvas.ActualWidth / 2 - _lenghtPart / 2, pointY),
                    new Point(_canvas.ActualWidth / 2 + _lenghtPart / 2, pointY), Brushes.Black, 2);
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

            _canvas.Children.Add(ellipse);
        }

        private void DrawFunc(List<Point> points)
        {
            for (int i = 0; i <= points.Count - 2; i++)
            {
                DrawLine(points[i].ToUiCoordinates(_canvas, _size), 
                    points[i + 1].ToUiCoordinates(_canvas, _size), Brushes.IndianRed, 2);
            }

            for (int i = 0; i < points.Count - 1; i++)
            {
                int radius = 3;
                Point center = new Point(points[i].ToUiCoordinates(_canvas, _size));
                DrawPoint(center, radius, Brushes.Red);
            }
        }
    }

}
