using System;

namespace StatementHelper.ViewModels
{
    /// <summary>
    /// Represents an actionable item displayed by a View.
    /// </summary>
    public class CommandViewModel : ViewModelBase
    {
        public CommandViewModel(string displayName, RelayCommand command)
        {
            if (command == null)
                throw new ArgumentNullException("command");

            base.DisplayName = displayName;
            this.Command = command;
            this.Command.PreExecute += CommandPreExecute;
            base.Enabled = false;
        }

        public CommandViewModel(string displayName, RelayCommand command, bool enabled)
        {
            if (command == null)
                throw new ArgumentNullException("command");

            base.DisplayName = displayName;
            this.Command = command;
            this.Command.PreExecute += CommandPreExecute;
            base.Enabled = enabled;
        }

        private void CommandPreExecute(object sender, EventArgs e)
        {
            OnCommandSelected();
            IsSelected = true;
        }

        private bool _isSelected;

        public bool IsSelected
        {
            get { return _isSelected; }
            set { _isSelected = value; OnPropertyChanged("IsSelected"); }
        }

        public event EventHandler CommandSelected;

        private void OnCommandSelected()
        {
            if (CommandSelected != null)
            {
                CommandSelected(this, new EventArgs());
            }
        }
 
        public RelayCommand Command { get; private set; }
    }
}