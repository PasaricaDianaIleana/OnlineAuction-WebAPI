using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionWebApi.ModelsDTO.Product
{
    public class CreateProductDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        [Range(0,1000000)]
        public int Price { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public bool InProcess { get; set; }
        [Required]
        public string CreatedDate { get; set; }
        [Required]
        public string UserId  { get; set; }
    }
}
