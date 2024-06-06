using CerealAPI.Controllers;
using CerealAPI.Enums;
using CerealAPI.Models;
using CerealAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CerealAPITests.Controllers
{
    public class ImageControllerTests
    {
        private static ImageController GetController(
            Mock<IImageService> imageService) =>
            new(imageService.Object);

        [Fact]
        public async Task GetImageByCerealId()
        {
            #region Arrange
            byte[] image = await File.ReadAllBytesAsync("../../../Images/Test.jpg");

            var imageService = new Mock<IImageService>(MockBehavior.Strict);
            imageService.Setup(x => x.GetImageByCerealId(10)).ReturnsAsync((image, ImageType.Jpeg));

            ImageController controller = GetController(imageService);
            #endregion

            #region Act
            IActionResult actual = await controller.GetImageByCerealId(10);
            #endregion

            #region Assert
            Assert.IsType<FileContentResult>(actual);
            imageService.VerifyAll();
            byte[]? returnedImage =
                (actual as FileContentResult)?.FileContents as byte[];
            string? returnedImageType =
                (actual as FileContentResult)?.ContentType as string;

            Assert.Equal(image, returnedImage);
            Assert.Equal("image/jpeg", returnedImageType);
            #endregion
        }

        [Fact]
        public async Task PostImage()
        {
            #region Arrange
            var imageEntry = new ImageEntry(0, 10, "./Images/Test.jpg");

            string filename = imageEntry.Path.Split("/").Last();
            string name = filename.Split(".")[0];

            FileStream stream = File.OpenRead("../../../Images/Test.jpg");

            FormFile formFile =
                new(stream, 0, stream.Length, name, filename)
                {
                    Headers = new HeaderDictionary(),
                    ContentType = "image/jpeg"
                };

            IList<IFormFile> formFiles = [formFile];

            var imageService = new Mock<IImageService>(MockBehavior.Strict);
            imageService.Setup(x => x.PostImage(imageEntry.CerealId, formFiles))
                .ReturnsAsync(imageEntry);

            ImageController controller = GetController(imageService);
            #endregion

            #region Act
            IActionResult actual = await controller.PostImage(imageEntry.CerealId, formFiles);
            #endregion

            #region Assert
            Assert.IsType<CreatedAtActionResult>(actual);
            imageService.VerifyAll();
            var returnedImageEntry =
                (actual as CreatedAtActionResult)?.Value as ImageEntry;
            Assert.Equal(imageEntry, returnedImageEntry);
            #endregion
        }

        [Fact]
        public async Task UpdateImage()
        {
            #region Arrange
            var imageEntry = new ImageEntry(0, 10, "./Images/Test.jpg");
            var newImageEntry = new ImageEntry(0, 10, "./Images/TestUpd.png");

            string filename = imageEntry.Path.Split("/").Last();
            string name = filename.Split(".")[0];

            FileStream stream = File.OpenRead("../../../Images/TestUpd.png");

            FormFile formFile =
                new(stream, 0, stream.Length, name, filename)
                {
                    Headers = new HeaderDictionary(),
                    ContentType = "image/png"
                };

            IList<IFormFile> formFiles = [formFile];

            var imageService = new Mock<IImageService>(MockBehavior.Strict);
            imageService.Setup(x => x.UpdateImage(imageEntry.CerealId, formFiles))
                .ReturnsAsync((newImageEntry, true));

            ImageController controller = GetController(imageService);
            #endregion

            #region Act
            IActionResult actual =
                await controller.UpdateImage(imageEntry.CerealId, formFiles);
            #endregion

            #region Assert
            Assert.IsType<OkObjectResult>(actual);
            imageService.VerifyAll();
            var updatedImage =
                (actual as OkObjectResult)?.Value as ImageEntry;
            Assert.Equal(newImageEntry, updatedImage);
            #endregion
        }

        [Fact]
        public async Task DeleteImage()
        {
            #region Arrange
            var imageEntry = new ImageEntry(0, 10, "null");

            var imageService = new Mock<IImageService>(MockBehavior.Strict);
            imageService.Setup(x => x.DeleteImageByCerealId(imageEntry.CerealId))
                .ReturnsAsync(true);

            ImageController controller = GetController(imageService);
            #endregion

            #region Act
            IActionResult actual =
                await controller.DeleteImageByCerealId(imageEntry.CerealId);
            #endregion

            #region Assert
            Assert.IsType<NoContentResult>(actual);
            imageService.VerifyAll();
            #endregion
        }
    }
}
