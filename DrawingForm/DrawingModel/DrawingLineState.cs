using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingModel
{
    class DrawingLineState : IState
    {
        const int LINE_MODE = 0;
        private double _firstPointX;
        private double _firstPointY;
        private double _secondX;
        private double _secondY;
        private bool _isPressed = false;
        private bool _isDrawed = false;

        //按下
        public void Press(double currentXCoordinate, double currentYCoordinate, IShape firstShape)
        {
            if (currentXCoordinate > 0 && currentYCoordinate > 0 && firstShape != null)
            {
                _firstPointX = _secondX = currentXCoordinate;
                _firstPointY = _secondY = currentYCoordinate;
                _isPressed = true;
                _isDrawed = false;
            }
        }

        //移動
        public IShape Move(double currentXCoordinate, double currentYCoordinate, IShape selectedShape)
        {
            if (_isPressed)
            {
                IShape currentShape = ShapeFactory.CreateShape(LINE_MODE);
                _secondX = currentXCoordinate;
                _secondY = currentYCoordinate;
                currentShape = SetShapeStatus(currentShape);
                _isDrawed = true;
                return currentShape;
            }
            return null;
        }

        //放開
        public IShape Release(IShape firstShape, IShape secondShape)
        {
            if (_isPressed && _isDrawed)
            {
                IShape shape = ShapeFactory.CreateShape(LINE_MODE);
                shape = SetLineStatus(shape, firstShape, secondShape);
                return shape;
            }
            return null;
        }

        //設定line
        private Line SetLineStatus(IShape shape, IShape firstShape, IShape secondShape)
        {
            Line line = new Line();
            if (firstShape != null && secondShape != null)
            {
                line.FirstShape = firstShape;
                line.SecondShape = secondShape;
                line.Refresh();
            }
            return line;
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
