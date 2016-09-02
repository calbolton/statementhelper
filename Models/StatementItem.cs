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

        public override bool Equals(object obj)
        {
            if (!(obj is StatementItem))
            {
                throw new Exception("Invalid object");
            }

            var compare = (StatementItem) obj;

            return Equals(compare);
        }

        private bool Equals(StatementItem other)
        {
            return DateTime.Equals(other.DateTime) && string.Equals(Description, other.Description) && Amount == other.Amount && Balance == other.Balance;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = DateTime.GetHashCode();
                hashCode = (hashCode*397) ^ (Description != null ? Description.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ Amount.GetHashCode();
                hashCode = (hashCode*397) ^ Balance.GetHashCode();
                return hashCode;
            }
        }
    }
}
