using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RentalCarWeb.Models.DTO
{
    public class Car
    {
        [Required]
        [DisplayName("Car ID")]
        public int carID { get; set; }
        [Required]
        [DisplayName("Car Plate")]
        public string plate { get; set; }
        [Required]
        [DisplayName("Manufacturer")]
        public string manufacturer { get; set; }
        [Required]
        [DisplayName("Model")]
        public string model { get; set; }
        [Required]
        [DisplayName("Price")]
        public double price { get; set; }
        [Required]
        [DisplayName("Location")]
        public string location { get; set; }

        public Car(int carID, string plate, string manufacturer, string model, double price, string location)
        {
            this.carID = carID;
            this.plate = plate;
            this.manufacturer = manufacturer;
            this.model = model;
            this.price = price;
            this.location = location;
        }

        public Car(string plate, string manufacturer, string model, double price, string location)
        {
            this.carID = -1;
            this.plate = plate;
            this.manufacturer = manufacturer;
            this.model = model;
            this.price = price;
            this.location = location;
        }

        public Car() { }

        public override string ToString()
        {
            return String.Format("Car ID:{0}, Car Plate:{1}, Manufacturare:{2}, Model:{3}, Price:{4}, Location:{5}", carID, plate, manufacturer, model, price, location);
        }
    }
}