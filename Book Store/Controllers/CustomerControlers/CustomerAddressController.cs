using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BusinessLayer.CustomerIntrfaces;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Book_Store.Controllers.CustomerControlers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = Role.Customer)]
    public class CustomerAddressController : ControllerBase
    {
        private readonly ICustomerAddressBL CustomerAddressBL;

        public CustomerAddressController(ICustomerAddressBL CustomerAddressBL)
        {
            this.CustomerAddressBL  = CustomerAddressBL;
        }

        [HttpPost]
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
                    address.CustomerID = CustomerID;
                    bool result = CustomerAddressBL.AddCustomerAddress(address);
                    if (result)
                    {
                        return Ok(new { success = true, Message = "Customer address added", result });
                    }

                }
                return BadRequest(new { success = false, Message = "Customer address adding Unsuccessful" });
            }
            catch (Exception exception)
            {
                return BadRequest(new { success = false, exception.Message });
            }
        }

        [HttpDelete("{AddressID}")]
        public IActionResult AddCustomerAddress(long AddressID)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                    string CustomerID = claims.Where(p => p.Type == "CustomerID").FirstOrDefault()?.Value;
                    string UserType = claims.Where(p => p.Type == "UserType").FirstOrDefault()?.Value;
                    bool result = CustomerAddressBL.DeleteCustomerAddress(CustomerID, AddressID);
                    if (result)
                    {
                        return Ok(new { success = true, Message = "Customer address deleted", result });
                    }
                }
                return BadRequest(new { success = false, Message = "Customer address delete Unsuccessful" });
            }
            catch (Exception exception)
            {
                return BadRequest(new { success = false, exception.Message });
            }
        }
        [HttpGet]
        public IActionResult GetAllCustomerAddress()
        {
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
                        ICollection<CustomerAddressResponse> address = CustomerAddressBL.GetAllCustomerAddress(CustomerID);
                        if (address != null)
                        {
                            return Ok(new { success = true, Message = "Customer address fetched", address });
                        }
                    }
                }
                return BadRequest(new { success = false, Message = "Customer address fetch Unsuccessful" });
            }
            catch (Exception exception)
            {
                return BadRequest(new { success = false, exception.Message });
            }
        }
    }
}
