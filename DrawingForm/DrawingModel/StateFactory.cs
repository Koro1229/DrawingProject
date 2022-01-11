using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingModel
{
    public class StateFactory
    {
        const int DRAWING_LINE_MODE = 0;
        const int DRAWING_RECTANGLE_MODE = 1;
        const int DRAWING_ELLIPSE_MODE = 2;

        //建立形狀 判斷
        public static IState CreateState(int mode)
        {
            switch (mode)
            {
                case DRAWING_LINE_MODE:
                    return new DrawingLineState();
                case DRAWING_RECTANGLE_MODE:
                    return new DrawingRectangleState();
                case DRAWING_ELLIPSE_MODE:
                    return new DrawingEllipseState();
                default:
                    return new PointerState();
            }
        }
    }
}
