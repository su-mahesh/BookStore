using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BusinessLayer.CustomerIntrfaces;
using CommonLayer.ResponseModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Book_Store.Controllers.CustomerControlers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartController : ControllerBase
    {
        ICustomerCartBL customerCartBL;

        public CartController(ICustomerCartBL customerCartBL)
        {
            this.customerCartBL = customerCartBL;
        }

        [HttpPost("Add/{BookID}")]
        public IActionResult AddBookToCart(long BookID) 
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
                        ICollection<CustomerCart> cart = customerCartBL.AddBookToCart(CustomerID, BookID);
                        if (cart != null)
                        {
                            return Ok(new { success = true, Message = "book added to cart", cart });
                        }
                    }
                }
                return BadRequest(new { success = false, Message = "book add to cart Unsuccessful" });
            }
            catch (Exception exception)
            {
                return BadRequest(new { success = false, exception.Message });
            }
        }
    }
}
