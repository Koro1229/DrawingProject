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
            const String ERROR = "No shape mode";
            const int LINE_MODE = 0;
            const int RECTANGLE_MODE = 1;
            const int ELLIPSE_MODE = 2;

            switch (mode)
            {
                case LINE_MODE:
                    return new Line();
                case RECTANGLE_MODE:
                    return new Rectangle();
                case ELLIPSE_MODE:
                    return new Ellipse();
                default:
                    throw new Exception(ERROR);
            }
        }

    }
}
