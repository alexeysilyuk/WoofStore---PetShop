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


        // Show single item in Shop, receives shop item ID and returns it's info
        public ActionResult SingleItem(int itemId)
        {
            ShopItemDAL dal = new ShopItemDAL();
            List<ShopItem> list_of_items = dal.ShopItems.ToList<ShopItem>();

            List<ShopItem> results = (from u in list_of_items where u.Id == itemId select u).ToList();

            if (results.Count > 0)
                return View("SingleItem", results[0]);
            else
                return View("Not_Found");
        }

        // update user money balanse
        public void updateUserBalance()
        {
            UserDAL dal = new UserDAL();
            User user = new User();

            string searchCriteria = Session["login"].ToString(); // get username to update profile from VIEW page
            List<User> resultList = (from x in dal.Users
                                     where x.username.Contains(searchCriteria)
                                     select x).ToList<User>();

            // if only one user been found, update him balance
            if (resultList.Count == 1)
            {
                user = resultList.ElementAt(0);
                user.money = Int32.Parse(Session["money"].ToString());
                dal.SaveChanges();
            }
        }

        // function to create orders
        public ActionResult makeOrder(String itemId, String itemPrice, String itemTitle, String itemPhoto)
        {
            int item_Id = Int32.Parse(itemId);
            int item_Price = Int32.Parse(itemPrice.Substring(2, itemPrice.Length - 2));
            int change = 0;

            // check username correctness for making new order
            if (Session["login"] != null && !(Session["login"].Equals("")) ) {

                OrderDAL ord = new OrderDAL();
                change = Int32.Parse(Session["money"].ToString()) - item_Price;
                // calculate change for user balance after ordering , check if enough money (current balance - item price)
                // if enough, create new order
                if (change >= 0)
                {       
                    Order obj = new Order();
                    obj.username = Session["login"].ToString();
                    obj.status = "New"; // set status to new
                    obj.price = item_Price;
                    obj.itemID = item_Id;
                    obj.img = itemPhoto;
                    obj.title = itemTitle;

                    ord.Orders.Add(obj); // add order to DB
                    ord.SaveChanges();

                    Session["money"] = change;
                    updateUserBalance(); // update user balance 
                }
                else
                {
                    return Json("Error", JsonRequestBehavior.AllowGet);
                }
                
              }

            return Json(change, JsonRequestBehavior.AllowGet);
        }

        // Show user orders
        public ActionResult UserOrders()
        {
            if (Session["login"] != null && !(Session["login"].Equals("")))
            {
                //find all orders from choosen user and return all results
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


        // same function but all result returns as JSON
        public ActionResult getShopItemsbyJSON()
        {
            ShopItemDAL dal = new ShopItemDAL();
            List<ShopItem> list_of_items = dal.ShopItems.ToList<ShopItem>();

            return Json(list_of_items, JsonRequestBehavior.AllowGet);
        }

    }
}