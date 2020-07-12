using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TODOLIST
{
    public class SeedData
    {
        //public static async Task InitializeAsync(IServiceProvider services)
        //{
        //    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        //    await EnsureRolesAsync(roleManager);
        //    var userManager = services
        //    .GetRequiredService<UserManager<IdentityUser>>(
        //    );
        //    await EnsureTestAdminAsync(userManager);
        //}
        public static async Task InitializeAsync(IServiceScope services)
        {
            var roleManager = services.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            await EnsureRolesAsync(roleManager);
            var userManager = services.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>(
            );
            await EnsureTestAdminAsync(userManager);
        }

        /// <summary>
        /// To check the there is a TestAdmin user in the database
        /// </summary>
        /// <param name="userManager">UserManager service</param>
        /// <returns></returns>
        private static async Task EnsureTestAdminAsync(UserManager<IdentityUser> userManager)
        {
            //Check to see if there is a test admin user in the db
            var testAdmin = await userManager.Users.Where(x => x.UserName == "admin@todo.local").SingleOrDefaultAsync();
            //return the testAdmin user if it does exist in the db
            if (testAdmin != null) return;
            //Instantiate a new IdentityUser for TestAdmin
            testAdmin = new IdentityUser
            {
                UserName = "admin@todo.local",
                Email = "admin@todo.local"
            };
            //It passess the Identityuser Defined to the db with the password defined
            await userManager.CreateAsync(testAdmin, "NotSecure123!!");
            //Assigns the new testAdmin the role of Administrator
            await userManager.AddToRoleAsync(testAdmin, Constants.AdministratorRole);
        }

        /// <summary>
        /// Creates the Administrator role if does not exist in the database
        /// </summary>
        /// <param name="roleManager"></param>
        /// <returns></returns>
        private static async Task EnsureRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            //Check to see if the ADministrator role exist in the db
            var alreadyExists = await roleManager.RoleExistsAsync(Constants.AdministratorRole);

            if (alreadyExists) return;
            //Otherwise add the Administrator role to the db
            await roleManager.CreateAsync(new IdentityRole(Constants.AdministratorRole));
        }
    }
  

}
