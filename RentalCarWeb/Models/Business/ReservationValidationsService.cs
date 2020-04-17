using RentalCarWeb.Models.DAO;
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
                //message.Text = "Car Plate field can not be empty!";
                plate = false;
            }
            else
            {
                int carP = carService.confirmID("CarID", carPlate);
                if (carP == 0)
                {
                    if (!Regex.IsMatch(carPlate, "[A-Z]{2} [0-9]{2} [A-Z]{3}"))
                    {
                        //message.Text = "Invalid input type, the car plate format should be: ZZ 00 ZZZ";
                        plate = false;
                    }
                    else
                    {
                        //message.Text = "The requested car does not exist, please enter another car plate!";
                        plate = false;
                    }

                }
                else
                {
                    //message.Text = "";
                }
            }
            return plate;
        }

        public bool validateClient(string clientID)
        {
            bool cl = true;
            if (String.IsNullOrEmpty(clientID))
            {
                //message.Text = "Client field can not be empty!";
                cl = false;
            }
            else if (!Regex.IsMatch(clientID, "[0-9]") && !String.IsNullOrEmpty(clientID))
            {
                //message.Text = "Invalid input type, the client ID format should be a number";
                cl = false;
            }
            else
            {
                int client = customerService.confirmID(clientID);
                if (client == 0)
                {
                    //message.Text = "This client does not exist, please enter another client!";
                    cl = false;
                }
                else
                {
                    //message.Text = "";
                }

            }
            return cl;
        }

        public bool validateCity(string loc, string plate)
        {
            bool client = true;
            if (String.IsNullOrEmpty(loc))
            {
                //message.Text = "City field can not be empty!";
                client = false;
            }
            else
            {
                int city = carService.confirmID("Location", plate);
                string location = carDAO.location;
                if (city == 0 || !location.Equals(loc))
                {
                    //message.Text = "The selected car is not available in this city!";
                    client = false;
                }
                else
                {
                    //message.Text = "";
                }
            }
            return client;
        }

        public bool validateDate(DateTime startDate, DateTime endDate)
        {
            bool date = true;
            if (startDate > endDate)
            {
                //message.Text = "End Date should be equal or higher than Start Date!";
                date = false;
            }
            else
            {
                //message.Text = "";
            }
            return date;
        }

        public bool validateRentPeriod(string plate, DateTime presentStartDate, DateTime presentEndDate, string condition)
        {
            bool selectedDate = true;
            DateTime startDate;
            DateTime endDate;
            int rStatus;
            DataTable dt;

            dt = reservationService.readByPlate(plate);

            foreach (DataRow row in dt.Rows)
            {
                startDate = DateTime.Parse(row["StartDate"].ToString());
                endDate = DateTime.Parse(row["EndDate"].ToString());
                rStatus = Int32.Parse(row["ReservStatsID"].ToString());

                if (((presentStartDate <= endDate && presentEndDate >= startDate) && condition == "INSERT" && rStatus == 1) || (((presentStartDate <= endDate && presentEndDate >= startDate) && (presentStartDate != startDate && presentEndDate != endDate)) && condition == "UPDATE" && rStatus == 1))
                {
                    selectedDate = false;
                    //message.Text = "The selected car was already rented in this period!";
                    break;
                }
                else
                {
                    //message.Text = "";
                }
            }
            return selectedDate;
        }
    }
}