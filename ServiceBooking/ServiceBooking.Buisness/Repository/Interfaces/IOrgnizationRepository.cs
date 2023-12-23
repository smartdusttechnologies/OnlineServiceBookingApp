using ServiceBooking.Buisness.Core.Model;
using System.Collections.Generic;
namespace ServiceBooking.Business.Data.Repository.Interfaces
{
    public interface IOrganizationRepository
    {
        List<Organization> Get();

    }
}