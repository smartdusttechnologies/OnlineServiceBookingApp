﻿using System;
using ServiceBooking.Business.Core.Model;

namespace ServiceBooking.Buisness.Core.Model.Security
{
    public class LoginToken : Entity
    {
        /// <summary>
        /// User Id.
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// User Name.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Role Id.
        /// </summary>
        public int RoleId { get; set; }
        /// <summary>
        /// Access Token.
        /// </summary>
        public string AccessToken { get; set; }
        /// <summary>
        /// Access Token Expiry.
        /// </summary>
        public DateTime AccessTokenExpiry { get; set; }
        /// <summary>
        /// Refresh Token.
        /// </summary>
        public string RefreshToken { get; set; }
        /// <summary>
        /// Refresh Token Expiry.
        /// </summary>
        public DateTime RefreshTokenExpiry { get; set; }
        /// <summary>
        /// Device Code.
        /// </summary>
        public string DeviceCode { get; set; }
        /// <summary>
        /// Device Name.
        /// </summary>
        public string DeviceName { get; set; }
    }
}
