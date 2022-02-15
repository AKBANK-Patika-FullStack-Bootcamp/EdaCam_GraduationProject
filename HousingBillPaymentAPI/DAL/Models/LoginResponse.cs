using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class LoginResponse
    {
        public bool isAdmin { get; set; }
        public string Username { get; set; }

        public string Token { get; set; }
    }
}
