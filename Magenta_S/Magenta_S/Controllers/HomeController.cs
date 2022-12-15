using Magenta_S.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Magenta_S.Controllers
{
    public class HomeController : Controller
    {
        
        // GET: Home

        magentadb mg = new magentadb();
        public ActionResult Index()
        {
            ProductA p = new ProductA();

            return View(p);
        }
        public ActionResult HomeAdmin()
        {
            ProductA p = new ProductA();
            p.Idu = (int)Session["iduser"];

            return View(p);
        }
        public ActionResult HomeDealer()
        {
            ProductA p = new ProductA();
            p.Idu = (int)Session["iduser"];
            return View(p);
        }

        [HttpPost]
        public ActionResult HomeDealer(int Idproduct, string selectc, string selects,int countp,int counttheproduct)
        {
            
            int iduser = (int)Session["iduser"];

            if (counttheproduct < countp)
            {
               
                ModelState.AddModelError("lll", " The Count of product is Not Available");
                ProductA p = new ProductA();
                p.Idu = (int)Session["iduser"];
                
                return View(p);
            }
            int ch = 0;
            try
            {
                 ch = mg.Buys.Where(x => x.Idu == iduser && x.Idproduct == Idproduct).Max(x => x.checkp);
            }

            catch {
                ch = 0;

            }
            Magenta_S.Models.Buy b = new Buy();
            b.Idproduct = Idproduct;
            b.bill = null;
            b.ColorP = selectc;
            b.Sizee = selects;
            b.Count = countp;
            b.Idu = (int)Session["iduser"];
            b.checkp = ch + 1; ;
            try
            {
                mg.Buys.Add(b);
                mg.SaveChanges();
            }
            catch {

                ModelState.AddModelError("count", " the bay aleardy exit in Carta ");
                ProductA p = new ProductA();
                p.Idu = (int)Session["iduser"];

                return View(p);
            }
            return RedirectToAction("HomeDealer");
        }

        public ActionResult HomeCustomer()
        {
            ProductA p = new ProductA();
            p.Idu = (int)Session["iduser"];
            return View(p);
        }
        [HttpPost]
        public ActionResult HomeCustomer(int Idproduct, string selectc, string selects, int countp, int counttheproduct)
        {
            int iduser = (int)Session["iduser"];
      
            if (counttheproduct < countp)
            {

                ModelState.AddModelError("lll", " The Count of product is Not Available");
                ProductA p = new ProductA();
                p.Idu = (int)Session["iduser"];

                return View(p);
            }

            int ch = 0;
            try
            {
                ch = mg.Buys.Where(x => x.Idu == iduser && x.Idproduct == Idproduct).Max(x => x.checkp);
            }

            catch
            {
                ch = 0;

            }
            Magenta_S.Models.Buy b = new Buy();
            b.Idproduct = Idproduct;
            b.bill = null;
            b.ColorP = selectc;
            b.Sizee = selects;
            b.Count = countp;
            b.checkp = ch + 1;
            b.Idu = (int)Session["iduser"];
           
            b.Ok = false;
            try
            {
                mg.Buys.Add(b);
                mg.SaveChanges();
            }
            catch
            {

                ModelState.AddModelError("count", " the bay aleardy exit in Carta ");
                ProductA p = new ProductA();
                p.Idu = (int)Session["iduser"];

                return View(p);

            }
            return RedirectToAction("HomeDealer");
        }

    }
}