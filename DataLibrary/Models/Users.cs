using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
  public  class Users:IdentityUser
    {
        public string ImageUrl { get; set; }
        public int BidId { get; set; }

    }
}
