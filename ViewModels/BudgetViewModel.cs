using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatementHelper.Models;

namespace StatementHelper.ViewModels
{
    public class BudgetViewModel : WorkspaceViewModel
    {
        private BudgetInstance _budget;
        private BudgetInstanceItem _currentBudgetInstanceItem;

        public BudgetInstance Budget
        {
            get { return _budget; }
            set { _budget = value; OnPropertyChanged("Budget");}
        }

        public string Test { get; set; }

        public BudgetInstanceItem CurrentBudgetInstanceItem
        {
            get { return _currentBudgetInstanceItem; }
            set { _currentBudgetInstanceItem = value; OnPropertyChanged("CurrentBudgetItem");}
        }

        public BudgetViewModel(BudgetInstance budget)
        {
            Budget = budget;
            Test = "HELLO";
        }
    }
}
