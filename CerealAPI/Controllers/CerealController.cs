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
        public ActionResult<List<CerealProduct>> GetCereal(
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
        public async Task<ActionResult<CerealProduct>> PostCereal(
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
        public async Task<ActionResult<CerealProduct>> UpdateCereal(
            [FromBody] CerealProduct oldCereal)
        {
            (CerealProduct? newCereal, bool existed) =
                await cerealService.UpdateCereal(oldCereal);

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
