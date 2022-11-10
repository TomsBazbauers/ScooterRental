using Microsoft.EntityFrameworkCore;
using ScooterRental.Core.Models;
using ScooterRental.Data;
using System;
using System.Collections.Generic;

namespace ScooterRental.Tests
{
    public class TestDatabase
    {
        public ScooterRentalDbContext _dbContext;

        public TestDatabase()
        {
            var testDatabase = new DbContextOptionsBuilder<ScooterRentalDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var testScooters = new List<Scooter>()
            {
                new Scooter(0.25m, false),
                new Scooter(0.5m, false),
                new Scooter(0.75m, false),
                new Scooter(1m, false),
                new Scooter(1.25m, false),

            };

            _dbContext = new ScooterRentalDbContext(testDatabase);
            _dbContext.Scooters.AddRange(testScooters);

            _dbContext.SaveChanges();
            _dbContext.Database.EnsureCreated();
        }

        public void Dispose()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }
    }
}
