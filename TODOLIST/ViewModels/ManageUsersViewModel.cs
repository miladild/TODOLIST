using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TODOLIST.ViewModels
{
    public class ManageUsersViewModel
    {
        public IEnumerable<IdentityUser> Administrators { get; set; }

        public IEnumerable<IdentityUser> Everyone { get; set; }
    }
}
