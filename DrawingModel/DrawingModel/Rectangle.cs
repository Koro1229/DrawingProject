using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingModel
{
    class Rectangle : Shape
    {
        public double x1;
        public double y1;
        public double x2;
        public double y2;
        public void Draw(IGraphics graphics)
        {
            graphics.DrawRectangle(x1, y1, x2, y2);
        }

        public double FirstX
        {
            get
            {
                return x1;
            }
            set
            {
                x1 = value;
            }
        }

        public double SecondX
        {
            get
            {
                return x2;
            }
            set
            {
                x2 = value;
            }
        }

        public double FirstY
        {
            get
            {
                return y1;
            }
            set
            {
                y1 = value;
            }
        }

        public double SecondY
        {
            get
            {
                return y2;
            }
            set
            {
                y2 = value;
            }
        }
    }
}
