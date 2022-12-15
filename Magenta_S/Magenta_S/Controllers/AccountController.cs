using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Magenta_S.Models;
using System.Data.Entity;

namespace Magenta_S.Controllers
{
    public class AccountController : Controller
    {
        
        magentadb mg = new magentadb();
        // GET: Account
        public ActionResult RegisterUser()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RegisterUser(RegisterUser Model)
        {
          
         
            //للتحقق بأن الأيميل مستخدم من قبل
            if (mg.Users.Any(x => x.Username == Model.Username))
            {
                ModelState.AddModelError("Username", " the Username is alerady in use ");
                return View();
            }

            //في حال كان هناك اي بيانات غير صحيحة بالنسبة للموديل
            //يتم ارجاع الواجهة مع عرض الأخطاء التي تم انشائها في المودل الخاص بأنشاء الحساب
            //في حال كانت البيانات صحيحة
            if (ModelState.IsValid)
            {
              
                //يتم انشاء حساب زبون 
                RegisterUser registerUser = new Models.RegisterUser();
                User user = registerUser.registerUser(Model);              
                mg.Users.Add(user);
                mg.SaveChanges();


               
                return RedirectToAction("index", "Home");
            }
            return View();
        }

        [HttpGet]
        public ActionResult RegisterDealer()
        {
            return View("RegisterUser");
        }




        [HttpPost]
        public ActionResult RegisterDealer(RegisterUser Model)
        {
            if (mg.Users.Any(x => x.Username == Model.Username))
            {
                ModelState.AddModelError("Username", " the Username is alerady in use ");
                return View("RegisterUser");
            }

            if (string.IsNullOrEmpty(Model.CommercialRecord)) 
            {

                ModelState.AddModelError("CommercialRecord", " the CommercialRecord is Required ");
                return View("RegisterUser");
            }
        
            if (ModelState.IsValid)
            {
                magentadb mg = new magentadb();
                RegisterUser registerUser = new Models.RegisterUser();
                User user = registerUser.RegisterDealer(Model);
                mg.Users.Add(user);
                mg.SaveChanges();
                return RedirectToAction("index", "Home");
            }
            return View();
        }
        // خاصة بتسجيل الدخول
        public ActionResult Login(string Username,string Password)
        {

           //في حالة كانت البيانات صحيحة
            if (ModelState.IsValid)
            {
               
                RegisterUser registerUser = new Models.RegisterUser();
                //ميثود خاصة بالتحقق
                if (registerUser.Logine(Username, Password))
                {
                    int idc;
                    idc= registerUser.checkid(Username,Password);
                    Session["iduser"] = registerUser.iduseesion;
                    if (idc == 2)
                    {
                        //يتم ارجاع الصفحة الخاصة بالزبائن 
                        return RedirectToAction("HomeCustomer", "Home");
                    }

                    else if (idc == 3)
                    {
                        return RedirectToAction("HomeDealer", "Home");
                    }
                    else if (idc == 1)
                    {
                        return RedirectToAction("HomeAdmin", "Home");

                    }
                }
                else
                {

                    ModelState.AddModelError("Username", " The Username is Not true or password ");
                    return View("RegisterUser");

                }
            }
            return View("RegisterUser");
        }
        //لتعديل الحساب
       
        public ActionResult UpdateAccount()
        {
            int id = (int)Session["iduser"];
            //magentadb mg = new magentadb();
            //User user = mg.Users.Where(x => x.Idu ==id).Single();
            UpdateAccount up = new UpdateAccount();
            up.UpadateAccount(id);
   
            return View(up);
        }
        [HttpPost]
        public ActionResult UpdateAccount(UpdateAccount Model)
        {
                magentadb mg = new magentadb();
            UpdateAccount UP = new UpdateAccount();
            if (!string.IsNullOrEmpty(Model.oldPassword)) { 
            if (Model.oldPassword != Models.UpdateAccount.togetpassword(Model.Idu))
            {
                ModelState.AddModelError("oldPassword", " the old in not True");
                    return View();
            }
            }
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(Model.Password))
                { Model.Password = Models.UpdateAccount.togetpassword(Model.Idu); }
                User user = UP.UpadateAccount(Model);
               
                mg.Entry(user).State = EntityState.Modified;
                mg.SaveChanges();
                if (user.Idc == 1)
                return RedirectToAction("HomeAdmin","Home");
                else if (user.Idc==2)
                    return RedirectToAction("HomeCustomer", "Home");

                else
                    return RedirectToAction("HomeDealer", "Home");

            }
            return RedirectToAction("UpdateAccount");
        }


    }
}