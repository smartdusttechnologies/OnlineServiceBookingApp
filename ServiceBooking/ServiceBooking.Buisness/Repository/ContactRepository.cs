using Dapper;
using ServiceBooking.Business.Common;
using ServiceBooking.Business.Data.Repository.Interfaces;
using System.Data;
using ServcieBooking.Business.Infrastructure;
using ServiceBooking.Buisness.Core.Model;

namespace ServiceBooking.Business.Repository
{
    public class ContactRepository : IContactRepository
    {
        private readonly IConnectionFactory _connectionFactory;

        public ContactRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public RequestResult<bool> Save(ContactModel contact)
        {
            string query = @"Insert into [Contact] (Name,Mail,Address,Subject,Phone,Message)
                                                  values (@Name,@Mail,@Address,@Subject,@Phone,@Message)";
            using IDbConnection db = _connectionFactory.GetConnection;

            var result = db.Execute(query, contact);
            if (result > 0)
            {
                return new RequestResult<bool>(true);
            }
            else
            {
                return new RequestResult<bool>(false);
            }
        }
    }
}
