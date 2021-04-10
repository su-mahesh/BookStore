﻿using System;
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
    public class WishListController : ControllerBase
    {
        readonly ICustomerWishListBL customerWishListBL;

        public WishListController(ICustomerWishListBL customerWishListBL)
        {
            this.customerWishListBL = customerWishListBL;
        }

        [HttpPost("{BookID}")]
        public IActionResult AddBookToWishList(long BookID)
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
                        ICollection<CustomerWishList> WishList = customerWishListBL.AddBookToWishList(CustomerID, BookID);
                        if (WishList != null)
                        {
                            return Ok(new { success = true, Message = "book added to WishList", WishList });
                        }
                    }
                }
                return BadRequest(new { success = false, Message = "book add to WishList Unsuccessful" });
            }
            catch (Exception exception)
            {
                return BadRequest(new { success = false, exception.Message });
            }
        }
        [HttpDelete("{BookID}")]
        public IActionResult RemoveBookFromWishList(long BookID)
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
                        ICollection<CustomerWishList> WishList = customerWishListBL.RemoveBookFromWishList(CustomerID, BookID);
                        if (WishList != null)
                        {
                            return Ok(new { success = true, Message = "Book removed From WishList", WishList });
                        }
                    }
                }
                return BadRequest(new { success = false, Message = "Book remove from WishList Unsuccessful" });
            }
            catch (Exception exception)
            {
                return BadRequest(new { success = false, exception.Message });
            }
        }
    }
}
