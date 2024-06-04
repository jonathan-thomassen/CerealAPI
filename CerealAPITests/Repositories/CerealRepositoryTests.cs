
using CerealAPI.Models;
using CerealAPI.Repositories;

namespace CerealAPITests.Repositories
{
    public class CerealRepositoryTests
    {
        private static FakeCerealRepository GetCerealRepository() => new();

        [Fact]
        public async Task AddCerealProduct()
        {
            #region Arrange
            var cereal = new CerealProduct(
                10,
                "null",
                'A',
                'C',
                10,
                10,
                10,
                10,
                10.0,
                10.0,
                10,
                10,
                10,
                10,
                10.0,
                10.0,
                10.0);
            FakeCerealRepository repository = GetCerealRepository();
            #endregion

            #region Act
            bool actual = await repository.PostCereal(cereal);
            #endregion

            #region Assert
            Assert.True(actual);
            #endregion
        }

        [Fact]
        public async Task GetCerealProduct()
        {
            #region Arrange
            await AddCerealProduct();
            var expected = new CerealProduct(
                1,
                "null",
                'A',
                'C',
                10,
                10,
                10,
                10,
                10.0,
                10.0,
                10,
                10,
                10,
                10,
                10.0,
                10.0,
                10.0);
            FakeCerealRepository repository = GetCerealRepository();
            #endregion

            #region Act
            CerealProduct? actual = await repository.GetCerealById(expected.Id);
            #endregion

            #region Assert
            Assert.Equal(expected, actual);
            #endregion
        }

        [Fact]
        public async void GetCerealProduct_DoesNotExist()
        {
            #region Arrange
            int erronousId = 1111;
            FakeCerealRepository repository = GetCerealRepository();
            #endregion

            #region Act
            SystemException ex = await Assert.ThrowsAsync<SystemException>(
                () => repository.GetCerealById(erronousId));
            #endregion

            #region Assert
            Assert.Equal($"Cereal does not exist with id {erronousId}.",
                ex.Message);
            #endregion
        }
    }
}
