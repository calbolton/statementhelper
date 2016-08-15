using System;
using System.Diagnostics;
using System.Windows.Input;

namespace StatementHelper.ViewModels
{
    public class RelayCommand : ICommand
    {
        #region Fields

        readonly Action<object> _execute;
        readonly Predicate<object> _canExecute;

        #endregion Fields

        #region Constructors

        public RelayCommand(Action<object> execute)
            : this(execute, null)
        {
        }

        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            _execute = execute;
            _canExecute = canExecute;
        }

        public RelayCommand()
        {
            // TODO: Complete member initialization
        }

        #endregion Constructors

        #region ICommand Members

        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return true;
            //return _canExecute == null ? true : _canExecute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            OnPreExecute();

            if (_canExecute == null ? true : _canExecute(parameter))
                _execute(parameter);
        }

        public event EventHandler PreExecute;

        private void OnPreExecute()
        {
            if (PreExecute != null)
            {
                PreExecute(this, new EventArgs());
            }
        }

        #endregion ICommand Members
    }
}