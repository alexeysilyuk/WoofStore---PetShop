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
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Info()
        {
            ViewBag.Message = " My info page";

            return View("myInfo");
        }


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