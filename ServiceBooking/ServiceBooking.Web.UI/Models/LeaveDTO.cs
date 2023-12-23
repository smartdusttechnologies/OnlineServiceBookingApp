using ServiceBooking.Web.Models;
using ServiceBooking.Business.Core.Model;

namespace ServiceBooking.DTO
{
    public class LeaveDTO : Entity
    {
        public DateOnly Date { get; set; }
        public int UserId { get; set; } 
        public string UserName { get; set; }
        public int LeaveStatus { get; set; }

    }
}
