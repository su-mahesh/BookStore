using System;
using System.Collections.Generic;
using System.Security.Claims;
using BusinessLayer;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Book_Store.Controllers.AdminController
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookManagementBL bookManagementBL;
        public BookController(IBookManagementBL bookManagementBL)
        {
            this.bookManagementBL = bookManagementBL;
        }
        [Authorize(Roles = Role.Admin)]
        [HttpPost]
        public IActionResult AddBook(RequestBook Book)
        {
            if (Book == null)
            {
                return BadRequest("Book is null.");
            }
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                    bool result = bookManagementBL.AddBook(Book);
                    if (result)
                    {
                        return Ok(new { success = true, Message = "book added", result });
                    }
                }
                return BadRequest(new { success = false, Message = "book adding Unsuccessful" });
            }
            catch (Exception exception)
            {
                return BadRequest(new { success = false, exception.Message });
            }
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetBooks()
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    ICollection<ResponseBook> books = bookManagementBL.GetBooks();
                    if (books != null)
                    {
                        return Ok(new { success = true, Message = "books fetched", books });
                    }
                }
                return BadRequest(new { success = false, Message = "books fetch Unsuccessful" });
            }
            catch (Exception exception)
            {
                return BadRequest(new { success = false, exception.Message });
            }
        }
        [Authorize(Roles = Role.Admin)]
        [HttpDelete("BookID")]
        public IActionResult DeleteBook(long BookID) 
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    bool result = bookManagementBL.DeleteBook(BookID);
                    if (result)
                    {
                        return Ok(new { success = true, Message = "books deleted", result });
                    }
                }
                return BadRequest(new { success = false, Message = "book delete Unsuccessful" });
            }
            catch (Exception exception)
            {
                return BadRequest(new { success = false, exception.Message });
            }
        }
    }
}
