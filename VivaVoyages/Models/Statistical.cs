using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VivaVoyages.Models
{
    public class Statistical
    {
        public Tour Tour { get; set; }

        [Display(Name = "Passenger Count")]
        public int PassengerCount { get; set; }
    }
}