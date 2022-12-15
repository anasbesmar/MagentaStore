using Magenta_S.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Magenta_S.Controllers
{
    public class RequistController : Controller
    {
        // GET: Requist
        magentadb mg = new magentadb();

        public ActionResult Requist()
        {
            RequistAdmin ra = new RequistAdmin();

            return View(ra);
        }
        [HttpPost]
        public ActionResult accept_product(int Idproduct,string submit)
        {
            if (submit == "send")
            {
                Product p = mg.Products.Where(x => x.Idproduct == Idproduct).Single();
                p.Ok = true;
                mg.Entry(p).State = EntityState.Modified;
                mg.SaveChanges();
                return RedirectToAction("Requist");
            }
            else if (submit == "delete")
            {

                Product p = mg.Products.Where(x => x.Idproduct == Idproduct).Single();
                mg.Products.Remove(p);
                mg.SaveChanges();
                return RedirectToAction("Requist");
            }
            else
                return View();
        }
        [HttpPost]
        public ActionResult accept_account(int idu, string submit)
        {
            if (submit == "Done")
            {
                User u = mg.Users.Where(x => x.Idu == idu).Single();
                u.Point = 0;
                mg.Entry(u).State = EntityState.Modified;
                mg.SaveChanges();
                return RedirectToAction("Requist");
            }
            else {

                User u = mg.Users.Where(x => x.Idu == idu).Single();
                mg.Entry(u).State = EntityState.Deleted;
                mg.SaveChanges();
                return RedirectToAction("Requist");
            }
        }

        public ActionResult bay(int idbill ,string submit) {
            if (submit == "donep")
            {
               Bill b=  mg.Bills.Where(x => x.Idbill == idbill).Single();
                b.ok = true;
                mg.Entry(b).State = EntityState.Modified;
                mg.SaveChanges();
                return RedirectToAction("Requist");
            }
            return RedirectToAction("Requist");
        }

    }
}