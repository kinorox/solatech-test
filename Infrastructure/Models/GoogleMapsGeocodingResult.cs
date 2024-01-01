using System.Collections.Generic;

namespace Infrastructure.Models
{
    public class GoogleMapsGeocodingResult
    {
        public string formatted_address { get; set; }
        public Geometry geometry { get; set; }
        public List<AddressComponent> address_components { get; set; }
    }
}
