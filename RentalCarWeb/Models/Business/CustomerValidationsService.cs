using RentalCarWeb.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI.WebControls;

namespace RentalCarWeb.Models.Business
{
    public class CustomerValidationsService
    {
        private static readonly CustomerValidationsService instance = new CustomerValidationsService();
        static CustomerValidationsService()
        {
        }
        private CustomerValidationsService()
        {
        }
        public static CustomerValidationsService Instance
        {
            get
            {
                return instance;
            }
        }

        private static CustomerService customerService = CustomerService.Instance;

        public bool validateClient(string clientName, Label message)
        {
            bool client = true;

            if (String.IsNullOrEmpty(clientName))
            {
                message.Text = "Client field can not be empty!";
                client = false;
            }
            else
            {
                message.Text = "";
            }
            return client;
        }

        public bool validateLocation(string location, Label message)
        {
            bool city = true;

            if (String.IsNullOrEmpty(location))
            {
                message.Text = "Location field can not be empty!";
                city = false;
            }
            else
            {
                message.Text = "";
            }
            return city;
        }

        public bool validateZIP(string ZIPCode, Label message)
        {
            bool zip = true;
            if (String.IsNullOrEmpty(ZIPCode))
            {
                message.Text = "ZIP Code can not be empty!";
                zip = false;
            }
            else if (!Regex.IsMatch(ZIPCode, "[0-9]{5}") && !String.IsNullOrEmpty(ZIPCode))
            {
                message.Text = "Invalid input type, the zip code format should be a number of 5 digits: 00000";
                zip = false;
            }
            else
            {
                message.Text = "";
            }
            return zip;
        }

        public bool validateClientID(string clientID, Label message, Customer customer)
        {
            bool client = true;
            if (String.IsNullOrEmpty(clientID))
            {
                message.Text = "Client ID can not be empty!";
                client = false;
            }
            else if (!Regex.IsMatch(clientID, "[0-9]") && !String.IsNullOrEmpty(clientID))
            {
                message.Text = "Invalid input type, the client ID format should be a number";
                client = false;
            }
            else
            {
                int customerID = customerService.confirmID(clientID);
                if (customerID == 0)
                {
                    message.Text = "This client does not exist, please enter another client!";
                    client = false;
                }
                else
                {
                    message.Text = "";
                }
            }
            return client;
        }

        public bool validateClientName(string clientName, Label message, Customer customer)
        {
            bool client = true;
            if (String.IsNullOrEmpty(clientName))
            {
                message.Text = "Client Name can not be empty!";
                client = false;
            }
            else
            {
                customer = customerService.search("", clientName);
                if (customer == null)
                {
                    message.Text = "This client name couldn't be found, please enter another client!";
                    client = false;
                }
                else
                {
                    message.Text = "";
                }
            }
            return client;
        }
    }
}