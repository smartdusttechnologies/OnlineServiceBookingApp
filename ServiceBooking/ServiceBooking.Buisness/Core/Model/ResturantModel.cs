using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBooking.Buisness.Core.Model
{
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
    }

    public class Location
    {
        public string __typename { get; set; }
        public List<string> coordinates { get; set; }
    }

    public class NearByRestaurants
    {
        public string __typename { get; set; }
        public List<object> offers { get; set; }
        public List<Section> sections { get; set; }
        public List<Restaurant> restaurants { get; set; }
    }

    public class OpeningTime
    {
        public string __typename { get; set; }
        public string day { get; set; }
        public List<Time> times { get; set; }
    }

    public class Restaurant
    {
        public string __typename { get; set; }
        public string _id { get; set; }
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
        public object rating { get; set; }
        public bool isAvailable { get; set; }
        public List<OpeningTime> openingTimes { get; set; }
    }

    public class Review
    {
        public string __typename { get; set; }
        public string _id { get; set; }
    }

    public class ReviewData
    {
        public string __typename { get; set; }
        public int total { get; set; }
        public double ratings { get; set; }
        public List<Review> reviews { get; set; }
    }

    public class ResturantModel
    {
        public NearByRestaurants nearByRestaurants { get; set; }
    }

    public class Section
    {
        public string __typename { get; set; }
        public string _id { get; set; }
        public string name { get; set; }
        public List<string> restaurants { get; set; }
    }

    public class Time
    {
        public string __typename { get; set; }
        public List<string> startTime { get; set; }
        public List<string> endTime { get; set; }
    }
}
