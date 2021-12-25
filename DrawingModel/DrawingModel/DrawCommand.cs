using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingModel
{
    public class DrawCommand : ICommand
    {
        IShape _shape;
        Model _model;
        public DrawCommand(Model model, IShape shape)
        {
            _shape = shape;
            _model = model;
        }

        public void Execute()
        {
            _model.AddNewShape(_shape); 
        }

        public void UnExecute()
        {
            _model.DeleteShape();
        }

        public void Clear()
        {
            _model.ClearAll();
        }
    }
}
