using System;

namespace ServiceBooking.Buisness.Core.Model.Security
{
    public class PasswordLogin
    {
        /// <summary>
        /// UserId
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// PasswordHash
        /// </summary>
        public string PasswordHash { get; set; }
        /// <summary>
        /// PasswordSlat
        /// </summary>
        public string PasswordSalt { get; set; }
        /// <summary>
        /// ChangeDate
        /// </summary>
        public DateTime ChangeDate { get; set; }

        /// <summary>
        /// RoleId
        /// </summary>
        public int RoleId { get; set; }
    }
}
