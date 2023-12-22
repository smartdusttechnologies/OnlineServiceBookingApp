using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBooking.Business.Models.Resturant
{
    public class Addon
    {
        public string __typename { get; set; }
        public string _id { get; set; }
        public List<object> options { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public int quantityMinimum { get; set; }
        public int quantityMaximum { get; set; }
    }

    public class Category
    {
        public string __typename { get; set; }
        public string _id { get; set; }
        public string title { get; set; }
        public List<Food> foods { get; set; }
    }

    public class Food
    {
        public string __typename { get; set; }
        public string _id { get; set; }
        public string title { get; set; }
        public string image { get; set; }
        public string description { get; set; }
        public List<Variation> variations { get; set; }
    }

    public class Location
    {
        public string __typename { get; set; }
        public List<string> coordinates { get; set; }
    }

    public class OpeningTime
    {
        public string __typename { get; set; }
        public string day { get; set; }
        public List<Time> times { get; set; }
    }

    public class Option
    {
        public string __typename { get; set; }
        public string _id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public int price { get; set; }
    }

    public class Order
    {
        public string __typename { get; set; }
        public User user { get; set; }
    }

    public class Restaurant
    {
        public string __typename { get; set; }
        public string _id { get; set; }
        public int orderId { get; set; }
        public string orderPrefix { get; set; }
        public string name { get; set; }
        public string image { get; set; }
        public string slug { get; set; }
        public string address { get; set; }
        public Location location { get; set; }
        public int deliveryTime { get; set; }
        public int minimumOrder { get; set; }
        public int tax { get; set; }
        public ReviewData reviewData { get; set; }
        public List<Category> categories { get; set; }
        public List<Option> options { get; set; }
        public List<Addon> addons { get; set; }
        public object zone { get; set; }
        public object rating { get; set; }
        public bool isAvailable { get; set; }
        public List<OpeningTime> openingTimes { get; set; }
    }

    public class Review
    {
        public string __typename { get; set; }
        public string _id { get; set; }
        public Order order { get; set; }
        public int rating { get; set; }
        public string description { get; set; }
        public string createdAt { get; set; }
    }

    public class ReviewData
    {
        public string __typename { get; set; }
        public int total { get; set; }
        public int ratings { get; set; }
        public List<Review> reviews { get; set; }
    }

    public class ResturantDetailModel
    {
        public Restaurant restaurant { get; set; }
    }

    public class Time
    {
        public string __typename { get; set; }
        public List<string> startTime { get; set; }
        public List<string> endTime { get; set; }
    }

    public class User
    {
        public string __typename { get; set; }
        public string _id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
    }

    public class Variation
    {
        public string __typename { get; set; }
        public string _id { get; set; }
        public string title { get; set; }
        public double price { get; set; }
        public int discounted { get; set; }
        public List<object> addons { get; set; }
    }


}
