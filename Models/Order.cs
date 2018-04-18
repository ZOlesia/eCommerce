using System;
using System.Collections.Generic;

namespace eCommerce.Models
{
    public class Order
    {
        public int orderid { get; set; }
        public int customerid { get; set; }
        public Customer customer { get; set; }
        public int productid { get; set; }
        public Product product { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public int quantity_order { get; set; }


        public Order()
        {
            this.created_at = DateTime.Now;
            this.updated_at = DateTime.Now;
        }

        public string order_duration {
            get{
                double dur = DateTime.Now.Subtract(this.created_at).TotalMinutes;
                string final_word = "minute(s)";
                if((int)dur >= 60)
                {
                    dur = DateTime.Now.Subtract(this.created_at).TotalHours;
                    final_word = "hour(s)";
                }
                else if(dur >= 24)
                {
                    dur = DateTime.Now.Subtract(this.created_at).TotalDays;
                    final_word = "day(s)";
                }
                else if(dur >= 7)
                {
                    dur = DateTime.Now.Subtract(this.created_at).TotalDays/7;
                    final_word = "week(s)";
                }
                return $"{(int)dur} {final_word} ago";
            }
        }
    }
}