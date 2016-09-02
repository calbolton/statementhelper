using System;
using StatementHelper.Models;

namespace StatementHelper.ViewModels
{
    public class ViewBudgetItemClickedEventArgs : EventArgs
    {
        public ViewBudgetItemClickedEventArgs(BudgetInstance budget)
        {
            Budget = budget;
        }

        public BudgetInstance Budget { get; set; }
    }
}