using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using CerealAPI.Services;
using CerealAPI.Enums;
using CerealAPI.Models;

namespace CerealAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ImageController(IImageService imageService) : ControllerBase
    {
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet(Name = "GetImageByCerealId")]
        public async Task<ActionResult<byte[]>> GetImageByCerealId(
            [FromQuery] int cerealId)
        {
            var tuple = await imageService.GetImageByCerealId(cerealId);

            if (tuple.image != null)
            {
                if (tuple.imageType == ImageType.Jpeg)
                {
                    return File(tuple.image, "image/jpeg");
                }
                else if (tuple.imageType == ImageType.Png)
                {
                    return File(tuple.image, "image/png");
                }
                else
                {
                    throw new SystemException(
                        "Image filetype not recognized by controller: " +
                        $": {tuple.imageType}.");
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
        public async Task<ActionResult<ImageEntry>> PostImage(
            [FromQuery] int cerealId,
            [FromForm] IList<IFormFile> fileList)
        {
            var newImage = await imageService.PostImage(cerealId, fileList);

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
    }
}
