﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RentalCarWeb.Models.DTO
{
    public class Reservation
    {
        [Required]
        [DisplayName("Car ID")]
        public int carID { get; set; }
        [Required]
        [RegularExpression(@"[A-Z]{2} [0-9]{2} [A-Z]{3}", ErrorMessage = "Invalid input type, the car plate format should be: ZZ 00 ZZZ")]
        [DisplayName("Car Plate")]
        public string carPlate { get; set; }
        [Required]
        [DisplayName("Customer ID")]
        public int costumerID { get; set; }
        [Required]
        [DisplayName("Reservation Status")]
        public int reservStatsID { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("Start Date")]
        public DateTime startDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("End Date")]
        public DateTime endDate { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Invalid input type, the name should contain only alphabetic characters")]
        [DisplayName("Location")]
        public string location { get; set; }
        [Required]
        [DisplayName("Coupon Code")]
        public string couponCode { get; set; }

        public Reservation(int carID, string carPlate, int costumerID, int reservStatsID, DateTime startDate, DateTime endDate, string location, string couponCode)
        {
            this.carID = carID;
            this.carPlate = carPlate;
            this.costumerID = costumerID;
            this.reservStatsID = reservStatsID;
            this.startDate = startDate;
            this.endDate = endDate;
            this.location = location;
            this.couponCode = couponCode;
        }

        public Reservation() { }

        public override string ToString()
        {
            return String.Format("Car ID:{0}, Car Plate:{1}, Costumer ID:{2}, Reservation Status:{3}, Start Date:{4}, End Date:{5}, Location:{6}, Coupon Code:{7}", carID, carPlate, costumerID, reservStatsID, startDate, endDate, location, couponCode);
        }

        public override bool Equals(object obj)
        {
            var other = obj as Reservation;
            if (other == null)
                return false;
            if (carID != other.carID || carPlate != other.carPlate || costumerID != other.costumerID || reservStatsID != other.reservStatsID || startDate != other.startDate || endDate != other.endDate || location != other.location || couponCode != other.couponCode)
                return false;

            return true;
        }

        public override int GetHashCode()
        {
            int hashCode = -593334014;
            hashCode = hashCode * -1521134295 + carID.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(carPlate);
            hashCode = hashCode * -1521134295 + costumerID.GetHashCode();
            hashCode = hashCode * -1521134295 + reservStatsID.GetHashCode();
            hashCode = hashCode * -1521134295 + startDate.GetHashCode();
            hashCode = hashCode * -1521134295 + endDate.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(location);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(couponCode);
            return hashCode;
        }
    }
}