//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Magenta_S.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Color_Size
    {
        public int Idprodact { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
    
        public virtual Product Product { get; set; }
    }
}
