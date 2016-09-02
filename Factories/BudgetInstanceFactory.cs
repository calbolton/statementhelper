using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatementHelper.Helpers;
using StatementHelper.Models;

namespace StatementHelper.Factories
{
    public static class BudgetInstanceFactory
    {
        public static BudgetInstance CreateBudgetInstance(Budget budget, DateTime date)
        {
            var items = budget.Items.Select(x => new BudgetInstanceItem(x.Name, x.AvailableAmount, x.BankStatementFilters, x.Category)).ToList();
            var fromDate = GetFromDate(budget.Period, date);
            var toDate = GetToDate(budget.Period, date);

            var budgetInstance = new BudgetInstance(fromDate, toDate, budget.Income, items);

            return budgetInstance;
        }

        private static DateTime GetToDate(BudgetPeriod period, DateTime date)
        {
            switch (period)
            {
                case BudgetPeriod.Monthly:
                    return date.LastDayOfMonth();
                case BudgetPeriod.Weekly:
                    return date.LastDayOfWeek();
                case BudgetPeriod.Annually:
                    return date.LastDayOfYear();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static DateTime GetFromDate(BudgetPeriod period, DateTime date)
        {
            switch (period)
            {
                case BudgetPeriod.Monthly:
                    return date.FirstDayOfMonth();
                case BudgetPeriod.Weekly:
                    return date.FirstDayOfWeek();
                case BudgetPeriod.Annually:
                    return date.FirstDayOfYear();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
