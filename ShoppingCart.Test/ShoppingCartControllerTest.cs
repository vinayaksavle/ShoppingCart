using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Controllers;
using ShoppingCart.Database;
using ShoppingCart.Model;
using ShoppingCart.Repo;
using ShoppingCart.Repo.IRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ShoppingCart.Test
{
    public class ShoppingCartControllerTest
    {
        private readonly ShoppingCartController _controller;
        private readonly IShoppingCartRepo _repo;

        public ShoppingCartControllerTest()
        {
            _repo = new ShoppingCartRepoFake();
            _controller = new ShoppingCartController(_repo);

            //_repo = new ShoppingCartRepo();
            //Uncomment above line when we connect to database also instead of creating fake service we can use mocking frameworks
        }

        [Fact]
        public void Get_WhenCalled_ReturnsListResult()
        {
            //Act
            var result = _repo.GetAllItems();

            //Assert
            Assert.IsType<List<ShoppingVM>>(result);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsAllItems()
        {
            //Act
            var result = _repo.GetAllItems();

            //Assert
            Assert.Equal(3, result.Count());
        }

        [Fact]
        public void GetById_UnknownGuidPassed_ReturnsNotFoundResult()
        {
            //Act
            var result = _controller.Get(Guid.NewGuid());

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void GetById_CheckExistingGuid_ReturnsOkResult()
        {
            // Arrange
            var testGuid = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200");

            // Act
            var okResult = _repo.GetById(testGuid);

            // Assert
            Assert.IsType<ShoppingVM>(okResult as ShoppingVM);
        }

        [Fact]
        public void GetById_ExistingGuidPassed_ReturnsExistingItem()
        {
            // Arrange
            var testGuid = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200");

            // Act
            var result = _repo.GetById(testGuid);

            // Assert
            Assert.Equal(testGuid, result.Id);
        }

        [Fact]
        public void Add_InvalidObjectPassed_ReturnsBadRequest()
        {
            // Arrange
            var nameMissingItem = new ShoppingVM()
            {
                Manufacturer = "Guinness",
                Price = 12.00M
            };

            _controller.ModelState.AddModelError("Name", "Required");

            // Act
            var badResponse = _controller.Post(nameMissingItem);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }

        [Fact]
        public void Add_ValidObjectPassed_ReturnsCreatedResponse()
        {
            // Arrange
            ShoppingVM testItem = new ShoppingVM()
            {
                Name = "Guinness Original 6 Pack",
                Manufacturer = "Guinness",
                Price = 12.00M
            };

            // Act
            var createdResponse = _controller.Post(testItem);

            // Assert
            Assert.IsType<CreatedAtActionResult>(createdResponse);
        }

        [Fact]
        public void Add_ValidObjectPassed_ReturnedResponseHasCreatedItem()
        {
            // Arrange
            var testItem = new ShoppingVM()
            {
                Name = "Guinness Original 6 Pack",
                Manufacturer = "Guinness",
                Price = 12.00M
            };

            // Act
            var createdResponse = _controller.Post(testItem) as CreatedAtActionResult;
            var item = createdResponse.Value as ShoppingVM;

            // Assert
            Assert.IsType<ShoppingVM>(item);
            Assert.Equal("Guinness Original 6 Pack", item.Name);
        }

        [Fact]
        public void Remove_NotExistingGuidPassed_ReturnsNotFoundResponse()
        {
            // Arrange
            var notExistingGuid = Guid.NewGuid();

            // Act
            var badResponse = _controller.Remove(notExistingGuid);

            // Assert
            Assert.IsType<NotFoundResult>(badResponse);
        }

        [Fact]
        public void Remove_ExistingGuidPassed_ReturnsNoContentResult()
        {
            // Arrange
            var existingGuid = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200");

            // Act
            var noContentResponse = _controller.Remove(existingGuid);

            // Assert
            Assert.IsType<NoContentResult>(noContentResponse);
        }

        //[Fact]
        //public void Remove_ExistingGuidPassed_RemovesOneItem()
        //{
        //    // Arrange
        //    var existingGuid = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200");

        //    // Act
        //    _repo.Remove(existingGuid);

        //    // Assert
        //    Assert.Equal(2, _repo.GetAllItems().Count());
        //}
    }
}
