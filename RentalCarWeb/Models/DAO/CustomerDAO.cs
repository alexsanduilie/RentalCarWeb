using RentalCarWeb.Models.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Windows.Forms;

namespace RentalCarWeb.Models.DAO
{
    public class CustomerDAO
    {
        private static readonly CustomerDAO instance = new CustomerDAO();

        static CustomerDAO()
        {
        }

        private CustomerDAO()
        {
        }

        public static CustomerDAO Instance
        {
            get
            {
                return instance;
            }
        }

        private static string table_Name = "Customers";

        public void create(Customer customer)
        {
            string insertSQL = "INSERT INTO " + table_Name + " VALUES(@Name, @Date, @City, @ZIP);";
            SqlDataAdapter dataAdapter = new SqlDataAdapter();

            using (SqlCommand cmd = new SqlCommand(insertSQL, MvcApplication.conn))
            {
                try
                {
                    cmd.Parameters.AddWithValue("@Name", customer.name);
                    cmd.Parameters.AddWithValue("@Date", customer.birthDate);
                    cmd.Parameters.AddWithValue("@City", customer.location);
                    cmd.Parameters.AddWithValue("@ZIP", customer.zipCode);

                    dataAdapter.InsertCommand = cmd;
                    dataAdapter.InsertCommand.ExecuteNonQuery();                   
                }
                catch (SqlException)
                {
                    throw;
                }
                finally
                {
                    cmd.Parameters.Clear();
                    cmd.Dispose();
                }
            }
        }

        public void update(Customer customer)
        {
            string updateSQL = "UPDATE Customers SET Name = @Name, BirthDate = @Date, Location = @City, ZIPCode = @ZIP WHERE CostumerID = @ID;";
            SqlDataAdapter dataAdapter = new SqlDataAdapter();

            using (SqlCommand cmd = new SqlCommand(updateSQL, MvcApplication.conn))
            {
                try
                {
                    cmd.Parameters.AddWithValue("@ID", customer.customerID);
                    cmd.Parameters.AddWithValue("@Name", customer.name);
                    cmd.Parameters.AddWithValue("@Date", customer.birthDate);
                    cmd.Parameters.AddWithValue("@City", customer.location);
                    cmd.Parameters.AddWithValue("@ZIP", customer.zipCode);

                    dataAdapter.UpdateCommand = cmd;
                    dataAdapter.UpdateCommand.ExecuteNonQuery();
                }
                catch (SqlException)
                {
                    throw;
                }
                finally
                {
                    cmd.Parameters.Clear();
                    cmd.Dispose();
                }
            }
        }

        public int confirmID(string paramValue)
        {
            int no = 0;
            string getID = "SELECT CostumerID FROM " + table_Name + " WHERE CostumerID = @CustomerID;";

            using (SqlCommand cmd = new SqlCommand(getID, MvcApplication.conn))
            {
                SqlDataReader dr = null;
                try
                {
                    cmd.Parameters.AddWithValue("@CustomerID", paramValue);
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        no = dr.GetInt32(no);
                    } 
                    return no;
                }
                catch (SqlException)
                {
                    return no;
                    throw;                   
                }
                finally
                {
                    dr.Close();
                    cmd.Parameters.Clear();
                    cmd.Dispose();
                }
            }

        }

        public List<Customer> readAll()
        {
            string readSQL = "SELECT * FROM " + table_Name;
            List<Customer> customers = new List<Customer>();

            using (SqlCommand cmd = new SqlCommand(readSQL, MvcApplication.conn))
            {
                SqlDataReader dr = null;
                try
                {
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        customers.Add(new Customer(Int32.Parse(dr["CostumerID"].ToString()), dr["Name"].ToString(), DateTime.Parse(dr["BirthDate"].ToString()), dr["Location"].ToString(), Int32.Parse(dr["ZIPCode"].ToString())));
                    }
                    return customers;
                }
                catch (SqlException)
                {
                    return customers;
                    throw;
                }
                finally
                {
                    dr.Close();
                    cmd.Parameters.Clear();
                    cmd.Dispose();
                }
            }
        }

    }
}