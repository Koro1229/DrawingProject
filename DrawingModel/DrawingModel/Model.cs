using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace DrawingModel
{
    class Model
    {
        public event ModelChangedEventHandler _modelChanged;
        public delegate void ModelChangedEventHandler();
        double _firstPointX;
        double _firstPointY;
        bool _isPressed = false;
        List<Shape> _shapes = new List<Shape>();
        Line _lineHint = new Line();
        Rectangle _rectangleHint = new Rectangle();
        Ellipse _ellipseHint = new Ellipse();
        private int _drawingMode = 0;//0 = line, 1 = rectangle, 2 = ellipse

        public int DrawingMode
        {
            set
            {
                _drawingMode = value;
            }
        }
        public void PointerPressed(double x, double y)
        {
            if (x > 0 && y > 0)
            {
                _firstPointX = x;
                _firstPointY = y;
                _lineHint.FirstX = _rectangleHint.FirstX = _ellipseHint.FirstX = _firstPointX;
                _lineHint.FirstY = _rectangleHint.FirstY = _ellipseHint.FirstY = _firstPointY;
                _isPressed = true;
            }
        }
        public void PointerMoved(double x, double y)
        {
            if (_isPressed)
            {
                _lineHint.SecondX = _rectangleHint.SecondX = _ellipseHint.SecondX = x;
                _lineHint.SecondY = _rectangleHint.SecondY = _ellipseHint.SecondY = y;
                NotifyModelChanged();
            }
        }
        public void PointerReleased(double x, double y)
        {
            if (_isPressed)
            {
                _isPressed = false;
                if (_drawingMode == 0)
                    AddNewLineShape(x, y);
                else if (_drawingMode == 1)
                    AddNewRectangleShape(x, y);
                else if (_drawingMode == 2)
                    AddNewEllipseShape(x, y);
                
                NotifyModelChanged();
            }
        }
        public void Clear()
        {
            _isPressed = false;
            _shapes.Clear();
            NotifyModelChanged();
        }

        //public void Draw(IGraphics graphics)
        //{
        //    graphics.ClearAll();
        //    foreach (Line aLine in _lines)
        //        aLine.Draw(graphics);
        //    if (_isPressed)
        //        _hint.Draw(graphics);
        //}

        public void Draw(IGraphics graphics)
        {
            graphics.ClearAll();
            foreach (Shape aShape in _shapes)
                aShape.Draw(graphics);
            if (_isPressed && _drawingMode == 0)
                _lineHint.Draw(graphics);
            else if (_isPressed && _drawingMode == 1)
                _rectangleHint.Draw(graphics);
            else if (_isPressed && _drawingMode == 2)
                _ellipseHint.Draw(graphics);
        }
        void NotifyModelChanged()
        {
            if (_modelChanged != null)
                _modelChanged();
        }

        public void ClearAll()
        {
            // OnPaint時會自動清除畫面，因此不需實作
        }

        private void AddNewLineShape(double x, double y)
        {
            Line hint = new Line();
            hint.FirstX = _firstPointX;
            hint.FirstY = _firstPointY;
            hint.SecondX = x;
            hint.SecondY = y;
            _shapes.Add(hint);
        }

        private void AddNewRectangleShape(double x, double y)
        {
            Rectangle hint = new Rectangle();
            hint.FirstX = _firstPointX;
            hint.FirstY = _firstPointY;
            hint.SecondX = x;
            hint.SecondY = y;
            _shapes.Add(hint);
        }

        private void AddNewEllipseShape(double x, double y)
        {
            Ellipse hint = new Ellipse();
            hint.FirstX = _firstPointX;
            hint.FirstY = _firstPointY;
            hint.SecondX = x;
            hint.SecondY = y;
            _shapes.Add(hint);
        }
    }
}