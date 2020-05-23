using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Computer_Shop.Models;
namespace Computer_Shop.Controllers
{
    public class HomeController : Controller
    {
        Computer_Management_SystemEntities db = new Computer_Management_SystemEntities();

        /*===========================================================================*/
        /*======================= MAIN FUNCTION =======================================*/
        /*===========================================================================*/
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

        /*===========================================================================*/
        /*======================= SHOP FUNCTION =======================================*/
        /*===========================================================================*/

        public ActionResult Shop()
        {
            List<Product> list = db.Products.ToList();
            return View(list);
        }

        public ActionResult Cart()
        {
            return View();
        }

        public ActionResult Checkout()
        {
            return View();
        }

        public ActionResult My_account()
        {
            return View();
        }

        public ActionResult Wishlist()
        {
            return View();
        }

        /*===========================================================================*/
        /*======================= ACCOUNT FUNCTION =======================================*/
        /*===========================================================================*/

        /*== LOGIN ==*/
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Account account)
        {
            try
            {
                var user = db.Accounts.SingleOrDefault(u => u.UserName.Equals(account.UserName));
                if (user != null)
                {
                    if (user.Password.Equals(account.Password))
                    {
                        Session["uname"] = user.UserName;
                        Session["uid"] = user.UserID;
                        Session["email"] = user.Email;
                        Session["ngaysinh"] = user.NgaySinh;
                        Session["mobile"] = user.Mobile;
                        if (user.Role == 1)
                        {
                            Session["role"] = "admin";
                        }
                        else
                        {
                            Session["role"] = "user";
                        }
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    return RedirectToAction("Login", "Home");
                }
            }
            catch (Exception e)
            {
                ViewBag.Msg = e.Message;
            }
            return View();
        }

        /*== CREATE ACCOUNT ==*/
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Register(Account account)
        {
            if (ModelState.IsValid)
            {
                var check = db.Accounts.FirstOrDefault(u => u.UserName.Equals(account.UserName));
                if (check == null && Session["role"] == null)
                {
                    Session["role"] = "user";
                    account.Password = account.Password;
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.Accounts.Add(account);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Username already exists";
                    return View();
                }


            }
            return View();
        }

        /*== LOGOUT ==*/
        [HttpGet]
        public ActionResult Logout()
        {
            Session.Clear();  /*Xóa hết session đang thực thi*/
            return RedirectToAction("Index");
        }

        /*===========================================================================*/
        /*======================= PRODUCT FUNCTION =======================================*/
        /*===========================================================================*/
        
        public ActionResult AddProduct()
        {
            if (Session["uname"] == null)
            {
                return RedirectToAction("Index","Home");
            }
            else if(Session["uname"] != null && Session["role"] != "admin")
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult AddProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                Session["pid"] = product.Product_ID;
                string filename1 = Path.GetFileNameWithoutExtension(product.ImageFile1.FileName);
                string filename2 = Path.GetFileNameWithoutExtension(product.ImageFile2.FileName);
                string filename3 = Path.GetFileNameWithoutExtension(product.ImageFile3.FileName);
                string extension1 = Path.GetExtension(product.ImageFile1.FileName);
                string extension2 = Path.GetExtension(product.ImageFile2.FileName);
                string extension3 = Path.GetExtension(product.ImageFile3.FileName);
                filename1 = filename1 + DateTime.Now.ToString("yymmssff") + extension1;
                filename2 = filename2 + DateTime.Now.ToString("yymmssff") + extension2;
                filename3 = filename3 + DateTime.Now.ToString("yymmssff") + extension3;
                product.Image1 = "/Content/Shop/images/" + filename1;
                product.Image2 = "/Content/Shop/images/" + filename2;
                product.Image3 = "/Content/Shop/images/" + filename3;
                filename1 = Path.Combine(Server.MapPath("/Content/Shop/images/"), filename1);
                filename2 = Path.Combine(Server.MapPath("/Content/Shop/images/"), filename2);
                filename3 = Path.Combine(Server.MapPath("/Content/Shop/images/"), filename3);
                product.ImageFile1.SaveAs(filename1);
                product.ImageFile2.SaveAs(filename2);
                product.ImageFile3.SaveAs(filename3);
                var check = db.Products.FirstOrDefault(u => u.Product_Name.Equals(product.Product_Name));
                if (check == null)
                {
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.Products.Add(product);
                    db.SaveChanges();
                    ModelState.Clear();
                    return RedirectToAction("Shop");
                }
            }
            return View();     
        }

        [HttpGet]  /*== Viewing EACH Product ==*/
        public ActionResult Product(int id)
        {
            var product = db.Products.SingleOrDefault(u => u.Product_ID.Equals(id));
            return View(product);
        }

        public ActionResult DeleteProduct(int id)
        {
            if (Session["uname"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (Session["uname"] != null && Session["role"] != "admin")
            {
                return RedirectToAction("Index", "Home");
            }
            var product = db.Products.SingleOrDefault(u => u.Product_ID.Equals(id));
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Shop", "Home");
        }

        public ActionResult EditProduct(int id)
        {
            if (Session["uname"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (Session["uname"] != null && Session["role"] != "admin")
            {
                return RedirectToAction("Index", "Home");
            }
            var product = db.Products.SingleOrDefault(u => u.Product_ID.Equals(id));
            return View(product);
        }

        [HttpPost,ActionName("EditProduct")]
        public ActionResult EditProduct(Product p)
        {
            var product = db.Products.SingleOrDefault(u => u.Product_ID.Equals(p.Product_ID));
            product.Product_Name = p.Product_Name;
            product.Product_ID = p.Product_ID;
            product.Made_in = p.Made_in;
            product.Quantity = p.Quantity;
            product.Warranty_Period = p.Warranty_Period;
            product.Price = p.Price;
            product.Image1 = p.Image1;
            product.Image2 = p.Image2;
            product.Image3 = p.Image3;
            db.SaveChanges();
            return RedirectToAction("Shop","Home");
        }
    }
}