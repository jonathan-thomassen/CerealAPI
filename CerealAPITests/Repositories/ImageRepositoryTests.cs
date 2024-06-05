using CerealAPI.Models;
using CerealAPI.Repositories;

namespace CerealAPITests.Repositories
{
    public class ImageRepositoryTests
    {
        private static ImageRepository GetImageRepository() => new();

        [Fact]
        public async Task AddImageEntry()
        {
            #region Arrange
            var imageEntry = new ImageEntry(0, 7204, "null");
            ImageRepository repository = GetImageRepository();
            #endregion

            #region Act
            bool actual = await repository.PostImageEntry(imageEntry);
            #endregion

            #region Assert
            Assert.True(actual);
            #endregion

            #region Cleanup
            await repository.DeleteImageEntry(imageEntry);
            #endregion
        }

        [Fact]
        public async Task GetImageEntryById()
        {
            #region Arrange
            var imageEntry = new ImageEntry(0, 7204, "null");
            ImageRepository repository = GetImageRepository();
            await repository.PostImageEntry(imageEntry);
            #endregion

            #region Act
            ImageEntry? actual =
                await repository.GetImageEntryById(imageEntry.Id);
            #endregion

            #region Assert
            Assert.Equal(imageEntry, actual);
            #endregion

            #region Cleanup
            await repository.DeleteImageEntry(imageEntry);
            #endregion
        }

        [Fact]
        public async void GetImageEntryById_DoesNotExist()
        {
            #region Arrange
            int erronousId = 1111;
            ImageRepository repository = GetImageRepository();
            #endregion

            #region Act
            ImageEntry? actual = await repository.GetImageEntryById(erronousId);
            #endregion

            #region Assert
            Assert.Null(actual);
            #endregion
        }
    }
}
