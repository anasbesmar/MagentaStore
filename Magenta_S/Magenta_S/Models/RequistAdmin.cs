﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Magenta_S.Models
{
    public class RequistAdmin
    {
        public int Idproduct { get; set; }
        public int Idu { get; set; }
        public string Type { get; set; }
        public string Genderoftype { get; set; }
        public string Quality { get; set; }
        public string Season { get; set; }
        public string Company { get; set; }
        public string Description { get; set; }
        public string Meadin { get; set; }
        public int Price { get; set; }
        public int count { get; set; }
        public bool Ok { get; set; }
        public string image { get; set; }
        public string size { get; set; }
        [DisplayFormat(DataFormatString = "{0:d}")]
        public System.DateTime date { get; set; }

        public int billforbuy { get; set; }

        public List<Product> getproduct
        {
            get
            {
                magentadb mg = new magentadb();

                return mg.Products.Where(x => x.Ok == false && x.Meadin != "unlock").ToList();

            }
            set { }
        }

        public List<User> getaccount
        {
            get
            {
                magentadb mg = new magentadb();
                return mg.Users.Where(x => x.Point == null).ToList();

            }

            set { }

        }
       
        public List<Bill> getbill
        {
            get
            {
                magentadb mg = new magentadb();
                return mg.Bills.Where(x=>x.ok==null).ToList();

            }

            set { }

        }
     
    }
}