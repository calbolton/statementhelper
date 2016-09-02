using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatementHelper.Models
{
    public class User
    {
        public string Id { get; private set; }
        public string Name { get; private set; }

        public ICollection<Account> Accounts { get; private set; }

        public Budget Budget { get; private set; }

        public User(string id, string name)
        {
            Id = id;
            Name = name;
            Accounts = new List<Account>();
        }

        public void SetBudget(Budget budget)
        {
            Budget = budget;
        }

        public void AddAccount(Account account)
        {
            Accounts.Add(account);
        }

        public void AddStatementItems(string accountNumber, ICollection<StatementItem> statementItems)
        {
            AddStatementItemsToAccount(accountNumber, statementItems);
            AddStatementItemsToBudget(statementItems);
        }

        private void AddStatementItemsToBudget(ICollection<StatementItem> statementItems)
        {
            Budget.AddStatementItems(statementItems);
        }

        private void AddStatementItemsToAccount(string accountNumber, ICollection<StatementItem> statementItems)
        {
            if (!HasAccount(accountNumber))
            {
                throw new Exception($"NO account found for {accountNumber}");
            }

            var account = GetAccount(accountNumber);

            account.AddStatementItems(statementItems);
        }

        private Account GetAccount(string accountNumber)
        {
            return Accounts.Single(x => x.AccountNumber == accountNumber);
        }

        private bool HasAccount(string accountNumber)
        {
            return Accounts.Any(x => x.AccountNumber == accountNumber);
        }
    }
}
