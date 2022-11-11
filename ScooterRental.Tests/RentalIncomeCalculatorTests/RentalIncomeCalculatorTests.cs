using FluentAssertions;
using ScooterRental.Core.Calculators;
using ScooterRental.Core.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace ScooterRental.Tests
{
    public class RentalIncomeCalculatorTests
    {
        private readonly IRentalIncomeCalculator _sut;
        private readonly List<RentalReport> _testReports;

        public RentalIncomeCalculatorTests()
        {
            _sut = new RentalIncomeCalculator();
            _testReports = new List<RentalReport>()
            {
                new RentalReport(1, 0.25m, new DateTime(2022, 1, 1, 8, 30, 0), new DateTime(2022, 1, 1, 12, 30, 0)),
                new RentalReport(2, 0.50m, new DateTime(2022, 1, 1, 8, 30, 0), new DateTime(2022, 1, 1, 12, 45, 0)),
                new RentalReport(3, 0.75m, new DateTime(2022, 1, 3, 5, 0, 0)),
                new RentalReport(4, 1m, new DateTime(2022, 1, 3, 5, 30, 0))
            };
        }

        [Fact]
        public void CalculateIncome_InputOnlyFinishedStatusRentals_ReturnsExpectedValue()
        {
            // Arrange
            var testReports = new List<RentalReport>() { _testReports[0], _testReports[1] };
            var expected = TestCalculator.SumTotal(testReports);

            // Act
            var actual = _sut.CalculateIncome(testReports);

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void CalculateIncome_InputOnlyRunningStatusRentals_ReturnsExpectedValue()
        {
            // Arrange
            var testReports = new List<RentalReport>() { _testReports[2], _testReports[3] };
            var expected = TestCalculator.SumTotal(testReports);

            // Act
            var actual = _sut.CalculateIncome(testReports);

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void CalculateIncome_InputMixedStatusRentals_ReturnsExpectedValue()
        {
            // Arrange
            var expected = TestCalculator.SumTotal(_testReports);

            // Act
            var actual = _sut.CalculateIncome(_testReports);

            // Assert
            actual.Should().Be(expected);
        }
    }
}