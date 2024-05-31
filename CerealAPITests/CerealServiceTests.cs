using CerealAPI.Models;
using CerealAPI.Repositories;
using CerealAPI.Services;
using Moq;

namespace CerealAPITests
{
    public class CerealServiceTests
    {
        private static CerealService GetService(
            Mock<ICerealRepository> cerealRepository,
            Mock<IImageRepository> imageRepository) =>
            new(cerealRepository.Object, imageRepository.Object);

        [Fact]
        public void GetCerealProducts()
        {
            #region Arrange
            var awesomeCereal = new CerealProduct(10, "skajdfkj", 'C', 'H', 20, 20, 10, 20, 20, 20, 20, 20, 20, 2, 120, 20, 99);
            List<CerealProduct> cerealList = new() { awesomeCereal };

            List<CerealProduct> expected = new() { awesomeCereal };

            var cerealRepository = new Mock<ICerealRepository>(MockBehavior.Strict);
            cerealRepository.Setup(x => x.GetAllCereal()).Returns(cerealList);

            var imageRepository = new Mock<IImageRepository>(MockBehavior.Strict);

            var service = GetService(cerealRepository, imageRepository);
            #endregion

            #region Act
            var actual = service.GetCereal();
            #endregion

            #region Assert
            Assert.Equal(expected, actual);
            #endregion
        }
    }
}
