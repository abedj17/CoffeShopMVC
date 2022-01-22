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
    public class UsersController : Controller
    {
        private UserDal db = new UserDal();

        // GET: Users
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Login()
        {
            
            return View();

        }
        public ActionResult Logout()
        {
            Session.Clear();
            return View("Login");

        }


        public ActionResult sLogin(Models.User u)
        {
    
            User user = db.Users.Where(x => x.username == u.username && x.password == u.password).FirstOrDefault();
            if(user == null)
            {
                ViewBag.massage = "Password or username is not correct.";
                return View("Login");
            }
            
            if (user.isAdmin == true)
            {
                Session["type"] = "admin";//admin page
                Session["ID"] = user.Id;
                return RedirectToAction("ProductsAdmin", "Products");
            }
            if (user.isBaresta == true)
            {
                Session["type"] = "baresta";//baresta page
                Session["ID"] = user.Id;
            }
            Session["ID"] = user.Id;
            return RedirectToAction("home","Home");//user
       
        }
        public ActionResult sRegister()
        {
            return View("Register");
        }
        public ActionResult Register(User user)
        {
            if(ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Login");
            }
            return View(user);
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
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

    }
}
