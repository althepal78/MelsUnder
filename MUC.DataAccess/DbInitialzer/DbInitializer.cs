using Microsoft.AspNetCore.Identity;
using MUC.DataAccess.Data;
using MUC.Models;
using MUC.Utilities;

namespace MUC.DataAccess.DbInitialzer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _db;

        public DbInitializer(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext db)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _db = db;
        }
        public void Initialize()
        {
            //implement migrations if none are applied


            if (!_roleManager.RoleExistsAsync(StaticDetails.Role_Admin).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(StaticDetails.Role_Admin)).GetAwaiter().GetResult();

                //if roles ar not create, then create an admin user
               _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "Melysundergroundcuisine@gmail.com",
                    Email = "Melysundergroundcuisine@gmail.com",
                    Name = "Mely",
                    PhoneNumber = "5089638048",
                    StreetAddress = "16 Tracy Place",
                    State = "MA",
                    PostalCode = "01603",
                    City = "Worcester"
                }, "LoveToCook85$$").GetAwaiter().GetResult();

                ApplicationUser user = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "Melysundergroundcuisine@gmail.com");
                _userManager.AddToRoleAsync(user, StaticDetails.Role_Admin).GetAwaiter().GetResult();

                _userManager.CreateAsync(new ApplicationUser
                
                {
                    UserName = "mister01610@yahoo.com",
                    Email = "mister01610@yahoo.com",
                    Name = "Jose",
                    PhoneNumber = "5085586288",
                    StreetAddress = "16 Tracy Place",
                    State = "MA",
                    PostalCode = "01603",
                    City = "Worcester"
                }, "LoveToCook77$$").GetAwaiter().GetResult();

                ApplicationUser user2 = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "mister01610@yahoo.com");
                _userManager.AddToRoleAsync(user2, StaticDetails.Role_Admin).GetAwaiter().GetResult();

                _userManager.CreateAsync(new ApplicationUser

                {
                    UserName = "althepal78@gmail.com",
                    Email = "althepal78@gmail.com",
                    Name = "Jose",
                    PhoneNumber = "9787993573",
                    StreetAddress = "6433 Toledo Road",
                    State = "FL",
                    PostalCode = "34606",
                    City = "Worcester"
                }, "adminUser78$$").GetAwaiter().GetResult();

                //    


                ApplicationUser user3 = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "althepal78@gmail.com");
                _userManager.AddToRoleAsync(user3, StaticDetails.Role_Admin).GetAwaiter().GetResult();
            }
            return;
        }
    }
}
