using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingModel
{
    public class CommandManager
    {
        Stack<ICommand> _undo = new Stack<ICommand>();
        Stack<ICommand> _redo = new Stack<ICommand>();

        //執行
        public void Execute(ICommand command)
        {
            command.Execute();
            _undo.Push(command);
            _redo.Clear();
        }

        //清空
        public void Clear()
        {
            _redo.Clear();
            _undo.Clear();
        }

        //上一步
        public void Undo()
        {
            if (_undo.Count > 0)
            {
                ICommand command = _undo.Pop();
                _redo.Push(command);
                command.ExecuteDisable();
            }
        }

        //下一步
        public void Redo()
        {
            if (_redo.Count > 0)
            {
                ICommand command = _redo.Pop();
                _undo.Push(command);
                command.Execute();
            }
        }

        public bool RedoStatus
        {
            get
            {
                return _redo.Count != 0;
            }
        }

        public bool UndoStatus
        {
            get
            {
                return _undo.Count != 0;
            }
        }
    }
}
