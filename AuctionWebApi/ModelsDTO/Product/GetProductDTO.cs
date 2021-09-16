using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionWebApi.ModelsDTO.Product
{
    public class GetProductDTO
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public int BidNr { get; set; }
    }
}
