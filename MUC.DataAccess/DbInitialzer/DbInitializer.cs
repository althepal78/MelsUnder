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
                    UserName = "papichulo@test.com",
                    Email = "papichulo@test.com",
                    Name = "papi chulo",
                    PhoneNumber = "9787999999",
                    StreetAddress = "my stree434",
                    State = "FL",
                    PostalCode = "33614",
                    City = "Tampa"
                }, "adminUSER78$$").GetAwaiter().GetResult();

                //   site name for now https://melysundergroundcusine.azurewebsites.net/


                ApplicationUser user = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "papichulo@test.com");
                _userManager.AddToRoleAsync(user, StaticDetails.Role_Admin).GetAwaiter().GetResult();
            }
            return;
        }
    }
}
