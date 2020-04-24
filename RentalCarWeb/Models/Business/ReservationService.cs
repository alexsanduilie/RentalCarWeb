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
    public class ReservationService
    {
        private static readonly ReservationService instance = new ReservationService();
        private ReservationDAO reservationDAO;
        static ReservationService()
        {
        }
        private ReservationService()
        {
            reservationDAO = ReservationDAO.Instance;
        }
        public static ReservationService Instance
        {
            get
            {
                return instance;
            }
        }

        public void create(Reservation reservation)
        {
            try
            {
                reservationDAO.create(reservation);
            }
            catch (SqlException)
            {
                throw;
                //MessageBox.Show("Error inserting customer: " + ex.Message);
            }
        }

        public void update(Reservation reservation)
        {
            try
            {
                reservationDAO.update(reservation);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error inserting customer: " + ex.Message);
            }
        }

        public Reservation search(string plate, string customerID)
        {
            try
            {
                return reservationDAO.search(plate, customerID);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error searching reservation: " + ex.Message);
                return null;
            }
        }

        public DataTable readByPlate(string plate)
        {
            try
            {
                return reservationDAO.readByPlate(plate);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error finding data: " + ex.Message);
                return null;
            }
        }

        public List<Reservation> readAll()
        {
            List<Reservation> reservations = new List<Reservation>();
            try
            {
                reservations = reservationDAO.readAll();
                return reservations;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error getting records: " + ex.Message);
                return reservations;
            }
        }

        public DataTable readAllInDataTable()
        {
            try
            {
                return reservationDAO.readAllInDataTable();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error finding data: " + ex.Message);
                return null;
            }
        }
        public DataTable readAllInDataTableByStatus(int status)
        {
            try
            {
                return reservationDAO.readAllInDataTableByStatus(status);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error finding data: " + ex.Message);
                return null;
            }
        }

        public List<Reservation> readByStatus(int status)
        {
            List<Reservation> reservations = new List<Reservation>();
            try
            {
                reservations = reservationDAO.readByStatus(status);
                return reservations;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error getting records: " + ex.Message);
                return reservations;
            }
        }
    }
}