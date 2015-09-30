using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DeveloperTest.Commands
{
    public class LambdaCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        public LambdaCommand(Action execute, Func<bool> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            _execute = new Action(execute);

            if (canExecute != null)
            {
                _canExecute = new Func<bool>(canExecute);
            }
        }

        public LambdaCommand(Action execute)
            : this(execute, null)
        {
        }
        public event EventHandler CanExecuteChanged;

        public void RaiseCanExecuteChanged()
        {

            var handler = CanExecuteChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
                return true;

            return _canExecute.Invoke();
        }

        public virtual void Execute(object parameter)
        {
            if (CanExecute(parameter)
                && _execute != null)
            {
                _execute.Invoke();
            }
        }
    }


    public class LambdaCommand<T> : ICommand
    {
        private readonly Action<T> _execute;

        private readonly Predicate<T> _canExecute;

        public LambdaCommand(Action<T> execute)
            : this(execute, null)
        { }
        public LambdaCommand(Action<T> execute, Predicate<T> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            _execute = execute;

            if (canExecute != null)
            {
                _canExecute = canExecute;
            }
        }

        public event EventHandler CanExecuteChanged;


        public void RaiseCanExecuteChanged()
        {

            var handler = CanExecuteChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
                return true;

            return _canExecute.Invoke((T)parameter);
        }

        public virtual void Execute(object parameter)
        {
            if (CanExecute(parameter)
                && _execute != null)
            {
                _execute.Invoke((T)parameter);
            }
        }
    }
}
