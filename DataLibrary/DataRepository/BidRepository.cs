using DataLibrary.Models;
using DataLibrary.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.DataRepository
{
    public class BidRepository : IBidRepository
    {
        private readonly AppDbContext _context;

        public BidRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddBid(Bid bid)
        {
            await _context.Bids.AddAsync(bid);
            await _context.SaveChangesAsync();
        }

        public async Task<Bid> GetBidById(int id)
        {
           return await _context.Bids.Where(b => b.BidId == id).FirstOrDefaultAsync();
        }
    }
}
