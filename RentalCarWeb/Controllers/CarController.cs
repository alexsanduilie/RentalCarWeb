using RentalCarWeb.Models.Business;
using RentalCarWeb.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RentalCarWeb.Controllers
{
    public class CarController : Controller
    {
        CarService carService = CarService.Instance;
        IEnumerable<Car> cars = new List<Car>();

        // GET: Car
        public ActionResult Index(string search, string sortOrder)
        {
            ViewBag.IdSortParam = String.IsNullOrEmpty(sortOrder) ? "Car ID_desc" : "";
            ViewBag.PlateSortParam = sortOrder == "Car Plate" ? "Car Plate_desc" : "Car Plate";
            ViewBag.ManufacturerSortParam = sortOrder == "Manufacturer" ? "Manufacturer_desc" : "Manufacturer";
            ViewBag.ModelSortParam = sortOrder == "Model" ? "Model_desc" : "Model";
            ViewBag.PriceSortParam = sortOrder == "Price" ? "Price_desc" : "Price";
            ViewBag.LocationSortParam = sortOrder == "Location" ? "Location_desc" : "Location";

            cars = carService.readAll();
            switch (sortOrder)
            {
                case "Car ID_desc":
                    cars = cars.OrderByDescending(c => c.carID);
                    break;
                case "Car Plate":
                    cars = cars.OrderBy(c => c.plate);
                    break;
                case "Car Plate_desc":
                    cars = cars.OrderByDescending(c => c.plate);
                    break;
                case "Manufacturer":
                    cars = cars.OrderBy(c => c.manufacturer);
                    break;
                case "Manufacturer_desc":
                    cars = cars.OrderByDescending(c => c.manufacturer);
                    break;
                case "Model":
                    cars = cars.OrderBy(c => c.model);
                    break;
                case "Model_desc":
                    cars = cars.OrderByDescending(c => c.model);
                    break;
                case "Price":
                    cars = cars.OrderBy(c => c.price);
                    break;
                case "Price_desc":
                    cars = cars.OrderByDescending(c => c.price);
                    break;
                case "Location":
                    cars = cars.OrderBy(c => c.location);
                    break;
                case "Location_desc":
                    cars = cars.OrderByDescending(c => c.location);
                    break;
                default:
                    cars = cars.OrderBy(c => c.carID);
                    break;
            }

            if (!String.IsNullOrEmpty(search))
            {
                cars = cars.Where(c => c.manufacturer.Contains(search) || c.location.Contains(search));
            }
            return View(cars);
        }
    }
}