using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using CerealAPI.Enums;
using CerealAPI.Models;
using CerealAPI.Services;

namespace CerealAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("cereal")]
    public class CerealController(ICerealService cerealService) : ControllerBase
    {
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet(Name = "GetCerealProducts")]
        public IActionResult GetCereal(
            [FromQuery] int? id = null,
            [FromQuery] string? name = null,
            [FromQuery] Manufacturer? manufacturer = null,
            [FromQuery] CerealType? cerealType = null,
            [FromQuery] short? minCalories = null,
            [FromQuery] bool? minCalIncl = null,
            [FromQuery] short? maxCalories = null,
            [FromQuery] bool? maxCalIncl = null,
            [FromQuery] byte? minProtein = null,
            [FromQuery] bool? minProIncl = null,
            [FromQuery] byte? maxProtein = null,
            [FromQuery] bool? maxProIncl = null,
            [FromQuery] byte? minFat = null,
            [FromQuery] bool? minFatIncl = null,
            [FromQuery] byte? maxFat = null,
            [FromQuery] bool? maxFatIncl = null,
            [FromQuery] short? minSodium = null,
            [FromQuery] bool? minSodIncl = null,
            [FromQuery] short? maxSodium = null,
            [FromQuery] bool? maxSodIncl = null,
            [FromQuery] double? minFiber = null,
            [FromQuery] bool? minFibIncl = null,
            [FromQuery] double? maxFiber = null,
            [FromQuery] bool? maxFibIncl = null,
            [FromQuery] double? minCarbohydrates = null,
            [FromQuery] bool? minCarbIncl = null,
            [FromQuery] double? maxCarbohydrates = null,
            [FromQuery] bool? maxCarbIncl = null,
            [FromQuery] short? minSugars = null,
            [FromQuery] bool? minSugIncl = null,
            [FromQuery] short? maxSugars = null,
            [FromQuery] bool? maxSugIncl = null,
            [FromQuery] short? minPotassium = null,
            [FromQuery] bool? minPotIncl = null,
            [FromQuery] short? maxPotassium = null,
            [FromQuery] bool? maxPotIncl = null,
            [FromQuery] short? minVitamins = null,
            [FromQuery] bool? minVitIncl = null,
            [FromQuery] short? maxVitamins = null,
            [FromQuery] bool? maxVitIncl = null,
            [FromQuery] double? minWeight = null,
            [FromQuery] bool? minWeightIncl = null,
            [FromQuery] double? maxWeight = null,
            [FromQuery] bool? maxWeightIncl = null,
            [FromQuery] double? minCups = null,
            [FromQuery] bool? minCupsIncl = null,
            [FromQuery] double? maxCups = null,
            [FromQuery] bool? maxCupsIncl = null,
            [FromQuery] double? minRating = null,
            [FromQuery] bool? minRatingIncl = null,
            [FromQuery] double? maxRating = null,
            [FromQuery] bool? maxRatingIncl = null,
            [FromQuery] byte? shelf = null,
            [FromQuery] CerealProperty? sortBy = null,
            [FromQuery] SortOrder sortOrder = SortOrder.Asc)
        {
            List<CerealProduct> cereals = cerealService.GetCereal(
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

            return NotFound();
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost(Name = "PostNewCerealProduct")]
        public async Task<IActionResult> PostCereal(
            [FromBody] CerealProduct cereal)
        {
            CerealProduct? newCereal = await cerealService.PostCereal(cereal);

            if (newCereal != null)
            {
                return CreatedAtAction(nameof(PostCereal),
                    new { id = newCereal.Id }, newCereal);
            }
            else
            {
                return BadRequest();
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPut(Name = "UpdateCerealProduct")]
        public async Task<IActionResult> UpdateCereal(
            [FromBody] CerealProduct updatedCereal)
        {
            (CerealProduct? newCereal, bool existed) =
                await cerealService.UpdateCereal(updatedCereal);

            if (newCereal != null)
            {
                if (existed)
                {
                    return Ok(newCereal);
                }
                else
                {
                    return CreatedAtAction(nameof(UpdateCereal),
                    new { id = newCereal.Id }, newCereal);
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpDelete(Name = "DeleteCerealProduct")]
        public async Task<IActionResult> DeleteCereal([FromQuery] int id)
        {
            bool? result = await cerealService.DeleteCereal(id);

            if (result != null)
            {
                return (bool)result ? NoContent() : BadRequest();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
