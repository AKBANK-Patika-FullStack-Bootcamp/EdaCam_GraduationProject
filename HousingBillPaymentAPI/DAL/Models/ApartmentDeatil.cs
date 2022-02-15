using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class ApartmentDeatil
    {
        public int Id { get; set; }
        public string? User { get; set; } = "";
        public string Block { get; set; }
        public bool IsEmpty { get; set; }
        public string Type { get; set; }
        public int Floor { get; set; }
        public int ApartmentNo { get; set; }
    }
}
