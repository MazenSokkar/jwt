using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using jwt.Dtos;
using System.Linq.Expressions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace jwt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        [HttpPost]
        public ActionResult login(userData data)
        {
            //validate username and password
            if(data.username =="admin" && data.password == "123")
            {
                #region Define Claims

                List<Claim> usData = new List<Claim>();
                usData.Add(new Claim("name",data.username));
                usData.Add(new Claim(ClaimTypes.MobilePhone,"0123456789"));

                #endregion


                //generate token
                //install 2 packages:
                //Microsoft.AspNetCore.Authentication.JwtBearer
                //System.IdentityModel.Tokens.Jwt
                #region Create Token
                // token consists of:
                // 1- header -> type, hashing algorithm
                // 2- payload -> claims, expiry date
                // 3- signture -> hashed secret key + header + payload

                // defining secret key (to define signing credentials type we define it with its name)
                // Define secret key
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("secret key"));
                var signcer = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    // no need for header information because type will be declared and hash algorithm will be used in signture
                    
                    // payload info
                    claims: usData,
                    expires: DateTime.Now.AddDays(30),

                    //signture info
                    signingCredentials: signcer // hash algo + secret key
                    );

                // NOW WE HAVE TOKEN AS A OBJECT, WE NEED TO ENCODE IT TO STRING WITH JWTSECURETYTOKENHANDLER
                var stringToken = new JwtSecurityTokenHandler().WriteToken(token);

                #endregion

                return Ok(stringToken);
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
