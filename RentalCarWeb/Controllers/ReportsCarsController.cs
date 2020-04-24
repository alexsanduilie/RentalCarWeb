using RentalCarWeb.Models.Business;
using RentalCarWeb.Models.Database;
using RentalCarWeb.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;

namespace RentalCarWeb.Controllers
{
    public class ReportsCarsController : Controller
    {
        IEnumerable<Models.DTO.NrCarReserv> carsNr = new List<Models.DTO.NrCarReserv>();
        // GET: ReportsCars
        public ActionResult Index(string startDate, string endDate, string condition)
        {
            //using LINQ
            CustomersRevervationsDataContext crdc = new CustomersRevervationsDataContext();
            ReservationValidationsService reservationValidations = ReservationValidationsService.Instance;

            if (!String.IsNullOrEmpty(condition) && !String.IsNullOrEmpty(startDate) && !String.IsNullOrEmpty(endDate))
            {
                List<NrCarReserv> mostRentedCars = (from reservations in crdc.Reservations
                                                    group reservations by reservations.CarPlate into g
                                                    join cars in crdc.Cars on g.FirstOrDefault().CarPlate equals cars.Plate
                                                    orderby g.Count() descending
                                                    select new NrCarReserv { plate = cars.Plate, manufacturer = cars.Manufacturer, model = cars.Model, price = cars.PricePerDay, location = cars.Location, reservNr = g.Count(r => r.StartDate.Date >= DateTime.Parse(startDate).Date && r.EndDate.Date <= DateTime.Parse(endDate).Date) }).Take(5).ToList();

                List<NrCarReserv> lessRentedCars = (from reservations in crdc.Reservations
                                                    group reservations by reservations.CarPlate into g
                                                    join cars in crdc.Cars on g.FirstOrDefault().CarPlate equals cars.Plate
                                                    orderby g.Count() ascending
                                                    select new NrCarReserv { plate = cars.Plate, manufacturer = cars.Manufacturer, model = cars.Model, price = cars.PricePerDay, location = cars.Location, reservNr = g.Count(r => r.StartDate.Date >= DateTime.Parse(startDate).Date && r.EndDate.Date <= DateTime.Parse(endDate).Date) }).Take(5).ToList();

                if (!reservationValidations.validateDate(DateTime.Parse(startDate), DateTime.Parse(endDate)))
                {
                    ViewBag.Message = "End Date should be equal or higher than Start Date";
                }
                else
                {
                    if (condition == "MOST")
                    {
                        carsNr = mostRentedCars;
                        return View(carsNr);
                    }
                    else
                    {
                        carsNr = lessRentedCars;
                        return View(carsNr);
                    }
                }
            }
            else
            {
                ViewBag.Message = "All field inputs are mandatory for returning the report";
            }

            return View(carsNr);
        }
    }
}