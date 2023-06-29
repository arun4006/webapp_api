using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webapp_api.Models;
using Webapp_api.ViewModels;

namespace Webapp_api.Controllers
{
   // [Authorize]
    public class CustomersController : Controller
    {
        // GET: Customers

        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
            // base.Dispose(disposing);
        }
     
        public ViewResult Index()
        {
            // var customers = GetCustomers();
            // var customers = _context.Customers.ToList();
            var customers = _context.Customers.Include(c => c.MembershipType).ToList();
            return View(customers);
        }

        public ActionResult CustomerForm()
        {
            var membershipTypes = _context.MembershipType.ToList();
            var viewmodel = new CustomerFormViewModel
            {
                Customers = new Customers(),
                MembershipType = membershipTypes
            };

            return View("CustomerForm", viewmodel);
        }

        public ActionResult Details(int id)
        {
            var customers = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);
            if (customers == null)
                return HttpNotFound();
            return View(customers);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customers customers)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel
                {
                    Customers = customers,
                    MembershipType = _context.MembershipType.ToList()
                };

                return View("CustomerForm", viewModel);
            }
            if (customers.Id == 0)
            {
                _context.Customers.Add(customers);
            }
            else
            {
                var customerInDB = _context.Customers.Single(c => c.Id == customers.Id);
                customerInDB.Name = customers.Name;
                customerInDB.Birthdate = customers.Birthdate;
                customerInDB.MembershipType = customers.MembershipType;
                customerInDB.IsSubscribedToNewsletter = customers.IsSubscribedToNewsletter;

            }

            _context.SaveChanges();
            return RedirectToAction("Index", "Customers");
        }

        public ActionResult Edit(int id)
        {
            var customers = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customers == null)
            {
                return HttpNotFound();
            }
            var viewmodel = new CustomerFormViewModel
            {
                Customers = customers,
                MembershipType = _context.MembershipType.ToList()
            };
            return View("CustomerForm", viewmodel);
        }

        //private IEnumerable<Customers> GetCustomers()
        //{
        //    return new List<Customers>
        //    {
        //     new Customers {Id=1,Name="johnsmith" },
        //     new Customers{Id=2,Name="williams"}

        //    };

        //}
    }
}