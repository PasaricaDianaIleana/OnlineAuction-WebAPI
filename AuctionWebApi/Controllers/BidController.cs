using DataLibrary.Repository;
using Microsoft.AspNetCore.Mvc;
using AuctionWebApi.ModelsDTO.Bid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLibrary.Models;

namespace AuctionWebApi.Controllers
{
    public class BidController : ControllerBase
    {
        private readonly IBidRepository _repo;

        public BidController(IBidRepository repo)
        {
            _repo = repo;
        }
        [HttpGet]
        [Route("{bidId}")]
        public async Task<ActionResult<GetBidDTO>> GetBidByIdAsync([FromRoute] int id)
        {
            var bid = await _repo.GetBidById(id);
            if (bid != null)
            {
                var data = new GetBidDTO
                {
                    Price = bid.Price,
                    ProductId = bid.ProductId,
                    UserId = bid.UserId

                };
                return Ok(data);
            }
            return NotFound();
        }
        [HttpPost]
        public async  Task<ActionResult<AddBidDTO>> AddBidAsync([FromBody] AddBidDTO bid)
        {
            if (bid==null || !ModelState.IsValid)
            {
                return BadRequest();
            }
            Bid data = new()
            {
                Price = bid.Price,
                ProductId = bid.ProductId,
                UserId = bid.UserId

            };
            await _repo.AddBid(data);
            return CreatedAtAction(nameof(GetBidByIdAsync), new { id = data.BidId }, data);
        }
    }
}
