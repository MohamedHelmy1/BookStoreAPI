using BookStoreAPI.DTOs.accountDTO;
using BookStoreAPI.DTOs.CustomerDTO;
using BookStoreAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        SignInManager<IdentityUser> _signin;
        UserManager<IdentityUser> _manager;
        public AccountController(SignInManager<IdentityUser> _signin, UserManager<IdentityUser> _manager)
        {
            this._signin = _signin;   
            this._manager = _manager;
        }
        //login
        [HttpPost]
        public IActionResult login(loginDTO logindata)
        {
         var r=   _signin.PasswordSignInAsync(logindata.username, logindata.password, false, false).Result;
            if (r.Succeeded)
            {
              var _user=  _manager.FindByNameAsync(logindata.username).Result;
                //generate JWT tokens....

                #region claims

                List<Claim> userdata = new List<Claim>();
                userdata.Add(new Claim(ClaimTypes.Name, _user.UserName));
                userdata.Add(new Claim(ClaimTypes.NameIdentifier,_user.Id));

                var roles = _manager.GetRolesAsync(_user).Result;
                foreach (var itemRole in roles)
                {
                    userdata.Add(new Claim(ClaimTypes.Role, itemRole));
                }
                #endregion
                #region secret key
                string key = "welcome to my secret key mohamed elshafie";
                var secertkey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
                #endregion

                var signingcer = new SigningCredentials(secertkey, SecurityAlgorithms.HmacSha256);
                #region generate token 
                var token = new JwtSecurityToken(
                     claims: userdata,
                     expires: DateTime.Now.AddDays(2),
                     signingCredentials: signingcer
                     );

                //token object => encoded string
                var tokenstring = new JwtSecurityTokenHandler().WriteToken(token);
                #endregion
                return Ok(tokenstring);
            }

            else return Unauthorized("inalid username or pasword");
        
        
        }


        //changepassword

        [HttpPost("changepassword")]
        [Authorize]
        public IActionResult changepassword(changePasswordDTO pass)
        {
            if (ModelState.IsValid)
            {
                var _cust = _manager.FindByNameAsync(User.Identity.Name).Result;
                var r = _manager.ChangePasswordAsync(_cust, pass.oldpassword, pass.newpassword).Result;
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

        //logout

        [HttpGet("logout")]
        [Authorize]
        public IActionResult logout()
        {
            _signin.SignOutAsync();
             return Ok();
        }
    }
}
