namespace backend.Models;

public class Flight
{
    public int FlightNumber { get; }
    public Plane PlaneType { get; }
    public string DepartureAirport { get; }
    public DateTime DepartureTime { get; }
    public string ArrivalAirport { get; }
    public DateTime ArrivalATime { get; }
    
    public List<int> AvailableSeats = new();
    // public List<Reservation> Reservations = new();

    public Flight(int flightNumber, Plane planeType, string departureAirport, DateTime departureTime, string arrivalAirport, DateTime arrivalATime)
    {
        FlightNumber = flightNumber;
        PlaneType = planeType;
        DepartureAirport = departureAirport;
        DepartureTime = departureTime;
        ArrivalAirport = arrivalAirport;
        ArrivalATime = arrivalATime;
    }

    public override string ToString() => $"[{FlightNumber}] {DepartureAirport} -> {ArrivalAirport} | {DepartureTime.ToString()}";
}