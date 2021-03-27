using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.Commands
{
    public class CommandQueue : ICommandQueue
    {
        public event EventHandler OnComplete;
        
        private readonly Queue<ICommand> _queue = new Queue<ICommand>();
        
        private int _completedCount;
        private int _totalCount;
        private bool _executing;
        
        public void Add(ICommand command)
        {
            if (_executing)
            {
                return;
            }
            
            _queue.Enqueue(command);
        }


        public void ExecuteAll()
        {
            if (_executing)
            {
                return;
            }
            
            _executing = true;
            _totalCount = _queue.Count;
            
            while (_queue.Any())
            {
                var command = _queue.Dequeue();
                command.OnComplete += OnCommandComplete;
                command.Execute();
            }
        }

        private void OnCommandComplete(object sender, EventArgs e)
        {
            ((ICommand) sender).OnComplete -= OnCommandComplete;
            
            if (++_completedCount >= _totalCount)
            {
                CompleteAllExecution();
            }
        }

        private void CompleteAllExecution()
        {
            OnComplete?.Invoke(this, EventArgs.Empty);
            _queue.Clear();
            _completedCount = 0;
            _totalCount = 0;
            _executing = false;
        }
    }
}