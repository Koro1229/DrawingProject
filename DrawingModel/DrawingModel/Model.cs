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
        //Tuple<double, double> _firstPoint;
        public double _firstPointX;
        public double _firstPointY;
        private bool _isPressed = false;
        private readonly List<IShape> _shapes = new List<IShape>();
        readonly Line _lineHint = new Line();
        readonly Rectangle _rectangleHint = new Rectangle();
        readonly Ellipse _ellipseHint = new Ellipse();

        const int LINE_MODE = 0;
        const int RECTANGLE_MODE = 1;
        const int ELLIPSE_MODE = 2;

        private int _drawingMode = LINE_MODE;//0 = line, 1 = rectangle, 2 = ellipse

        //get for test ,set for function
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

        //存取按下的資料
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

        //移動時的資料(讓畫面可以跟著滑鼠畫圖的東西)
        public void MovePointer(double currentXCoordinate, double currentYCoordinate)
        {
            if (_isPressed)
            {
                _lineHint.SecondX = _rectangleHint.SecondX = _ellipseHint.SecondX = currentXCoordinate;
                _lineHint.SecondY = _rectangleHint.SecondY = _ellipseHint.SecondY = currentYCoordinate;
                NotifyModelChanged();
            }
        }

        //滑鼠按鍵放開後做的事情
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

        //清空
        public void Clear()
        {
            _isPressed = false;
            _shapes.Clear();
            NotifyModelChanged();
        }

        //畫圖
        public void Draw(IGraphics graphics)
        {
            graphics.ClearAll();
            foreach (IShape aShape in _shapes)
                aShape.Draw(graphics);
            if (_isPressed && _drawingMode == LINE_MODE)
                _lineHint.Draw(graphics);
            else if (_isPressed && _drawingMode == RECTANGLE_MODE)
                _rectangleHint.Draw(graphics);
            else if (_isPressed && _drawingMode == ELLIPSE_MODE)
                _ellipseHint.Draw(graphics);
        }

        //observer
        public void NotifyModelChanged()
        {
            if (_modelChanged != null)
                _modelChanged();
        }

        //清空全部 目前沒實作，因為畫面自己有在做事
        public void ClearAll()
        {
            // OnPaint時會自動清除畫面，因此不需實作
        }

        //增加shape Line
        private void AddNewLineShape(double secondXCoordinate, double secondYCoordinate)
        {
            Line hint = new Line
            { 
                FirstX = _firstPointX,
                FirstY = _firstPointY,
                SecondX = secondXCoordinate,
                SecondY = secondYCoordinate };
            _shapes.Add(hint);
        }

        //增加shape Rectangle
        private void AddNewRectangleShape(double secondXCoordinate, double secondYCoordinate)
        {
            Rectangle hint = new Rectangle
            { 
                FirstX = _firstPointX,
                FirstY = _firstPointY,
                SecondX = secondXCoordinate,
                SecondY = secondYCoordinate };
            _shapes.Add(hint);
        }

        //增加shape Ellipse
        private void AddNewEllipseShape(double secondXCoordinate, double secondYCoordinate)
        {
            Ellipse hint = new Ellipse
            { 
                FirstX = _firstPointX,
                FirstY = _firstPointY,
                SecondX = secondXCoordinate,
                SecondY = secondYCoordinate };
            _shapes.Add(hint);
        }

        //current for test 確認有按著
        public bool IsPressed()
        {
            return _isPressed;
        }

        //current for test 得到shapes
        public List<IShape> GetShapes()
        {
            return _shapes;
        }
    }
}