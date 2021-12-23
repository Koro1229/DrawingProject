using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingModel
{
    public class Model
    {
        public event ModelChangedEventHandler _modelChanged;
        public delegate void ModelChangedEventHandler();

        CommandManager _commandManager = new CommandManager();
        double _firstPointX;
        double _firstPointY;
        double _secondX;
        double _secondY;
        private bool _isPressed = false;
        private readonly List<IShape> _shapes = new List<IShape>();

        const int DEFAULT_MODE = -1;
        private int _drawingMode = DEFAULT_MODE;//-1 = NO SHAPE, 0 = line, 1 = rectangle, 2 = ellipse

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
        //for test
        public double FirstPointX
        {
            get
            {
                return _firstPointX;
            }
        }

        //for test
        public double FirstPointY
        {
            get
            {
                return _firstPointY;
            }
        }

        public bool RedoStatus
        {
            get
            {
                return _commandManager.RedoStatus;
            }
        }

        public bool UndoStatus
        {
            get
            {
                return _commandManager.UndoStatus;
            }
        }

        //存取按下的資料
        public void PressPointer(double currentXCoordinate, double currentYCoordinate)
        {
            if (currentXCoordinate > 0 && currentYCoordinate > 0 && _drawingMode != -1 && (_drawingMode != 0 || GetOnShape(currentXCoordinate, currentYCoordinate) != null))
            {
                _firstPointX = currentXCoordinate;
                _firstPointY = currentYCoordinate;
                _isPressed = true;
            }
        }

        //移動時的資料(讓畫面可以跟著滑鼠畫圖的東西)
        public void MovePointer(double currentXCoordinate, double currentYCoordinate)
        {
            if (_isPressed)
            {
                _secondX = currentXCoordinate;
                _secondY = currentYCoordinate;
                NotifyModelChanged();
            }
        }

        //滑鼠按鍵放開後做的事情
        public void ReleasePointer(double currentXCoordinate, double currentYCoordinate)
        {
            if (_isPressed && (_drawingMode != 0 || GetOnShape(currentXCoordinate, currentYCoordinate) != null))
            {
                IShape shape = ShapeFactory.CreateShape(_drawingMode);
                //if (shape.GetType() == new Line().GetType())
                //{
                //    //_commandManager.Execute(new DrawCommand(this, SetLineStatus(shape)));
                //}
                //else
                //{
                    shape = SetShapeStatus(shape);
                    _commandManager.Execute(new DrawCommand(this, shape));
                //}
            }

            _isPressed = false;
            NotifyModelChanged();
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
                if (aShape.GetType() == new Line().GetType())
                        aShape.Draw(graphics);
            foreach (IShape aShape in _shapes)
                if (aShape.GetType() != new Line().GetType())
                        aShape.Draw(graphics);
            if (_isPressed)
            {
                IShape currentShape = ShapeFactory.CreateShape(_drawingMode);
                currentShape = SetShapeStatus(currentShape);
                currentShape.Draw(graphics);
            }
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

        //增加新形狀
        public void AddNewShape(IShape shape)
        {
            _shapes.Add(shape);
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

        //設定shape狀態
        private IShape SetShapeStatus(IShape shape)
        {
            shape.FirstX = _firstPointX;
            shape.FirstY = _firstPointY;
            shape.SecondX = _secondX;
            shape.SecondY = _secondY;
            return shape;
        }

        public void DeleteShape()
        {
            _shapes.RemoveAt(_shapes.Count - 1);
        }

        public void Undo()
        {
            _commandManager.Undo();
            NotifyModelChanged();
        }

        public void Redo()
        {
            _commandManager.Redo();
            NotifyModelChanged();
        }

        private IShape GetOnShape(double currentXCoordinate, double currentYCoordinate)
        {
            for (int i = 0; i < _shapes.Count; i++)
            {
                if (PointInShape(currentXCoordinate, currentYCoordinate, i))
                    return _shapes[i];
            }
            return null;
        }

        private bool PointInShape(double currentXCoordinate, double currentYCoordinate, int index)
        {
            IShape shape = _shapes[index];
            double firstX = shape.FirstX < shape.SecondX ? shape.FirstX : shape.SecondX;
            double secondX = shape.FirstX < shape.SecondX ? shape.SecondX : shape.FirstX;
            if (currentXCoordinate >= firstX && currentXCoordinate <= secondX)
            {
                double firstY = shape.FirstY < shape.SecondY ? shape.FirstY : shape.SecondY;
                double secondY = shape.FirstY < shape.SecondY ? shape.SecondY : shape.FirstY;
                if (currentYCoordinate >= firstY && currentYCoordinate <= secondY)
                    return true;
            }
            return false;
        }

        //private Line SetLineStatus(IShape shape)
        //{
        //    const int AVERAGE = 2;
        //    Line line = new Line();
        //    IShape firstShape = _shapes[GetOnShape(shape.FirstX, shape.FirstY)];
        //    IShape secondShape = _shapes[GetOnShape(shape.SecondX, shape.SecondY)];
        //    line.FirstX = (firstShape.FirstX + firstShape.SecondX) / AVERAGE;
        //    line.FirstY = (firstShape.FirstY + firstShape.SecondY) / AVERAGE;
        //    line.SecondX = (secondShape.FirstX + secondShape.SecondX) / AVERAGE;
        //    line.SecondY = (secondShape.FirstY + secondShape.SecondY) / AVERAGE;
        //    line.FirseShape = firstShape;
        //    line.SecondShape = secondShape;
        //    return line;
        //}
    }
}