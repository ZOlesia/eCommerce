using System;
using System.Collections.Generic;

namespace eCommerce.Models
{
    public class Customer
    {
        public int customerid { get; set; }
        public string name { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public List<Order> myorders { get; set; }

        public Customer()
        {
            this.created_at = DateTime.Now;
            this.updated_at = DateTime.Now;
            myorders = new List<Order>();
        }

        public string displayDate {
            get {

                return this.created_at.ToString("MMM dd, yyyy");
            }
        }
        public string duration {
            get{
                double time = DateTime.Now.Subtract(this.created_at).TotalMinutes;
                string word = "minute(s)";
                if((int)time >= 60)
                {
                    time = DateTime.Now.Subtract(this.created_at).TotalHours;
                    word = "hour(s)";
                }
                else if(time >= 24)
                {
                    time = DateTime.Now.Subtract(this.created_at).TotalDays;
                    word = "day(s)";
                }
                else if(time >= 7)
                {
                    time = DateTime.Now.Subtract(this.created_at).TotalDays/7;
                    word = "week(s)";
                }
                return $"{(int)time} {word} ago";
            }
        }
        
    }
}