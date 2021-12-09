using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingModel
{ 
    interface Shape
    {

        void Draw(IGraphics graphics);

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
    }
}
