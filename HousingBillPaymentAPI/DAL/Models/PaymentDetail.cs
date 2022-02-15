using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class PaymentDetail
    {
        public int Id { get; set; }
        public String User { get; set; }
        public DateTime DebtDate { get; set; }
        public double Amount { get; set; }
        public String DebtType { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
