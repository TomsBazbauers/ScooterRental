using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ScooterRental.Controllers;
using ScooterRental.Core.Models;
using ScooterRental.Core.Services;
using ScooterRental.Core.Validations;
using ScooterRental.Models;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ScooterRental.Tests.AdminControllerTests
{
    public class AdminControllerTests : TestDatabase
    {
        private readonly AdminController _sut;
        private readonly Mock<IScooterService> _scooterServiceMock;
        private readonly Mock<IReportService> _reportServiceMock;
        private readonly Mock<IMapper> _autoMapperMock;
        private readonly IEnumerable<IScooterValidator> _scooterValidators;

        public AdminControllerTests()
        {
            _scooterServiceMock = new Mock<IScooterService>();
            _reportServiceMock = new Mock<IReportService>();
            _autoMapperMock = new Mock<IMapper>();
            _scooterValidators = new List<IScooterValidator>()
            {
                new ScooterPriceValidator(),
                new ScooterStatusValidator()
            };

            _sut = new AdminController(_scooterServiceMock.Object,
                _reportServiceMock.Object, _scooterValidators, _autoMapperMock.Object);
        }

        [Fact]
        public void AddScooter_InputValid_ScooterAddedCorrectly()
        {
            // Arrange
            var testRequest = new ScooterRequest(0.25m, false);
            var testScooter = new Scooter(testRequest.PricePerMinute, testRequest.IsRented);

            _autoMapperMock
               .Setup(m => m.Map<Scooter>(testRequest))
               .Returns(testScooter);

            _scooterServiceMock.Setup(m => m.CreateScooter(testScooter)).Returns(new ServiceResult(true));

            _autoMapperMock
                .Setup(m => m.Map<ScooterRequest>(testScooter))
                .Returns(new ScooterRequest(testScooter.PricePerMinute, testScooter.IsRented));

            // Act
            var actionResult = _sut.AddScooter(testRequest) as ObjectResult;

            // Assert
            actionResult.Should().NotBeNull();
            actionResult.Should().BeOfType<CreatedResult>();

            // Assert
            var actionValue = actionResult.Value as ScooterRequest;
            actionValue.PricePerMinute.Should().Be(testRequest.PricePerMinute);
            actionValue.IsRented.Should().Be(testRequest.IsRented);
        }

        [Theory]
        [InlineData(-0.25)]
        [InlineData(-1.25)]
        [InlineData(-99.25)]
        public void AddScooter_InputInvalidPricePerMinute_ReturnsBadRequest(decimal testPricePerMinute)
        {
            // Arrange
            var testRequest = new ScooterRequest(testPricePerMinute, false);
            var testScooter = new Scooter(testRequest.PricePerMinute, testRequest.IsRented);
            var currentCountInDb = _dbContext.Scooters.Count();

            _autoMapperMock
               .Setup(m => m.Map<Scooter>(testRequest))
               .Returns(testScooter);

            // Act
            var actionResult = _sut.AddScooter(testRequest);

            // Assert
            actionResult.Should().BeOfType<BadRequestResult>();

            // Assert
            _dbContext.Scooters.Count().Should().Be(currentCountInDb);
        }

        [Fact]
        public void AddScooter_InputInvalidRentalStatus_ReturnsBadRequest()
        {
            // Arrange
            var testRequest = new ScooterRequest(0.25m, true);
            var testScooter = new Scooter(testRequest.PricePerMinute, testRequest.IsRented);
            var currentCountInDb = _dbContext.Scooters.Count();

            _autoMapperMock
               .Setup(m => m.Map<Scooter>(testRequest))
               .Returns(testScooter);

            // Act
            var actionResult = _sut.AddScooter(testRequest);

            // Assert
            actionResult.Should().BeOfType<BadRequestResult>();

            // Assert
            _dbContext.Scooters.Count().Should().Be(currentCountInDb);
        }

        [Fact]
        public void GetScooter_InputValid_ReturnsCorrectScooter()
        {
            // Arrange
            var testScooter = _dbContext.Scooters.First();
            var testId = testScooter.Id;

            _scooterServiceMock.Setup(m => m.GetScooterById(testId)).Returns(testScooter);

            _autoMapperMock
                .Setup(m => m.Map<ScooterRequest>(testScooter))
                .Returns(new ScooterRequest(testScooter.PricePerMinute, testScooter.IsRented));

            // Act
            var actionResult = _sut.GetScooter(testId) as ObjectResult;

            // Assert
            actionResult.Should().BeOfType<OkObjectResult>();

            // Assert
            var actionValue = actionResult.Value;
            actionValue.Should().BeOfType<ScooterRequest>();

            var returnedObject = actionValue as ScooterRequest;
            returnedObject.PricePerMinute.Should().Be(testScooter.PricePerMinute);
            returnedObject.IsRented.Should().Be(testScooter.IsRented);
        }

        [Fact]
        public void GetScooter_InputInvalidId_ReturnsNotFound()
        {
            // Arrange
            var testId = 120;

            _scooterServiceMock.Setup(m => m.GetScooterById(testId)).Returns((Scooter)null);

            // Act
            var actionResult = _sut.GetScooter(testId) as ObjectResult;

            // Assert
            actionResult.Should().BeOfType<NotFoundObjectResult>();
            actionResult.Value.Should().Be(testId);
        }

        [Fact]
        public void DeleteScooter_InputValid_ReturnsOk()
        {
            // Arrange
            var testScooter = _dbContext.Scooters.First();
            var testId = testScooter.Id;

            _scooterServiceMock.Setup(m => m.GetScooterById(testId)).Returns(testScooter);
            _scooterServiceMock.Setup(m => m.DeleteScooter(testScooter)).Returns(new ServiceResult(true));

            // Act
            var actionResult = _sut.DeleteScooter(testId) as ObjectResult;

            // Assert
            actionResult.Should().BeOfType<OkObjectResult>($"Scooter: {testId} has been deleted");
        }

        [Fact]
        public void DeleteScooter_InputInvalidId_ReturnsNotFound()
        {
            // Arrange
            var testId = 120;

            _scooterServiceMock.Setup(m => m.GetScooterById(testId)).Returns((Scooter)null);

            // Act
            var actionResult = _sut.DeleteScooter(testId) as ObjectResult;

            // Assert
            actionResult.Should().BeOfType<NotFoundObjectResult>();
            actionResult.Value.Should().Be(testId);
        }

        [Fact]
        public void GetIncomeReport_InputValidSpecific_ReturnsOk()
        {
            // Arrange
            var testYear = 2022;
            var testIncludeRunningRentals = true;
            var testReport = new IncomeReport(2022, 1500m, 50);

            _reportServiceMock.Setup(m => m.GetIncomeForPeriod(testYear, testIncludeRunningRentals)).Returns(testReport);

            // Act
            var actionResult = _sut.GetIncomeReport(testYear, testIncludeRunningRentals) as ObjectResult;

            // Assert
            actionResult.Should().BeOfType<OkObjectResult>();
            actionResult.Value.Should().BeOfType<IncomeReport>();
            actionResult.Value.Should().BeSameAs(testReport);
        }

        [Fact]
        public void GetIncomeReport_InputValidNonSpecific_ReturnsOk()
        {
            // Arrange
            var testYear = 2022;
            var testReport = new IncomeReport(2022, 1500m, 50);

            _reportServiceMock.Setup(m => m.GetIncomeForPeriod(testYear, false)).Returns(testReport);

            // Act
            var actionResult = _sut.GetIncomeReport(testYear, false) as ObjectResult;

            // Assert
            actionResult.Should().BeOfType<OkObjectResult>();
            actionResult.Value.Should().BeOfType<IncomeReport>();
            actionResult.Value.Should().BeSameAs(testReport);
        }

        [Fact]
        public void GetIncomeReport_InputInvalidYear_ReturnsBadRequest()
        {
            // Arrange
            var testYear = 2025;

            // Act
            var actionResult = _sut.GetIncomeReport(testYear, true) as ObjectResult;

            // Assert
            actionResult.Should().BeOfType<BadRequestObjectResult>();
            actionResult.Value.Should().Be(testYear);
        }
    }
}