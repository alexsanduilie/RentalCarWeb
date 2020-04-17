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
    public class CarDAO
    {
        private static readonly CarDAO instance = new CarDAO();

        static CarDAO()
        {
        }

        private CarDAO()
        {
        }

        public static CarDAO Instance
        {
            get
            {
                return instance;
            }
        }

        private static string table_Name = "Cars";
        public string location = "";

        public int confirmID(string column, string paramValue)
        {
            int no = 0;
            string getID = "SELECT " + column + " FROM " + table_Name + " WHERE Plate = @Plate;";

            using (SqlCommand cmd = new SqlCommand(getID, MvcApplication.conn))
            {
                try
                {
                    cmd.Parameters.AddWithValue("@Plate", paramValue);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        if (column == "Location")
                        {
                            no = 1;
                            location = dr["Location"].ToString();
                        }
                        else
                        {
                            no = dr.GetInt32(no);
                        }
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

        public List<Car> readAll()
        {
            string readSQL = "SELECT * FROM " + table_Name;
            List<Car> cars = new List<Car>();

            using (SqlCommand cmd = new SqlCommand(readSQL, MvcApplication.conn))
            {
                try
                {
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        cars.Add(new Car(Int32.Parse(dr["CarID"].ToString()), dr["Plate"].ToString(), dr["Manufacturer"].ToString(), dr["Model"].ToString(), Double.Parse(dr["PricePerDay"].ToString()), dr["Location"].ToString()));
                    }

                    dr.Close();
                    cmd.Parameters.Clear();
                    cmd.Dispose();
                    return cars;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("SQL error: " + ex.Message);
                    return cars;
                }
            }

        }
    }
}