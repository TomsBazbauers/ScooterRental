using FluentAssertions;
using ScooterRental.Core.Models;
using ScooterRental.Core.Validations;
using Xunit;

namespace ScooterRental.Tests.ScooterValidationsTests
{
    public class ScooterStatusValidatorTests
    {
        private IScooterValidator _sut;

        public ScooterStatusValidatorTests()
        {
            _sut = new ScooterStatusValidator();
        }

        [Theory]
        [InlineData(-10, false, true)]
        [InlineData(10.5, false, true)]
        [InlineData(99.55, false, true)]
        public void IsValid_InputValid_ReturnsTrue(decimal testPrice, bool testStatus, bool expected)
        {
            // Arrange
            var testScooter = new Scooter(testPrice, testStatus);

            // Assert
            _sut.IsValid(testScooter).Should().Be(expected);
        }

        [Theory]
        [InlineData(-10, true, false)]
        [InlineData(10.5, true, false)]
        [InlineData(99.55, true, false)]
        public void IsValid_InputInvalid_ReturnsFalse(decimal testPrice, bool testStatus, bool expected)
        {
            // Arrange
            var testScooter = new Scooter(testPrice, testStatus);

            // Assert
            _sut.IsValid(testScooter).Should().Be(expected);
        }
    }
}