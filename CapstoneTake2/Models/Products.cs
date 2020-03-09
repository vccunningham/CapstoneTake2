using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneTake2.Models {
    public class Products {
        public int Id { get; set; }
        public string PartNbr { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Unit { get; set; }
        public string PhotoPath { get; set;}
        public int VendorId { get; set; }

        public virtual Vendors Vendor { get; set; }




        public Products() { }

    }
}
