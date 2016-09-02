using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatementHelper.Factories;
using StatementHelper.Helpers;

namespace StatementHelper.Models
{
    public enum BudgetPeriod
    {
        Monthly,
        Weekly,
        Annually
    }

    public class Budget
    {
        public Budget(decimal income, BudgetPeriod period) : this(income, period, new List<BudgetItem>())
        {
            Income = income;
            Period = period;
        }

        public Budget(decimal income, BudgetPeriod period, ICollection<BudgetItem> items)
        {
            Income = income;
            Items = items;
            Period = period;
            BudgetInstances = new List<BudgetInstance>();
        }

        public decimal Income { get; set; }

        public ICollection<BudgetItem> Items { get; set; }

        public BudgetPeriod Period { get; set; }

        public ICollection<BudgetInstance> BudgetInstances { get; private set; }

        public ICollection<Category> Categories { get; set; }

        public void AddStatementItems(ICollection<StatementItem> statementItems)
        {
            foreach (var statementItem in statementItems)
            {
                AddStatementItem(statementItem);
            }
        }

        private void AddStatementItem(StatementItem statementItem)
        {
            if (HasInstance(statementItem.DateTime))
            {
                AddStatementItemToExistingInstance(statementItem);
            }
            else
            {
                CreateNewInstance(statementItem);
            }
        }

        private void CreateNewInstance(StatementItem statementItem)
        {
            var newInstance = BudgetInstanceFactory.CreateBudgetInstance(this, statementItem.DateTime);
            newInstance.AddStatementItem(statementItem);
            BudgetInstances.Add(newInstance);
        }

        private void AddStatementItemToExistingInstance(StatementItem statementItem)
        {
            var existingInstance = GetInstance(statementItem.DateTime);
            existingInstance.AddStatementItem(statementItem);
        }

        private BudgetInstance GetInstance(DateTime dateTime)
        {
            return BudgetInstances.First(x => x.IsForDate(dateTime));
        }

        private bool HasInstance(DateTime dateTime)
        {
            return BudgetInstances.Any(x => x.IsForDate(dateTime));
        }
    }
}
