    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    namespace VivaVoyages.Models;

    public partial class Place
    {
        public int PlaceId { get; set; }

        public string PlaceName { get; set; }

        public string Address { get; set; }

        public string Description { get; set; }

        [NotMapped]
        public IFormFile Image { get; set; }

        public string? ImagePath { get; set; }

        public virtual ICollection<Destination> Destinations { get; set; } = new List<Destination>();
    }
