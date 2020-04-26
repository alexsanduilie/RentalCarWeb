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
            }
        }

        public void update(Reservation reservation)
        {
            try
            {
                reservationDAO.update(reservation);
            }
            catch (SqlException)
            {
                throw;
            }
        }

        public DataTable readByPlate(string plate)
        {
            try
            {
                return reservationDAO.readByPlate(plate);
            }
            catch (SqlException)
            {
                return null;
                throw;                
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
            catch (SqlException)
            {
                return reservations;
                throw;
            }
        }

    }
}