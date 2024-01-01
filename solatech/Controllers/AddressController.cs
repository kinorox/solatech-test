using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Infrastructure;
using Infrastructure.Extensions;
using solatech.Models;
using solatech.Validators;

namespace solatech.Controllers
{
    public class AddressController : Controller
    {
        private readonly IGoogleMapsService _googleMapsService;

        public AddressController(IGoogleMapsService googleMapsService)
        {
            _googleMapsService = googleMapsService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> Validate(string address)
        {
            var response = await _googleMapsService.GetAddress(address);

            var validator = new AddressValidator();

            return Json(validator.IsValid(response));
        }

        [HttpPost]
        public async Task<JsonResult> SearchAddresses(string address)
        {
            var response = await _googleMapsService.GetAddress(address);
            return Json(response);
        }

        [HttpPost]
        public async Task<JsonResult> SearchByZipCode(string zipCode)
        {
            var response = await _googleMapsService.GetAddressByPostalCode(zipCode);

            var first = response?.Results.FirstOrDefault();

            if (first == null) return null;

            var addressComponents = first.address_components;

            var resultAddress = new Address
            { 
                ZipCode = addressComponents.GetValueByType("postal_code"),
                Neighborhood = addressComponents.GetValueByType("administrative_area_level_4"),
                City = addressComponents.GetValueByType("administrative_area_level_2"),
                State = addressComponents.GetValueByType("administrative_area_level_1"),
                Country = addressComponents.GetValueByType("country")
            };

            return Json(resultAddress);
        }
    }
}