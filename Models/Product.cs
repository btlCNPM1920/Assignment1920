//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Computer_Shop.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Web;

    public partial class Product
    {
        public int Product_ID { get; set; }
        public string Product_Name { get; set; }
        public string Made_in { get; set; }
        public string Brand { get; set; }
        public string Quantity { get; set; }
        public string Warranty_Period { get; set; }
        public double Price { get; set; }
        [DisplayName ("Upload Image")]
        public string Image1 { get; set; }
        public HttpPostedFileBase ImageFile1 { get; set; }
        public string Image2 { get; set; }
        public HttpPostedFileBase ImageFile2 { get; set; }
        public string Image3 { get; set; }
        public HttpPostedFileBase ImageFile3 { get; set; }
        public string Description { get; set; }
    }
}