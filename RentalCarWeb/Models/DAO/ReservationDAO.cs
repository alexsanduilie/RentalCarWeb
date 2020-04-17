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
    public class ReservationDAO
    {
        private static readonly ReservationDAO instance = new ReservationDAO();

        static ReservationDAO()
        {
        }

        private ReservationDAO()
        {
        }

        public static ReservationDAO Instance
        {
            get
            {
                return instance;
            }
        }

        private static string table_Name = "Reservations";

        public void create(Reservation reservation)
        {
            string insertSQL = "INSERT INTO " + table_Name + " VALUES(@carID, @carPlate, @clientID, @reservStatsID, @startDate, @endDate, @city, @couponCode);";
            SqlDataAdapter dataAdapter = new SqlDataAdapter();

            using (SqlCommand cmd = new SqlCommand(insertSQL, MvcApplication.conn))
            {
                try
                {
                    cmd.Parameters.AddWithValue("@carID", reservation.carID);
                    cmd.Parameters.AddWithValue("@carPlate", reservation.carPlate);
                    cmd.Parameters.AddWithValue("@clientID", reservation.costumerID);
                    cmd.Parameters.AddWithValue("@reservStatsID", reservation.reservStatsID);
                    cmd.Parameters.AddWithValue("@startDate", reservation.startDate);
                    cmd.Parameters.AddWithValue("@endDate", reservation.endDate);
                    cmd.Parameters.AddWithValue("@city", reservation.location);
                    cmd.Parameters.AddWithValue("@couponCode", reservation.couponCode);

                    dataAdapter.InsertCommand = cmd;
                    dataAdapter.InsertCommand.ExecuteNonQuery();

                    cmd.Parameters.Clear();
                    cmd.Dispose();
                    //MessageBox.Show("Reservation created successfully");
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("SQL error: " + ex.Message);
                }
            }

        }

        public void update(Reservation reservation)
        {
            string updateSQL = "UPDATE " + table_Name + " SET ReservStatsID =  @reservStatsID, StartDate =  @startDate, EndDate =  @endDate, Location = @city, CouponCode = @couponCode WHERE CarPlate = @carPlate AND CostumerID = @clientID;";
            SqlDataAdapter dataAdapter = new SqlDataAdapter();

            using (SqlCommand cmd = new SqlCommand(updateSQL, MvcApplication.conn))
            {
                try
                {
                    cmd.Parameters.AddWithValue("@carID", reservation.carID);
                    cmd.Parameters.AddWithValue("@carPlate", reservation.carPlate);
                    cmd.Parameters.AddWithValue("@clientID", reservation.costumerID);
                    cmd.Parameters.AddWithValue("@reservStatsID", reservation.reservStatsID);
                    cmd.Parameters.AddWithValue("@startDate", reservation.startDate);
                    cmd.Parameters.AddWithValue("@endDate", reservation.endDate);
                    cmd.Parameters.AddWithValue("@city", reservation.location);
                    cmd.Parameters.AddWithValue("@couponCode", reservation.couponCode);

                    dataAdapter.UpdateCommand = cmd;
                    dataAdapter.UpdateCommand.ExecuteNonQuery();

                    cmd.Parameters.Clear();
                    cmd.Dispose();
                    //MessageBox.Show("Reservation updated successfully");
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("SQL error: " + ex.Message);
                }
            }

        }

        public Reservation search(string plate, string customerID)
        {
            string searchSQL;
            int counter = 0;
            Reservation reservation = null;

            if (plate != "" && customerID != "")
            {
                searchSQL = "SELECT * FROM Reservations WHERE CarPlate = @plate AND CostumerID = @CostumerID;";
            }
            else
            {
                searchSQL = "SELECT * FROM Reservations WHERE CarPlate = @plate OR CostumerID = @CostumerID;";
            }

            using (SqlCommand cmd = new SqlCommand(searchSQL, MvcApplication.conn))
            {
                try
                {
                    cmd.Parameters.AddWithValue("@plate", plate);
                    cmd.Parameters.AddWithValue("@CostumerID", customerID);
                    SqlDataReader dr = cmd.ExecuteReader();

                    List<Reservation> reserv = new List<Reservation>();
                    var message = "";
                    while (dr.Read())
                    {
                        reservation = new Reservation();
                        reservation.carPlate = dr["CarPlate"].ToString();
                        reservation.costumerID = Int32.Parse(dr["CostumerID"].ToString());
                        reservation.startDate = DateTime.Parse(dr["StartDate"].ToString());
                        reservation.carID = Int32.Parse(dr["CarID"].ToString());
                        reservation.reservStatsID = Int32.Parse(dr["ReservStatsID"].ToString());
                        reservation.endDate = DateTime.Parse(dr["EndDate"].ToString());
                        reservation.location = dr["Location"].ToString();
                        reservation.couponCode = dr["CouponCode"].ToString();

                        reserv.Add(reservation);
                        counter++;
                    }
                    message = string.Join(Environment.NewLine, reserv);

                    if (counter == 1)
                    {
                        MessageBox.Show("Rent retrieved successfully:" + reservation);
                        dr.Close();
                        cmd.Parameters.Clear();
                        cmd.Dispose();
                        return reservation;
                    }
                    else if (counter > 1)
                    {

                        MessageBox.Show("Multiple Entries found:\n\n" + message + "\n\n" + "Please enter the Car Plate - Client ID association for finalizing the search!");
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

        public List<Reservation> readAll()
        {
            string readSQL = "SELECT * FROM " + table_Name;
            List<Reservation> reservation = new List<Reservation>();

            using (SqlCommand cmd = new SqlCommand(readSQL, MvcApplication.conn))
            {
                try
                {
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        reservation.Add(new Reservation(Int32.Parse(dr["CarID"].ToString()), dr["CarPlate"].ToString(), Int32.Parse(dr["CostumerID"].ToString()), Int32.Parse(dr["ReservStatsID"].ToString()), DateTime.Parse(dr["StartDate"].ToString()), DateTime.Parse(dr["EndDate"].ToString()), dr["Location"].ToString(), dr["CouponCode"].ToString()));
                    }

                    dr.Close();
                    cmd.Parameters.Clear();
                    cmd.Dispose();
                    return reservation;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("SQL error: " + ex.Message);
                    return reservation;
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

        public DataTable readAllInDataTableByStatus(int status)
        {
            string readSQL = "SELECT * FROM " + table_Name + " WHERE ReservStatsID = @reservID;";
            SqlDataAdapter dataAdapter;
            DataTable dt = new DataTable();

            using (SqlCommand cmd = new SqlCommand(readSQL, MvcApplication.conn))
            {
                try
                {
                    cmd.Parameters.AddWithValue("@reservID", status);
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

        public List<Reservation> readByStatus(int status)
        {
            string readSQL = "SELECT * FROM " + table_Name + " WHERE ReservStatsID = @reservID;";
            List<Reservation> reservation = new List<Reservation>();

            using (SqlCommand cmd = new SqlCommand(readSQL, MvcApplication.conn))
            {
                try
                {
                    cmd.Parameters.AddWithValue("@reservID", status);
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        reservation.Add(new Reservation(Int32.Parse(dr["CarID"].ToString()), dr["CarPlate"].ToString(), Int32.Parse(dr["CostumerID"].ToString()), Int32.Parse(dr["ReservStatsID"].ToString()), DateTime.Parse(dr["StartDate"].ToString()), DateTime.Parse(dr["EndDate"].ToString()), dr["Location"].ToString(), dr["CouponCode"].ToString()));
                    }

                    dr.Close();
                    cmd.Parameters.Clear();
                    cmd.Dispose();
                    return reservation;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("SQL error: " + ex.Message);
                    return reservation;
                }
            }

        }

        public DataTable readByPlate(string plate)
        {
            string readSQL = "SELECT * FROM Reservations WHERE CarPlate = @plate;";
            SqlDataAdapter dataAdapter;
            DataTable dt = new DataTable();

            using (SqlCommand cmd = new SqlCommand(readSQL, MvcApplication.conn))
            {
                try
                {
                    cmd.Parameters.AddWithValue("@plate", plate);
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