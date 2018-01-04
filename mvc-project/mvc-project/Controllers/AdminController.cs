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
            UserDAL udal = new UserDAL();
            List<User> list_of_users = udal.Users.ToList<User>();

            ShopItemDAL idal = new ShopItemDAL();
            List<ShopItem> list_of_items = idal.ShopItems.ToList<ShopItem>();


            OrderDAL odal = new OrderDAL();
            List<Order> list_of_orders = odal.Orders.ToList<Order>();

            MessageDAL mdal = new MessageDAL();
            List<Message> list_of_message = mdal.Messages.ToList<Message>();

            if (Convert.ToBoolean(Session["admin"]))
            return View("Index", new AdminViewModel(list_of_users, list_of_items, list_of_orders, list_of_message));


            else return View("~/Views/Home/Index.cshtml");

        }

        [HttpPost]
        public ActionResult EditUser(String  username, String fname, String lname, String phone, String email, String balance, String password
            )
        {
            if (Convert.ToBoolean(Session["admin"]))
            {
                UserDAL udal = new UserDAL();
                List<User> list_of_users = udal.Users.ToList<User>();
                User obj = list_of_users.Find(x => x.username == username);



                if (ModelState.IsValid)
                {
                    try
                    {

                        if (obj != null)
                        {
                            udal.Users.Remove(obj);
                            udal.SaveChanges();

                            obj.fname = fname;
                            obj.lname = lname;
                            obj.phone = phone;
                            obj.email = email;
                            obj.money = Int32.Parse(balance);
                            obj.password = password;
                            obj.ecryptedPassword = HashPass.GenerateHash(password);

                            udal.Users.Add(obj);
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



        [HttpPost]
        public ActionResult DeleteUser(String username)
        {
            UserDAL udal = new UserDAL();
            List<User> list_of_users = udal.Users.ToList<User>();

            if (Convert.ToBoolean(Session["admin"]))
            {

                udal = new UserDAL();
                list_of_users = udal.Users.ToList<User>();
                var obj = list_of_users.Find(x => x.username == username);


                if (obj != null)
                {
                    udal.Users.Remove(obj);
                    udal.SaveChanges();
                    list_of_users = udal.Users.ToList<User>();
                }
            }

            return Json(list_of_users, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public ActionResult DeleteItem(String id)
        {
            if (Convert.ToBoolean(Session["admin"]))
            {
                    ShopItemDAL idal = new ShopItemDAL();
                List<ShopItem> list_of_items = idal.ShopItems.ToList<ShopItem>();
                var obj = list_of_items.Find(x => x.Id == Int32.Parse(id));


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

                // edit
                if (iId != null && iId != "")
                {
                    ShopItemDAL idal = new ShopItemDAL();
                    List<ShopItem> list_of_items = idal.ShopItems.ToList<ShopItem>();
                    ShopItem obj = list_of_items.Find(x => x.Id == Int32.Parse(iId));



                    if (ModelState.IsValid)
                    {
                        if (obj != null)
                        {
                            idal.ShopItems.Remove(obj);
                            idal.SaveChanges();

                            obj.Name = iName;
                            obj.Description = iDescription;
                            obj.price = Int32.Parse(iPrice);
                            obj.photo_url = iPhoto_url;


                            idal.ShopItems.Add(obj);
                            idal.SaveChanges();
                        }
                    }

                }

                // add new 
                else
                {
                    ShopItemDAL idal = new ShopItemDAL();
                    List<ShopItem> list_of_items = idal.ShopItems.ToList<ShopItem>();
                    ShopItem i = new ShopItem();
                    i.Name = iName;
                    i.Description = iDescription;
                    i.price = Int32.Parse(iPrice);
                    i.photo_url = iPhoto_url;

                    if (ModelState.IsValid)
                    {
                        idal.ShopItems.Add(i);
                        idal.SaveChanges();

                        return Json(i, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            return null;
        }



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
                    idal.Orders.Remove(obj);
                    idal.SaveChanges();

                    obj.status = status;
                    idal.Orders.Add(obj);
                    idal.SaveChanges();

                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
            }
            return null;
        }


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