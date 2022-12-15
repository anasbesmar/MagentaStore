using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Magenta_S.Models
{
    public class my_buy
    {
        public int idu { get; set; }
        public int idbill { get; set; }
        public int idseesion { get; set; }
        public List<Buy> getbuy { get {
                    magentadb mg = new magentadb();
               return mg.Buys.Where(x => x.Idu == idu&&x.bill==idbill).ToList();

            }
            
        }
        public List<Bill> getbill
        {
            get
            {
                magentadb mg = new magentadb();
                if (idseesion == 1)
                {
                    return mg.Bills.ToList();

                }
                else
                return mg.Bills.Where(x => x.Idu == idu).ToList();

            }
            set {}
           
        }

        public List<Buy> getbuyforadmin
        {
            get
            {
               
                magentadb mg = new magentadb();
                return mg.Buys.Where(x=>x.bill==idbill).ToList();

            }
           
        }
        public List<Bill> getbillforadmin
        {
            get
            {
                magentadb mg = new magentadb();
                return mg.Bills.ToList();

            }
            
        }



    }
}