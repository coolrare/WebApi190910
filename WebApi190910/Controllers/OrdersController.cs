﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Description;
using WebApi190910.Models;

namespace WebApi190910.Controllers
{
    public class OrdersController : ApiController
    {
        private FabricsEntities db = new FabricsEntities();

        public OrdersController()
        {
            db.Configuration.LazyLoadingEnabled = false;
        }

        // GET: api/Orders
        public IQueryable<Order> GetOrder()
        {
            return db.Order;
        }

        [Route("orders/{*date:datetime}")]
        public IQueryable<Order> GetOrderByDate(DateTime date)
        {
            var sd = date.Date;
            var ed = date.AddDays(1).Date;
            return db.Order.Where(p => p.OrderDate >= sd && p.OrderDate < ed );
        }

        // GET: api/Orders/5
        [ResponseType(typeof(Order))]
        [HttpGet, Route("orders/{id:int}")]
        public HttpResponseMessage GetOrder(int id)
        {
            Order order = db.Order.Find(id);
            if (order == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, order);
        }

        // GET: api/Orders/5
        [ResponseType(typeof(Order))]
        [HttpGet, Route("orders/{id:int}/raw")]
        public HttpResponseMessage GetOrderID(int id)
        {
            Order order = db.Order.Find(id);
            if (order == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                ReasonPhrase = "GOOD",
                Content = new StringContent("酷奇資訊訂單" + order.OrderId, Encoding.GetEncoding("big5"))
            };
        }

        // PUT: api/Orders/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOrder(int id, Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != order.OrderId)
            {
                return BadRequest();
            }

            db.Entry(order).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Orders
        [ResponseType(typeof(Order))]
        public IHttpActionResult PostOrder(Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Order.Add(order);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (OrderExists(order.OrderId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = order.OrderId }, order);
        }

        // DELETE: api/Orders/5
        [ResponseType(typeof(Order))]
        public IHttpActionResult DeleteOrder(int id)
        {
            Order order = db.Order.Find(id);
            if (order == null)
            {
                return NotFound();
            }

            db.Order.Remove(order);
            db.SaveChanges();

            return Ok(order);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrderExists(int id)
        {
            return db.Order.Count(e => e.OrderId == id) > 0;
        }
    }
}