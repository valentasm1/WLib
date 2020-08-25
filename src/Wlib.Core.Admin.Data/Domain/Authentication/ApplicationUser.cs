using System;
using Microsoft.AspNetCore.Identity;

namespace Wlib.Core.Admin.Data.Domain.Authentication
{
    public class ApplicationUser : IdentityUser//<string>
    {


        public string Name { get; set; }
        public string Address { get; set; }
        public string Postcode { get; set; }
        public string Telephone { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }

        public ApplicationUserType ApplicationUserType { get; set; }
    }
}
