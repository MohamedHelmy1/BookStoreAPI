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
    public class CustomerController : ControllerBase
    {
        UserManager<IdentityUser> usermanager;
        RoleManager<IdentityRole> rolemanager;
        public CustomerController(UserManager<IdentityUser> usermanager, RoleManager<IdentityRole> rolemanager)
        {
            this.usermanager = usermanager;
            this.rolemanager = rolemanager;
        }

        [HttpPost]
        
        public IActionResult create(addAuthorDTO ct)
        {
           // IdentityRole _role = rolemanager.FindByNameAsync("admin").Result;

            Customer cust = new Customer()
            {
                Email = ct.email,
                UserName = ct.username,
                fullname = ct.fullname,
                address = ct.address,
                PhoneNumber = ct.phonenumber,
            };
          IdentityResult r=  usermanager.CreateAsync(cust, ct.password).Result;
            if (r.Succeeded)
            {
                
                    IdentityResult rr = usermanager.AddToRoleAsync(cust,"customer").Result;
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
                Customer _cust = (Customer)usermanager.FindByIdAsync(_customer.id).Result;
                if (_cust == null) return NotFound();
                _cust.fullname = _customer.fullname;
                _cust.address = _customer.address;
                _cust.PhoneNumber= _customer.phonenumber;
                _cust.UserName = _customer.username;
                _cust.Email = _customer.email;

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
                Customer _cust = (Customer)usermanager.FindByIdAsync(pass.id).Result;
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
            var users = usermanager.GetUsersInRoleAsync("customer").Result.OfType<Customer>().ToList();
            if (!users.Any()) return NotFound();
            List<SelectCustomerDTO> custDTO = new List<SelectCustomerDTO>();
            foreach (var user in users) {
                SelectCustomerDTO cDTO = new SelectCustomerDTO()
                {
                    id = user.Id,
                    fullname = user.fullname,
                    address = user.address,
                    username = user.UserName,
                    email = user.Email,
                    phonenumber = user.PhoneNumber
                };
                custDTO.Add(cDTO);
            }
            return Ok(custDTO);
        }
        [HttpGet("{id}")]
        public IActionResult getbyid(string id)
        {
          
           var cu =(Customer) usermanager.GetUsersInRoleAsync("customer").Result.Where(n => n.Id == id).FirstOrDefault();
           // var cu = usermanager.Users.Where(n => n.Id == id).FirstOrDefault();
            if(cu == null) return NotFound();
            SelectCustomerDTO custdto = new SelectCustomerDTO()
            {
                address = cu.address,
                fullname = cu.fullname,
                email = cu.Email,
                phonenumber = cu.PhoneNumber,
                id = cu.Id,
                username = cu.UserName
            };

            return Ok(custdto);
        }

    }
}
