using CerealAPI.Models;
using CerealAPI.Repositories;
using CerealAPI.Services;
using Moq;

namespace CerealAPITests.Services
{
    public class CerealServiceTests
    {
        private static CerealService GetService(
            Mock<ICerealRepository> cerealRepository,
            Mock<IImageRepository> imageRepository) =>
            new(cerealRepository.Object, imageRepository.Object);

        [Fact]
        public async Task GetCerealProducts()
        {
            #region Arrange
            var awesomeCereal = new CerealProduct(
                10, "Awesome Cereal", 'C', 'H', 20, 20, 10,
                20, 20, 20, 20, 20, 20, 2, 120, 20, 99);
            var lessAwesomeCereal = new CerealProduct(
                11, "Less Awesome Cereal", 'Q', 'C', 20, 20, 10,
                20, 20, 20, 20, 20, 20, 2, 120, 20, 22);
            List<CerealProduct> cerealList = [awesomeCereal, lessAwesomeCereal];

            List<CerealProduct> expected =
                [awesomeCereal, lessAwesomeCereal];

            var cerealRepository =
                new Mock<ICerealRepository>(MockBehavior.Strict);
            cerealRepository.Setup(x => x.GetAllCereal()).ReturnsAsync(cerealList);

            var imageRepository =
                new Mock<IImageRepository>(MockBehavior.Strict);

            CerealService service = GetService(cerealRepository, imageRepository);
            #endregion

            #region Act
            List<CerealProduct> actual = await service.GetCereal();
            #endregion

            #region Assert
            Assert.Equal(expected, actual);
            #endregion
        }

        [Fact]
        public async void PostCerealProduct()
        {
            #region Arrange
            var awesomeCereal = new CerealProduct(
                10, "Awesome Cereal", 'C', 'H', 20, 20, 10,
                20, 20, 20, 20, 20, 20, 2, 120, 20, 99);

            CerealProduct expected = awesomeCereal;

            var cerealRepository =
                new Mock<ICerealRepository>(MockBehavior.Strict);
            cerealRepository.Setup(
                x => x.PostCereal(awesomeCereal)).ReturnsAsync(true);

            var imageRepository =
                new Mock<IImageRepository>(MockBehavior.Strict);

            CerealService service =
                GetService(cerealRepository, imageRepository);
            #endregion

            #region Act
            CerealProduct? actual = await service.PostCereal(awesomeCereal);
            #endregion

            #region Assert
            Assert.Equal(expected, actual);
            #endregion
        }

        [Fact]
        public async void UpdateCerealProduct()
        {
            #region Arrange
            var awesomeCereal = new CerealProduct(
                10, "Awesome Cereal", 'C', 'H', 20, 20, 10,
                20, 20, 20, 20, 20, 20, 2, 120, 20, 99);
            var awesomerCereal = new CerealProduct(
                10, "Awesomer Cereal", 'C', 'H', 20, 20, 10,
                20, 20, 20, 20, 20, 20, 2, 120, 20, 100);

            (CerealProduct?, bool) expected = (awesomerCereal, true);

            var cerealRepository =
                new Mock<ICerealRepository>(MockBehavior.Strict);
            cerealRepository.Setup(
                x => x.UpdateCereal(awesomeCereal, awesomerCereal)
                ).ReturnsAsync(true);
            cerealRepository.Setup(
                x => x.GetCerealById(awesomeCereal.Id)
                ).ReturnsAsync(awesomeCereal);

            var imageRepository =
                new Mock<IImageRepository>(MockBehavior.Strict);

            CerealService service =
                GetService(cerealRepository, imageRepository);
            #endregion

            #region Act
            (CerealProduct?, bool) actual =
                await service.UpdateCereal(awesomerCereal);
            #endregion

            #region Assert
            Assert.Equal(expected, actual);
            #endregion
        }

        [Fact]
        public async void DeleteCerealProduct()
        {
            #region Arrange
            var cerealThatSucks = new CerealProduct(
                10, "Cereal that Sucks", 'C', 'H', 20, 20, 10,
                20, 20, 20, 20, 20, 20, 2, 120, 20, 1);
            var image = new ImageEntry(
                1, 10, "null");

            bool? expected = true;

            var cerealRepository =
                new Mock<ICerealRepository>(MockBehavior.Strict);
            cerealRepository.Setup(
                x => x.GetCerealById(cerealThatSucks.Id))
                .ReturnsAsync(cerealThatSucks);
            cerealRepository.Setup(
                x => x.DeleteCereal(cerealThatSucks)).ReturnsAsync(true);

            var imageRepository =
                new Mock<IImageRepository>(MockBehavior.Strict);            
            imageRepository.Setup(
                x => x.GetImageEntryByCerealId(cerealThatSucks.Id))
                .ReturnsAsync(image);            

            CerealService service =
                GetService(cerealRepository, imageRepository);
            #endregion

            #region Act
            bool? actual =
                await service.DeleteCereal(cerealThatSucks.Id);
            #endregion

            #region Assert
            Assert.Equal(expected, actual);
            #endregion
        }
    }
}
