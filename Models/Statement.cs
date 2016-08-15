using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatementHelper.Models
{
    public class Statement
    {
        public DateTime FromDateTime { get; set; }

        public DateTime ToDateTime { get; set; } 

        public ICollection<Statement> Statements { get; set; }
    }
}
