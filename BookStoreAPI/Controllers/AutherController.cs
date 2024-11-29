using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BookStoreAPI.Models;
using BookStoreAPI.DTOs.CustomerDTO;
using Microsoft.AspNetCore.Authorization;

namespace BookStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AutherController : ControllerBase
    {
        UserManager<IdentityUser> usermanager;
        RoleManager<IdentityRole> rolemanager;
        public AutherController(UserManager<IdentityUser> usermanager, RoleManager<IdentityRole> rolemanager)
        {
            this.usermanager = usermanager;
            this.rolemanager = rolemanager;
        }

        [HttpPost]
        
        public IActionResult create(addAuthorDTO ct)
        {
           // IdentityRole _role = rolemanager.FindByNameAsync("admin").Result;

            Author cust = new Author()
            {
                Email = ct.email,
                UserName = ct.username,
                name = ct.fullname,
                bio = ct.bio,
                PhoneNumber = ct.phonenumber,
                numberOfBooks=ct.numberOfBooks
            };
          IdentityResult r=  usermanager.CreateAsync(cust, ct.password).Result;
            if (r.Succeeded)
            {
                
                    IdentityResult rr = usermanager.AddToRoleAsync(cust,"Author").Result;
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

        [HttpPut]
        public IActionResult editprofile(EditAuthorDTO _customer)
        {
           
            if (ModelState.IsValid)
            {
                Author _cust = (Author)usermanager.FindByIdAsync(_customer.id).Result;
                if (_cust == null) return NotFound();
                _cust.name = _customer.fullname;
               
                _cust.PhoneNumber= _customer.phonenumber;
                _cust.UserName = _customer.username;
                _cust.Email = _customer.email;
                _cust.age = _customer.age;
                _cust.bio=_customer.bio;
                _cust.numberOfBooks= _customer.numberOfBooks;

              var r=  usermanager.UpdateAsync(_cust).Result;
            
                if (r.Succeeded)
                    return NoContent();
                else
                    return BadRequest(r.Errors);

                       
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPost("changepassword")]
        public IActionResult changepassword(changePasswordDTO pass)
        {
            if (ModelState.IsValid)
            {
                Author _cust = (Author)usermanager.FindByIdAsync(pass.id).Result;
                var r = usermanager.ChangePasswordAsync(_cust, pass.oldpassword, pass.newpassword).Result;
                if (r.Succeeded)
                    return Ok();
                else
                    return BadRequest(r.Errors);
            }
            else
            {

            return BadRequest(ModelState); 
            }
        }

        [HttpGet]
        // [AllowAnonymous]
         [Authorize(Roles ="admin,customer")] //or

        //[Authorize(Roles = "admin")]//and
        //[Authorize(Roles = "customer")]
        public IActionResult getall()
        {
            var users = usermanager.GetUsersInRoleAsync("Author").Result.OfType<Author>().ToList();
            if (!users.Any()) return NotFound();
            List<SelectCustomerDTO> custDTO = new List<SelectCustomerDTO>();
            foreach (var user in users) {
                SelectCustomerDTO cDTO = new SelectCustomerDTO()
                {
                    id = user.Id,
                    fullname = user.name,
                    username = user.UserName,
                    email = user.Email,
                    phonenumber = user.PhoneNumber,
                    numberOfBooks=user.numberOfBooks,
                    age=user.age,
                    bio=user.bio
                };
                custDTO.Add(cDTO);
            }
            return Ok(custDTO);
        }
        [HttpGet("{id}")]
        public IActionResult getbyid(string id)
        {
          
           var user = (Author) usermanager.GetUsersInRoleAsync("Author").Result.Where(n => n.Id == id).FirstOrDefault();
           // var cu = usermanager.Users.Where(n => n.Id == id).FirstOrDefault();
            if(user == null) return NotFound();
            SelectCustomerDTO custdto = new SelectCustomerDTO()
            {
                id = user.Id,
                fullname = user.name,
                username = user.UserName,
                email = user.Email,
                phonenumber = user.PhoneNumber,
                numberOfBooks = user.numberOfBooks,
                age = user.age,
                bio = user.bio
            };

            return Ok(custdto);
        }

    }
}
