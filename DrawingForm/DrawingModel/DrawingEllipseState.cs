using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingModel
{
    class DrawingEllipseState : IState
    {
        const int ELLIPSE_MODE = 2;
        private double _firstPointX;
        private double _firstPointY;
        private double _secondX;
        private double _secondY;
        private bool _isPressed = false;
        private bool _isDrawed = false;

        //按下
        public void Press(double currentXCoordinate, double currentYCoordinate, IShape firstShape)
        {
            if (currentXCoordinate > 0 && currentYCoordinate > 0)
            {
                _firstPointX = _secondX = currentXCoordinate;
                _firstPointY = _secondY = currentYCoordinate;
                _isPressed = true;
                _isDrawed = false;
            }
        }

        //移動時的資料(讓畫面可以跟著滑鼠畫圖的東西)
        public IShape Move(double currentXCoordinate, double currentYCoordinate, IShape selectedShape)
        {
            if (_isPressed)
            {
                IShape currentShape = ShapeFactory.CreateShape(ELLIPSE_MODE);
                _secondX = currentXCoordinate;
                _secondY = currentYCoordinate;
                currentShape = SetShapeStatus(currentShape);
                _isDrawed = true;
                return currentShape;
            }
            return null;
        }

        //滑鼠按鍵放開後做的事情
        public IShape Release(IShape firstShape, IShape secondShape)
        {
            if (_isPressed && _isDrawed)
            {
                IShape shape = ShapeFactory.CreateShape(ELLIPSE_MODE);
                shape = SetShapeStatus(shape);
                return shape;
            }
            return null;
        }

        //設定pic
        private IShape SetShapeStatus(IShape shape)
        {
            shape.SetShape(_firstPointX, _firstPointY, _secondX, _secondY);
            shape.IsSelected = false;
            return shape;
        }
    }
}
