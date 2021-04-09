using System;
using System.Collections.Generic;
using System.Text;
using BusinessLayer.CustomerIntrfaces;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.CutomerInterfaces;

namespace BusinessLayer.CustomerServices
{
    public class CustomerAccountBL : ICustomerAccountBL
    {
        private readonly ICustomerAccountRL customerAccountRL;
        private readonly IConfiguration config;
        PasswordEncryption passwordEncryption = new PasswordEncryption();

        public CustomerAccountBL(ICustomerAccountRL customerAccountRL, IConfiguration config)
        {
            this.customerAccountRL = customerAccountRL;
            this.config = config;
        }

        public CustomerAccount RegisterCustomer(RegisterCustomerAccount registerCustomerAccount)
        {
            try
            {
                registerCustomerAccount.Password = passwordEncryption.EncryptPassword(registerCustomerAccount.Password);
                return customerAccountRL.RegisterCustomer(registerCustomerAccount);
            }
            catch (Exception)
            {
                throw;
            }
            
        }
    }
}
