﻿using ScooterRental.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScooterRental.Core.Services
{
    public interface IScooterService : IEntityService<Scooter>
    {
        Scooter GetScooterById(int id);

        bool IsFound(Scooter scooter);

        public Scooter UpdateScooter(Scooter scooterToUpdate, Scooter scooterToMatch);

        void Clear();
    }
}