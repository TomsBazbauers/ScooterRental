using FluentAssertions;
using ScooterRental.Core.Models;
using ScooterRental.Core.Services;
using ScooterRental.Services;
using System.Linq;
using Xunit;

namespace ScooterRental.Tests
{
    public class ScooterServiceTests : TestDatabase
    {
        private readonly IScooterService _sut;

        public ScooterServiceTests()
        {
            _sut = new ScooterService(_dbContext);
        }

        [Fact]
        public void GetScooterById_InputValidScooter_ReturnsExpectedScooter()
        {
            // Arrange
            var testScooter = _sut.CreateScooter(new Scooter(0.55m, false));
            var testId = testScooter.Entity.Id;

            // Act
            var actionResult = _sut.GetScooterById(testId);

            // Assert
            actionResult.Should().BeOfType<Scooter>();
            actionResult.Id.Should().Be(testId);
            actionResult.PricePerMinute.Should().Be(0.55m);
            actionResult.IsRented.Should().BeFalse();
        }

        [Fact]
        public void CreateScooter_InputValidScooter_ScooterCreatedCorrectly()
        {
            // Arrange
            var testScooter = new Scooter(0.25m, false);
            var currentCountInDb = _dbContext.Scooters.Count();

            // Act
            var actionResult = _sut.CreateScooter(testScooter);

            // Assert
            actionResult.Success.Should().BeTrue();

            // Assert
            _dbContext.Scooters
                .First(scooter => scooter.Id == actionResult.Entity.Id).PricePerMinute
                .Should().Be(testScooter.PricePerMinute);
            _dbContext.Scooters
                .First(scooter => scooter.Id == actionResult.Entity.Id).IsRented.Should().BeFalse();
            _dbContext.Scooters.Count().Should().Be(currentCountInDb + 1);
        }

        [Fact]
        public void DeleteScooter_InputValidScooter_CorrectScooterDeleted()
        {
            // Arrange
            var currentCountInDb = _dbContext.Scooters.Count();
            var scooterToDelete = _dbContext.Scooters.First();

            // Act
            var actionResult = _sut.DeleteScooter(scooterToDelete);

            // Assert
            actionResult.Success.Should().BeTrue();

            // Assert
            _dbContext.Scooters.Any(scooter => scooter.Id == scooterToDelete.Id).Should().BeFalse();
            _dbContext.Scooters.Count().Should().Be(currentCountInDb - 1);
        }

        [Fact]
        public void StartRental_InputValid_CorrectScooterRented()
        {
            // Arrange
            var testScooter = _sut.CreateScooter(new Scooter(0.25m, false));
            var testId = testScooter.Entity.Id;

            // Act
            var actionResult = _sut.StartRental(testId);

            // Assert
            actionResult.Success.Should().BeTrue();

            // Assert
            _dbContext.Scooters
                .Any(scooter => scooter.Id == testId && !scooter.IsRented).Should().BeFalse();
        }

        [Fact]
        public void StartRental_InputInvalid_ReturnsFalseStatusRemains()
        {
            // Arrange
            var testScooter = _dbContext.Scooters.First();
            var testId = testScooter.Id;
            testScooter.IsRented = true;

            // Act
            var actionResult = _sut.StartRental(testId);

            // Assert
            actionResult.Success.Should().BeFalse();

            // Assert
            testScooter.IsRented.Should().BeTrue();
        }

        [Fact]
        public void EndRental_InputValid_CorrectScooterRentalEnded()
        {
            // Arrange
            var testScooter = _sut.CreateScooter(new Scooter(0.25m, true));
            var testId = testScooter.Entity.Id;

            // Act
            var actionResult = _sut.EndRental(testId);

            // Assert
            actionResult.Success.Should().BeTrue();

            // Assert
            _dbContext.Scooters
                .Any(scooter => scooter.Id == testId && scooter.IsRented).Should().BeFalse();
        }

        [Fact]
        public void EndRental_InputInvalid_ReturnsFalseStatusRemains()
        {
            // Arrange
            var testScooter = _dbContext.Scooters.First();
            var testId = testScooter.Id;
            testScooter.IsRented = false;

            // Act
            var actionResult = _sut.EndRental(testId);

            // Assert
            actionResult.Success.Should().BeFalse();

            // Assert
            testScooter.IsRented.Should().BeFalse();
        }
    }
}
