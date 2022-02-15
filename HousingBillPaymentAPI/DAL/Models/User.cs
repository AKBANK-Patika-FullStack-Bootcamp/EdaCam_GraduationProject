using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string TCKN { get; set; }
        public string EMail { get; set; }
        public string Phone { get; set; }
        public string CarPlate { get; set; }
        public string? Password { get; set; }
        public bool IsAdmin { get; set; }

    }
}
