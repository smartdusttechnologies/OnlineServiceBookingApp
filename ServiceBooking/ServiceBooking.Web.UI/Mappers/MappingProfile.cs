using AutoMapper;
using ServiceBooking.Buisness.Core.Model;
using ServiceBooking.Web.Models;

namespace ServiceBooking.Web.UI.Mappers
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
           CreateMap<UserDTO,UserModel>().ReverseMap();
        }
    }
}