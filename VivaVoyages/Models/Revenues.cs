using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VivaVoyages.Models
{
    public class Revenues
    {
        public int TourId { get; set; }
        public string TourName { get; set; }
        public int TotalPassengers { get; set; }
        public decimal ProfitPreTour { get; set; }
        public decimal TotalProfit { get; set; }
    }
}