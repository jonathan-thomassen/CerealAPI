
using CerealAPI.Contexts;
using CerealAPI.Models;
using CerealAPI.Repositories;
using CerealAPI.Services;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace CerealAPITests.Repositories
{
    public class CerealRepositoryTests
    {
        private static CerealRepository GetCerealRepository() => new();

        [Fact]
        public async Task AddCerealProduct()
        {
            #region Arrange
            var cereal = new CerealProduct(
                0,
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
            CerealRepository repository = GetCerealRepository();
            #endregion

            #region Act
            bool actual = await repository.PostCereal(cereal);
            #endregion

            #region Assert
            Assert.True(actual);
            #endregion

            #region Cleanup
            await repository.DeleteCereal(cereal);
            #endregion
        }

        [Fact]
        public async Task GetCerealProduct()
        {
            #region Arrange
            await AddCerealProduct();
            var cereal = new CerealProduct(
                0,
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
            CerealRepository repository = GetCerealRepository();
            await repository.PostCereal(cereal);
            #endregion

            #region Act
            CerealProduct? actual = await repository.GetCerealById(cereal.Id);
            #endregion

            #region Assert
            Assert.Equal(cereal, actual);
            #endregion

            #region Cleanup
            await repository.DeleteCereal(cereal);
            #endregion
        }

        [Fact]
        public async Task GetCerealProduct_DoesNotExist()
        {
            #region Arrange
            int erronousId = 1111;
            CerealRepository repository = GetCerealRepository();
            #endregion

            #region Act

            CerealProduct? actual = await repository.GetCerealById(erronousId);
            #endregion

            #region Assert
            Assert.Null(actual);
            #endregion
        }
    }
}
