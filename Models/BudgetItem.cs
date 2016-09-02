using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatementHelper.Models
{
    public class BudgetItem
    {
        public BudgetItem(string name, decimal availableAmount)
        {
            Name = name;
            AvailableAmount = availableAmount;
        }

        public BudgetItem(string name, decimal availableAmount, ICollection<string> bankStatementFilters)
        {
            Name = name;
            AvailableAmount = availableAmount;
            BankStatementFilters = bankStatementFilters;
        }

        public string Name { get; set; }

        public decimal AvailableAmount { get; set; }

        public ICollection<string> BankStatementFilters { get; set; }

        public Category Category { get; set; }
    }
}
