using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Magenta_S.Models
{
    public class UpdateAccount
    {
        public int iduseesion { get; set; }


        public int Idu { get; set; }
        public int Idc { get; set; }
        [Required]
        public string Fname { get; set; }
        [Required]
        public string Lname { get; set; }
     
        public string Gender { get; set; }
        [Required]
        public string address { get; set; }
        [Required]
        [Display(Name = "Email")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string Username { get; set; }

        public DateTime Hiredate { get; set; }
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        public System.DateTime birthday { get; set; }
        public Nullable<int> Point { get; set; }
        public string CommercialRecord { get; set; }
        public Nullable<int> CountProduct { get; set; }
        [Required]
        public string Phone { get; set; }

        [System.Web.Mvc.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        public string oldPassword { get; set; }
        //  user ميقود لجلب داتا من نوع 
        public void UpadateAccount(int id)
        {
            magentadb mg = new magentadb();
            User u = mg.Users.Where(x => x.Idu == id).Single();
            Fname = u.Fname;
            Lname = u.Lname;
            Username = u.Username;
            Password = u.Password;
            address = u.address;
            Gender = u.Gender;
            Phone = u.Phone;
            Idc = u.Idc;
            Idu = u.Idu;
            birthday = u.birthday;

        }
        public User UpadateAccount(UpdateAccount UserR)
        {
            magentadb mg = new magentadb();
            User u = mg.Users.Where(x => x.Idu == UserR.Idu).Single();

            User user = new User();
            user.Idc = u.Idc;
            user.Idu = u.Idu;
            user.Fname = UserR.Fname;
            user.Lname = UserR.Lname;
            user.Username = u.Username;
            user.Password = UserR.Password;
            user.address = UserR.address;
            user.Hiredate = u.Hiredate;
            user.Gender = u.Gender;
            user.Phone = UserR.Phone;
            user.birthday = UserR.birthday;
            user.Point = u.Point;
            user.CommercialRecord = u.CommercialRecord;
            return user;
        }

        public static string togetpassword(int id) {

            magentadb mg = new magentadb();
            User u = mg.Users.Where(x => x.Idu == id).Single();
            return u.Password;
        }

    }
}