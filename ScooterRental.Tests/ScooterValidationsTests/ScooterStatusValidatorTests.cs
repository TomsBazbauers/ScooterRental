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
    public class ScooterStatusValidatorTests
    {
        private IScooterValidator _sut;
        private List<Scooter> _testScooters;

        public ScooterStatusValidatorTests()
        {
            _sut = new ScooterStatusValidator();
            _testScooters = new List<Scooter>()
            {
                new Scooter(0, false),
                new Scooter(1, false),
                new Scooter(),
                new Scooter(0, true),
                new Scooter(1, true),
            };
        }

        [Theory]
        [InlineData(0, 1, 2)]
        public void IsValid_InputValid_ReturnsTrue(int caseA, int caseB, int caseC)
        {
            // Act
            var actualA = _sut.IsValid(_testScooters[caseA]);
            var actualB = _sut.IsValid(_testScooters[caseB]);
            var actualC = _sut.IsValid(_testScooters[caseC]);

            // Assert
            actualA.Should().BeTrue();
            actualB.Should().BeTrue();
            actualC.Should().BeTrue();
        }

        [Theory]
        [InlineData(3, 4)]
        public void IsValid_InputInvalid_ReturnsFalse(int caseA, int caseB)
        {
            // Act
            var actualA = _sut.IsValid(_testScooters[caseA]);
            var actualB = _sut.IsValid(_testScooters[caseB]);

            // Assert
            actualA.Should().BeFalse();
            actualB.Should().BeFalse();
        }
    }
}