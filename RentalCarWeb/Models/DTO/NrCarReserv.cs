using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RentalCarWeb.Models.DTO
{
    public class NrCarReserv
    {
        [DisplayName("Car ID")]
        public int carID { get; set; }
        [DisplayName("Car Plate")]
        public string plate { get; set; }
        [DisplayName("Manufacturer")]
        public string manufacturer { get; set; }
        [DisplayName("Model")]
        public string model { get; set; }
        [DisplayName("Price")]
        public decimal price { get; set; }
        [DisplayName("Location")]
        public string location { get; set; }
        [DisplayName("Number of Reservations")]
        public int reservNr { get; set; }

    }
}