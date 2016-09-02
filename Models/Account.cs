using System;
using System.Collections.Generic;
using System.Linq;

namespace StatementHelper.Models
{
    public class Account
    {
        public string AccountNumber { get; private set; }

        public ICollection<StatementItem> StatementItems { get; private set; }
        public Account(string accountNumber)
        {
            AccountNumber = accountNumber;
            StatementItems = new List<StatementItem>();
        }

        public void AddStatementItems(ICollection<StatementItem> statementItems)
        {
            foreach (var statementItem in statementItems)
            {
                AddStatementItem(statementItem);
            }
        }

        private void AddStatementItem(StatementItem statementItem)
        {
            if (HasStatementItem(statementItem))
            {
                return;
            }

            StatementItems.Add(statementItem);
        }

        private bool HasStatementItem(StatementItem statementItem)
        {
            return StatementItems.Any(x => x.Equals(statementItem));
        }
    }
}