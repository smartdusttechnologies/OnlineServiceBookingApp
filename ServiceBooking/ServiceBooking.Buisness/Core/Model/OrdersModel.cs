using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBooking.Buisness.Core.Model.Orders
{
    public class OrdersModel
    {
        public  List<Orders> Orders { get; set; }
    }
    public class Location
    {
        public List<double> Coordinates { get; set; }
    }

    public class Variation
    {
        public string _id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public decimal Discounted { get; set; }
    }

    public class Option
    {
        public string _id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }

    public class Addon
    {
        public string _id { get; set; }
        public List<Option> Options { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int QuantityMinimum { get; set; }
        public int QuantityMaximum { get; set; }
    }

    public class Item
    {
        public string _id { get; set; }
        public string Title { get; set; }
        public string Food { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public Variation Variation { get; set; }
        public List<Addon> Addons { get; set; }
    }

    public class Restaurant
    {
        public string _id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Slug { get; set; }
        public string Address { get; set; }
        public Location Location { get; set; }
    }

    public class DeliveryAddress
    {
        public Location Location { get; set; }
        public string DeliveryAddres { get; set; }
    }

    public class User
    {
        public string _id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
    }

    public class Rider
    {
        public string _id { get; set; }
        public string Name { get; set; }
    }

    public class Review
    {
        public string _id { get; set; }
    }

    public class Orders
    {
        public string _id { get; set; }
        public string OrderId { get; set; }
        public Restaurant Restaurant { get; set; }
        public DeliveryAddress DeliveryAddress { get; set; }
        public List<Item> Items { get; set; }
        public User User { get; set; }
        public Rider Rider { get; set; }
        public Review Review { get; set; }
        public string PaymentMethod { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal OrderAmount { get; set; }
        public string OrderStatus { get; set; }
        public decimal DeliveryCharges { get; set; }
        public decimal Tipping { get; set; }
        public decimal TaxationAmount { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ExpectedTime { get; set; }
        public bool IsPickedUp { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime CompletionTime { get; set; }
        public DateTime? CancelledAt { get; set; }
        public DateTime AssignedAt { get; set; }
        public DateTime DeliveredAt { get; set; }
        public DateTime AcceptedAt { get; set; }
        public DateTime PickedAt { get; set; }
        public int PreparationTime { get; set; }
    }

}
