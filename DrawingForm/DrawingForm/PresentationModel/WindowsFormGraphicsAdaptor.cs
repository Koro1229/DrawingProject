using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using DrawingModel;

namespace DrawingForm.PresentationModel
{
    class WindowsFormsGraphicsAdaptor : IGraphics
    {
        readonly Graphics _graphics;
        public WindowsFormsGraphicsAdaptor(Graphics graphics)
        {
            this._graphics = graphics;
        }
        public void ClearAll()
        {
            // OnPaint時會自動清除畫面，因此不需實作
        }

        public void DrawLine(double x1, double y1, double x2, double y2)
        {
            _graphics.DrawLine(Pens.Black, (float)x1, (float)y1, (float)x2, (float)y2);
        }

        public void DrawRectangle(double x1, double y1, double x2, double y2)
        {
            double width = x2 > x1? x2 - x1 : x1 - x2;
            double height = y2 > y1? y2 - y1 : y1 - y2;
            double leftTopXCoordinate = x2 > x1 ? x1 : x2;
            double leftTopYCoordinate = y2 > y1 ? y1 : y2;

            SolidBrush solidBrush = new SolidBrush(Color.Yellow);
            _graphics.FillRectangle(solidBrush, (float)leftTopXCoordinate, (float)leftTopYCoordinate, (float)width, (float)height);
            _graphics.DrawRectangle(Pens.Black, (float)leftTopXCoordinate, (float)leftTopYCoordinate, (float)width, (float)height);
        }

        public void DrawEllipse(double x1, double y1, double x2, double y2)
        {
            double width = x2 > x1 ? x2 - x1 : x1 - x2;
            double height = y2 > y1 ? y2 - y1 : y1 - y2;
            double leftTopXCoordinate = x2 > x1 ? x1 : x2;
            double leftTopYCoordinate = y2 > y1 ? y1 : y2;

            SolidBrush solidBrush = new SolidBrush(Color.Orange);
            _graphics.FillEllipse(solidBrush, (float)leftTopXCoordinate, (float)leftTopYCoordinate, (float)width, (float)height);
            _graphics.DrawEllipse(Pens.Black, (float)leftTopXCoordinate, (float)leftTopYCoordinate, (float)width, (float)height);
        }
    }
}
