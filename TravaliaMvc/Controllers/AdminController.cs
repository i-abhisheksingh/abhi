using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using TravaliaMvc.Models;

namespace TravaliaMvc.Controllers
{

    public class AdminController : Controller
    {
        AdminDBEntities ll = new AdminDBEntities();


        // GET: Admin
        public ActionResult AdminHome()
        {
            ViewBag.totalPack = ll.PackageTables.Count();
            return View();
        }
        public ActionResult AdminLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AdminLogin(string email,string password)
        {
            var q = (from a in ll.AdminTables
                     where a.Email == email && a.Password == password
                     select a).FirstOrDefault();
            if (q != null)
            {
                return RedirectToAction("/AdminHome");
            }
            else
            {
                Response.Write("<script>alert('Invalid User')</script>");

            }
            return View();
        }

        public ActionResult AddPackage()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddPackage(FormCollection fc, HttpPostedFileBase Pic)
        { 
            PackageTable pt = new PackageTable();
            pt.PackageName = fc["PName"];
            pt.Price = fc["Price"];
            pt.Category = fc["Cat"];
            Pic.SaveAs(Server.MapPath("../AdminLayout/PackagePic/" + Pic.FileName));
            pt.Pic = "/AdminLayout/PackagePic/" + Pic.FileName;
            ll.PackageTables.Add(pt);
            ll.SaveChanges();
            Response.Write("<script>alert('Feedback send successfully..!')</script>");
            return View();
        }
        public ActionResult EditPackage()
        {
            return View(ll.PackageTables.ToList());
        }
        public ActionResult sendnotification()
        {
            ViewBag.ddl = new SelectList(ll.UserTables.ToList(), "Email", "Name");
            return View();
        }
        [HttpPost]
        public ActionResult sendnotification(string ddlEmail,string Sub, string Msg)
        {
            ViewBag.ddl = new SelectList(ll.UserTables.ToList(), "Email", "Name");
            MailMessage mg = new MailMessage();
            mg.From = new MailAddress("pandeyck420@gmail.com");
            mg.To.Add(ddlEmail);
            mg.Subject = Sub;
            mg.Body = Msg;
            mg.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;

            NetworkCredential nkcd = new NetworkCredential();
            nkcd.UserName = "pandeyck420@gmail.com";
            nkcd.Password = "UPSN/15/135777";
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = nkcd;
            smtp.EnableSsl = true;
            smtp.Send(mg);
            Response.Write("<script>alert('Notification Send Successfully')</script>");
            return View();
        }
        public ActionResult ViewFeedback()
        {
            return View();
        }
        public ActionResult jsonData()
        {
            return Json(new { data=ll.ContactTables.ToList()},JsonRequestBehavior.AllowGet);
        }
        public ActionResult ViewOrders()
        {
            List<AllOrders> ao = new List<AllOrders>();
            var q = from a in ll.OrderTables
                    join b in ll.PackageTables on a.PId equals b.Id
                    join c in ll.UserTables on a.UId equals c.Id
                    select new
                    {
                        a.Oid,
                        c.Name,
                        c.Email,
                        b.PackageName,
                        b.Price,
                        Date=a.Date,
                        Time=a.Date,
                    };
            foreach (var k in q)
            {
                AllOrders a = new AllOrders();
                a.Oid = k.Oid;
                a.Name = k.Name;
                a.Email = k.Email;
                a.Date = k.Date.Value.ToShortDateString();
                a.Time = k.Time.Value.ToLongTimeString();
                a.PackageName = k.PackageName;
                a.Price = k.Price;
                ao.Add(a);
            }
            return View(ao);
        }
        public ActionResult ViewUsers()
        {
            return View(ll.UserTables.ToList());
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            Session.Clear();
            return Redirect("/Admin/AdminLogin");
        }
    }

}