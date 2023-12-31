using Moq;
using Service.Catalog.Application.DTOs;
using Service.Catalog.Application.Services;
using Service.Catalog.Domain.Aggregates;
using Service.Catalog.Infrastructure.Repositories;

namespace Service.Catalog.Component.UnitTests;

[TestClass]
public class CartAppServiceTests
{
    private Mock<ICartRepository> _cartRepositoryMock;
    private CartAppService _cartAppService;

    [TestInitialize]
    public void SetUp()
    {
        _cartRepositoryMock = new Mock<ICartRepository>();

        _cartRepositoryMock.Setup(repo => repo.GetById(It.IsAny<int>()))
            .Returns(new Cart() {Id = 1});

        _cartRepositoryMock.Setup(repo => repo.Save(It.IsAny<Cart>()))
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
        _cartAppService.AddItemToCart(cartId, itemDto);

        // Assert
        _cartRepositoryMock.Verify(repo => repo.Save(It.IsAny<Cart>()), Times.Once);
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
        _cartAppService.AddItemToCart(cartId, itemDto);
    }


    [TestCleanup]
    public void TearDown()
    {
        _cartRepositoryMock = null;
        _cartAppService = null;
    }
}