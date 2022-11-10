using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ScooterRental.Controllers;
using ScooterRental.Core.Models;
using ScooterRental.Core.Services;
using System;
using System.Linq;
using Xunit;

namespace ScooterRental.Tests.CustomerControllerTests
{
    public class CustomerControllerTests : TestDatabase
    {
        private readonly CustomerController _sut;
        private readonly Mock<IScooterService> _scooterServiceMock;
        private readonly Mock<IReportService> _reportServiceMock;

        public CustomerControllerTests()
        {
            _scooterServiceMock = new Mock<IScooterService>();
            _reportServiceMock = new Mock<IReportService>();

            _sut = new CustomerController(_scooterServiceMock.Object, _reportServiceMock.Object);
        }

        [Fact]
        public void StartRental_InputValid_RentalStarted()
        {
            // Arrange
            var testScooter = _dbContext.Scooters.First();
            var testId = testScooter.Id;

            _scooterServiceMock.Setup(m => m.GetScooterById(testId)).Returns(testScooter);
            _scooterServiceMock.Setup(m => m.StartRental(testId)).Returns(new ServiceResult(true));
            _reportServiceMock.Setup(m => m.CreateReport(testScooter, DateTime.Now)).Returns(new ServiceResult(true));

            // Act
            var actionResult = _sut.StartRental(testId) as ObjectResult;

            // Assert
            actionResult.Should().BeOfType<OkObjectResult>();
            actionResult.Value.Should().Be(testId);
        }

        [Fact]
        public void StartRental_InputInvalidId_ReturnsNotFound()
        {
            // Arrange
            var testId = 120;

            _scooterServiceMock.Setup(m => m.GetScooterById(testId)).Returns((Scooter)null);

            // Act
            var actionResult = _sut.StartRental(testId) as ObjectResult;

            // Assert
            actionResult.Should().BeOfType<NotFoundObjectResult>();
            actionResult.Value.Should().Be(testId);
        }

        [Fact]
        public void StartRental_InputInvalidStatus_ReturnsBadRequest()
        {
            // Arrange
            var testScooter = _dbContext.Scooters.First();
            var testId = testScooter.Id;
            testScooter.IsRented = true;

            _scooterServiceMock.Setup(m => m.GetScooterById(testId)).Returns(testScooter);
            _scooterServiceMock.Setup(m => m.StartRental(testId)).Returns(new ServiceResult(false));

            // Act
            var actionResult = _sut.StartRental(testId) as ObjectResult;

            // Assert
            actionResult.Should().BeOfType<BadRequestObjectResult>();
            actionResult.Value.Should().Be(testId);
        }

        [Fact]
        public void EndRental_InputValid_RentalEndedReportReturned()
        {
            // Arrange
            var testScooter = _dbContext.Scooters.First();
            var testId = testScooter.Id;
            testScooter.IsRented = true;

            var testReport = new RentalReport(testId, testScooter.PricePerMinute, DateTime.Now, DateTime.Now.AddMinutes(-5));
            testReport.RentalIncome = 5m;

            _scooterServiceMock.Setup(m => m.GetScooterById(testId)).Returns(testScooter);
            _scooterServiceMock.Setup(m => m.EndRental(testId)).Returns(new ServiceResult(true));
            _reportServiceMock.Setup(m => m.GetSingleReport(testId)).Returns(testReport);

            // Act
            var actionResult = _sut.EndRental(testId) as ObjectResult;

            // Assert
            actionResult.Should().BeOfType<OkObjectResult>();
            actionResult.Value.Should().Be(testReport);
        }

        [Fact]
        public void EndRental_InputInvalidId_ReturnsNotFound()
        {
            // Arrange
            var testId = 12;

            _scooterServiceMock.Setup(m => m.EndRental(testId)).Returns(new ServiceResult(false));

            // Act
            var actionResult = _sut.EndRental(testId) as ObjectResult;

            // Assert
            actionResult.Should().BeOfType<NotFoundObjectResult>();
            actionResult.Value.Should().Be(testId);
        }

        [Fact]
        public void EndRental_InputInvalidStatus_ReturnsNotFound()
        {
            // Arrange
            var testScooter = _dbContext.Scooters.First();
            var testId = testScooter.Id;
            testScooter.IsRented = false;

            _scooterServiceMock.Setup(m => m.EndRental(testId)).Returns(new ServiceResult(false));

            // Act
            var actionResult = _sut.EndRental(testId) as ObjectResult;

            // Assert
            actionResult.Should().BeOfType<NotFoundObjectResult>();
            actionResult.Value.Should().Be(testId);
        }
    }
}