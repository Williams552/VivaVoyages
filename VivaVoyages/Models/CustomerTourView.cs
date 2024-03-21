using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace VivaVoyages.Models
{
    public class CustomerTourView
    {
        public int OrderId { get; set; }

        [DisplayName("Tour Name")]
        public string TourName { get; set; }

        [DisplayName("Passenger Count")]
        public int PassengerCount { get; set; }

        [DisplayName("Date Start")]
        public string DateStart { get; set; }

        [DisplayName("Tour Dates")]
        public int TourDates { get; set; }

        [DisplayName("Tour Guide")]
        public string TourGuide { get; set; }
        public decimal? Total { get; set; }
        public string Status { get; set; }
    }
}