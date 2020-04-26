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

        public List<Reservation> readAll()
        {
            string readSQL = "SELECT * FROM " + table_Name;
            List<Reservation> reservation = new List<Reservation>();

            using (SqlCommand cmd = new SqlCommand(readSQL, MvcApplication.conn))
            {
                SqlDataReader dr = null;
                try
                {
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        reservation.Add(new Reservation(Int32.Parse(dr["CarID"].ToString()), dr["CarPlate"].ToString(), Int32.Parse(dr["CostumerID"].ToString()), Int32.Parse(dr["ReservStatsID"].ToString()), DateTime.Parse(dr["StartDate"].ToString()), DateTime.Parse(dr["EndDate"].ToString()), dr["Location"].ToString(), dr["CouponCode"].ToString()));
                    }
                    return reservation;
                }
                catch (SqlException)
                {
                    return reservation;
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