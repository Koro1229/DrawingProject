using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingModel
{
    class DrawingLineState : IState
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
            if (currentXCoordinate > 0 && currentYCoordinate > 0 && isOnShape)
            {
                _firstPointX = _secondX = currentXCoordinate;
                _firstPointY = _secondY = currentYCoordinate;
                _isPressed = true;
                _isDrawed = false;
            }
        }

        //移動
        public void Move(double currentXCoordinate, double currentYCoordinate)
        {
            if (_isPressed)
            {
                _secondX = currentXCoordinate;
                _secondY = currentYCoordinate;
                _isDrawed = true;
            }
        }

        public IShape Release(int drawingMode, bool isOnShape, List<IShape> shapes)
        {
            if (_isPressed && _isDrawed)
            {
                IShape shape = ShapeFactory.CreateShape(drawingMode);
                shape = SetLineStatus(shape, shapes);
                return shape;
            }
            return null;
        }

        //設定line
        private Line SetLineStatus(IShape shape, List<IShape> shapes)
        {
            Line line = new Line();
            IShape firstShape = GetOnShape(shape.FirstX, shape.FirstY, shapes);
            IShape secondShape = GetOnShape(shape.SecondX, shape.SecondY, shapes);
            line.SetShape(firstShape.Center.Item1, firstShape.Center.Item2, secondShape.Center.Item1, secondShape.Center.Item2);
            line.FirstShape = firstShape;
            line.SecondShape = secondShape;
            return line;
        }


        //得到點擊的shape
        private IShape GetOnShape(double currentXCoordinate, double currentYCoordinate, List<IShape> shapes)
        {
            for (int i = shapes.Count - 1; i >= 0; i--)
            {
                if (shapes[i].IsInShape(currentXCoordinate, currentYCoordinate))
                {
                    return shapes[i];
                }
            }
            return null;
        }
    }
}
