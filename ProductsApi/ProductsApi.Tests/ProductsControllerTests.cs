using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using ProductsApi.Models;

namespace ProductsApi.Tests
{
    
    /// <summary>
    /// 
    /// </summary>
    public class ProductsControllerTests
    {
        private Product CreateProduct(int id)
        {
            return new Product()
            {
                ProductId = id,
                ModifiedDate = DateTime.Now,
                Class = string.Empty,
                Name = string.Empty,
                ProductModel = new ProductModel() { },
                ProductSubcategory = new ProductSubcategory() { ProductCategory = new ProductCategory() },
                SizeUnitMeasureCodeNavigation = new UnitMeasure(),
                WeightUnitMeasureCodeNavigation = new UnitMeasure()
            };
        }

        public async Task Get_ReturnsNoResult_WhenIdIsNotFoundAsync()
        {
            var mockDbContext = new Mock<AwDbContext>();
            mockDbContext.Setup(context => context.Product.FindAsync(123)).ReturnsAsync((Product)null);

            var controller = new ProductsApi.Controllers.ProductsController(mockDbContext.Object);
            var result = await controller.GetProduct(123);
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }

        [Test]
        public async Task Post_ReturnsCreateResult_WhenSuccess()
        {
            var testProduct = new Product();

            var mockDbContext = new Mock<AwDbContext>();
            mockDbContext.Setup(context => context.Product.Add(testProduct));
            var controller = new ProductsApi.Controllers.ProductsController(mockDbContext.Object);

            var result = await controller.PostProduct(testProduct);

            Assert.IsInstanceOf<CreatedAtActionResult>(result.Result);
        }

        [Test]
        public async System.Threading.Tasks.Task Delete_ReturnsNoResult_WhenIdIsNotFoundAsync()
        {
            var mockDbContext = new Mock<AwDbContext>();
            mockDbContext.Setup(context => context.Product.FindAsync(123)).ReturnsAsync((Product)null);

            var controller = new ProductsApi.Controllers.ProductsController(mockDbContext.Object);
            var result = await controller.DeleteProduct(123);
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }

        [Test]
        public void Put_ReturnsBadRequest_WhenInconsistentInputProductId()
        {
            var mockDbContext = new Mock<AwDbContext>();
            var controller = new ProductsApi.Controllers.ProductsController(mockDbContext.Object);
            var result = controller.PutProduct(123, new Product() { ProductId = 321 });
            Assert.IsInstanceOf<BadRequestResult>(result.Result);
        }
        /*
        [Test]
        public async System.Threading.Tasks.Task Put_ReturnsNoContent_WhenSuccess()
        {
            var tempProduct = new Product() { ProductId = 123 };
            var mockDbContext = new Mock<AwDbContext>();
            //mockDbContext.Setup(context => context.Entry(tempProduct)).Returns(new Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<Product>(null));

            var controller = new ProductsApi.Controllers.ProductsController(mockDbContext.Object);

            var result = await controller.PutProduct(123, tempProduct);

            Assert.IsInstanceOf<NoContentResult>(result);
        }
        */

    }
}
