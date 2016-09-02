using System;
using System.Collections.Generic;
using System.Linq;

namespace StatementHelper.Models
{
    public class BudgetInstanceItem
    {
        public BudgetInstanceItem(string name, decimal availableAmount, ICollection<string> bankStatementFilters, Category category)
        {
            Name = name;
            AvailableAmount = availableAmount;
            BankStatementFilters = bankStatementFilters;
            Category = category;
            StatementItems = new List<StatementItem>();
        }

        public string   Name { get; protected set; }

        public decimal AvailableAmount { get; protected set; }

        public decimal SpentAmount => StatementItems.Where(x => x.Amount < 0).Sum(x => x.Amount) * -1;

        public decimal RemainingAmount => AvailableAmount - SpentAmount;

        public ICollection<string> BankStatementFilters { get; protected set; }

        public Category Category { get; protected set; }

        public ICollection<StatementItem> StatementItems { get; protected set; }

        public bool IsForStatementItem(StatementItem statementItem)
        {
            return BankStatementFilters
                .Any(bankStatementFilter => IsFilterForStatementItem(bankStatementFilter, statementItem));
        }

        public void AddStatementItem(StatementItem statementItem)
        {
            if (!IsForStatementItem(statementItem))
            {
                throw new Exception("Invalid statement item");
            }

            if (HasStatementItem(statementItem))
            {
                return;
            }

            StatementItems.Add(statementItem);
        }

        public bool HasStatementItem(StatementItem statementItem)
        {
            return StatementItems.Any(x => x.Description == statementItem.Description &&
                                           x.DateTime == statementItem.DateTime &&
                                           x.Amount == statementItem.Amount);
        }

        private bool IsFilterForStatementItem(string bankStatementFilter, StatementItem statementItem)
        {
            return statementItem.Description.ToLower().Contains(bankStatementFilter.ToLower());
        }
    }
}