using AutoMapper;
using Moq;
using NUnit.Framework;
using Rusada.Aviation.Core.Entities;
using Rusada.Aviation.Core.Interface;
using Rusada.Aviation.Core.Mappings;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rusada.Aviation.Tests.Services
{
    [TestFixture]
    public class SightingService
    {
        private Mock<IGenericRepository<Sighting>> _sightingRepositoryMock;
        private IMapper _mapper;
        private Core.Service.SightingService _sightingService;
        private IList<Sighting> _sightingsList;

        [SetUp]
        public void Init()
        {
            _sightingRepositoryMock = new Mock<IGenericRepository<Sighting>>();
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            _mapper = config.CreateMapper();

            _sightingService = new Core.Service.SightingService(_sightingRepositoryMock.Object, _mapper);

            _sightingsList = new List<Sighting> {
                new Sighting{
                    Id = 1
                }
            };
        }

        [Test]
        public async Task DeleteAsync_WhenSightingNotFound_ShouldReturnFalse()
        {
            // Arrange 
            _sightingRepositoryMock.Setup(a => a.GetAll()).Returns(() => _sightingsList.AsQueryable());

            // Act 
            var result = await _sightingService.DeleteAsync(It.IsAny<int>());

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(false, result);
        }

        [Test]
        public async Task DeleteAsync_WhenOperationSuccess_ShouldReturnTrue()
        {
            // Arrange 
            _sightingRepositoryMock.Setup(a => a.GetAll()).Returns(() => _sightingsList.AsQueryable());
            _sightingRepositoryMock.Setup(a => a.Update(It.IsAny<Sighting>()));
            _sightingRepositoryMock.Setup(a => a.Save());

            // Act 
            var result = await _sightingService.DeleteAsync(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(true, result);
        }

        [Test]
        public async Task GetByIdAsync_WhenSightingNotFound_ShouldReturnNull()
        {
            // Arrange 
            _sightingRepositoryMock.Setup(a => a.GetAll()).Returns(() => _sightingsList.AsQueryable());

            // Act 
            var result = await _sightingService.GetByIdAsync(It.IsAny<int>());

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task GetByIdAsync_WhenSightingIsFound_ShouldReturnObject()
        {
            // Arrange 
            _sightingRepositoryMock.Setup(a => a.GetAll()).Returns(() => _sightingsList.AsQueryable());

            // Act 
            var result = await _sightingService.GetByIdAsync(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1,result.Id);
        }
    }
}
