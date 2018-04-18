using System;
using System.Linq;
using System.Collections.Generic;

namespace eCommerce.Models
{
    public class Product
    {
        public int productid { get; set; }
        public string productname { get; set; }
        public string image { get; set; }
        public string description { get; set; }
        public int quantity { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public List<Order> orders { get; set; }


        public Product()
        {
            this.created_at = DateTime.Now;
            this.updated_at = DateTime.Now;
            orders = new List<Order>();
        }
    }
}

