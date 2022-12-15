using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace Magenta_S.Models
{
    public class ProductA
    {
        
        [Required]
        public List<SelectListItem> Selectcolor { get; set; }
        [Required]
        public List<SelectListItem> Selectsize { get; set; }

        public void dataforSelectsize() {

            SelectListItem s0 = new SelectListItem()
            {
                Text = "35",
                Value = "35"

            };
            SelectListItem s1 = new SelectListItem()
            {
                Text = "36",
                Value = "36"
            };
            SelectListItem s2 = new SelectListItem()
            {
                Text = "37",
                Value = "37"
            };
            SelectListItem s3 = new SelectListItem()
            {

                Text = "38",
                Value = "38"
            };
            SelectListItem s4 = new SelectListItem()
            {
                Text = "39",
                Value = "39"
            };
            SelectListItem s5 = new SelectListItem()
            {
                Text = "40",
                Value = "40"
            };
            SelectListItem s6 = new SelectListItem()
            {
                Text = "41",
                Value = "41"
            };
            SelectListItem s7 = new SelectListItem()
            {
                Text = "42",
                Value = "42"
            };
            SelectListItem s8 = new SelectListItem()
            {
                Text = "43",
                Value = "43"
            };

            SelectListItem s9 = new SelectListItem()
            {
                Text = "44",
                Value = "44"
            };
            SelectListItem s10 = new SelectListItem()
            {
                Text = "45",
                Value = "45"
            };
            SelectListItem s11 = new SelectListItem()
            {
                Text = "46",
                Value = "46"
            };
            SelectListItem s12 = new SelectListItem()
            {
                Text = "small",
                Value = "small"
            };
            SelectListItem s13 = new SelectListItem()
            {
                Text = "medium",
                Value = "medium"
            };
            SelectListItem s14 = new SelectListItem()
            {
                Text = "large",
                Value = "large"
            };
            SelectListItem s15 = new SelectListItem()
            {
                Text = "X large",
                Value = "X large"
            };
            SelectListItem s16 = new SelectListItem()
            {
                Text = "XX large",
                Value = "XX large"
            };
            List<SelectListItem> l = new List<SelectListItem>() { s0, s1, s2, s3, s4, s5, s6, s7, s8, s9, s10, s11, s12, s13,s14,s15,s16 };
            Selectsize = l;


        }

        public void dataforSelectcolor() {
           
            SelectListItem s0 = new SelectListItem()
            {
                Text = "Red",
                Value = "Red"

            };
            SelectListItem s1 = new SelectListItem()
            {
                Text = "Green",
                Value = "Green"
            };
            SelectListItem s2 = new SelectListItem()
            {
                Text = "Yallow",
                Value = "Yallow"
            };
            SelectListItem s3 = new SelectListItem() {

                Text = "Silver",
                Value = "Silver"
            };
            SelectListItem s4 = new SelectListItem()
            {
                Text = "Vinous",
                Value = "Vinous"
            };
            SelectListItem s5 = new SelectListItem()
            {
                Text = "Grey",
                Value = "Grey"
            };
            SelectListItem s6 = new SelectListItem()
            {
                Text = "White",
                Value = "White"
            };
            SelectListItem s7 = new SelectListItem()
            {
                Text = "Pink",
                Value = "Pink"
            };
            SelectListItem s8 = new SelectListItem()
            {
                Text = "Blue",
                Value = "Blue"
            };

            SelectListItem s9 = new SelectListItem()
            {
                Text = "Black",
                Value = "Black"
            };
            SelectListItem s10 = new SelectListItem()
            {
                Text = "Iron",
                Value = "Iron"
            };
            SelectListItem s11= new SelectListItem()
            {
                Text = "Orange",
                Value = "Orange"
            };
            SelectListItem s12 = new SelectListItem()
            {
                Text = "Purple",
                Value = "Purple"
            };
            SelectListItem s13 = new SelectListItem()
            {
                Text = "Magenta",
                Value = "Magenta"
            };
            List<SelectListItem> l = new List<SelectListItem>() { s0,s1, s2, s3, s4, s5, s6, s7, s8, s9, s10, s11, s12, s13 };
            Selectcolor = l;


        }
  

        
        public int Idproduct { get; set; }
        public int Idu { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string Genderoftype { get; set; }
        [Required]
        public string Quality { get; set; }
        [Required]
        public string Season { get; set; }
        [Required]
        public string Company { get; set; }
        [Required]
        public string Description { get; set; }
        public string image { get; set; }
        [Required]
        public string Meadin { get; set; }
        [Required]
        public int Price { get;
              
             set; }
        [Required]
        public int count { get; set; }
        public bool Ok { get; set; }
        public System.DateTime date { get; set; }
        public virtual ICollection<Color> Colors { get; set; }
        public virtual ICollection<Size> Sizes { get; set; }


        

        public List<Product> getallproduct {
            get {
                magentadb mg = new magentadb();
                var m = mg.Products.Where(x => x.Idu == Idu && x.Ok == true).ToList();
                var c = from item in m
                        orderby item.date descending
                        select item;
                return c.ToList();



            }
            set { } }

        public List<Product> productallshow {

            get {
                
                magentadb mg = new magentadb();
              var m=   mg.Products.Where(x => x.Ok == true&&x.count>0);              
                var c = from item in m
                        orderby item.date descending
                        select item;
            
                return c.ToList();

            }
            set { }
        }
        public Product getproduct(ProductA A)
        { 
            Product p = new Product();
            p.Type = A.Type;
            p.Genderoftype = A.Genderoftype;
            p.Quality = A.Quality;
            p.Season = A.Season;
            p.Company = A.Company;
            p.Description = A.Description;   
            p.Meadin = A.Meadin;
            p.Price = A.Price;
            p.count = A.count;           
            p.date= DateTime.UtcNow;
            p.Ok = false;
            return p;

        }

        public Product UpdateProduct(ProductA A) {
            magentadb mg = new magentadb();
          Product Productdb= mg.Products.Where(x => x.Idproduct == A.Idproduct).Single();
            
            Product p = new Product();
 
            p.Idproduct = Productdb.Idproduct;
            p.Type = Productdb.Type;
            p.Idu = Productdb.Idu;
            p.Genderoftype = Productdb.Genderoftype;
            p.Quality = Productdb.Quality;
            p.Season = Productdb.Season;
            p.Company = Productdb.Company;
            p.Description = A.Description;
            p.Meadin = Productdb.Meadin;
            p.image = Productdb.image;
            p.Price = A.Price;
            p.count = A.count;
            p.date = Productdb.date;
            p.Ok =Productdb.Ok;
            return p;

        }
        public void  UpdateProduct(int id)
        {
            magentadb mg = new magentadb();
            Product Productdb = mg.Products.Where(x => x.Idproduct ==id).Single();         
            Type = Productdb.Type;
            Genderoftype = Productdb.Genderoftype;
            Quality = Productdb.Quality;
            Season = Productdb.Season;
            Company = Productdb.Company;
            Description = Productdb.Description;
            image= Productdb.image;
            Meadin = Productdb.Meadin;
            Price = Productdb.Price;
            count = Productdb.count;
            date = Productdb.date;
            Idu = Productdb.Idu;
            Idproduct = Productdb.Idproduct;
            Ok = Productdb.Ok;

        }




    }
}