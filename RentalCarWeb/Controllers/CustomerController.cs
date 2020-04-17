using RentalCarWeb.Models.Business;
using RentalCarWeb.Models.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace RentalCarWeb.Controllers
{
    public class CustomerController : Controller
    {
        CustomerService customerService = CustomerService.Instance;
        List<Customer> customers = new List<Customer>();

        // GET: Customer
        public ActionResult Index()
        {
            customers = customerService.readAll();
            return View(customers);
        }

        // GET: Customer/Details/5
        public ActionResult Details(string id)
        {
            customers = customerService.readAll();
            Customer customer = customers.FirstOrDefault(c => c.customerID.ToString() == id);
            if(customer == null)
            {
                return HttpNotFound();
            } else
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
            customerService.create(customer);
            ViewBag.Message = "User created";
            //return View(customer);
            return RedirectToAction("Index");   
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(string id)
        {
            customers = customerService.readAll();
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
            customers = customerService.readAll();
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
                } else
                {                   
                    customerToEdit.customerID = customer.customerID;
                    customerToEdit.name = customer.name;
                    customerToEdit.birthDate = customer.birthDate;
                    customerToEdit.location = customer.location;
                    customerToEdit.zipCode = customer.zipCode;                     
                    customerService.update(customerToEdit);
                    ViewBag.Message = "Customer updated successfully";
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
