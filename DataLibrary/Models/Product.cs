﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
  public  class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
      
       public bool InProcess { get; set; }
        public string UserId { get; set; }

        public ICollection<Bid> Bids { get; set; }
        public string CreatedDate { get; set; }
    public virtual Category Category { get; set; }
    }
}
