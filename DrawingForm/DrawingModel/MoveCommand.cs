using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingModel
{
    public class MoveCommand : ICommand
    {
        IShape _shape;
        Model _model;
        Tuple<double, double, double, double> _move;
        public MoveCommand(Model model, IShape shape, Tuple<double, double, double, double> move)
        {
            _shape = shape;
            _model = model;
            _move = move;
        }

        //執行
        public void Execute()
        {
            _model.SaveShapeMove(_shape, _move);
        }

        //令執行無效(笑死 老師命名不給過)
        public void ExecuteDisable()
        {
            _model.DeleteShapeMove(_shape);
        }

        //清空 目前還沒用
        public void Clear()
        {
            _model.ClearAll();
        }
    }
}
