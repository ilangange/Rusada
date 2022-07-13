using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Rusada.Aviation.API.Controllers.V1;
using Rusada.Aviation.Core.Contracts.Requests;
using Rusada.Aviation.Core.Interface;
using System.Threading.Tasks;

namespace Rusada.Aviation.Tests.Controllers
{
    [TestFixture]
    public class SightingController
    {
        private Mock<ISightingService> _sightingServiceMock;
        private API.Controllers.V1.SightingController _sightingController;
        private SightingModel _sightingModel;

        [SetUp]
        public void Init()
        {
            _sightingServiceMock = new Mock<ISightingService>();
            _sightingController = new API.Controllers.V1.SightingController(_sightingServiceMock.Object);

            _sightingModel = new SightingModel
            {
                Make = "Boeing",
                Model = "777-300ER",
            };
        }

        [Test]
        public async Task SaveSighting_WhenModelIsInValid_ShouldReturnError() {
            // Arrange 
            _sightingServiceMock.Setup(a => a.SaveSightingAsync(It.IsAny<SightingModel>())).ReturnsAsync(() => null);
            _sightingController.ModelState.AddModelError("Make", "Value Required");

            // Act 
            var result = await _sightingController.SaveSighting(_sightingModel);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(400, ((ObjectResult)result).StatusCode);
        }

        [Test]
        public async Task SaveSighting_WhenModelInValid_ShouldReturnSuccess()
        {
            // Arrange 
            _sightingServiceMock.Setup(a => a.SaveSightingAsync(It.IsAny<SightingModel>())).ReturnsAsync(() => _sightingModel);

            // Act 
            var result = await _sightingController.SaveSighting(_sightingModel);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, ((ObjectResult)result).StatusCode);
        }

        [Test]
        public async Task Delete_WhenOperationSuccess_ShouldReturnSuccess()
        {
            // Arrange 
            _sightingServiceMock.Setup(a => a.DeleteAsync(It.IsAny<int>())).ReturnsAsync(() => true);
            _sightingController.ModelState.AddModelError("Make", "Value Required");

            // Act 
            var result = await _sightingController.Delete(It.IsAny<int>());

            // Assert
            var contentResult = result as ObjectResult;
            dynamic returnedObj = contentResult.Value;

            var responseStatus = returnedObj.GetType().GetProperty("success").GetValue(returnedObj, null);

            Assert.IsNotNull(result);
            Assert.AreEqual(true, responseStatus);
        }

        [Test]
        public async Task Delete_WhenOperationFailed_ShouldReturnError()
        {
            // Arrange 
            _sightingServiceMock.Setup(a => a.DeleteAsync(It.IsAny<int>())).ReturnsAsync(() => false);
            _sightingController.ModelState.AddModelError("Make", "Value Required");

            // Act 
            var result = await _sightingController.Delete(It.IsAny<int>());

            // Assert
            var contentResult = result as ObjectResult;
            dynamic returnedObj = contentResult.Value;

            var responseStatus = returnedObj.GetType().GetProperty("success").GetValue(returnedObj, null);

            Assert.IsNotNull(result);
            Assert.AreEqual(true, responseStatus);
        }
    }
}
