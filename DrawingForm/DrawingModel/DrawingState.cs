using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingModel
{
    class DrawingState : IState
    {
        private double _firstPointX;
        private double _firstPointY;
        private double _secondX;
        private double _secondY;
        private bool _isPressed = false;
        private bool _isDrawed = false;

        //按下
        public void Press(double currentXCoordinate, double currentYCoordinate, bool isOnShape)
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
        public void Move(double currentXCoordinate, double currentYCoordinate)
        {
            if (_isPressed)
            {
                _secondX = currentXCoordinate;
                _secondY = currentYCoordinate;
                _isDrawed = true;
            }
        }

        //滑鼠按鍵放開後做的事情
        public IShape Release(int drawingMode, bool isOnShape, List<IShape> shapes)
        {
            if (_isPressed && _isDrawed)
            {
                IShape shape = ShapeFactory.CreateShape(drawingMode);
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
