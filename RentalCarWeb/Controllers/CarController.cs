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
        List<Car> cars = new List<Car>();

        // GET: Car
        public ActionResult Index()
        {
            cars = carService.readAll();
            return View(cars);
        }
    }
}