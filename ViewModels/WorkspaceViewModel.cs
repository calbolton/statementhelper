using System;
using System.Windows.Input;

namespace StatementHelper.ViewModels
{
    /// <summary>
    /// This ViewModelBase subclass requests to be removed 
    /// from the UI when its CloseCommand executes.
    /// This class is abstract.
    /// </summary>
    public abstract class WorkspaceViewModel : ViewModelBase
    {
        #region Fields

        RelayCommand _closeCommand;

        #endregion // Fields

        #region Constructor

        protected WorkspaceViewModel()
        {
        }

        #endregion // Constructor
        
        #region CloseCommand

        /// <summary>
        /// Returns the command that, when invoked, attempts
        /// to remove this workspace from the user interface.
        /// </summary>
        public ICommand CloseCommand
        {
            get
            {
                if (_closeCommand == null)
                    _closeCommand = new RelayCommand(param => this.OnRequestClose());

                return _closeCommand;
            }
        }

        #endregion // CloseCommand

        #region RequestClose [event]

        /// <summary>
        /// Raised when this workspace should be removed from the UI.
        /// </summary>
        public event EventHandler RequestClose;

        void OnRequestClose()
        {
            EventHandler handler = this.RequestClose;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        #endregion // RequestClose [event]

        #region Helper Methods

        private string _errorText;

        public string ErrorText
        {
            get { return _errorText; }
            set
            {
                _errorText = value;
                OnPropertyChanged("ErrorText");
            }
        }

        private bool _isErrorTextVisible;

        public bool IsErrorTextVisible
        {
            get { return _isErrorTextVisible; }
            set
            {
                _isErrorTextVisible = value;
                OnPropertyChanged("IsErrorTextVisible");
            }
        }

        public void ShowError(string error)
        {
            ErrorText = error;
            IsErrorTextVisible = true;
        }

        public void HideError()
        {
            ErrorText = "";
            IsErrorTextVisible = false;
        }
        #endregion
    }
}