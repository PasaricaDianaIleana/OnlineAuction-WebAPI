using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionWebApi.ModelsDTO.Bid
{
    public class AddBidDTO
    {
       
        public int Price { get; set; }
        public int ProductId { get; set; }
        public string UserId { get; set; }
    }
}
