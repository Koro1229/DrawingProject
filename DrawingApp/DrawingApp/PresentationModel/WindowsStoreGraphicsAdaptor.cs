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
        readonly Canvas _canvas;

        public WindowsStoreGraphicsAdaptor(Canvas canvas)
        {
            this._canvas = canvas;
        }

        //清空全部
        public void ClearAll()
        {
            _canvas.Children.Clear();
        }

        //畫線
        public void DrawLine(double x1, double y1, double x2, double y2)
        {
            Windows.UI.Xaml.Shapes.Line line = new Windows.UI.Xaml.Shapes.Line
            {
                X1 = x1,
                Y1 = y1,
                X2 = x2,
                Y2 = y2,
                Stroke = new SolidColorBrush(Colors.Black)
            };
            _canvas.Children.Add(line);
        }

        //畫方形
        public void DrawRectangle(double x1, double y1, double x2, double y2)
        {
            Windows.UI.Xaml.Shapes.Rectangle rectangle = new Windows.UI.Xaml.Shapes.Rectangle
            {
                Margin = GetMarginAttribute(x1, y1, x2, y2),
                Width = x2 > x1 ? x2 - x1 : x1 - x2,
                Height = y2 > y1 ? y2 - y1 : y1 - y2,
                Fill = new SolidColorBrush(Colors.Yellow),
                Stroke = new SolidColorBrush(Colors.Black)
            };
            _canvas.Children.Add(rectangle);
        }

        //畫橢圓
        public void DrawEllipse(double x1, double y1, double x2, double y2)
        {
            Windows.UI.Xaml.Shapes.Ellipse ellipse = new Windows.UI.Xaml.Shapes.Ellipse
            {
                Margin = GetMarginAttribute(x1, y1, x2, y2),
                Width = x2 > x1 ? x2 - x1 : x1 - x2,
                Height = y2 > y1 ? y2 - y1 : y1 - y2,
                Fill = new SolidColorBrush(Colors.Orange),
                Stroke = new SolidColorBrush(Colors.Black)
            };
            _canvas.Children.Add(ellipse);
        }

        //設定圖形Margin的值
        private Thickness GetMarginAttribute(double x1, double y1, double x2, double y2)
        {
            double left = x2 > x1 ? x1 : x2;
            double right = _canvas.MaxWidth - (x2 > x1 ? x2 : x1);
            double top = y2 > y1 ? y1 : y2;
            double bottom = _canvas.MaxHeight - (y2 > y1 ? y2 : y1);
            return new Thickness(left, top, right, bottom);
        }
    }
}
