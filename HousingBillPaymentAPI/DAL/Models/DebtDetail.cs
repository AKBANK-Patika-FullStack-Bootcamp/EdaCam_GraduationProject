using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class DebtDetail
    {
        public int Id { get; set; }
        public string User { get; set; } = "";
        public String DebtType { get; set; } = "";
        public DateTime Date { get; set; }
        public double Amount { get; set; }
    }
}
