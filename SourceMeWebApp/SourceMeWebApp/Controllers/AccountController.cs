using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SourceMe.Storage.Data.Entities;
using SourceMeWebApp.Models;

namespace SourceMeWebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<StoreUser> _signInManager;
        private readonly UserManager<StoreUser> _userManager;
        private readonly IConfiguration _config;

        public AccountController(SignInManager<StoreUser> signInManager,UserManager<StoreUser>userManager,IConfiguration config)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _config = config;
        }

        public IActionResult Login()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index","App");
            }
            return View();


        }

        [HttpPost]
        public async Task<IActionResult> Login(Login model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    if (Request.Query.Keys.Contains("ReturnUrl"))
                    {
                        Redirect(Request.Query["ReturnUrl"].First());
                    }
                    else
                    {
                        RedirectToAction("Index", "App");
                    }

                }
            }

            ModelState.AddModelError("", "Failed to login");
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> CreateToken([FromBody]Login model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);

                if (user != null)
                {
                    var matchPassword = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
                    if (matchPassword.Succeeded)
                    {
                        var claims = new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Sub,user.Email),
                            new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.UniqueName,user.UserName)
                        };
                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:key"]));
                        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                        var token = new JwtSecurityToken(
                            _config["Tokens:Issuer"],
                            _config["Tokens:Audience"],
                            claims,
                            expires: DateTime.UtcNow.AddHours(9),
                            signingCredentials: cred

                            );


                        var results = new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo
                        };

                        return Created("", results);
                    }

                }
                else
                {
                }
            }

            return BadRequest();
        }



        [HttpPost]
        public async Task<IActionResult> SignUpUserasync([FromBody] Registration model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                var name =await _userManager.FindByNameAsync(model.UserName);
                if (user == null&&name==null)
                {
                    user = new StoreUser()
                    {
                        FirstName=model.FirstName,
                        LastName=model.LastName,
                        Email=model.Email,
                        UserName=model.UserName,
                       // PasswordHash=model.Password

                    };
                }
                var result =await _userManager.CreateAsync(user, model.Password);
                if (result != IdentityResult.Success)
                {

                    throw new InvalidOperationException("Sorry Master!I have failed you!");
                }
                return Ok();
               
                
                

            }
            return BadRequest();
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}