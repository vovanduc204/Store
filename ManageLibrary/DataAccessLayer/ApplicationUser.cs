using Domain.Model.Helpers.Extensions;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser() { }
        public ApplicationUser(string userName, string email, int profileId)
        {
            UserName = userName;
            Email = email;
        }
        public ApplicationUser(string userName, string email, string password)
        {
            UserName = userName;
            Email = email;
            PasswordHash = password.HashPassword();
        }
        [NotMapped]
        public string Token { get; set; }
        [NotMapped]
        public string? RefreshToken { get; set; }
        [NotMapped]
        public DateTime RefreshTokenExpiryTime { get; set; }
        [NotMapped]
        public List<ApplicationRole> Roles { get; set; } = new List<ApplicationRole>();
        public void Modify(string userName, string email, List<ApplicationRole> roles)
        {
            Roles = roles;
            UserName = userName;
            Email = email;
        }
    }
}
