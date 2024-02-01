namespace ServiceBooking.Buisness.Core.Model
{
    public class Address
    {
        public string Label { get; set; }
        public string DeliveryAddress { get; set; }
        public string Details { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
    }

    public class OrderInput
    {
        public string Food { get; set; }
        public int Quantity { get; set; }
        public string Variation { get; set; }
        public List<object> Addons { get; set; }
        public string SpecialInstructions { get; set; }
    }

    public class OrderModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Restaurant { get; set; }
        public List<OrderInput> OrderInput { get; set; }
        public string PaymentMethod { get; set; }
        public double Tipping { get; set; }
        public double TaxationAmount { get; set; }
        public Address Address { get; set; }
        public DateTime OrderDate { get; set; }
        public bool IsPickedUp { get; set; }
        public int DeliveryCharges { get; set; }
    }
}
