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
            [FromQuery] int? id,
            [FromQuery] string? name,
            [FromQuery] Manufacturer? manufacturer,
            [FromQuery] CerealType? cerealType,
            [FromQuery] short? minCalories,
            [FromQuery] bool? minCalIncl,
            [FromQuery] short? maxCalories,
            [FromQuery] bool? maxCalIncl,
            [FromQuery] byte? minProtein,
            [FromQuery] bool? minProIncl,
            [FromQuery] byte? maxProtein,
            [FromQuery] bool? maxProIncl,
            [FromQuery] byte? minFat,
            [FromQuery] bool? minFatIncl,
            [FromQuery] byte? maxFat,
            [FromQuery] bool? maxFatIncl,
            [FromQuery] short? minSodium,
            [FromQuery] bool? minSodIncl,
            [FromQuery] short? maxSodium,
            [FromQuery] bool? maxSodIncl,
            [FromQuery] double? minFiber,
            [FromQuery] bool? minFibIncl,
            [FromQuery] double? maxFiber,
            [FromQuery] bool? maxFibIncl,
            [FromQuery] double? minCarbohydrates,
            [FromQuery] bool? minCarbIncl,
            [FromQuery] double? maxCarbohydrates,
            [FromQuery] bool? maxCarbIncl,
            [FromQuery] short? minSugars,
            [FromQuery] bool? minSugIncl,
            [FromQuery] short? maxSugars,
            [FromQuery] bool? maxSugIncl,
            [FromQuery] short? minPotassium,
            [FromQuery] bool? minPotIncl,
            [FromQuery] short? maxPotassium,
            [FromQuery] bool? maxPotIncl,
            [FromQuery] short? minVitamins,
            [FromQuery] bool? minVitIncl,
            [FromQuery] short? maxVitamins,
            [FromQuery] bool? maxVitIncl,
            [FromQuery] double? minWeight,
            [FromQuery] bool? minWeightIncl,
            [FromQuery] double? maxWeight,
            [FromQuery] bool? maxWeightIncl,
            [FromQuery] double? minCups,
            [FromQuery] bool? minCupsIncl,
            [FromQuery] double? maxCups,
            [FromQuery] bool? maxCupsIncl,
            [FromQuery] double? minRating,
            [FromQuery] bool? minRatingIncl,
            [FromQuery] double? maxRating,
            [FromQuery] bool? maxRatingIncl,
            [FromQuery] byte? shelf,
            [FromQuery] CerealProperty? sortBy,
            [FromQuery] SortOrder sortOrder = SortOrder.Asc)
        {
            var cereals = cerealService.GetCereal(
                id,
                name,
                manufacturer,
                cerealType,
                minCalories,
                minCalIncl,
                maxCalories,
                maxCalIncl,
                minProtein,
                minProIncl,
                maxProtein,
                maxProIncl,
                minFat,
                minFatIncl,
                maxFat,
                maxFatIncl,
                minSodium,
                minSodIncl,
                maxSodium,
                maxSodIncl,
                minFiber,
                minFibIncl,
                maxFiber,
                maxFibIncl,
                minCarbohydrates,
                minCarbIncl,
                maxCarbohydrates,
                maxCarbIncl,
                minSugars,
                minSugIncl,
                maxSugars,
                maxSugIncl,
                minPotassium,
                minPotIncl,
                maxPotassium,
                maxPotIncl,
                minVitamins,
                minVitIncl,
                maxVitamins,
                maxVitIncl,
                minWeight,
                minWeightIncl,
                maxWeight,
                maxWeightIncl,
                minCups,
                minCupsIncl,
                maxCups,
                maxCupsIncl,
                minRating,
                minRatingIncl,
                maxRating,
                maxRatingIncl,
                shelf,
                sortBy,
                sortOrder);

            if (cereals.Count > 0)
                return Ok(cereals);

            return NoContent();
        }

        [HttpPost(Name = "PostNewCerealProduct")]
        public async Task<ActionResult> PostCereal(
            [FromBody] CerealProduct cereal)
        {
            var newCereal = await cerealService.PostCereal(cereal);

            if (newCereal != null)
            {
                return Created("newCereal.Id", newCereal);
            }
            else
            {
                return BadRequest();
            }
            
        }
    }
}
