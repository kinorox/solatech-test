using System.Linq;
using Infrastructure.Interfaces;
using Infrastructure.Models;

namespace solatech.Validators
{
    public class AddressValidator : IValidator<GoogleMapsGeocodingResponse>
    {
        public bool IsValid(GoogleMapsGeocodingResponse obj)
        {
            if (obj == null || !obj.Results.Any()) return false;

            var result = obj.Results.FirstOrDefault();

            return result != null && !string.IsNullOrEmpty(result.geometry?.location?.lat) && !string.IsNullOrEmpty(result.geometry?.location?.lng);
        }
    }
}