
using Microsoft.AspNetCore.Mvc;

namespace DAL.Models
{
    public class LoginDto
    {
        [FromHeader]
        public string UserMail { get; set; } = string.Empty;
        [FromHeader]
        public string Password { get; set; } = string.Empty;
    }
}
