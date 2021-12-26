using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingModel
{
    class Line : IShape
    {
        const int TWO = 2;
        private double _x1;
        private double _y1;
        private double _x2;
        private double _y2;
        private bool _isSelected;
        //畫圖
        public void Draw(IGraphics graphics)
        {
            graphics.DrawLine(_x1, _y1, _x2, _y2);
        }

        //選取
        public void Selected(IGraphics graphics)
        {
            if (IsSelected)
                graphics.DrawSelectedItem(_x1, _y1, _x2, _y2);
        }

        //設定shape
        public void SetShape(double firstX, double firstY, double secondX, double secondY)
        {
            _x1 = firstX;
            _y1 = firstY;
            _x2 = secondX;
            _y2 = secondY;
        }

        //是否在shape中
        public bool IsInShape(double xCoordinate, double yCoordinate)
        {
            return xCoordinate >= FirstX && xCoordinate <= SecondX && yCoordinate >= FirstY && yCoordinate <= SecondY;
        }

        //取得座標字串
        public String GetDataString()
        {
            const String LEFT_BRACKET = "(";
            const String COMMA = ", ";
            const String RIGHT_BRACKET = ")";
            return LEFT_BRACKET + ((int)FirstX).ToString() + COMMA + ((int)FirstY).ToString() + COMMA + ((int)SecondX).ToString() + COMMA + ((int)SecondY).ToString() + RIGHT_BRACKET;
        }

        public double FirstX
        {
            get
            {
                return _x1 < _x2 ? _x1 : _x2;
            }
        }

        public double SecondX
        {
            get
            {
                return _x1 > _x2 ? _x1 : _x2;
            }
        }

        public double FirstY
        {
            get
            {
                return _y1 < _y2 ? _y1 : _y2;
            }
        }

        public double SecondY
        {
            get
            {
                return _y1 > _y2 ? _y1 : _y2;
            }
        }

        public Tuple<double, double> Center
        {
            get
            {
                double centerX = (_x1 + _x2) / TWO;
                double centerY = (_y1 + _y2) / TWO;
                return new Tuple<double, double>(centerX, centerY);
            }
        }

        public IShape FirstShape
        {
            get; set;
        }

        public IShape SecondShape
        {
            get; set;
        }

        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }
            set
            {
                _isSelected = value;
            }
        }
    }
}
