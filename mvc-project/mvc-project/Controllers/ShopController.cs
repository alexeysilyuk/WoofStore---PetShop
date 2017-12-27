using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mvc_project.Controllers
{
    public class ShopController : Controller
    {
        // GET: Shop
        public ActionResult Index()
        {
            return View("Shop");
        }


        public ActionResult Cart()
        {
             return View();
        }

        public ActionResult SingleItem()
        {
            return View();
        }
    }
}