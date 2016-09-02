using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using StatementHelper.Helpers;
using StatementHelper.Models;

namespace StatementHelper.ViewModels
{
    public class MainWindowViewModel : WorkspaceViewModel
    {
        private BudgetSearchViewModel _budgetSearchViewModel;
        private WorkspaceViewModel _currentWorkspace;

        public MainWindowViewModel()
        {
            _budgetSearchViewModel = new BudgetSearchViewModel();
            _budgetSearchViewModel.ViewBudgetClicked += BudgetSearchViewModelOnViewBudgetClicked;
            CurrentWorkspace = _budgetSearchViewModel;
        }

        private void BudgetSearchViewModelOnViewBudgetClicked(object sender, ViewBudgetItemClickedEventArgs e)
        {
            CurrentWorkspace = new BudgetViewModel(e.Budget);
        }

        public WorkspaceViewModel CurrentWorkspace
        {
            get { return _currentWorkspace; }
            set { _currentWorkspace = value; OnPropertyChanged("CurrentWorkspace"); }
        }
    }
}
