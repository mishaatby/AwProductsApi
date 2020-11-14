using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using ProductsApi.Models;

namespace ProductsApi.Tests
{
    
    public class ProductsControllerTests
    {
        [Test]
        public async System.Threading.Tasks.Task Delete_ReturnsNoResult_WhenIdIsOmittedAsync() {
            var mockDbContext = new Mock<AwDbContext>();
            mockDbContext.Setup(context => context.Product.FindAsync(123)).ReturnsAsync((Product)null);

            var controller = new ProductsApi.Controllers.ProductsController(mockDbContext.Object);
            var result = await controller.DeleteProduct(123);
            Assert.IsInstanceOf(typeof(Microsoft.AspNetCore.Mvc.NotFoundResult), result.Result);
        }

    }
}
