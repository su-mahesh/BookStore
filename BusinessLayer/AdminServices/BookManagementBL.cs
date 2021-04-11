﻿using System;
using System.Collections.Generic;
using System.Text;
using BusinessLayer.AdminInterfaces;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using RepositoryLayer.AdminInterfaces;

namespace BusinessLayer.AdminServices
{
    public class BookManagementBL : IBookManagementBL
    {
        IBookManagementRL bookManagementRL;

        public BookManagementBL(IBookManagementRL bookManagementRL)
        {
            this.bookManagementRL = bookManagementRL;
        }

        public bool AddBook(RequestBook book)
        {
            try
            {
               return bookManagementRL.AddBook(book);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ICollection<ResponseBook> GetBooks()
        {
            try
            {
                return bookManagementRL.GetBooks();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
