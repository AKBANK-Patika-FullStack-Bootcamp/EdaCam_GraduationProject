using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Debt
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int DebtTypeId { get; set; }
        public DateTime Date { get; set; }
        public double Amount { get; set; }

    }
}
