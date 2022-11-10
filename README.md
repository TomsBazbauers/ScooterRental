# ScooterRental

### Description:
A simple API application for registering, renting scooters and calculating, getting rental income reports.

---

### Brief:

- You can update list of available scooters at any time
- You can rent scooters for any time period
- You must calculate rental price when rental ends
- You must calculate rental company income from all rentals and provide yearly report if requested
- Max charge per day is set at 20 euros

---

- Use TDD approach
- Think about OOP design patterns and S.O.L.I.D. principles

### We are giving some interfaces and classes:

```
public interface IRentalCompany
{
    string Name { get; }
    
    void StartRent(string id);
    
    decimal EndRent(string id);
    
    decimal CalculateIncome(int? year, bool includeNotCompletedRentals);
}

public interface IScooterService
{
    void AddScooter(string id, decimal pricePerMinute);
    
    void RemoveScooter(string id);
    
    IList<Scooter> GetScooters();
    
    Scooter GetScooterById(string scooterId);
}

public class Scooter
{
    public Scooter(string id, decimal pricePerMinute)
    {
    Id = id;
    PricePerMinute = pricePerMinute;
    }
    
    public string Id { get; }
  
    public decimal PricePerMinute { get; }

    public bool IsRented { get; set; }
}

```
