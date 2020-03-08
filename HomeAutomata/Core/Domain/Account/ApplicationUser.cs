using Microsoft.AspNetCore.Identity;
using System;

namespace HomeAutomata.Core.Domain.Account
{
    public class ApplicationUser : IdentityUser
    {
        public string Fullname { get; set; }
        public DateTime? LastLogin { get; set; }
    }
}