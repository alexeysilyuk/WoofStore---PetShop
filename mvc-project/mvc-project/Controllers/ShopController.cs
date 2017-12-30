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
            ShopItemDAL dal = new ShopItemDAL();
            
            List<ShopItem> list_of_items = dal.ShopItems.ToList<ShopItem>();
            return View("Shop", new ShopItemViewModel(new ShopItem(), list_of_items));
        }

        public ActionResult SingleItem(int itemId)
        {
            ShopItemDAL dal = new ShopItemDAL();
            List<ShopItem> list_of_items = dal.ShopItems.ToList<ShopItem>();

            List<ShopItem> q = (from u in list_of_items where u.Id == itemId select u).ToList();
            if (q.Count > 0)
                return View("SingleItem", q[0]);
            else
                return View("Not_Found");
        }

        public ActionResult Cart()
        {
            return View();
        }



    }
}