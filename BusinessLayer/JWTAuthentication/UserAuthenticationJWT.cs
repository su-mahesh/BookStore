using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BusinessLayer.JWTAuthentication
{
    /// <summary>
    /// JW Token generator
    /// </summary>
    public class UserAuthenticationJWT
    {
        private readonly IConfiguration config;
        public UserAuthenticationJWT(IConfiguration config)
        {
            this.config = config;
        }

        public string GenerateCustomerSessionJWT(CustomerAccount userInfo)
        {
            DateTime ExpireTime = DateTime.Now.AddMinutes(120);
            IEnumerable<Claim> Claims = new Claim[] {
                new Claim("UserType", "Customer"),
                new Claim("CustomerID", userInfo.CustomerID.ToString()),
                new Claim("Email", userInfo.Email) };
            return GenerateJSONWebToken(userInfo, ExpireTime, Claims);
        }

        public string GenerateCustomerPasswordResetJWT(ForgetPasswordModel userInfo)
        {
            DateTime ExpireTime = DateTime.Now.AddMinutes(120);
            IEnumerable<Claim> Claims = new Claim[] {
                new Claim("UserType", "Customer"),
                new Claim("Email", userInfo.Email) };
            return GeneratePasswordResetJWT(userInfo, ExpireTime, Claims);
        }

        public string GeneratePasswordResetJWT(ForgetPasswordModel userInfo, DateTime ExpireTime, IEnumerable<Claim> Claims)
        {
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);


            var token = new JwtSecurityToken(config["Jwt:Issuer"],
              claims: Claims,
              expires: ExpireTime,
              signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public string GenerateJSONWebToken(CustomerAccount userInfo, DateTime ExpireTime, IEnumerable<Claim> Claims)
        {
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
 

            var token = new JwtSecurityToken(config["Jwt:Issuer"],
              claims: Claims,
              expires: ExpireTime,
              signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
