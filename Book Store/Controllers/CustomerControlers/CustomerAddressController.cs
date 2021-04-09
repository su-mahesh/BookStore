﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BusinessLayer.CustomerIntrfaces;
using CommonLayer.RequestModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Book_Store.Controllers.CustomerControlers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomerAddressController : ControllerBase
    {
        private readonly IConfiguration config;
        private readonly ICustomerAccountBL CustomerAccountBL;
        private readonly ICustomerAddressBL CustomerAddressBL;

        public CustomerAddressController(IConfiguration config, ICustomerAccountBL customerAccountBL, ICustomerAddressBL CustomerAddressBL)
        {
            this.config = config;
            CustomerAccountBL = customerAccountBL;
            this.CustomerAddressBL  = CustomerAddressBL;
        }

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
                        bool result = CustomerAddressBL.AddCustomerAddress(address);
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
