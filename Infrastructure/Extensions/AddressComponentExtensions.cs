using System.Collections.Generic;
using System.Linq;
using Infrastructure.Models;

namespace Infrastructure.Extensions
{
    public static class AddressComponentExtensions
    {
        public static string GetValueByType(this IEnumerable<AddressComponent> components, string type)
        {
            return components
                .FirstOrDefault(component => component.types.Any(c => c == type))
                ?.long_name ?? string.Empty;
        }
    }
}
