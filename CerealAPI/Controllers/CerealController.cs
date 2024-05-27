using Microsoft.AspNetCore.Mvc;

using CerealAPI.Enums;
using CerealAPI.Models;
using CerealAPI.Services;

namespace CerealAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CerealController(ICerealService cerealService) : ControllerBase
    {
        [HttpGet(Name = "GetCerealProducts")]
        public ActionResult<IEnumerable<CerealProduct>> GetCereal(
            [FromQuery]
            int? id,
            string? name,
            Manufacturer? manufacturer,
            CerealType? cerealType,
            short? minCalories,
            bool? minCalInc,
            short? maxCalories,
            bool? maxCalInc,
            byte? minProtein,
            bool? minProInc,
            byte? maxProtein,
            bool? maxProInc,
            byte? minFat,
            byte? maxFat,
            short? minSodium,
            short? maxSodium,
            double? minFiber,
            double? maxFiber,
            short? minSugar,
            short? maxSugar,
            short? minPotass,
            short? maxPotass,
            short? minVitamins,
            short? maxVitamins,
            byte? shelf,
            double? minWeight,
            double? maxWeight,
            double? minCups,
            double? maxCups,
            double? minRating,
            double? maxRating,
            CerealProperty? sortBy,
            bool sortAsc = true)
        {
            var cereals = cerealService.GetCereal(
                id,
                name,
                manufacturer,
                cerealType,
                minCalories,
                minCalInc,
                maxCalories,
                maxCalInc,
                minProtein,
                minProInc,
                maxProtein,
                maxProInc,
                minFat,
                maxFat,
                minSodium,
                maxSodium,
                minFiber,
                maxFiber,
                minSugar,
                maxSugar,
                minPotass,
                maxPotass,
                minVitamins,
                maxVitamins,
                shelf,
                minWeight,
                maxWeight,
                minCups,
                maxCups,
                minRating,
                maxRating,
                sortBy,
                sortAsc);

            if (cereals.Count > 0)
                return Ok(cereals);

            return NoContent();
        }
    }
}
