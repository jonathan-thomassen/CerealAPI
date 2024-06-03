using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using CerealAPI.Services;
using CerealAPI.Enums;
using CerealAPI.Models;

namespace CerealAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("image")]
    public class ImageController(IImageService imageService) : ControllerBase
    {
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet(Name = "GetImageByCerealId")]
        public async Task<IActionResult> GetImageByCerealId(
            [FromQuery] int cerealId)
        {
            (byte[]? image, ImageType? imageType) =
                await imageService.GetImageByCerealId(cerealId);

            if (image != null)
            {
                if (imageType == ImageType.Jpeg)
                {
                    return File(image, "image/jpeg");
                }
                else if (imageType == ImageType.Png)
                {
                    return File(image, "image/png");
                }
                else
                {
                    throw new SystemException(
                        "Image filetype not recognized by controller: " +
                        $": {imageType}.");
                }
            }
            else
            {
                return NotFound();
            }
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost(Name = "PostImage")]
        public async Task<IActionResult> PostImage(
            [FromQuery] int cerealId,
            [FromForm] IList<IFormFile> fileList)
        {
            ImageEntry? newImage =
                await imageService.PostImage(cerealId, fileList);

            if (newImage != null)
            {
                return CreatedAtAction(nameof(PostImage),
                    new { id = newImage.Id }, newImage);
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
        [HttpPut(Name = "UpdateImage")]
        public async Task<IActionResult> UpdateImage(
            [FromQuery] int cerealId,
            [FromForm] IList<IFormFile> fileList)
        {
            (ImageEntry? newImageEntry, bool existed) =
                await imageService.UpdateImage(cerealId, fileList);

            if (newImageEntry != null)
            {
                if (existed)
                {
                    return Ok(newImageEntry);
                }
                else
                {
                    return CreatedAtAction(nameof(UpdateImage),
                    new { id = newImageEntry.Id }, newImageEntry);
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
        [HttpDelete(Name = "DeleteImageByCerealId")]
        public async Task<IActionResult> DeleteImageByCerealId(
            [FromQuery] int cerealId)
        {
            bool? result = await imageService.DeleteImageByCerealId(cerealId);

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
