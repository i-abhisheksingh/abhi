using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TravaliaMvc.Models
{
    public class AllOrders
    {
        [Key]
        public int Oid { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PackageName { get; set; }
        public string Price { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
    }
}