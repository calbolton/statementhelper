using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatementHelper.Models
{
    public class BudgetInstance
    {
        public BudgetInstance(DateTime fromDate, DateTime toDate, decimal income, ICollection<BudgetInstanceItem> budgetItems)
        {
            FromDate = fromDate;
            ToDate = toDate;
            Income = income;
            Items = budgetItems;
            UnassignedStatementItems = new List<StatementItem>();
        }

        public DateTime FromDate { get; protected set; }

        public DateTime ToDate { get; protected set; }

        public decimal Income { get; protected set; }

        public decimal BudgetedAmount => Items.Sum(x => x.AvailableAmount);

        public decimal SpentAmount => Items.Sum(x => x.SpentAmount) + UnassignedStatementItems.Sum(x => x.Amount *-1);

        public decimal RemainingAmount => BudgetedAmount - SpentAmount;

        public ICollection<BudgetInstanceItem> Items { get; protected set; }

        public ICollection<StatementItem> UnassignedStatementItems { get; protected set; }

        public bool IsForStatementItem(StatementItem statementItem)
        {
            return IsForDate(statementItem.DateTime);
        }

        

        public void AddStatementItems(ICollection<StatementItem> statementItems)
        {
            foreach (var statementItem in statementItems)
            {
                AddStatementItem(statementItem);
            }
        }

        public void AddStatementItem(StatementItem statementItem)
        {
            if (!IsForStatementItem(statementItem))
            {
                return;
            }

            var applicableBudgetItems = Items.Where(budgetItem => budgetItem.IsForStatementItem(statementItem)).ToList();

            if (applicableBudgetItems.Count() > 1)
            {
                throw new Exception("Multiple budget items found");
            }

            if (!applicableBudgetItems.Any())
            {
                UnassignedStatementItems.Add(statementItem);
                return;
            }

            applicableBudgetItems.First().AddStatementItem(statementItem);
        }

        public bool IsForDate(DateTime dateTime)
        {
            return dateTime >= FromDate &&
                   dateTime <= ToDate;
        }
    }
}
