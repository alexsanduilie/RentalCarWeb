using Microsoft.Ajax.Utilities;
using PagedList;
using RentalCarWeb.Models.Business;
using RentalCarWeb.Models.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using PagedList.Mvc;

namespace RentalCarWeb.Controllers
{
    public class CustomerController : Controller
    {
        CustomerService customerService = CustomerService.Instance;
        IEnumerable<Customer> customers = new List<Customer>();

        // GET: Customer
        public ActionResult Index(string search, string sortOrder)
        {
            ViewBag.IdSortParam = String.IsNullOrEmpty(sortOrder) ? "Customer ID_desc" : "";
            ViewBag.NameSortParam = sortOrder == "Customer Name" ? "Customer Name_desc" : "Customer Name";
            ViewBag.BirthDateSortParam = sortOrder == "Birth Date" ? "Birth Date_desc" : "Birth Date";
            ViewBag.LocationSortParam = sortOrder == "Location" ? "Location_desc" : "Location";

            try
            {
                customers = customerService.readAll();
            }
            catch (SqlException ex)
            {
                ViewBag.Message = "SQL error: " + ex.Message;
            }

            switch (sortOrder)
            {
                case "Customer ID_desc":
                    customers = customers.OrderByDescending(c => c.customerID);
                    break;
                case "Customer Name":
                    customers = customers.OrderBy(c => c.name);
                    break;
                case "Customer Name_desc":
                    customers = customers.OrderByDescending(c => c.name);
                    break;
                case "Birth Date":
                    customers = customers.OrderBy(c => c.birthDate);
                    break;
                case "Birth Date_desc":
                    customers = customers.OrderByDescending(c => c.birthDate);
                    break;
                case "Location":
                    customers = customers.OrderBy(c => c.location);
                    break;
                case "Location_desc":
                    customers = customers.OrderByDescending(c => c.location);
                    break;
                default:
                    customers = customers.OrderBy(c => c.customerID);
                    break;
            }

            if (!String.IsNullOrEmpty(search))
            {
                customers = customers.Where(c => c.customerID.ToString() == search.Trim() || c.name.ToLower().Contains(search.Trim().ToLower()));
            }
            return View(customers);
        }

        // GET: Customer/Details/5
        public ActionResult Details(string id)
        {
            try
            {
                customers = customerService.readAll();
            }
            catch (SqlException ex)
            {
                ViewBag.Message = "SQL error: " + ex.Message;
            }
            Customer customer = customers.FirstOrDefault(c => c.customerID.ToString() == id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(customer);
            }

        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        [HttpPost]
        public ActionResult Create(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return View(customer);
            }
            try
            {
                customerService.create(customer);
            }
            catch (SqlException ex)
            {
                ViewBag.Message = "SQL error: " + ex.Message;
                return View(customer);
            }
            return RedirectToAction("Index");
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(string id)
        {
            try
            {
                customers = customerService.readAll();
            }
            catch (SqlException ex)
            {
                ViewBag.Message = "SQL error: " + ex.Message;
            }
            Customer customer = customers.FirstOrDefault(c => c.customerID.ToString() == id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(customer);
            }
        }

        // POST: Customer/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, Customer customer)
        {
            try
            {
                customers = customerService.readAll();
            }
            catch (SqlException ex)
            {
                ViewBag.Message = "SQL error: " + ex.Message;
            }
            var customerToEdit = customers.FirstOrDefault(c => c.customerID.ToString() == id);

            if (customer == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(customer);
                }
                else
                {
                    customerToEdit.customerID = customer.customerID;
                    customerToEdit.name = customer.name;
                    customerToEdit.birthDate = customer.birthDate;
                    customerToEdit.location = customer.location;
                    customerToEdit.zipCode = customer.zipCode;
                    try
                    {
                        customerService.update(customerToEdit);
                    }
                    catch (SqlException ex)
                    {
                        ViewBag.Message = "SQL error: " + ex.Message;
                        return View(customer);
                    }
                    return RedirectToAction("Index");
                }
            }

        }

        // GET: Customer/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Customer/Delete/5
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
