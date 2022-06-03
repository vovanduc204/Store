using Domain.Model;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() : base() { }
        public ApplicationRole(string name, int roleCategoryId) : base(name)
        {
            RoleCategoryId = roleCategoryId;
        }
        // For Fake Data
        public ApplicationRole(string name, RoleCategory roleCategory) : base(name)
        {
            RoleCategory = roleCategory;
            RoleCategoryId = roleCategory.Id;
        }
        public int RoleCategoryId { get; set; }
        public virtual RoleCategory RoleCategory { get; set; }
        public void Modify(string name, int roleCategoryId)
        {
            Name = name;
            RoleCategoryId = roleCategoryId;
        }
    }
}
