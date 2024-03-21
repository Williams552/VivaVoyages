using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VivaVoyages.Models
{
    public class CustomerTourView
    {
        public int OrderId { get; set; }
        public string TourName { get; set; }
        public int PassengerCount { get; set; }
        public string DateStart { get; set; }
        public int TourDates { get; set; }
        public string TourGuide { get; set; }
        public decimal? Total { get; set; }

        public string Status { get; set; }
    }
}