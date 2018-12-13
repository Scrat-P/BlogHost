using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace BlogHost.DAL.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public byte[] ProfilePicture { get; set; }
    }
}
