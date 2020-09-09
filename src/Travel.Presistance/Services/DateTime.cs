using System;
using System.Collections.Generic;
using System.Text;
using Travel.Application.Interface;

namespace Travel.Presistance.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;

        public DateTime UtcNow => DateTime.Now;  
    }
}
