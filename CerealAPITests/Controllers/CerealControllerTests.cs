using CerealAPI.Controllers;
using CerealAPI.Enums;
using CerealAPI.Models;
using CerealAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CerealAPITests.Controllers
{
    public class CerealControllerTests
    {
        private static CerealController GetController(
            Mock<ICerealService> cerealService) =>
            new(cerealService.Object);

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
            List<CerealProduct> cerealList = [awesomeCereal, lessAwesomeCereal];

            var cerealService = new Mock<ICerealService>(MockBehavior.Strict);
            cerealService.Setup(x => x.GetCereal(
                null, null, null, null, null, null, null, null, null, null,
                null, null, null, null, null, null, null, null, null, null,
                null, null, null, null, null, null, null, null, null, null,
                null, null, null, null, null, null, null, null, null, null,
                null, null, null, null, null, null, null, null, null, null,
                null, null, null, null, SortOrder.Asc)).Returns(cerealList);

            CerealController controller = GetController(cerealService);
            #endregion

            #region Act
            IActionResult actual = controller.GetCereal();
            #endregion

            #region Assert
            Assert.IsType<OkObjectResult>(actual);
            cerealService.VerifyAll();
            var returnedCerealList =
                (actual as OkObjectResult)?.Value as List<CerealProduct>;
            Assert.Equal(cerealList, returnedCerealList);
            #endregion
        }

        [Fact]
        public async void PostCerealProduct()
        {
            #region Arrange
            var awesomeCereal = new CerealProduct(
                10, "Awesome Cereal", 'C', 'H', 20, 20, 10,
                20, 20, 20, 20, 20, 20, 2, 120, 20, 99);

            var cerealService = new Mock<ICerealService>(MockBehavior.Strict);
            cerealService.Setup(x => x.PostCereal(awesomeCereal))
                .ReturnsAsync(awesomeCereal);

            CerealController controller = GetController(cerealService);
            #endregion

            #region Act
            IActionResult actual = await controller.PostCereal(awesomeCereal);
            #endregion

            #region Assert
            Assert.IsType<CreatedAtActionResult>(actual);
            cerealService.VerifyAll();
            var returnedCereal =
                (actual as CreatedAtActionResult)?.Value as CerealProduct;
            Assert.Equal(awesomeCereal, returnedCereal);
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

            var cerealService = new Mock<ICerealService>(MockBehavior.Strict);
            cerealService.Setup(x => x.UpdateCereal(awesomerCereal))
                .ReturnsAsync((awesomerCereal, true));

            CerealController controller = GetController(cerealService);
            #endregion

            #region Act
            IActionResult actual =
                await controller.UpdateCereal(awesomerCereal);
            #endregion

            #region Assert
            Assert.IsType<OkObjectResult>(actual);
            cerealService.VerifyAll();
            var updatedCereal =
                (actual as OkObjectResult)?.Value as CerealProduct;
            Assert.Equal(awesomerCereal, updatedCereal);
            #endregion
        }

        [Fact]
        public async void DeleteCerealProduct()
        {
            #region Arrange
            var cerealThatSucks = new CerealProduct(
                10, "Cereal that Sucks", 'C', 'H', 20, 20, 10,
                20, 20, 20, 20, 20, 20, 2, 120, 20, 1);

            var cerealService = new Mock<ICerealService>(MockBehavior.Strict);
            cerealService.Setup(x => x.DeleteCereal(cerealThatSucks.Id))
                .ReturnsAsync(true);

            CerealController controller = GetController(cerealService);
            #endregion

            #region Act
            IActionResult actual =
                await controller.DeleteCereal(cerealThatSucks.Id);
            #endregion

            #region Assert
            Assert.IsType<NoContentResult>(actual);
            cerealService.VerifyAll();
            #endregion
        }
    }
}
