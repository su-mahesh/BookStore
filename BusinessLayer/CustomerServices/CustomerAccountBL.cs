﻿using System;
using System.Collections.Generic;
using System.Text;
using BusinessLayer.CustomerIntrfaces;
using BusinessLayer.JWTAuthentication;
using BusinessLayer.MSMQ;
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
        UserAuthenticationJWT AuthJWT;
        PasswordEncryption passwordEncryption = new PasswordEncryption();
        MSMQService MSMQ;

        public CustomerAccountBL(ICustomerAccountRL customerAccountRL, IConfiguration config)
        {
            this.customerAccountRL = customerAccountRL;
            this.config = config;
            AuthJWT = new UserAuthenticationJWT(config);
            MSMQ = new MSMQService(config);
        }

        public CustomerAccount LoginCustomer(LoginCustomerAccount loginCustomerAccount)
        {
            try
            {
                
                loginCustomerAccount.Password = passwordEncryption.EncryptPassword(loginCustomerAccount.Password);
                
                var result = customerAccountRL.LoginCustomer(loginCustomerAccount);
                if (result != null)
                {
                    result.token = AuthJWT.GenerateCustomerSessionJWT(result);
                }
                return result;
            }
            catch (Exception)
            {
                throw;
            }
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

        public bool SendForgottenPasswordLink(ForgetPasswordModel forgetPasswordModel)
        {
            try
            {
                bool result = customerAccountRL.CheckCustomer(forgetPasswordModel);
                if (result)
                {
                    MSMQ.SendPasswordResetLink(forgetPasswordModel);
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
