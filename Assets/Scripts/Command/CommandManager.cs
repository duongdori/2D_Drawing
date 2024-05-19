using System.Collections.Generic;
using UnityEngine;

public class CommandManager : MonoBehaviour
{
    private Stack<ICommand> _commandStack = new();

    public void ExecuteCommand(ICommand command)
    {
        command.Execute();
        _commandStack.Push(command);
    }

    public void Undo()
    {
        if (_commandStack.Count > 0)
        {
            ICommand command = _commandStack.Pop();
            command.Undo();
        }
    }
}
