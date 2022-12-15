using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.IO;
using Magenta_S.Models;
using System.Data.Entity;

namespace Magenta_S.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        magentadb mg = new magentadb();
        public ActionResult Add_Product()
        {

            ProductA pa = new ProductA();
            pa.dataforSelectsize();

            pa.dataforSelectcolor();

            return View(pa);
        }

        [HttpPost]
        public ActionResult Add_Product(ProductA a, HttpPostedFileBase Upload)
        {

            if (Upload == null) {

                ModelState.AddModelError("imageu", " Please Enter IMage ");
                return View();

            }

            if (ModelState.IsValid)
            {
                string path = Path.Combine(Server.MapPath("~/imageproduct"), Upload.FileName);
                Upload.SaveAs(path);
                ProductA pa = new ProductA();
                Product product = pa.getproduct(a);
                product.image = Upload.FileName;
                bool checkforsize = false;
                bool checkforcolor = false;
                foreach (var item in a.Selectcolor)
                {
                   
                    if (item.Selected)
                    {
                        
                        checkforcolor = true;
                        Color c = new Magenta_S.Models.Color();
                        c.ColorP = item.Value;
                        product.Colors.Add(c);
                        
                    }
                }
                foreach (var item in a.Selectsize)
                {
                    if (item.Selected)
                    {
                        checkforsize = true;
                        Size s = new Size();
                        s.size1 = item.Value;
                        product.Sizes.Add(s);

                    }
                }
                if (checkforsize == false )
                {
                    ModelState.AddModelError("checks", " Please Enter Size ");

                    pa.dataforSelectsize();

                    pa.dataforSelectcolor();

                    return View(pa);

                }
                if (checkforcolor == false)
                {

                    ModelState.AddModelError("checkc", " Please Enter Color  ");
                    pa.dataforSelectsize();

                    pa.dataforSelectcolor();

                    return View(pa);

                }
                product.Idu = (int)(Session["iduser"]);

                mg.Products.Add(product);

             
                mg.SaveChanges();


                return RedirectToAction("HomeDealer", "Home");
            }
            return View();
        }



        public ActionResult My_Product()
        {
            ProductA p = new ProductA();
            p.Idu = (int)Session["iduser"];
            return View(p);
        }
        [HttpPost]
        [ActionName("My_Product")]
        public ActionResult My_Products(int Idproduct)
        {
         

            Product p = mg.Products.Where(x => x.Idproduct == Idproduct).Single();
            try
            {

                mg.Products.Remove(p);
                mg.SaveChanges();
            }
            catch
            {
                p.Ok = false;
                mg.Entry(p).State = EntityState.Modified;
                mg.SaveChanges();
            }

            return RedirectToAction("My_Product");
      
        }



        [HttpGet]
        public ActionResult Modification(int Idproduct)
        {
            int idu = (int)Session["iduser"];
            ProductA pa = new ProductA();
            pa.UpdateProduct(Idproduct);
            if (pa.Idu != idu)
            {
                throw new HttpException(404, "Bad Request");

            }
            pa.dataforSelectsize();
            pa.dataforSelectcolor();
            return View(pa);
        }
        [HttpPost]
        public ActionResult Modification(ProductA p)

        {
               bool checkforsize = false;
                bool checkforcolor = false;
            ProductA pa = new ProductA();
                   
            Product product = pa.UpdateProduct(p);
        

            foreach (var item in p.Selectcolor)
            {
                if (item.Selected)
                {
                    checkforsize = true;
                    Color c = new Magenta_S.Models.Color();
                    c.ColorP = item.Value;
                    c.Product = product;
                    c.Idprodact = product.Idproduct;
                    product.Colors.Add(c);
                    

                }
            }
            foreach (var item in p.Selectsize)
            {
                if (item.Selected)
                {
                    checkforcolor = true;
                    Size s = new Magenta_S.Models.Size();
                    s.size1 = item.Value;
                    s.idproduct = product.Idproduct;
                    s.Product = product;
                    product.Sizes.Add(s);

                }
            }
            if (checkforsize == false)
            {
                ModelState.AddModelError("checks", " Please Enter Size ");
                pa.UpdateProduct(p.Idproduct);
                pa.dataforSelectsize();
                pa.dataforSelectcolor();
                return View(pa);

            }
            if (checkforcolor == false)
            {

                ModelState.AddModelError("checkc", " Please Enter Color  ");

                pa.UpdateProduct(p.Idproduct);
                pa.dataforSelectsize();
                pa.dataforSelectcolor();
                return View(pa);


            }


            mg.Entry(product).State = EntityState.Modified;
            mg.SaveChanges();

            mg.Sizes.RemoveRange(mg.Sizes.Where(c => c.idproduct == product.Idproduct));
            mg.SaveChanges();

            mg.Sizes.AddRange(product.Sizes).Where(x => x.idproduct == product.Idproduct);
            mg.SaveChanges();

            mg.Colors.RemoveRange(mg.Colors.Where(c => c.Idprodact == product.Idproduct));
            mg.SaveChanges();

            mg.Colors.AddRange(product.Colors).Where(x => x.Idprodact == product.Idproduct);
            mg.SaveChanges();
            

            return RedirectToAction("HomeDealer", "Home");
        }









        [HttpGet]
        public ActionResult Offer(int Idproduct)
        {
            Product p = mg.Products.Where(x => x.Idproduct == Idproduct).Single();
            return View(p);
        }

        [HttpPost]
        public ActionResult Offer()
        {
            //Product p = mg.Products.Where(x => x.Idproduct == Idproduct).Single();
            return View();
        }







       
        public ActionResult All_Product()
        {
            return View();
        }
        public ActionResult Products_For_Admin()
        {
            return View();
        }

    }
}