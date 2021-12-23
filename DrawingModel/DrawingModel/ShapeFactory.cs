using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingModel
{
    public class ShapeFactory
    {
        //建立形狀 判斷
        public static IShape CreateShape(int mode)
        {
            switch (mode)
            {
                case 0:
                    return new Line();
                case 1:
                    return new Rectangle();
                case 2:
                    return new Ellipse();
                default:
                    throw new Exception("no shape mode");
            }
        }

    }
}
