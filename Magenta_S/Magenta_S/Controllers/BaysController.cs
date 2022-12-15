using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Magenta_S.Models;
using System.Data.Entity;

namespace Magenta_S.Controllers
{
    public class BaysController : Controller
    {
        // GET: Bays
        public ActionResult Carta()
        {
            Bay_Product ba = new Bay_Product();
            
            ba.Idu = (int)Session["iduser"];
            magentadb mg = new magentadb();
            User u = mg.Users.Where(x => x.Idu == ba.Idu).Single();
            ba.idseesion = u.Idc;
            return View(ba);
        }
        [HttpPost]
        public ActionResult Carta(string Sizee,string ColorP,int Count,int Idproduct,bool Ok,int Idu)
        {

            

            Buy bay = new Buy();
            bay.Idproduct = Idproduct;
            bay.Idu = Idu;
            bay.Ok = Ok;
            bay.ColorP = ColorP;
            bay.Sizee = Sizee;
            bay.bill = null;
            bay.Count = Count;


            magentadb mg = new magentadb();
            mg.Entry(bay).State = EntityState.Deleted;
            mg.SaveChanges();
            return RedirectToAction("Carta");
        }

        [HttpPost]
        public ActionResult buy(string phone,string address,int Idu) {
            

            


            magentadb mg = new magentadb();
            Bill B = new Bill();
            B.Idu = Idu;
            B.Newphone = phone;
            B.Newplace = address;
            B.date = DateTime.Now;
            mg.Bills.Add(B);
            mg.SaveChanges();
         List<Buy> listbue=  mg.Buys.Where(x => x.Idu == Idu && x.Ok == false).ToList();
            foreach (var item in listbue)
            {
                item.Ok = true;
                item.Bill1 = B;
                mg.Entry(item).State = EntityState.Modified;
                mg.SaveChanges();
            }
          List<Buy> listbuy=  mg.Buys.Where(x => x.bill == B.Idbill).ToList();
            double totalprice=0;
            int count = 0;
            foreach (var item in listbuy)
            {
                totalprice += item.Product.Price * item.Count * 1.05;
                count += item.Count;

            }
            B.Totalprice = (int)totalprice;
            mg.Entry(B).State = EntityState.Modified;
            mg.SaveChanges();

           User user=  mg.Users.Where(x => x.Idu == Idu).Single();
            user.CountProduct = count;
            mg.Entry(user).State = EntityState.Modified;
            mg.SaveChanges();

            foreach (var item in listbuy)
            {
                Product p=item.Product;
                p.count = p.count - item.Count;
                mg.Entry(p).State = EntityState.Modified;
                mg.SaveChanges();
            }
            if (user.Idc == 2)
            {
                return RedirectToAction("HomeCustomer", "Home");
            }

            return RedirectToAction("HomeDealer","Home");
        }
        public ActionResult My_Buy(int id)
        {
            int iduser = (int)Session["iduser"];
            magentadb mg = new magentadb();
            User u = mg.Users.Where(x => x.Idu == id).Single();
            my_buy m = new my_buy();
            
            m.idu = id;
            m.idseesion = u.Idc;
           
      
            return View(m);
        }
    }
}