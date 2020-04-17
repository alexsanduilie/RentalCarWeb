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
        private static int searchCounter = 0;

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

                    cmd.Parameters.Clear();
                    cmd.Dispose();
                    //MessageBox.Show("Customer created successfully");
                }
                catch (SqlException ex)
                {
                    //MessageBox.Show("SQL error: " + ex.Message);
                    throw new Exception(ex.Message);
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

                    cmd.Parameters.Clear();
                    cmd.Dispose();
                    //MessageBox.Show("Customer updated successfully");
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("SQL error: " + ex.Message);
                }
            }

        }

        public Customer search(string customerID, string Name)
        {
            string searchSQL;
            Customer customer = null;

            if (Name == "")
            {
                searchSQL = "SELECT * FROM Customers WHERE CostumerID = @CostumerID;";
            }
            else
            {
                searchSQL = "SELECT * FROM Customers WHERE CostumerID = @CostumerID OR Name LIKE '%" + Name + "%';";
            }

            using (SqlCommand cmd = new SqlCommand(searchSQL, MvcApplication.conn))
            {
                try
                {
                    cmd.Parameters.AddWithValue("@CostumerID", customerID);
                    cmd.Parameters.AddWithValue("@Name", Name);
                    SqlDataReader dr = cmd.ExecuteReader();

                    List<Customer> cust = new List<Customer>();
                    int counter = 0;
                    var message = "";
                    while (dr.Read())
                    {
                        customer = new Customer();
                        customer.customerID = Int32.Parse(dr["CostumerID"].ToString());
                        customer.name = dr["Name"].ToString();
                        customer.birthDate = DateTime.Parse(dr["BirthDate"].ToString());
                        customer.location = dr["Location"].ToString();
                        customer.zipCode = Int32.Parse(dr["ZIPCode"].ToString());

                        cust.Add(customer);
                        counter++;
                        searchCounter++;

                    }
                    message = string.Join(Environment.NewLine, cust);

                    if (counter == 1)
                    {
                        if (searchCounter == 1)
                        {
                            MessageBox.Show("Records retrieved successfully\n\n" + message);
                        }
                        dr.Close();
                        cmd.Parameters.Clear();
                        cmd.Dispose();
                        return customer;

                    }
                    else if (counter > 1)
                    {

                        MessageBox.Show("Multimple Names found:\n\n" + message + "\n\n" + "Please enter the full name for finalizing your search!");
                        dr.Close();
                    }

                    dr.Close();
                    cmd.Parameters.Clear();
                    cmd.Dispose();
                    return null;

                }
                catch (SqlException ex)
                {
                    MessageBox.Show("SQL error: " + ex.Message);
                    return null;
                }
            }

        }

        public int getMaxID(string customerID)
        {
            string getID = "SELECT MAX(" + customerID + ") FROM " + table_Name + ";";
            int no = 0;
            using (SqlCommand cmd = new SqlCommand(getID, MvcApplication.conn))
            {
                try
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        no = dr.GetInt32(no) + 1;
                    }
                    dr.Close();
                    cmd.Parameters.Clear();
                    cmd.Dispose();
                    return no;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("SQL error: " + ex.Message);
                    return no;
                }
            }

        }

        public int confirmID(string paramValue)
        {
            int no = 0;
            string getID = "SELECT CostumerID FROM " + table_Name + " WHERE CostumerID = @CustomerID;";

            using (SqlCommand cmd = new SqlCommand(getID, MvcApplication.conn))
            {
                try
                {
                    cmd.Parameters.AddWithValue("@CustomerID", paramValue);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        no = dr.GetInt32(no);
                    }
                    dr.Close();
                    cmd.Parameters.Clear();
                    cmd.Dispose();
                    return no;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("SQL error: " + ex.Message);
                    return no;
                }
            }

        }

        public List<Customer> readAll()
        {
            string readSQL = "SELECT * FROM " + table_Name;
            List<Customer> customers = new List<Customer>();

            using (SqlCommand cmd = new SqlCommand(readSQL, MvcApplication.conn))
            {
                try
                {
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        customers.Add(new Customer(Int32.Parse(dr["CostumerID"].ToString()), dr["Name"].ToString(), DateTime.Parse(dr["BirthDate"].ToString()), dr["Location"].ToString(), Int32.Parse(dr["ZIPCode"].ToString())));
                    }

                    dr.Close();
                    cmd.Parameters.Clear();
                    cmd.Dispose();
                    return customers;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("SQL error: " + ex.Message);
                    return customers;
                }
            }

        }

        public DataTable readAllInDataTable()
        {
            string readSQL = "SELECT * FROM " + table_Name;
            SqlDataAdapter dataAdapter;
            DataTable dt = new DataTable();

            using (SqlCommand cmd = new SqlCommand(readSQL, MvcApplication.conn))
            {
                try
                {
                    dataAdapter = new SqlDataAdapter(cmd);
                    dataAdapter.Fill(dt);

                    cmd.Parameters.Clear();
                    cmd.Dispose();
                    return dt;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("SQL error: " + ex.Message);
                    return dt;
                }
            }

        }
    }
}