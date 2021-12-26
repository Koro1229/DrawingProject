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

        //清空全部 目前沒實作，因為畫面自己有在做事
        public void ClearAll()
        {
            // OnPaint時會自動清除畫面，因此不需實作
        }

        //畫線
        public void DrawLine(double x1, double y1, double x2, double y2)
        {
            _graphics.DrawLine(Pens.Black, (float)x1, (float)y1, (float)x2, (float)y2);
        }

        //畫方形
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

        //畫橢圓
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

        //畫出選擇的物件
        public void DrawSelectedItem(double x1, double y1, double x2, double y2)
        {
            double width = x2 > x1 ? x2 - x1 : x1 - x2;
            double height = y2 > y1 ? y2 - y1 : y1 - y2;
            double leftTopXCoordinate = x2 > x1 ? x1 : x2;
            double leftTopYCoordinate = y2 > y1 ? y1 : y2;
            const float THICKNESS = 3;
            const float DASH = 7;
            Pen pen = new Pen(Color.Red, THICKNESS);
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Custom;
            pen.DashPattern = new float[] { DASH, THICKNESS };
            _graphics.DrawRectangle(pen, (float)leftTopXCoordinate, (float)leftTopYCoordinate, (float)width, (float)height);
            DrawCorner(x1, y1, x2, y2);
        }

        //畫角落
        private void DrawCorner(double x1, double y1, double x2, double y2)
        {
            DrawPoint(x1, y1);
            DrawPoint(x1, y2);
            DrawPoint(x2, y1);
            DrawPoint(x2, y2);
        }

        //畫角落的點
        private void DrawPoint(double xCoordinate, double yCoordinate)
        {
            const float RADIUS = 6;
            const float HALF_RADIUS = 3;
            const float THICKNESS = 2;
            Pen pen = new Pen(Color.Black, THICKNESS);
            SolidBrush solidBrush = new SolidBrush(Color.White);
            _graphics.FillEllipse(solidBrush, (float)xCoordinate - (HALF_RADIUS), (float)yCoordinate - (HALF_RADIUS), RADIUS, RADIUS);
            _graphics.DrawEllipse(pen, (float)xCoordinate - (HALF_RADIUS), (float)yCoordinate - (HALF_RADIUS), RADIUS, RADIUS);
        }
    }
}
