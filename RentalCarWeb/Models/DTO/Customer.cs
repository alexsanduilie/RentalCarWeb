using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RentalCarWeb.Models.DTO
{
    public class Customer
    {   [Required]
        [DisplayName("Customer ID")]
        public int customerID { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Invalid input type, the name should contain only alphabetic characters")]
        [DisplayName("Customer Name")]
        public string name { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayName("Birth Date")]
        public DateTime birthDate { get; set; }
        [Required]
        [DisplayName("Location")]
        public string location { get; set; }
        [Required]
        [RegularExpression(@"[0-9]{5}", ErrorMessage = "Invalid input type, the zip code format should be a number of 5 digits: 00000")]
        [DisplayName("ZIP Code")]
        public int zipCode { get; set; }

        public Customer(int customerID, string name, DateTime birthDate, string location, int zipCode)
        {
            this.customerID = customerID;
            this.name = name;
            this.birthDate = birthDate;
            this.location = location;
            this.zipCode = zipCode;
        }

        public Customer(string name, DateTime birthDate, string location, int zipCode)
        {
            this.customerID = -1;
            this.name = name;
            this.birthDate = birthDate;
            this.location = location;
            this.zipCode = zipCode;

        }

        public Customer() { }

        public override string ToString()
        {
            return String.Format("Customer ID:{0}, Name:{1}, BirthDate:{2}, Location:{3}, ZIP Code:{4}", customerID, name, birthDate, location, zipCode);
        }
    }
}