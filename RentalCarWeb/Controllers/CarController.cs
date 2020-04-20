using RentalCarWeb.Models.Business;
using RentalCarWeb.Models.Database;
using RentalCarWeb.Models.DTO;
using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RentalCarWeb.Controllers
{
    public class CarController : Controller
    {
        CarService carService = CarService.Instance;
        IEnumerable<Models.Database.Car> allCars;

        // GET: Car
        public ActionResult Index(string search, string sortOrder, string startDate, string EndDate)
        {
            CarsDataContext cdc = new CarsDataContext();

            /*IEnumerable<Models.Database.Car>    availableCars = from cars in cdc.Cars
                                                join reservations in cdc.Reservations on cars.CarID equals reservations.CarID
                                                where DateTime.Parse(startDate) != reservations.StartDate && DateTime.Parse(EndDate) != reservations.EndDate
                                                select cars;*/

            ViewBag.IdSortParam = String.IsNullOrEmpty(sortOrder) ? "Car ID_desc" : "";
            ViewBag.PlateSortParam = sortOrder == "Car Plate" ? "Car Plate_desc" : "Car Plate";
            ViewBag.ManufacturerSortParam = sortOrder == "Manufacturer" ? "Manufacturer_desc" : "Manufacturer";
            ViewBag.ModelSortParam = sortOrder == "Model" ? "Model_desc" : "Model";
            ViewBag.PriceSortParam = sortOrder == "Price" ? "Price_desc" : "Price";
            ViewBag.LocationSortParam = sortOrder == "Location" ? "Location_desc" : "Location";

            allCars = from c in cdc.Cars select c;

            switch (sortOrder)
            {
                case "Car ID_desc":
                    allCars = allCars.OrderByDescending(c => c.CarID);
                    break;
                case "Car Plate":
                    allCars = allCars.OrderBy(c => c.Plate);
                    break;
                case "Car Plate_desc":
                    allCars = allCars.OrderByDescending(c => c.Plate);
                    break;
                case "Manufacturer":
                    allCars = allCars.OrderBy(c => c.Manufacturer);
                    break;
                case "Manufacturer_desc":
                    allCars = allCars.OrderByDescending(c => c.Manufacturer);
                    break;
                case "Model":
                    allCars = allCars.OrderBy(c => c.Model);
                    break;
                case "Model_desc":
                    allCars = allCars.OrderByDescending(c => c.Model);
                    break;
                case "Price":
                    allCars = allCars.OrderBy(c => c.PricePerDay);
                    break;
                case "Price_desc":
                    allCars = allCars.OrderByDescending(c => c.PricePerDay);
                    break;
                case "Location":
                    allCars = allCars.OrderBy(c => c.Location);
                    break;
                case "Location_desc":
                    allCars = allCars.OrderByDescending(c => c.Location);
                    break;
                default:
                    allCars = allCars.OrderBy(c => c.CarID);
                    break;
            }

            if (!String.IsNullOrEmpty(search))
            {
                allCars = allCars.Where(c => c.Manufacturer.Contains(search) || c.Manufacturer.Contains(search));
            }
            /*if (!String.IsNullOrEmpty(startDate) && !String.IsNullOrEmpty(EndDate))
            {
                allCars = availableCars;
            }*/
            return View(allCars);

        }

    }
}