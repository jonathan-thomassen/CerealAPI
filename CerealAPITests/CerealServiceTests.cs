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
            var awesomeCereal = new CerealProduct(
                10, "Awesome Cereal", 'C', 'H', 20, 20, 10,
                20, 20, 20, 20, 20, 20, 2, 120, 20, 99);
            var lessAwesomeCereal = new CerealProduct(
                11, "Less Awesome Cereal", 'Q', 'C', 20, 20, 10,
                20, 20, 20, 20, 20, 20, 2, 120, 20, 22);
            List<CerealProduct> cerealList =
                [awesomeCereal, lessAwesomeCereal];

            List<CerealProduct> expected =
                [awesomeCereal, lessAwesomeCereal];

            var cerealRepository =
                new Mock<ICerealRepository>(MockBehavior.Strict);
            cerealRepository.Setup(x => x.GetAllCereal()).Returns(cerealList);

            var imageRepository =
                new Mock<IImageRepository>(MockBehavior.Strict);

            CerealService service = GetService(cerealRepository, imageRepository);
            #endregion

            #region Act
            List<CerealProduct> actual = service.GetCereal();
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

            var trueResult = Task.FromResult(true);

            cerealRepository.Setup(
                x => x.PostCereal(awesomeCereal)).Returns(trueResult);

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
        public async void PutCerealProduct()
        {
            #region Arrange
            var awesomeCereal = new CerealProduct(
                10, "Awesome Cereal", 'C', 'H', 20, 20, 10,
                20, 20, 20, 20, 20, 20, 2, 120, 20, 99);
            var awesomerCereal = new CerealProduct(
                10, "Awesome Cereal", 'C', 'H', 20, 20, 10,
                20, 20, 20, 20, 20, 20, 2, 120, 20, 100);

            (CerealProduct?, bool) expected = (awesomerCereal, true);

            var cerealRepository =
                new Mock<ICerealRepository>(MockBehavior.Strict);

            var trueResult = Task.FromResult(true);
            var cerealResult = Task.FromResult((CerealProduct?)awesomeCereal);

            cerealRepository.Setup(
                x => x.UpdateCereal(awesomeCereal, awesomerCereal)
                ).Returns(trueResult);
            cerealRepository.Setup(
                x => x.GetCerealById(awesomeCereal.Id)
                ).Returns(cerealResult);

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
            var imageRepository =
                new Mock<IImageRepository>(MockBehavior.Strict);

            var cerealResult = Task.FromResult((CerealProduct?)cerealThatSucks);
            var imageResult = Task.FromResult((ImageEntry?)image);
            var trueResult = Task.FromResult(true);

            cerealRepository.Setup(
                x => x.GetCerealById(cerealThatSucks.Id))
                .Returns(cerealResult);
            imageRepository.Setup(
                x => x.GetImageEntryByCerealId(cerealThatSucks.Id))
                .Returns(imageResult);
            cerealRepository.Setup(
                x => x.DeleteCereal(cerealThatSucks)).Returns(trueResult);

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
