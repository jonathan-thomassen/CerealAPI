
using CerealAPI.Models;
using CerealAPI.Repositories;

namespace CerealAPITests.Repositories
{
    public class ImageRepositoryTests
    {
        private static FakeImageRepository GetImageRepository() => new();

        [Fact]
        public async Task AddImageEntry()
        {
            #region Arrange
            var imageEntry = new ImageEntry(10, 10, "null");
            FakeImageRepository repository = GetImageRepository();
            #endregion

            #region Act
            bool actual = await repository.PostImageEntry(imageEntry);
            #endregion

            #region Assert
            Assert.True(actual);
            #endregion
        }

        [Fact]
        public async Task GetImageEntryById()
        {
            #region Arrange
            await AddImageEntry();
            var expected = new ImageEntry(1, 10, "null");
            FakeImageRepository repository = GetImageRepository();
            #endregion

            #region Act
            ImageEntry? actual = await repository.GetImageEntryById(expected.Id);
            #endregion

            #region Assert
            Assert.Equal(expected, actual);
            #endregion
        }

        [Fact]
        public async void GetImageEntryById_DoesNotExist()
        {
            #region Arrange
            int erronousId = 1111;
            FakeImageRepository repository = GetImageRepository();
            #endregion

            #region Act
            SystemException ex = await Assert.ThrowsAsync<SystemException>(
                () => repository.GetImageEntryById(erronousId));
            #endregion

            #region Assert
            Assert.Equal($"Image entry does not exist with id {erronousId}.",
                ex.Message);
            #endregion
        }
    }
}
