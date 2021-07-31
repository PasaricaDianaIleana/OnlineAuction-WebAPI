using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
   public class Contact
    {
        public int Id { get; set; }
        public Users Users { get; set; }
        public string Message { get; set; }
    }
}
