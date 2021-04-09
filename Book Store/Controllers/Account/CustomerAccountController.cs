using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using BusinessLayer.CustomerIntrfaces;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Book_Store.Controllers.Account
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerAccountController : ControllerBase
    {
        private readonly IConfiguration config;
        private readonly ICustomerAccountBL CustomerAccountBL;

        public CustomerAccountController(IConfiguration config, ICustomerAccountBL customerAccountBL)
        {
            this.config = config;
            CustomerAccountBL = customerAccountBL;
        }

        [HttpPost("Register")]
        public IActionResult RegisterCustomer(RegisterCustomerAccount Customer)
        {
            if (Customer == null)
            {
                return BadRequest("Customer is null.");
            }
            try
            {
                CustomerAccount result = CustomerAccountBL.RegisterCustomer(Customer);
                if (result != null)
                {
                    return Ok(new { success = true, Message = "Customer Registration Successful", Customer = result });
                }
                else
                {
                    return BadRequest(new { success = false, Message = "Customer Registration Unsuccessful" });
                }
            }
            catch (Exception exception)
            {
                return BadRequest(new { success = false, exception.Message });
            }
        }

        [HttpPost("Login")]
        public IActionResult LoginUser(LoginCustomerAccount Customer)
        {
            if (Customer == null)
            {
                return BadRequest("Customer is null.");
            }
            try
            {
                CustomerAccount result = CustomerAccountBL.LoginCustomer(Customer);
                if (result != null)
                {
                    return Ok(new { success = true, Message = "Customer login Successful", Customer = result });
                }
                else
                {
                    return BadRequest(new { success = false, Message = "Customer login Unsuccessful" });
                }
            }
            catch (Exception exception)
            {
                return BadRequest(new { success = false, exception.Message });
            }
        }
        [HttpPost("ForgetPassword")]
        public IActionResult ResetForgottenPassword(ForgetPasswordModel forgetPasswordModel)
        {
            try
            {
                bool result = CustomerAccountBL.SendForgottenPasswordLink(forgetPasswordModel);

                if (result)
                {

                    return Ok(new { success = true, Message = "password reset link has been sent to your email id", email = forgetPasswordModel.Email });
                }
                else
                {
                    return BadRequest(new { success = false, Message = "email id don't exist" });
                }
            }
            catch (Exception exception)
            {
                return BadRequest(new { success = false, exception.Message });
            }
        }
        [Authorize]
        [HttpPost("ResetPassword")]
        public IActionResult ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                { 
                    IEnumerable<Claim> claims = identity.Claims;
                    var Email = claims.Where(p => p.Type == "Email").FirstOrDefault()?.Value;
                    resetPasswordModel.Email = Email;
                    bool result = CustomerAccountBL.ResetCustomerAccountPassword(resetPasswordModel);
                    if (result)
                    {
                        return Ok(new { success = true, Message = "password changed successfully" });
                    }
                }
                return BadRequest(new { success = false, Message = "password change unsuccessfull" });
            }
            catch (Exception exception)
            {
                return BadRequest(new { success = false, exception.Message });
            }
        }

        [Authorize]
        [HttpPost("Address/Add")]
        public IActionResult AddCustomerAddress(CustomerAddress address)
        {
            if (address == null)
            {
                return BadRequest("address is null.");
            }
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                    string CustomerID = claims.Where(p => p.Type == "CustomerID").FirstOrDefault()?.Value;
                    string UserType = claims.Where(p => p.Type == "UserType").FirstOrDefault()?.Value;
                    if (UserType.Equals("Customer"))
                    {
                        address.CustomerID = CustomerID;
                        bool result = CustomerAccountBL.AddCustomerAddress(address);
                        if (result)
                        {
                            return Ok(new { success = true, Message = "Customer address added", result });
                        }
                    }
                    
                }
                return BadRequest(new { success = false, Message = "Customer address adding Unsuccessful" });

            }
            catch (Exception exception)
            {
                return BadRequest(new { success = false, exception.Message });
            }
        }
    }
}
