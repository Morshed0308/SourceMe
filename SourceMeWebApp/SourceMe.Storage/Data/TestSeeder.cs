using Microsoft.AspNetCore.Hosting;
using SourceMe.Storage.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace SourceMe.Storage.Data
{
    public class TestSeeder
    {
        private readonly IHostingEnvironment _host;
        private readonly UserManager<StoreUser> _userManager;

        //  private readonly UserManager<StoreUser> userManager;

        public TestSeeder(IHostingEnvironment host,UserManager<StoreUser>userManager)
        {
            _host = host;
            _userManager = userManager;
        }
        public async Task seed()
        {
            var user =await _userManager.FindByEmailAsync("Dummy@mail.com");
            if (user==null)
            {
                user = new StoreUser()
                {
                    FirstName = "Jhonny",
                    LastName = "Bravo",
                    UserName = "Dummy@mail.com",
                    Email = "Dummy@mail.com"                
                };
                var result =await _userManager.CreateAsync(user,"P@ssw0rd!");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Failed to create user");
                }
            }
        }
    }
}
