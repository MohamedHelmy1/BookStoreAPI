using BookStoreAPI.DTOs.AdminDTOs;
using BookStoreAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        UserManager<IdentityUser> usermanager;
        RoleManager<IdentityRole> rolemanager;
        public AdminController(UserManager<IdentityUser> usermanager, RoleManager<IdentityRole> rolemanager)
        {
            this.usermanager = usermanager;
            this.rolemanager = rolemanager;
        }

        [HttpPost]
        public IActionResult create(AddAdminDTO ad)
        {
            // IdentityRole _role = rolemanager.FindByNameAsync("admin").Result;

            Admin _admin = new Admin()
            {
                Email = ad.email,
                UserName = ad.username,
                PhoneNumber = ad.phonenumber,
            };
            IdentityResult r = usermanager.CreateAsync(_admin, ad.password).Result;
            if (r.Succeeded)
            {

                IdentityResult rr = usermanager.AddToRoleAsync(_admin, "admin").Result;
                //IdentityResult rrr = usermanager.AddToRoleAsync(_admin, "customer").Result;
                if (rr.Succeeded)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest(rr.Errors);
                }

            }
            else
                return BadRequest(r.Errors);

        }

        [HttpGet]
        public IActionResult get()
        {
           if(User.Identity.IsAuthenticated)
            {
                return Ok();
            }
            else
            {
                return Unauthorized();
            }
        }

    }
}
