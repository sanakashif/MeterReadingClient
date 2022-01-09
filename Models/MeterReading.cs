using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeterReadingClient.Models
{
    public class MeterReading
    {
        public string AccountId { get; set; }

        public string MeterReadingDateTime { get; set; }

        public string MeterReadValue { get; set; }
    }
}