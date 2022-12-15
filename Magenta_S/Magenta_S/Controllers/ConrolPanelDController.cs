using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Magenta_S.Models;
using System.Data.Entity;

namespace Magenta_S.Controllers
{
    public class ConrolPanelDController : Controller
    {
        // GET: ConrolPanelD
        public ActionResult ConrolPanelD()
        {
            return View();
        }
    
        public ActionResult Dealers_Reviews()
        {
            magentadb mg = new magentadb();
            List<User> user=  mg.Users.Where(x=>x.Idc==3&&x.Password!= "unlockmagenta").ToList();
            return View(user);
        }

        [HttpPost]
        public ActionResult Dealers_Reviews(int id, string but)
        {
          

            if (but == "delete")
            {
                magentadb mg = new magentadb();
                User user = mg.Users.Where(x => x.Idu == id).Single();
                try
                {
                    mg.Users.Remove(user);
                    mg.SaveChanges();

                }
                catch
                {
                    user.Password = "unlockmagenta";
                    mg.Entry(user).State = EntityState.Modified;
                    mg.SaveChanges();
                    try
                    {
                        List<Product> p = mg.Products.Where(x => x.Idu == user.Idu).ToList();
                        foreach (var item in p)
                        {
                            item.Ok = false;
                           
                            item.count = 0;
                            mg.Entry(item).State = EntityState.Modified;
                            mg.SaveChanges();
                            
                        }

                    }
                    catch { }
                }
                return RedirectToAction("Dealers_Reviews");
            }
            else {

                return  RedirectToAction("My_Buy", "Bays", new { @id = id });
            }
        }
  
        public ActionResult All_Customer()
        {
            magentadb mg = new magentadb();
            List<User> user = mg.Users.Where(x => x.Idc == 2 && x.Password != "unlockmagenta").ToList();
            return View(user);
        }
        [HttpPost]
        public ActionResult All_Customer(int id,string but)
        {
            magentadb mg = new magentadb();
            if (but == "delete")
            {
                User user = mg.Users.Where(x => x.Idu == id).Single();

                try
                {
                    mg.Users.Remove(user);
                    mg.SaveChanges();
                    return RedirectToAction("All_Customer");
                }
                catch
                {
                    user.Password = "unlockmagenta";
                    mg.Entry(user).State = EntityState.Modified;
                    mg.SaveChanges();
                    return RedirectToAction("All_Customer");
                }

         
            }
            else
            {
               return RedirectToAction("My_Buy", "Bays",new  {@id=id });

            }
            
        }




    }
}