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
    
    public partial class Tbl_Customer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tbl_Customer()
        {
            this.Tbl_CustAddress = new HashSet<Tbl_CustAddress>();
            this.Tbl_Factor = new HashSet<Tbl_Factor>();
            this.Tbl_ProcReview = new HashSet<Tbl_ProcReview>();
        }
    
        public int ID { get; set; }
        public string IP { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public string Phone { get; set; }
        public string Pass { get; set; }
        public string NatCode { get; set; }
        public string Email { get; set; }
        public Nullable<bool> IsInNewsletter { get; set; }
        public bool Status { get; set; }
        public string Token { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_CustAddress> Tbl_CustAddress { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_Factor> Tbl_Factor { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_ProcReview> Tbl_ProcReview { get; set; }
    }
}
