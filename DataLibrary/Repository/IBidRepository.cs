using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Repository
{
   public interface IBidRepository
    {

        Task AddBid(Bid bid);
        Task<Bid> GetBidById(int id);
    }
}
