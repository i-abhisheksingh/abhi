//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TravaliaMvc.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class OrderTable
    {
        public int Oid { get; set; }
        public Nullable<int> UId { get; set; }
        public Nullable<int> PId { get; set; }
        public string Status { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
    
        public virtual PackageTable PackageTable { get; set; }
        public virtual UserTable UserTable { get; set; }
    }
}
