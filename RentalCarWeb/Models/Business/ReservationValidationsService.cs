using RentalCarWeb.Models.DAO;
using RentalCarWeb.Models.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI.WebControls;

namespace RentalCarWeb.Models.Business
{
    public class ReservationValidationsService
    {
        private static readonly ReservationValidationsService instance = new ReservationValidationsService();
        static ReservationValidationsService()
        {
        }
        private ReservationValidationsService()
        {
        }
        public static ReservationValidationsService Instance
        {
            get
            {
                return instance;
            }
        }

        private static CarService carService = CarService.Instance;
        private static CustomerService customerService = CustomerService.Instance;
        private static ReservationService reservationService = ReservationService.Instance;
        private static CarDAO carDAO = CarDAO.Instance;

        public bool validateCarPlate(string carPlate)
        {
            bool plate = true;
            if (String.IsNullOrEmpty(carPlate))
            {
                plate = false;
            }
            else
            {
                int carP = carService.confirmID("CarID", carPlate);
                if (carP == 0)
                {
                    if (!Regex.IsMatch(carPlate, "[A-Z]{2} [0-9]{2} [A-Z]{3}"))
                    {
                        plate = false;
                    }
                    else
                    {
                        plate = false;
                    }
                }
            }
            return plate;
        }

        public bool validateClient(string clientID)
        {
            bool cl = true;
            if (String.IsNullOrEmpty(clientID))
            {
                cl = false;
            }
            else if (!Regex.IsMatch(clientID, "^[0-9]+$") && !String.IsNullOrEmpty(clientID))
            {
                cl = false;
            }
            else
            {
                int client = customerService.confirmID(clientID);
                if (client == 0)
                {
                    cl = false;
                }
            }
            return cl;
        }

        public bool validateCity(string loc, string plate)
        {
            bool client = true;
            if (String.IsNullOrEmpty(loc))
            {
                client = false;
            }
            else if (!Regex.IsMatch(loc, "^[a-zA-Z\\s]+$") && !String.IsNullOrEmpty(loc))
            {
                client = false;
            }
            else
            {
                int city = carService.confirmID("Location", plate);
                string location = carDAO.location;
                if (city == 0 || !location.Equals(loc))
                {
                    client = false;
                }
            }
            return client;
        }

        public bool validateDate(DateTime startDate, DateTime endDate)
        {
            bool date = true;
            if (startDate > endDate)
            {
                date = false;
            }
            return date;
        }

        public bool validateRentPeriod(string plate, DateTime presentStartDate, DateTime presentEndDate, string condition, Reservation reservation)
        {
            bool selectedDate = true;
            DateTime startDate;
            DateTime endDate;
            int rStatus;
            DataTable dt;
            Reservation dbR = new Reservation();

            dt = reservationService.readByPlate(plate);

            foreach (DataRow row in dt.Rows)
            {
                dbR.carPlate = row["CarPlate"].ToString();
                dbR.costumerID = Int32.Parse(row["CostumerID"].ToString());
                dbR.startDate = DateTime.Parse(row["StartDate"].ToString());
                dbR.carID = Int32.Parse(row["CarID"].ToString());
                dbR.reservStatsID = Int32.Parse(row["ReservStatsID"].ToString());
                dbR.endDate = DateTime.Parse(row["EndDate"].ToString());
                dbR.location = row["Location"].ToString();
                dbR.couponCode = row["CouponCode"].ToString();

                startDate = DateTime.Parse(row["StartDate"].ToString());
                endDate = DateTime.Parse(row["EndDate"].ToString());
                rStatus = Int32.Parse(row["ReservStatsID"].ToString());

                if (((presentStartDate <= endDate && presentEndDate >= startDate) && condition == "INSERT" && rStatus == 1) || (((presentStartDate <= endDate && presentEndDate >= startDate) && !reservation.Equals(dbR)) && condition == "UPDATE" && rStatus == 1))
                {
                    selectedDate = false;
                    break;
                }
            }
            return selectedDate;
        }
    }
}