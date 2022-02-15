using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int DebtId { get; set; }
        public DateTime Date { get; set; }
    }
}
