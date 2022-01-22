using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CoffeShopMVC.Dal;
using CoffeShopMVC.Models;

namespace CoffeShopMVC.Controllers
{
    public class OrdersController : Controller
    {
        private OrderDal db = new OrderDal();
        private ProductDal idb = new ProductDal();
        private UserDal udb = new UserDal();

        // GET: Orders
        public ActionResult Index()
        {
            return View(db.Orders.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,username,productId,price,seat")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                
                return RedirectToAction("Index");
            }

            return View(order);
        }
        public ActionResult Create1([Bind(Include = "Id,username,productId,price,seat")] Order order)
        {
            foreach (Order o in db.Orders.ToList())
            {
                if (order.seat == o.seat)
                {
                    ViewBag.m = "Seat Ocoppied!";
                    return View("CreateOrder", order);
                }
            }
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index","Products");
            }

            return View(order);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,username,productId,price,seat")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                User u = udb.Users.Find(Session["ID"]);
                return RedirectToAction("Index");
            }
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            User u = udb.Users.Find(Session["ID"]);
            if (u.isAdmin != true)
                return RedirectToAction("MyCart");
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult ChooseSeat(int? id)
        {
            if (id == null || Session["ID"] == null)
            {
                return RedirectToAction("Login", "Users");
            }

            Product product = idb.Products.Find(id);
            int p = (int)(product.OnSale ? product.NewPrice : product.Price);
            User user = udb.Users.Find(Session["ID"]);
            Order order = new Order { price = p,productId = product.Name,username = user.username };

            return View("CreateOrder",order);
        }

        public ActionResult MyCart(int? id)
        {
            if(Session["ID"] != null)
            {
                User u1 = udb.Users.Find(Session["ID"]);
                return View(db.Orders.Where(x => x.username == u1.username).ToList());

            }
            if(id == null)
            {
                return RedirectToAction("Login", "Users");
            }
            User u = udb.Users.Find(id);
            return View(db.Orders.Where(x => x.username == u.username).ToList());
        }
    }
}
