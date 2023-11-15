using Moq;
using Service.Carting.Application.DTOs;
using Service.Carting.Application.Services;
using Service.Carting.Domain.Aggregates;
using Service.Carting.Infrastructure.Repositories;

namespace Service.Carting.Component.UnitTests;

[TestClass]
public class CartAppServiceTests
{
    private Mock<ICartRepository> _cartRepositoryMock;
    private CartAppService _cartAppService;

    [TestInitialize]
    public void SetUp()
    {
        _cartRepositoryMock = new Mock<ICartRepository>();

        _cartRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(new Cart() {Id = 1});

        _cartRepositoryMock.Setup(repo => repo.SaveAsync(It.IsAny<Cart>()))
            .Verifiable();

        _cartAppService = new CartAppService(_cartRepositoryMock.Object);
    }

    [TestMethod]
    public void AddItemToCart_ValidItem_ItemAdded()
    {
        // Arrange
        var cartId = 1;
        var itemDto = new CartItemDto
        {
            Id = 1,
            Name = "TestItem",
            Price = 10.0M,
            Quantity = 2,
            ImageAltText = "Test",
            ImageUrl = "test"
        };

        // Act
        _cartAppService.AddItemToCartAsync(cartId, itemDto);

        // Assert
        _cartRepositoryMock.Verify(repo => repo.SaveAsync(It.IsAny<Cart>()), Times.Once);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void AddItemToCart_InvalidPrice_ThrowsException()
    {
        // Arrange
        var cartId = 1;
        var itemDto = new CartItemDto
        {
            Id = 1,
            Name = "TestItem",
            Price = -10.0M,  // Invalid price
            Quantity = 2
        };

        // Act
        _cartAppService.AddItemToCartAsync(cartId, itemDto);
    }


    [TestCleanup]
    public void TearDown()
    {
        _cartRepositoryMock = null;
        _cartAppService = null;
    }
}