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
            catch (SqlException ex)
            {
                MessageBox.Show("Error inserting customer: " + ex.Message);
            }
        }

        public void update(Customer customer)
        {
            try
            {
                customerDAO.update(customer);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error updating customer: " + ex.Message);
            }
        }

        public Customer search(string customerID, string Name)
        {
            try
            {
                return customerDAO.search(customerID, Name);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error updating customer: " + ex.Message);
                return null;
            }
        }

        public int getMaxID(string customerID)
        {
            try
            {
                return customerDAO.getMaxID(customerID);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error getting customer ID: " + ex.Message);
                return 0;
            }
        }

        public int confirmID(string paramValue)
        {
            try
            {
                return customerDAO.confirmID(paramValue);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error getting customer ID: " + ex.Message);
                return 0;
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
            catch (SqlException ex)
            {
                MessageBox.Show("Error getting records: " + ex.Message);
                return customers;
            }
        }

        public DataTable readAllInDataTable()
        {
            try
            {
                return customerDAO.readAllInDataTable();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error finding data: " + ex.Message);
                return null;
            }
        }
    }
}