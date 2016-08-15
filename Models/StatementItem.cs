using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatementHelper.Models
{
    public class StatementItem
    {
        public DateTime DateTime { get; set; }

        public string Description { get; set; }

        public decimal Amount { get; set; }

        public decimal Balance { get; set; }

        public Category Category { get; set; }
    }
}
