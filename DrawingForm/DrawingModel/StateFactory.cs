using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingModel
{
    public class StateFactory
    {
        //建立形狀 判斷
        public static IState CreateState(int mode)
        {
            const String ERROR = "No shape mode";
            const int LINE_MODE = 0;
            const int RECTANGLE_MODE = 1;
            const int ELLIPSE_MODE = 2;

            switch (mode)
            {
                case LINE_MODE:
                    return new DrawingLineState();
                case RECTANGLE_MODE:
                    return new DrawingRectangleState();
                case ELLIPSE_MODE:
                    return new DrawingEllipseState();
                default:
                    return new PointerState();
            }
        }

    }
}
