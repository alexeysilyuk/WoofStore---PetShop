using mvc_project.DAL;
using mvc_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mvc_project.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            UserDAL udal = new UserDAL();
            List<User> list_of_users = udal.Users.ToList<User>();

            ShopItemDAL idal = new ShopItemDAL();
            List<ShopItem> list_of_items = idal.ShopItems.ToList<ShopItem>();


            OrderDAL odal = new OrderDAL();
            List<Order> list_of_orders = odal.Orders.ToList<Order>();

            if (Convert.ToBoolean(Session["admin"]))
            {
                return View("Index", new AdminViewModel(list_of_users, list_of_items, list_of_orders));
            }

            else
            {
                 return View("~/Views/Home/Index.cshtml");
            }
        }

        [HttpPost]
        public ActionResult EditUser(User model)
        {

            UserDAL udal = new UserDAL();
            List<User> list_of_users = udal.Users.ToList<User>();
            var obj = list_of_users.Find(x => x.username == model.username);


            if (obj != null)
            {
                udal.Entry(obj).CurrentValues.SetValues(model);
                udal.SaveChanges();
            }





            return null;
        }

    }
}