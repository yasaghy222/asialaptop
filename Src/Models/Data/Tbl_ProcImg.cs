//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Src.Models.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Tbl_ProcImg
    {
        public int ID { get; set; }
        public int ProcID { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
    
        public virtual Tbl_Product Tbl_Product { get; set; }
    }
}
