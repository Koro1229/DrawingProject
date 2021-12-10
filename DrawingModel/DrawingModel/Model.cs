using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace DrawingModel
{
    public class Model
    {
        public event ModelChangedEventHandler _modelChanged;
        public delegate void ModelChangedEventHandler();
        public double _firstPointX;
        public double _firstPointY;
        private bool _isPressed = false;
        private readonly List<Shape> _shapes = new List<Shape>();
        readonly Line _lineHint = new Line();
        readonly Rectangle _rectangleHint = new Rectangle();
        readonly Ellipse _ellipseHint = new Ellipse();

        const int LINE_MODE = 0;
        const int RECTANGLE_MODE = 1;
        const int ELLIPSE_MODE = 2;

        private int _drawingMode = LINE_MODE;//0 = line, 1 = rectangle, 2 = ellipse

        //get for test set for function
        public int DrawingMode
        {
            get
            {
                return _drawingMode;
            }
            set
            {
                _drawingMode = value;
            }
        }

        public void PressPointer(double currentXCoordinate, double currentYCoordinate)
        {
            if (currentXCoordinate > 0 && currentYCoordinate > 0)
            {
                _firstPointX = currentXCoordinate;
                _firstPointY = currentYCoordinate;
                _lineHint.FirstX = _rectangleHint.FirstX = _ellipseHint.FirstX = _firstPointX;
                _lineHint.FirstY = _rectangleHint.FirstY = _ellipseHint.FirstY = _firstPointY;
                _isPressed = true;
            }
        }

        public void MovePointer(double currentXCoordinate, double currentYCoordinate)
        {
            if (_isPressed)
            {
                _lineHint.SecondX = _rectangleHint.SecondX = _ellipseHint.SecondX = currentXCoordinate;
                _lineHint.SecondY = _rectangleHint.SecondY = _ellipseHint.SecondY = currentYCoordinate;
                NotifyModelChanged();
            }
        }

        public void ReleasePointer(double currentXCoordinate, double currentYCoordinate)
        {
            if (_isPressed)
            {
                _isPressed = false;
                if (_drawingMode == LINE_MODE)
                    AddNewLineShape(currentXCoordinate, currentYCoordinate);
                else if (_drawingMode == RECTANGLE_MODE)
                    AddNewRectangleShape(currentXCoordinate, currentYCoordinate);
                else if (_drawingMode == ELLIPSE_MODE)
                    AddNewEllipseShape(currentXCoordinate, currentYCoordinate);

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
            if (_isPressed && _drawingMode == LINE_MODE)
                _lineHint.Draw(graphics);
            else if (_isPressed && _drawingMode == RECTANGLE_MODE)
                _rectangleHint.Draw(graphics);
            else if (_isPressed && _drawingMode == ELLIPSE_MODE)
                _ellipseHint.Draw(graphics);
        }

        public void NotifyModelChanged()
        {
            if (_modelChanged != null)
                _modelChanged();
        }

        public void ClearAll()
        {
            // OnPaint時會自動清除畫面，因此不需實作
        }

        private void AddNewLineShape(double secondXCoordinate, double secondYCoordinate)
        {
            Line hint = new Line();
            hint.FirstX = _firstPointX;
            hint.FirstY = _firstPointY;
            hint.SecondX = secondXCoordinate;
            hint.SecondY = secondYCoordinate;
            _shapes.Add(hint);
        }

        private void AddNewRectangleShape(double secondXCoordinate, double secondYCoordinate)
        {
            Rectangle hint = new Rectangle();
            hint.FirstX = _firstPointX;
            hint.FirstY = _firstPointY;
            hint.SecondX = secondXCoordinate;
            hint.SecondY = secondYCoordinate;
            _shapes.Add(hint);
        }

        private void AddNewEllipseShape(double secondXCoordinate, double secondYCoordinate)
        {
            Ellipse hint = new Ellipse();
            hint.FirstX = _firstPointX;
            hint.FirstY = _firstPointY;
            hint.SecondX = secondXCoordinate;
            hint.SecondY = secondYCoordinate;
            _shapes.Add(hint);
        }

        //current for test
        public bool IsPressed()
        {
            return _isPressed;
        }

        //current for test 得到shapes
        public List<Shape> GetShapes()
        {
            return _shapes;
        }
    }
}