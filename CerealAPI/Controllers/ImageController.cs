using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using CerealAPI.Services;
using static System.Net.Mime.MediaTypeNames;
using CerealAPI.Enums;
using CerealAPI.Models;
using System;

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
        [HttpGet(Name = "PostImage")]
        public async Task<ActionResult<ImageEntry>> PostImage(
            [FromQuery] int cerealId,
            [FromHeader(Name = "Content-Type")] string? contentType,
            [FromBody] byte[] image)
        {
            ImageType imageType;
            if (contentType != null)
            {
                if (contentType == "image/png")
                {
                    imageType = ImageType.Png;
                }
                else if (contentType == "image/jpeg")
                {
                    imageType = ImageType.Jpeg;
                }
                else
                {
                    return BadRequest();
                }
            } else
            {
                return BadRequest();
            }

            var newImage = await imageService.PostImage(
                cerealId, imageType, image);

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
