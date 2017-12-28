using mvc_project.DAL;
using mvc_project.Models;
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


        public ActionResult displayAddItem()
        {
            return View("addItem");
        }

        public ActionResult ShopItemSubmit()
        {
            ShopItem o = new Models.ShopItem();
            o.Name = Request.Form["Name"];
            o.Description = Request.Form["Description"];
            o.photo_url = Request.Form["photo_url"];
            o.price = Int32.Parse(Request.Form["price"]);


            if (ModelState.IsValid)
            {
                ShopItemDAL dal = new ShopItemDAL();
                dal.ShopItems.Add(o);
                dal.SaveChanges();


                return View("Shop");
            }
            else
                return View("addItem", o);
        }
    }
}