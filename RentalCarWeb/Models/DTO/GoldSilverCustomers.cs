using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace RentalCarWeb.Models.DTO
{
    public class GoldSilverCustomers
    {
        [DisplayName("Customer Name")]
        public string name { get; set; }
        [DisplayName("Number of reservations")]
        public int reservationsNr { get; set; }
    }
}