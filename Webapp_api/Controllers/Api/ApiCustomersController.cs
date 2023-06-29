using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Webapp_api.Dtos;
using Webapp_api.Models;

namespace Webapp_api.Controllers.Api
{
    public class ApiCustomersController : ApiController
    {

        private ApplicationDbContext _context;

        public ApiCustomersController()
        {
            _context = new ApplicationDbContext();
        }

        // GET /Api/Customers
        [HttpGet]
        [Route("api/getcustomer")]
        public IEnumerable<CustomerDtos> ListCustomers()
        {
            return _context.Customers.ToList().Select(Mapper.Map<Customers, CustomerDtos>);
        }

        //public IEnumerable<Customers> ListCustomers()
        //{
        //    return _context.Customers.ToList();
        //}



        //GET /Api/Customers/1

        [HttpGet]
        [Route("api/getcustomerbyid/{id}")]
        public IHttpActionResult ListCustomersById(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
                return NotFound();
            return Ok(Mapper.Map<Customers,CustomerDtos>(customer));
        }

        //public Customers ListCustomersById(int id)
        //{
        //    var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
        //    if (customer == null)
        //        throw new HttpResponseException(HttpStatusCode.NotFound);
        //    return customer;
        //}

        //POST /Api/Customers

        [HttpPost]
        [Route("api/newcustomers")]
        public IHttpActionResult CreateCustomers(CustomerDtos customersDtos)
        {
            if (!ModelState.IsValid)
            {
                BadRequest();
            }
            var customer=Mapper.Map<CustomerDtos,Customers>(customersDtos);

            _context.Customers.Add(customer);
            _context.SaveChanges();
            customersDtos.Id = customer.Id;

            return Created(new Uri(Request.RequestUri+"/"+ customer.Id), customersDtos) ;
        }

        //public Customers CreateCustomers(Customers customers)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        throw new HttpResponseException(HttpStatusCode.BadRequest);
        //    }
        //    _context.Customers.Add(customers);
        //    _context.SaveChanges();
        //    return customers;
        //}

        //Put  /Api/Customers/1
        
        [HttpPut]
        [Route("api/updatecustomer/{id}")]

        public void ModifyCustomers(CustomerDtos customersDtos, int id)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            var customersInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customersInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            Mapper.Map(customersDtos, customersInDb);

            _context.SaveChanges();

        }

        //public void ModifyCustomers(Customers customers, int id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        throw new HttpResponseException(HttpStatusCode.BadRequest);
        //    }
        //    var customersInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

        //    if (customersInDb == null)
        //    {
        //        throw new HttpResponseException(HttpStatusCode.NotFound);
        //    }
        //    customersInDb.Name = customers.Name;
        //    customersInDb.Birthdate = customers.Birthdate;
        //    customersInDb.IsSubscribedToNewsletter = customers.IsSubscribedToNewsletter;
        //    customersInDb.MembershipType = customers.MembershipType;

        //    _context.SaveChanges();

        //}

        [HttpDelete]
        [Route("api/delete_customer/{id}")]
        public void DeletedCustomer(int id)
        {
            var customerInDb = _context.Customers.SingleOrDefault(x => x.Id == id);

            if (customerInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            _context.Customers.Remove(customerInDb);
            _context.SaveChanges();

        }

    }
    }


