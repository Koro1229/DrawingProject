using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingModel
{
    public interface IState
    {
        //存取按下的資料
        void Press(double currentXCoordinate, double currentYCoordinate, IShape firstShape);
        //移動
        IShape Move(double currentXCoordinate, double currentYCoordinate, IShape selectedShape);
        //放開
        IShape Release(IShape firstShape, IShape secondShape);
    }
}
