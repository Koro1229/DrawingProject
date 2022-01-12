using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingModel
{
    class Rectangle : IShape
    {
        const int TWO = 2;
        private List<Tuple<double, double, double, double>> _pointHistory = new List<Tuple<double, double, double, double>>();
        private double _currentX1;
        private double _currentX2;
        private double _currentY1;
        private double _currentY2;
        private bool _isSelected;

        //畫圖
        public void Draw(IGraphics graphics)
        {
            graphics.DrawRectangle(_currentX1, _currentY1, _currentX2, _currentY2);
        }

        //選取
        public void Selected(IGraphics graphics)
        {
            if (IsSelected)
                graphics.DrawSelectedItem(_currentX1, _currentY1, _currentX2, _currentY2);
        }

        //設定shape
        public void SetShape(double firstX, double firstY, double secondX, double secondY)
        {
            _pointHistory.Add(new Tuple<double, double, double, double>(firstX, firstY, secondX, secondY));
            MoveShape(0, 0);
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

        //移動
        public void MoveShape(double deltaX, double deltaY)
        {
            _currentX1 = GetLatestPoint().Item1 + deltaX;
            _currentY1 = GetLatestPoint().Item2 + deltaY;
            _currentX2 = GetLatestPoint().Item3 + deltaX;
            _currentY2 = GetLatestPoint().Item4 + deltaY;
        }

        //當前座標
        public Tuple<double, double, double, double> GetCurrentTuple()
        {
            return new Tuple<double, double, double, double>(FirstX, FirstY, SecondX, SecondY);
        }

        //刷新
        public void Refresh()
        {
            //沒東西刷新
        }

        //取得移動數據
        public Tuple<double, double, double, double> GetMoveTuple()
        {
            return new Tuple<double, double, double, double>(_currentX1, _currentY1, _currentX2, _currentY2);
        }

        //儲存shape
        public void SaveMove(Tuple<double, double, double, double> moveResult)
        {
            _pointHistory.Add(moveResult);
            _currentX1 = moveResult.Item1;
            _currentY1 = moveResult.Item2;
            _currentX2 = moveResult.Item3;
            _currentY2 = moveResult.Item4;
        }

        //取消移動
        public void MoveDisable()
        {
            _pointHistory.RemoveAt(_pointHistory.Count - 1);
            _currentX1 = GetLatestPoint().Item1;
            _currentY1 = GetLatestPoint().Item2;
            _currentX2 = GetLatestPoint().Item3;
            _currentY2 = GetLatestPoint().Item4;

        }

        //取得最後的點
        private Tuple<double, double, double, double> GetLatestPoint()
        {
            return _pointHistory.Last();
        }

        //取得名稱
        public String GetShapeName()
        {
            const String NAME = "Rectangle";
            return NAME;
        }

        //取得Mode flag
        public String GetShapeMode()
        {
            const String MODE = "1";
            return MODE;
        }

        public double FirstX
        {
            get
            {
                return _currentX1 < _currentX2 ? _currentX1 : _currentX2;
            }
        }

        public double SecondX
        {
            get
            {
                return _currentX1 > _currentX2 ? _currentX1 : _currentX2;
            }
        }

        public double FirstY
        {
            get
            {
                return _currentY1 < _currentY2 ? _currentY1 : _currentY2;
            }
        }

        public double SecondY
        {
            get
            {
                return _currentY1 > _currentY2 ? _currentY1 : _currentY2;
            }
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

        public Tuple<double, double> Center
        {
            get
            {
                double centerX = (_currentX1 + _currentX2) / TWO;
                double centerY = (_currentY1 + _currentY2) / TWO;
                return new Tuple<double, double>(centerX, centerY);
            }
        }
    }
}
