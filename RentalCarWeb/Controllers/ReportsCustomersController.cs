﻿using RentalCarWeb.Models;
using RentalCarWeb.Models.Database;
using RentalCarWeb.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace RentalCarWeb.Controllers
{
    public class ReportsCustomersController : Controller
    {
        IEnumerable<Models.DTO.GoldSilverCustomers> customers;
        // GET: Reports
        public ActionResult Index()
        {
            //using LINQ
            CustomersRevervationsDataContext crdc = new CustomersRevervationsDataContext();
            var goldCustomers = (from reservations in crdc.Reservations
                                 group reservations by reservations.CostumerID into g
                                 join customers in crdc.Customers on g.FirstOrDefault().CostumerID equals customers.CostumerID
                                 where g.FirstOrDefault().StartDate.Date >= DateTime.Now.AddDays(-30).Date && (g.Count() >=3)
                                 orderby g.Count() descending
                                 select new GoldSilverCustomers { name = customers.Name, reservationsNr = (g.Count()) }).ToList();

            customers = goldCustomers;
            return View(goldCustomers);
        }

        public ActionResult Menu()
        {
            return View();
        }
    }
}