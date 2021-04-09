using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.CustomerIntrfaces;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using Microsoft.AspNetCore.Http;
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

        [HttpPost("RegisterUser")]
        public IActionResult RegisterUser(RegisterCustomerAccount Customer)
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
    }
}
