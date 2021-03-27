using System;

namespace Common.Commands
{
    public interface ICommandQueue
    {
        event EventHandler OnComplete;
        
        void Add(ICommand command);
        void ExecuteAll();
    }
}