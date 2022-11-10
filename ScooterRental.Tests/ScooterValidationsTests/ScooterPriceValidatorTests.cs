using FluentAssertions;
using ScooterRental.Core.Models;
using ScooterRental.Core.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ScooterRental.Tests.ScooterValidationsTests
{
    public class ScooterPriceValidatorTests
    {
        private IScooterValidator _sut;

        public ScooterPriceValidatorTests()
        {
            _sut = new ScooterPriceValidator();
        }

        [Theory]
        [InlineData(0, true)]
        [InlineData(1, true)]
        [InlineData(1.5, true)]
        [InlineData(99.9, true)]
        public void IsValid_InputValidVarious_ReturnsTrue(decimal testPrice, bool expected)
        {
            // Arrange 
            var testScooter = new Scooter(testPrice, false);

            // Assert
            _sut.IsValid(testScooter).Should().Be(expected);
        }

        [Theory]
        [InlineData(-1, false)]
        [InlineData(-1.5, false)]
        [InlineData(-99.9, false)]
        public void IsValid_InputInvalidVarious_ReturnsFalse(decimal testPrice, bool expected)
        {
            // Arrange 
            var testScooter = new Scooter(testPrice, false);

            // Assert
            _sut.IsValid(testScooter).Should().Be(expected);
        }
    }
}