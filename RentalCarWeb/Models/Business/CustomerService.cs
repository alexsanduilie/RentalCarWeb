using RentalCarWeb.Models.DAO;
using RentalCarWeb.Models.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Windows.Forms;

namespace RentalCarWeb.Models.Business
{
    public class CustomerService
    {
        private static readonly CustomerService instance = new CustomerService();
        private CustomerDAO customerDAO;
        static CustomerService()
        {
        }
        private CustomerService()
        {
            customerDAO = CustomerDAO.Instance;
        }
        public static CustomerService Instance
        {
            get
            {
                return instance;
            }
        }

        public void create(Customer customer)
        {
            try
            {
                customerDAO.create(customer);
            }
            catch (SqlException)
            {
                throw;
            }
        }

        public void update(Customer customer)
        {
            try
            {
                customerDAO.update(customer);
            }
            catch (SqlException)
            {
                throw;
            }
        }

        public int confirmID(string paramValue)
        {
            try
            {
                return customerDAO.confirmID(paramValue);
            }
            catch (SqlException)
            {
                return 0;
                throw;                
            }
        }

        public List<Customer> readAll()
        {
            List<Customer> customers = new List<Customer>();
            try
            {
                customers = customerDAO.readAll();
                return customers;
            }
            catch (SqlException)
            {
                return customers;
                throw;               
            }
        }

    }
}