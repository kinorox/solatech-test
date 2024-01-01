using System.Collections.Generic;

namespace Infrastructure.Models
{
    public class GoogleMapsGeocodingResponse
    {
        public string Status { get; set; }
        public List<GoogleMapsGeocodingResult> Results { get; set; }
    }
}
