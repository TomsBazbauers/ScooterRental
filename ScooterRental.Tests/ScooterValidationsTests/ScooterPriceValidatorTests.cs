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
        private List<Scooter> _testScooters;

        public ScooterPriceValidatorTests()
        {
            _sut = new ScooterPriceValidator();
            _testScooters = new List<Scooter>()
            {
                new Scooter(-2.5m, false),
                new Scooter(-2, false),
                new Scooter(-1m, true),
                new Scooter(-0.1m, false),
                new Scooter(0, false),
                new Scooter(0.1m, true),
                new Scooter(1, false),
                new Scooter(2, false),
                new Scooter(2.5m, true),
                new Scooter(100, false)
            };
        }

        [Theory]
        [InlineData(4, 5, 6, 7, 8, 9)]
        public void IsValid_InputValidVarious_ReturnsTrue(int caseA, int caseB, int caseC, int caseD, int caseE, int caseF)
        {
            // Assert
            _sut.IsValid(_testScooters[caseA]).Should().BeTrue();
            _sut.IsValid(_testScooters[caseB]).Should().BeTrue();
            _sut.IsValid(_testScooters[caseC]).Should().BeTrue();
            _sut.IsValid(_testScooters[caseD]).Should().BeTrue();
            _sut.IsValid(_testScooters[caseE]).Should().BeTrue();
            _sut.IsValid(_testScooters[caseF]).Should().BeTrue();
        }

        [Theory]
        [InlineData(0, 1, 2, 3)]
        public void IsValid_InputInvalidVarious_ReturnsFalse(int caseA, int caseB, int caseC, int caseD)
        {
            // Assert
            _sut.IsValid(_testScooters[caseA]).Should().BeFalse();
            _sut.IsValid(_testScooters[caseB]).Should().BeFalse();
            _sut.IsValid(_testScooters[caseC]).Should().BeFalse();
            _sut.IsValid(_testScooters[caseD]).Should().BeFalse();
        }
    }
}