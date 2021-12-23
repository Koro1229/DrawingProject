using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingModel
{
    public class CommandManager
    {
        Stack<ICommand> undo = new Stack<ICommand>();
        Stack<ICommand> redo = new Stack<ICommand>();


        public void Execute(ICommand command)
        {
            command.Execute();
            undo.Push(command);
            redo.Clear();
        }

        public void Undo()
        {
            if (undo.Count > 0)
            {
                ICommand command = undo.Pop();
                redo.Push(command);
                command.UnExecute();
            }
        }

        public void Redo()
        {
            if (redo.Count > 0)
            {
                ICommand command = redo.Pop();
                undo.Push(command);
                command.Execute();
            }
        }

        public bool RedoStatus
        {
            get
            {
                return redo.Count != 0;
            }
        }

        public bool UndoStatus
        {
            get
            {
                return undo.Count != 0;
            }
        }
    }
}
