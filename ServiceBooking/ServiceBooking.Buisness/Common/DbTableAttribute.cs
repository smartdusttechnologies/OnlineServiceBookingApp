﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceBooking.Business.Common
{

    [System.AttributeUsage(System.AttributeTargets.Class)]
    public class DbTableAttribute : System.Attribute
    {
        public string Name { get; set; }

        public DbTableAttribute(string name)
        {
            Name = name;
        }
    }
}
