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


        public void updateUserBalance()
        {
            UserDAL dal = new UserDAL();
            User user = new User();

            string searchCriteria = Session["login"].ToString();
            List<User> resultList = (from x in dal.Users
                                     where x.username.Contains(searchCriteria)
                                     select x).ToList<User>();


            if (resultList.Count == 1)
            {
                user = resultList.ElementAt(0);

                dal.Users.Remove(user);
                dal.SaveChanges();

                user.money = Int32.Parse(Session["money"].ToString());
                dal.Users.Add(user);
                dal.SaveChanges();

            }
        }

        public ActionResult makeOrder(String itemId, String itemPrice, String itemTitle, String itemPhoto)
        {
            int item_Id = Int32.Parse(itemId);
            int item_Price = Int32.Parse(itemPrice.Substring(2, itemPrice.Length - 2));
            int sdacha = 0;

            if (Session["login"] != null && !(Session["login"].Equals("")) ) {

                OrderDAL ord = new OrderDAL();
                sdacha = Int32.Parse(Session["money"].ToString()) - item_Price;
                if (sdacha >= 0)
                {
                    

                    Order obj = new Order();
                    obj.username = Session["login"].ToString();
                    obj.status = "New";
                    obj.price = item_Price;
                    obj.itemID = item_Id;
                    obj.img = itemPhoto;
                    obj.title = itemTitle;

                    ord.Orders.Add(obj);
                    ord.SaveChanges();

                    Session["money"] = sdacha;
                    updateUserBalance();

                }
                else
                {
                    return Json("Error", JsonRequestBehavior.AllowGet);
                }
                
              }

            return Json(sdacha, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UserOrders()
        {
            if (Session["login"] != null && !(Session["login"].Equals("")))
            {
                String query = Session["login"].ToString();
                OrderDAL dal = new OrderDAL();
                List<Order> resultList = (from x in dal.Orders
                                          where x.username == query
                                          select x).ToList<Order>();

                return View("Cart", new OrderVM(resultList));
            }
            else
            {
                return View("Cart");
            }
        }

        public ActionResult getShopItemsbyJSON()
        {
            ShopItemDAL dal = new ShopItemDAL();
            List<ShopItem> list_of_items = dal.ShopItems.ToList<ShopItem>();

            return Json(list_of_items, JsonRequestBehavior.AllowGet);
        }

    }
}