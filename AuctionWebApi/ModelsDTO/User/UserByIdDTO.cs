using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionWebApi.ModelsDTO.User
{
    public class UserByIdDTO
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        public string Description { get; set; }

    }
}
