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
    
    public partial class Tbl_State_City
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tbl_State_City()
        {
            this.Tbl_CustAddress = new HashSet<Tbl_CustAddress>();
            this.Tbl_State_City1 = new HashSet<Tbl_State_City>();
        }
    
        public int ID { get; set; }
        public string Title { get; set; }
        public int PID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_CustAddress> Tbl_CustAddress { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_State_City> Tbl_State_City1 { get; set; }
        public virtual Tbl_State_City Tbl_State_City2 { get; set; }
    }
}
