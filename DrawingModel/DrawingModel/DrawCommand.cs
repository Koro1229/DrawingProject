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

        //執行
        public void Execute()
        {
            _model.AddNewShape(_shape); 
        }

        //令執行無效(笑死 老師命名不給過)
        public void ExecuteDisable()
        {
            _model.DeleteShape();
        }

        //清空 目前還沒用
        public void Clear()
        {
            _model.ClearAll();
        }
    }
}
