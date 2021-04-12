using System;
using System.Collections.Generic;
using System.Text;
using BusinessLayer.AdminInterfaces;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using RepositoryLayer;
using RepositoryLayer.AdminInterfaces;

namespace BusinessLayer
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

        public bool DeleteBook(long bookID)
        {
            try
            {
                return bookManagementRL.DeleteBook(bookID);
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

        public ResponseBook UpdateBook(RequestBook book)
        {
            try
            {
                return bookManagementRL.UpdateBook(book);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
