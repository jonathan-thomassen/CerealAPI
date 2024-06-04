using CerealAPI.Enums;
using CerealAPI.Models;
using CerealAPI.Repositories;
using CerealAPI.Services;
using Microsoft.AspNetCore.Http;
using Moq;

namespace CerealAPITests.Services
{
    public class ImageServiceTests
    {
        private static ImageService GetService(
            Mock<IImageRepository> imageRepository) =>
            new(imageRepository.Object);

        [Fact]
        public async void GetImageByCerealId()
        {
            #region Arrange
            var imageEntry = new ImageEntry(1, 10, "../../../Images/Test.jpg");

            byte[]? expectedImage = await File.ReadAllBytesAsync(imageEntry.Path);
            ImageType? expectedType = ImageType.Jpeg;
            (byte[]? image, ImageType? imageType) expected =
                (expectedImage, expectedType);

            var imageRepository =
                new Mock<IImageRepository>(MockBehavior.Strict);
            imageRepository
                .Setup(x => x.GetImageEntryByCerealId(imageEntry.CerealId))
                .ReturnsAsync(imageEntry);

            ImageService service = GetService(imageRepository);
            #endregion

            #region Act
            (byte[]? image, ImageType? imageType) actual =
                await service.GetImageByCerealId(imageEntry.CerealId);
            #endregion

            #region Assert
            Assert.Equal(expected.image, actual.image);
            Assert.Equal(expected.imageType, actual.imageType);
            #endregion
        }

        [Fact]
        public async void PostImage()
        {
            #region Arrange
            var imageEntry = new ImageEntry(0, 10, "./Images/10.jpg");

            ImageEntry expected = imageEntry;

            var imageRepository =
                new Mock<IImageRepository>(MockBehavior.Strict);
            imageRepository.Setup(
                x => x.PostImageEntry(imageEntry)).ReturnsAsync(true);

            string filename = imageEntry.Path.Split("/").Last();
            string name = filename.Split(".")[0];

            FileStream stream = File.OpenRead("../../../Images/Test.jpg");

            FormFile formFile =
                new FormFile(stream, 0, stream.Length, name, filename)
                {
                    Headers = new HeaderDictionary(),
                    ContentType = "image/jpeg"
                };

            ImageService service = GetService(imageRepository);
            #endregion

            #region Act
            ImageEntry? actual =
                await service.PostImage(imageEntry.CerealId, [formFile]);
            #endregion

            #region Assert
            Assert.Equal(expected, actual);
            #endregion

            #region Cleanup
            if (File.Exists(imageEntry.Path))
            {
                File.Delete(imageEntry.Path);
            }
            #endregion
        }

        [Fact]
        public async void UpdateImage()
        {
            #region Arrange
            var oldImageEntry = new ImageEntry(0, 10, "./Images/10.jpg");
            var newImageEntry = new ImageEntry(0, 10, "./Images/10.png");

            (ImageEntry?, bool) expected = (newImageEntry, true);

            var imageRepository =
                new Mock<IImageRepository>(MockBehavior.Strict);
            imageRepository.Setup(
                x => x.GetImageEntryByCerealId(oldImageEntry.CerealId)
                ).ReturnsAsync(oldImageEntry);
            imageRepository.Setup(
                x => x.UpdateImageEntry(oldImageEntry, newImageEntry)
                ).ReturnsAsync(true);

            FileStream stream = File.OpenRead("../../../Images/TestUpd.png");
            string filename = stream.Name.Split("\\").Last();
            string name = filename.Split(".")[0];

            FormFile formFile =
                new FormFile(stream, 0, stream.Length, name, filename)
                {
                    Headers = new HeaderDictionary(),
                    ContentType = "image/png"
                };

            ImageService service = GetService(imageRepository);
            #endregion

            #region Act
            (ImageEntry?, bool) actual =
                await service.UpdateImage(oldImageEntry.CerealId, [formFile]);
            #endregion

            #region Assert
            Assert.Equal(expected, actual);
            #endregion

            #region Cleanup
            if (File.Exists(newImageEntry.Path))
            {
                File.Delete(newImageEntry.Path);
            }
            #endregion
        }

        [Fact]
        public async void DeleteImage()
        {
            #region Arrange
            var imageEntry = new ImageEntry(1, 10, "null");

            bool? expected = true;

            var imageRepository =
                new Mock<IImageRepository>(MockBehavior.Strict);
            imageRepository.Setup(
                x => x.GetImageEntryByCerealId(imageEntry.CerealId))
                .ReturnsAsync(imageEntry);
            imageRepository.Setup(
                x => x.DeleteImageEntry(imageEntry))
                .ReturnsAsync(true);

            ImageService service =
                GetService(imageRepository);
            #endregion

            #region Act
            bool? actual =
                await service.DeleteImageByCerealId(imageEntry.CerealId);
            #endregion

            #region Assert
            Assert.Equal(expected, actual);
            #endregion
        }
    }
}
