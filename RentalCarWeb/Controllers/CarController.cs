﻿using RentalCarWeb.Models.Business;
using RentalCarWeb.Models.Database;
using RentalCarWeb.Models.DTO;
using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Reservation = RentalCarWeb.Models.DTO.Reservation;

namespace RentalCarWeb.Controllers
{
    public class CarController : Controller
    {
        CarService carService = CarService.Instance;
        IEnumerable<Models.Database.Car> allCars;
        CarValidationsService carValidations = CarValidationsService.Instance;
        ReservationValidationsService reservationValidations = ReservationValidationsService.Instance;

        // GET: Car
        public ActionResult Index(string plate, string model, string city, string sortOrder, string startDate, string EndDate)
        {
            CustomersRevervationsDataContext crdc = new CustomersRevervationsDataContext();

            ViewBag.IdSortParam = String.IsNullOrEmpty(sortOrder) ? "Car ID_desc" : "";
            ViewBag.PlateSortParam = sortOrder == "Car Plate" ? "Car Plate_desc" : "Car Plate";
            ViewBag.ManufacturerSortParam = sortOrder == "Manufacturer" ? "Manufacturer_desc" : "Manufacturer";
            ViewBag.ModelSortParam = sortOrder == "Model" ? "Model_desc" : "Model";
            ViewBag.PriceSortParam = sortOrder == "Price" ? "Price_desc" : "Price";
            ViewBag.LocationSortParam = sortOrder == "Location" ? "Location_desc" : "Location";

            //USING LINQ
            allCars = from c in crdc.Cars select c;           

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

            //this reservation is just for testing, for not having a null one when the INSERT condtion is not met because the reservation status is <> 1 (when searching db records)
            Reservation r = new Reservation(0, "", 0, 1, DateTime.Now, DateTime.Now, "", "");
            if (!String.IsNullOrEmpty(model) && String.IsNullOrEmpty(plate) && String.IsNullOrEmpty(city))
            {
                if (!carValidations.validateCarModel(model.Trim()))
                {
                    ViewBag.Message = "This model does not exist, please enter another model";
                }
                else
                {
                    allCars = allCars.Where(c => c.Model.ToLower().Equals(model.Trim().ToLower()));
                    return View(allCars);
                }
            }
            else if (String.IsNullOrEmpty(model) && String.IsNullOrEmpty(plate) && !String.IsNullOrEmpty(city))
            {
                if (!carValidations.validateCity(city.Trim()))
                {
                    ViewBag.Message = "This location does not exist, please enter another location";
                }
                else
                {
                    allCars = allCars.Where(c => c.Location.ToLower().Equals(city.Trim().ToLower()));
                    return View(allCars);
                }
            }
            else if (String.IsNullOrEmpty(model) && !String.IsNullOrEmpty(plate) && String.IsNullOrEmpty(city) && String.IsNullOrEmpty(startDate) && String.IsNullOrEmpty(EndDate))
            {
                if (!carValidations.validateCarPlate(plate.Trim()))
                {
                    if (!Regex.IsMatch(plate, "[A-Z]{2} [0-9]{2} [A-Z]{3}"))
                    {
                        ViewBag.Message = "Invalid input type, the car plate format should be: ZZ 00 ZZZ";
                    }
                    else
                    {
                        ViewBag.Message = "This car plate does not exist, please enter another plate";
                    }
                }
                else
                {
                    allCars = allCars.Where(c => c.Plate.ToLower().Equals(plate.Trim().ToLower()));
                    return View(allCars);
                }
            }
            else if (String.IsNullOrEmpty(model) && !String.IsNullOrEmpty(plate) && String.IsNullOrEmpty(city) && !String.IsNullOrEmpty(startDate) && !String.IsNullOrEmpty(EndDate))
            {
                if (!carValidations.validateCarPlate(plate.Trim().ToLower()))
                {
                    if (!Regex.IsMatch(plate.Trim(), "[A-Z]{2} [0-9]{2} [A-Z]{3}"))
                    {
                        ViewBag.Message = "Invalid input type, the car plate format should be: ZZ 00 ZZZ";
                    }
                    else
                    {
                        ViewBag.Message = "This car plate does not exist, please enter another plate";
                    }
                }
                else
                {
                    allCars = allCars.Where(c => c.Plate.ToLower().Equals(plate.Trim().ToLower()));
                    if (!reservationValidations.validateDate(DateTime.Parse(startDate), DateTime.Parse(EndDate)))
                    {
                        ViewBag.Message = "End Date should be equal or higher than Start Date";
                    }
                    else if (!reservationValidations.validateRentPeriod(allCars.FirstOrDefault().Plate, DateTime.Parse(startDate), DateTime.Parse(EndDate), "INSERT", r))
                    {
                        ViewBag.Message = "The selected car was already rented in this period";
                    }
                    else
                    {
                        return View(allCars);
                    }
                }
            }
            else if (String.IsNullOrEmpty(model) && String.IsNullOrEmpty(plate) && String.IsNullOrEmpty(city) && String.IsNullOrEmpty(startDate) && String.IsNullOrEmpty(EndDate))
            {
                return View(allCars);
            }
            else
            {
                ViewBag.Message = (" Each search field is independent, and ONLY the Car Plate is linked to the Start/End Dates;\n" +
                                    " You can search only by a single criteria, or by Car Plate and Start/End Dates, or leave all criterias blank for returning all cars;\n");
            }
            return View(allCars);
        }

    }
}