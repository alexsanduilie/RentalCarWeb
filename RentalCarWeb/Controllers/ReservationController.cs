using RentalCarWeb.Models.Business;
using RentalCarWeb.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace RentalCarWeb.Controllers
{
    public class ReservationController : Controller
    {
        ReservationService reservationService = ReservationService.Instance;
        ReservationStatusesService reservationStatusesService = ReservationStatusesService.Instance;
        CarService carService = CarService.Instance;
        CouponService couponService = CouponService.Instance;
        ReservationValidationsService reservationValidations = ReservationValidationsService.Instance;
        IEnumerable<Reservation> reservations = new List<Reservation>();
        List<ReservationStatuses> reservationStatuses = new List<ReservationStatuses>();
        List<Coupon> coupons = new List<Coupon>();

        // GET: Reservation
        public ActionResult Index(string search, string search2, string sortOrder)
        {
            ViewBag.CarIDSortParam = String.IsNullOrEmpty(sortOrder) ? "Car ID_desc" : "";
            ViewBag.PlateSortParam = sortOrder == "Car Plate" ? "Car Plate_desc" : "Car Plate";
            ViewBag.CustomerSortParam = sortOrder == "Customer ID" ? "Customer ID_desc" : "Customer ID";
            ViewBag.RStatusSortParam = sortOrder == "Reservation Status" ? "Reservation Status_desc" : "Reservation Status";
            ViewBag.StartDateSortParam = sortOrder == "Start Date" ? "Start Date_desc" : "Start Date";
            ViewBag.EndDateSortParam = sortOrder == "End Date" ? "End Date_desc" : "End Date";
            ViewBag.LocationSortParam = sortOrder == "Location" ? "Location_desc" : "Location";
            ViewBag.CouponCodeSortParam = sortOrder == "Coupon Code" ? "Coupon Code_desc" : "Coupon Code";

            reservations = reservationService.readAll();
            switch (sortOrder)
            {
                case "Car ID_desc":
                    reservations = reservations.OrderByDescending(r => r.carID);
                    break;
                case "Car Plate":
                    reservations = reservations.OrderBy(r => r.carPlate);
                    break;
                case "Car Plate_desc":
                    reservations = reservations.OrderByDescending(r => r.carPlate);
                    break;
                case "Customer ID":
                    reservations = reservations.OrderBy(r => r.costumerID);
                    break;
                case "Customer ID_desc":
                    reservations = reservations.OrderByDescending(r => r.costumerID);
                    break;
                case "Reservation Status":
                    reservations = reservations.OrderBy(r => r.reservStatsID);
                    break;
                case "Reservation Status_desc":
                    reservations = reservations.OrderByDescending(r => r.reservStatsID);
                    break;
                case "Start Date":
                    reservations = reservations.OrderBy(r => r.startDate);
                    break;
                case "Start Date_desc":
                    reservations = reservations.OrderByDescending(r => r.startDate);
                    break;
                case "End Date":
                    reservations = reservations.OrderBy(r => r.endDate);
                    break;
                case "End Date_desc":
                    reservations = reservations.OrderByDescending(r => r.endDate);
                    break;
                case "Location":
                    reservations = reservations.OrderBy(r => r.location);
                    break;
                case "Location_desc":
                    reservations = reservations.OrderByDescending(r => r.location);
                    break;
                case "Coupon Code":
                    reservations = reservations.OrderBy(r => r.couponCode);
                    break;
                case "Coupon Code_desc":
                    reservations = reservations.OrderByDescending(r => r.couponCode);
                    break;
                default:
                    reservations = reservations.OrderBy(r => r.carID);
                    break;
            }

            if (!String.IsNullOrEmpty(search))
            {
                reservations = reservations.Where(r => r.carPlate.Contains(search) || r.carID.ToString() == search);
            } else if (!String.IsNullOrEmpty(search2))
            {
                reservations = reservations.Where(r => r.reservStatsID.ToString() == search2);
            }
            return View(reservations);
        }

        // GET: Reservation/Details/5
        public ActionResult Details(string carID, string customerID)
        {
            reservations = reservationService.readAll();
            Reservation reservation = reservations.FirstOrDefault(r => r.carID.ToString() == carID && r.costumerID.ToString() == customerID);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(reservation);
            }
        }

        // GET: Reservation/Create
        public ActionResult Create()
        {
            List<SelectListItem> couponCodes = new List<SelectListItem>();
            coupons = couponService.readAll();
            foreach (Coupon c in coupons)
            {
                couponCodes.Add(new SelectListItem { Text = c.couponCode, Value = c.couponCode });
            }
            ViewData["ListCoupon"] = couponCodes;
            return View();
        }

        // POST: Reservation/Create
        [HttpPost]
        public ActionResult Create(Reservation reservation)
        {
            List<SelectListItem> couponCodes = new List<SelectListItem>();
            coupons = couponService.readAll();
            foreach (Coupon c in coupons)
            {
                couponCodes.Add(new SelectListItem { Text = c.couponCode, Value = c.couponCode });
            }

            if (!ModelState.IsValid)
            {
                ViewData["ListCoupon"] = couponCodes;
                return View(reservation);
            }
            if (!reservationValidations.validateCarPlate(reservation.carPlate))
            {
                ViewBag.Message = "The requested car does not exist, please enter another car plate";
                ViewData["ListCoupon"] = couponCodes;
                return View(reservation);
            }
            else if (!reservationValidations.validateClient(reservation.costumerID.ToString()))
            {
                ViewBag.Message = "This client does not exist, please enter another client";
                ViewData["ListCoupon"] = couponCodes;
                return View(reservation);
            }
            else if (!reservationValidations.validateCity(reservation.location, reservation.carPlate))
            {
                ViewBag.Message = "The selected car is not available in this city";
                ViewData["ListCoupon"] = couponCodes;
                return View(reservation);
            }
            else if (!reservationValidations.validateDate(reservation.startDate, reservation.endDate))
            {
                ViewBag.Message = "End Date should be equal or higher than Start Date";
                ViewData["ListCoupon"] = couponCodes;
                return View(reservation);
            }
            else if (!reservationValidations.validateRentPeriod(reservation.carPlate, reservation.startDate, reservation.endDate, "INSERT"))
            {
                ViewBag.Message = "The selected car was already rented in this period";
                ViewData["ListCoupon"] = couponCodes;
                return View(reservation);
            }
            else
            {
                int cardID = carService.confirmID("CarID", reservation.carPlate);
                reservation.carID = cardID;
                reservation.reservStatsID = 1; //default reservation status will always be OPEN, we don't need to enter it in the create form
                reservationService.create(reservation);
                return RedirectToAction("Index");
            }

        }

        // GET: Reservation/Edit/5
        public ActionResult Edit(string carID, string customerID)
        {
            List<SelectListItem> couponCodes = new List<SelectListItem>();
            coupons = couponService.readAll();
            foreach (Coupon c in coupons)
            {
                couponCodes.Add(new SelectListItem { Text = c.couponCode, Value = c.couponCode });
            }
            ViewData["ListCoupon"] = couponCodes;

            List<SelectList> reservationStatuses = new List<SelectList>();
            var dictionary = new Dictionary<int, string>
            {
                { 1, ReservationStatuses.OPEN.ToString() },
                { 2, ReservationStatuses.CLOSED.ToString() },
                { 3, ReservationStatuses.CANCELED.ToString() }
            };
            ViewBag.SelectList = new SelectList(dictionary, "Key", "Value");
            ViewData["ViewBag.SelectList"] = reservationStatuses;

            reservations = reservationService.readAll();
            Reservation reservation = reservations.FirstOrDefault(r => r.carID.ToString() == carID && r.costumerID.ToString() == customerID);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(reservation);
            }
        }

        // POST: Reservation/Edit/5
        [HttpPost]
        public ActionResult Edit(string carID, string customerID, Reservation reservation)
        {
            List<SelectListItem> couponCodes = new List<SelectListItem>();
            coupons = couponService.readAll();
            foreach (Coupon c in coupons)
            {
                couponCodes.Add(new SelectListItem { Text = c.couponCode, Value = c.couponCode });
            }
            ViewData["ListCoupon"] = couponCodes;

            List<SelectList> reservationStatuses = new List<SelectList>();
            var dictionary = new Dictionary<int, string>
            {
                { 1, ReservationStatuses.OPEN.ToString() },
                { 2, ReservationStatuses.CLOSED.ToString() },
                { 3, ReservationStatuses.CANCELED.ToString() }
            };
            ViewBag.SelectList = new SelectList(dictionary, "Key", "Value");
            ViewData["ViewBag.SelectList"] = reservationStatuses;

            reservations = reservationService.readAll();
            var reservationToEdit = reservations.FirstOrDefault(r => r.carID.ToString() == carID && r.costumerID.ToString() == customerID);

            if (reservation == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    ViewData["ListCoupon"] = couponCodes;
                    ViewData["ViewBag.SelectList"] = reservationStatuses;
                    return View(reservation);
                }
                if (!reservationValidations.validateCarPlate(reservation.carPlate))
                {
                    ViewBag.Message = "The requested car does not exist, please enter another car plate";
                    ViewData["ListCoupon"] = couponCodes;
                    ViewData["ViewBag.SelectList"] = reservationStatuses;
                    return View(reservation);
                }
                else if (!reservationValidations.validateClient(reservation.costumerID.ToString()))
                {
                    ViewBag.Message = "This client does not exist, please enter another client";
                    ViewData["ListCoupon"] = couponCodes;
                    ViewData["ViewBag.SelectList"] = reservationStatuses;
                    return View(reservation);
                }
                else if (!reservationValidations.validateCity(reservation.location, reservation.carPlate))
                {
                    ViewBag.Message = "The selected car is not available in this city";
                    ViewData["ListCoupon"] = couponCodes;
                    ViewData["ViewBag.SelectList"] = reservationStatuses;
                    return View(reservation);
                }
                else if (!reservationValidations.validateDate(reservation.startDate, reservation.endDate))
                {
                    ViewBag.Message = "End Date should be equal or higher than Start Date";
                    ViewData["ListCoupon"] = couponCodes;
                    ViewData["ViewBag.SelectList"] = reservationStatuses;
                    return View(reservation);
                }
                else if (!reservationValidations.validateRentPeriod(reservation.carPlate, reservation.startDate, reservation.endDate, "UPDATE"))
                {
                    ViewBag.Message = "The selected car was already rented in this period";
                    ViewData["ListCoupon"] = couponCodes;
                    ViewData["ViewBag.SelectList"] = reservationStatuses;
                    return View(reservation);
                }
                else
                {
                    reservationToEdit.carID = reservation.carID;
                    reservationToEdit.carPlate = reservation.carPlate;
                    reservationToEdit.costumerID = reservation.costumerID;
                    reservationToEdit.reservStatsID = reservation.reservStatsID;
                    reservationToEdit.startDate = reservation.startDate;
                    reservationToEdit.endDate = reservation.endDate;
                    reservationToEdit.location = reservation.location;
                    reservationToEdit.couponCode = reservation.couponCode;

                    reservationService.update(reservationToEdit);
                    ViewBag.Message = "Reservation updated successfully";
                    return RedirectToAction("Index");
                }
            }
        }

        // GET: Reservation/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Reservation/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
