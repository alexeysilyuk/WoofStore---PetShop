using mvc_project.DAL;
using mvc_project.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mvc_project.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "About Us";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact Form";

            return View();
        }

        public ActionResult Info()
        {
            ViewBag.Message = " Information page";

            return View("myInfo");
        }

        // contact form, receives message from user and stores it in DB
        public ActionResult contactUs(String name, String email, String comments)
        {
            MessageDAL dal = new MessageDAL();
            Message obj = new Message();
            obj.name = name;
            obj.email = email;
            obj.comments = comments;

            try
            {
                dal.Messages.Add(obj);
                dal.SaveChanges();

                return Json("OK", JsonRequestBehavior.AllowGet);
            }
            catch (DbEntityValidationException )
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }

        }


    }
}