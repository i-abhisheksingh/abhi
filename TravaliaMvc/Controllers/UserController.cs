using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravaliaMvc.Models;

namespace TravaliaMvc.Controllers
{
    public class UserController : Controller
    {
        AdminDBEntities ll = new AdminDBEntities();
        // GET: User
        public ActionResult HomePage()
        {
            return View();
        }
        public ActionResult PackageList()
        {
            return View(ll.PackageTables.ToList());
        }
        public ActionResult PackageDetails(int id)
        {
            var q = (from a in ll.PackageTables
                     where a.Id == id
                     select a).ToList();
            return View(q);
        }
        public ActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignUp(FormCollection fc)
        {
            SignUpTable st = new SignUpTable();
            st.UserName = fc["UName"];
            st.Email = fc["email"];
            st.Password = fc["password"];
            ll.SignUpTables.Add(st);
            ll.SaveChanges();
            Response.Write("<script>alert('Welcome to Travalia..!')</script>");
            return View();
        }
        public ActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignIn(string email,string password)
        {
            var q = (from a in ll.SignUpTables
                     where a.Email == email && a.Password == password
                     select a).FirstOrDefault();
            if(q!=null)
            {
                Session["uid"] = q.Id;
                return RedirectToAction("HomePage");
            }
            else
            {
                Response.Write("<script>alert('Invalid User Name..!')</script>");

            }
            return View();
        }
        public ActionResult ContactUs()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ContactUs(FormCollection fc)
        {
            ContactTable ct = new ContactTable();
            ct.Name = fc["Name"];
            ct.Email = fc["Email"];
            ct.Subject = fc["Subject"];
            ct.Message = fc["Message"];
            ll.ContactTables.Add(ct);
            ll.SaveChanges();
            Response.Write("<script>alert('Feedback send successfully..!')</script>");
            return View();
        }
        public ActionResult SendBooking(int id)
        {
            int uid = Convert.ToInt32(Session["uid"]);
            OrderTable ot = new OrderTable();
            ot.UId = uid;
            ot.PId = id;
            ot.Date = DateTime.Now;
            ot.Status = "u";
            ll.OrderTables.Add(ot);
            ll.SaveChanges();
            TempData["msg"] = "Your Order has been Placed Successfully..!";
            return View("confirm");
        }
        public ActionResult confirm()
        {
            return View();

        }
    }

}