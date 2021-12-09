using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Shapes;
using Windows.UI.Xaml.Media;
using DrawingModel;
using Windows.UI.Xaml;

namespace DrawingApp.PresentationModel
{
    class WindowsStoreGraphicsAdaptor : IGraphics
    {
        Canvas _canvas;
        public WindowsStoreGraphicsAdaptor(Canvas canvas)
        {
            this._canvas = canvas;
        }
        public void ClearAll()
        {
            _canvas.Children.Clear();
        }
        public void DrawLine(double x1, double y1, double x2, double y2)
        {
            Windows.UI.Xaml.Shapes.Line line = new Windows.UI.Xaml.Shapes.Line();
            line.X1 = x1;
            line.Y1 = y1;
            line.X2 = x2;
            line.Y2 = y2;
            line.Stroke = new SolidColorBrush(Colors.Black);
            _canvas.Children.Add(line);
        }

        public void DrawRectangle(double x1, double y1, double x2, double y2)
        {
            Windows.UI.Xaml.Shapes.Rectangle rectangle = new Windows.UI.Xaml.Shapes.Rectangle();
            rectangle.CenterPoint = (new System.Numerics.Vector3((float)(x2 + x1)/2, (float)(y2 + y1)/2, (float)0));
            double left = x2 > x1 ? x1 : x2;
            double right = _canvas.MaxWidth - (x2 > x1 ? x2 : x1);
            double top = y2 > y1 ? y1 : y2;
            double bottom = _canvas.MaxHeight - (y2 > y1 ? y2 : y1);
            rectangle.Margin = new Thickness(left, top, right, bottom);
            rectangle.Width = x2 > x1 ? x2 - x1 : x1 - x2;
            rectangle.Height = y2 > y1 ? y2 - y1 : y1 - y2;
            rectangle.Fill = new SolidColorBrush(Colors.Yellow);
            rectangle.Stroke = new SolidColorBrush(Colors.Black);
            _canvas.Children.Add(rectangle);
        }

        public void DrawEllipse(double x1, double y1, double x2, double y2)
        {
            Windows.UI.Xaml.Shapes.Ellipse ellipse = new Windows.UI.Xaml.Shapes.Ellipse();
            
            ellipse.CenterPoint = (new System.Numerics.Vector3((float)(x2 + x1)/2, (float)(y2 + y1)/2, (float)0));
            double left = x2 > x1 ? x1 : x2;
            double right = _canvas.MaxWidth - (x2 > x1 ? x2 : x1);
            double top = y2 > y1 ? y1 : y2;
            double bottom = _canvas.MaxHeight - (y2 > y1 ? y2 : y1);
            ellipse.Margin = new Thickness(left, top, right, bottom);
            ellipse.Width = x2 > x1 ? x2 - x1 : x1 - x2;
            ellipse.Height = y2 > y1 ? y2 - y1 : y1 - y2;
            ellipse.Fill = new SolidColorBrush(Colors.Orange);
            ellipse.Stroke = new SolidColorBrush(Colors.Black);
            _canvas.Children.Add(ellipse);
        }

    }
}
