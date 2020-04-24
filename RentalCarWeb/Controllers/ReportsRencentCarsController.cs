using RentalCarWeb.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;

namespace RentalCarWeb.Controllers
{
    public class ReportsRencentCarsController : Controller
    {
        IEnumerable<Models.DTO.Reservation> recent;

        // GET: ReportsRencentCars
        public ActionResult Index()
        {
            //using LINQ
            CustomersRevervationsDataContext crdc = new CustomersRevervationsDataContext();
            var recentCars = (from reservations in crdc.Reservations
                              where reservations.StartDate.Date >= DateTime.Now.AddDays(-6).Date && reservations.ReservStatsID == 1
                              orderby reservations.StartDate descending
                              select new Models.DTO.Reservation { carPlate = reservations.CarPlate, costumerID = reservations.CostumerID, startDate = reservations.StartDate, endDate = reservations.EndDate, location = reservations.Location, couponCode = reservations.CouponCode }).ToList();

            recent = recentCars;
            return View(recent);
        }
    }
}