using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
   public class Bid
    {
        public int BidId { get; set; }
        public int Price { get; set; }
        public int ProductId { get; set; }

        public string UserId { get; set; }

    }
}
