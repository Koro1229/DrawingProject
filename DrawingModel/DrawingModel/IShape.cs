using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingModel
{ 
    public interface IShape
    {
        //畫圖
        void Draw(IGraphics graphics);
        //選擇
        void Selected(IGraphics graphics);
        //設定shape
        void SetShape(double firstX, double firstY, double secondX, double secondY);
        //是否在shape中
        bool IsInShape(double xCoordinate, double yCoordinate);
        //取得座標字串
        String GetDataString();

        double FirstX
        {
            get;
        }

        double SecondX
        {
            get;
        }

        double FirstY
        {
            get;
        }

        double SecondY
        {
            get;
        }

        Tuple<double, double> Center
        {
            get;
        }

        bool IsSelected
        {
            get; set;
        }
    }
}
