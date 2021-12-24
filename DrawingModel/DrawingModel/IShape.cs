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

        double FirstX
        {
            get; set;
        }

        double SecondX
        {
            get; set;
        }

        double FirstY
        {
            get; set;
        }

        double SecondY
        {
            get; set;
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
