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
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {

            // Get data from DB for all lists
            UserDAL udal = new UserDAL();
            List<User> list_of_users = udal.Users.ToList<User>();

            ShopItemDAL idal = new ShopItemDAL();
            List<ShopItem> list_of_items = idal.ShopItems.ToList<ShopItem>();

            OrderDAL odal = new OrderDAL();
            List<Order> list_of_orders = odal.Orders.ToList<Order>();

            MessageDAL mdal = new MessageDAL();
            List<Message> list_of_message = mdal.Messages.ToList<Message>();

            // If logged as admin, show all data, else redirect to main page
            if (Convert.ToBoolean(Session["admin"]))
                return View("Index", new AdminViewModel(list_of_users, list_of_items, list_of_orders, list_of_message));
            else
                return View("~/Views/Home/Index.cshtml");

        }


         //Edit user filds in DB
        [HttpPost]
        public ActionResult EditUser(String  username, String fname, String lname, String phone, String email, String balance, String password)
        {
            // Get user group from session
            if (Convert.ToBoolean(Session["admin"]))
            {
                UserDAL udal = new UserDAL();
                List<User> list_of_users = udal.Users.ToList<User>();
                User obj = list_of_users.Find(x => x.username == username);



                // update all user fields
                if (ModelState.IsValid)
                {
                    try
                    {
                        if (obj != null)
                        {
                            obj.fname = fname;
                            obj.lname = lname;
                            obj.phone = phone;
                            obj.email = email;
                            obj.money = Int32.Parse(balance);
                            obj.ecryptedPassword = HashPass.GenerateHash(password);

                            udal.SaveChanges();
                        }

                    }


                    catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                    {
                        Exception raise = dbEx;
                        foreach (var validationErrors in dbEx.EntityValidationErrors)
                        {
                            foreach (var validationError in validationErrors.ValidationErrors)
                            {
                                string message = string.Format("{0}:{1}",
                                    validationErrors.Entry.Entity.ToString(),
                                    validationError.ErrorMessage);

                                raise = new InvalidOperationException(message, raise);
                            }
                        }
                        throw raise;
                    }
                }

                
            }
            return null;
        }


        // Delete user from DB
        [HttpPost]
        public ActionResult DeleteUser(String username)
        {
            UserDAL udal = new UserDAL();
            List<User> list_of_users = udal.Users.ToList<User>();

            // If user have permission to delete
            if (Convert.ToBoolean(Session["admin"]))
            {
                udal = new UserDAL();
                list_of_users = udal.Users.ToList<User>();
                var obj = list_of_users.Find(x => x.username == username); // find user and remove it

                if (obj != null)
                {
                    udal.Users.Remove(obj);
                    udal.SaveChanges();
                    list_of_users = udal.Users.ToList<User>(); // return renewed list
                }
            }

            return Json(list_of_users, JsonRequestBehavior.AllowGet);
        }


        // Delete item from store
        [HttpPost]
        public ActionResult DeleteItem(String id)
        {
            if (Convert.ToBoolean(Session["admin"]))
            {
                ShopItemDAL idal = new ShopItemDAL();
                List<ShopItem> list_of_items = idal.ShopItems.ToList<ShopItem>();
                var obj = list_of_items.Find(x => x.Id == Int32.Parse(id));
                // Find and remove choosen item from shop

                if (obj != null)
                {
                    idal.ShopItems.Remove(obj);
                    idal.SaveChanges();
                }
            }
            return null;
        }




        
        [HttpPost]
        public ActionResult EditItem(String iId, String iName, String iDescription, String iPrice, String iPhoto_url)
        {
            if (Convert.ToBoolean(Session["admin"]))
            {
                // if item ID been received, update it's fields
                if (iId != null && iId != "")
                {
                    ShopItemDAL idal = new ShopItemDAL();
                    List<ShopItem> list_of_items = idal.ShopItems.ToList<ShopItem>();
                    ShopItem obj = list_of_items.Find(x => x.Id == Int32.Parse(iId));

                    if (ModelState.IsValid)
                    {
                        if (obj != null)
                        {
                            obj.Name = iName;
                            obj.Description = iDescription;
                            obj.price = Int32.Parse(iPrice);
                            obj.photo_url = iPhoto_url;

                            idal.SaveChanges();
                        }
                    }

                }

                // add new if not exists
                else
                {
                    ShopItemDAL idal = new ShopItemDAL();
                    List<ShopItem> list_of_items = idal.ShopItems.ToList<ShopItem>();
                    ShopItem item = new ShopItem();
                    item.Name = iName;
                    item.Description = iDescription;
                    item.price = Int32.Parse(iPrice);
                    item.photo_url = iPhoto_url;

                    if (ModelState.IsValid)
                    {
                        idal.ShopItems.Add(item);
                        idal.SaveChanges();
                        return Json(item, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            return null;
        }


        // Delete selected order 
        [HttpPost]
        public ActionResult DeleteOrder(String id)
        {
            if (Convert.ToBoolean(Session["admin"]))
            {
                OrderDAL idal = new OrderDAL();
                List<Order> list_of_items = idal.Orders.ToList<Order>();
                var obj = list_of_items.Find(x => x.orderID == Int32.Parse(id));


                if (obj != null)
                {
                    idal.Orders.Remove(obj);
                    idal.SaveChanges();
                }
            }
            return null;
        }


        // Update status for order
        [HttpPost]
        public ActionResult setStatusOrder(String id, String status)
        {
            if (Convert.ToBoolean(Session["admin"]))
            {
                OrderDAL idal = new OrderDAL();
                List<Order> list_of_items = idal.Orders.ToList<Order>();
                var obj = list_of_items.Find(x => x.orderID == Int32.Parse(id));

                if (obj != null)
                {
                    obj.status = status;
                    idal.SaveChanges();

                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
            }
            return null;
        }

        // delete messages from users
        public ActionResult DeleteMessages(String order)
        {
            if (Convert.ToBoolean(Session["admin"]))
            {
                MessageDAL db = new MessageDAL();
                List<Message> l = db.Messages.ToList<Message>();
                var all = from c in l select c;
                db.Messages.RemoveRange(all);
                db.SaveChanges();
            }
            return null;
        }

    }   
}