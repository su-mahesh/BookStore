﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.AdminInterfaces;

namespace RepositoryLayer
{
    public class BookManagementRL : IBookManagementRL
    {
        readonly SqlConnection connection = new SqlConnection();
        readonly string sqlConnectString;
        private readonly IConfiguration config;
        readonly string InserBookRecordSQL = "InserBookRecord";
        readonly string GetBookRecordSQL = "GetBookRecord";

        public BookManagementRL(IConfiguration config)
        {
            this.config = config;
            sqlConnectString = config.GetConnectionString("BookStoreConnection");
            connection.ConnectionString = sqlConnectString + "Connection Timeout=30;Connection Lifetime=0;Min Pool Size=0;Max Pool Size=100;Pooling=true;";
        }
        public bool AddBook(RequestBook book)
        {
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(InserBookRecordSQL, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("BookName", book.BookName);
                cmd.Parameters.AddWithValue("BookDiscription", book.BookDiscription);
                cmd.Parameters.AddWithValue("BookImage", book.BookImage);
                cmd.Parameters.AddWithValue("BookPrice", book.BookPrice);
                cmd.Parameters.AddWithValue("BookQuantity", book.Quantity);
                cmd.Parameters.AddWithValue("AuthorName", book.AuthorName);
                var returnParameter = cmd.Parameters.Add("@Result", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;
                SqlDataReader rd = cmd.ExecuteReader();
                var result = returnParameter.Value;
                if (result != null && result.Equals(1))
                {
                    return true;
                }
                return false;
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
                ResponseBook Book;
                ICollection<ResponseBook> Books = new List<ResponseBook>();
                connection.Open();
                SqlCommand cmd = new SqlCommand(GetBookRecordSQL, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    Book = new ResponseBook();
                    Book.BookID = rd["BookID"] == DBNull.Value ? default : rd.GetInt64("BookID");
                    Book.BookDiscription = rd["BookDiscription"] == DBNull.Value ? default : rd.GetString("BookDiscription");
                    Book.BookPrice = rd["BookPrice"] == DBNull.Value ? default : rd.GetInt32("BookPrice");
                    Book.BookName = rd["BookName"] == DBNull.Value ? default : rd.GetString("BookName");
                    Book.AuthorName = rd["AuthorName"] == DBNull.Value ? default : rd.GetString("AuthorName");
                    Book.BookImage = rd["BookImage"] == DBNull.Value ? default : rd.GetString("BookImage");
                    Book.Quantity = rd["BookQuantity"] == DBNull.Value ? default : rd.GetInt32("BookQuantity");
                    Books.Add(Book);
                }
                return Books;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}