
using CerealAPI.Models;
using CerealAPI.Repositories;

namespace PizzaPlace.Test.Repositories;

public class CerealRepositoryTests
{
    private static ICerealRepository GetCerealRepository() => new FakeCerealRepository();

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
        ICerealRepository repository = GetCerealRepository();
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
        var repository = GetCerealRepository();
        #endregion

        #region Act
        var actual = await repository.GetCerealById(expected.Id);
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
        var repository = GetCerealRepository();
        #endregion

        #region Act
        SystemException ex = await Assert.ThrowsAsync<SystemException>(
            () => repository.GetCerealById(erronousId));
        #endregion

        #region Assert
        Assert.Equal($"Cereal does not exists with id {erronousId}.",
            ex.Message);
        #endregion
    }
}
