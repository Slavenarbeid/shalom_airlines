namespace backend.Models;

public class Flight
{
    public int flightNumber { get; }
    public Plane PlaneType { get; }
    public string DepartureAirport { get; }
    public DateTime DepartureTime { get; }
    public string ArrivalAirport { get; }
    public DateTime ArrivalATime { get; }
    
    public List<int> AvailableSeats = new();
    public List<Reservation> Reservations = new();

    public Flight(int flightNumber, Plane planeType, string departureAirport, DateTime departureTime, string arrivalAirport, DateTime arrivalATime)
    {
        this.flightNumber = flightNumber;
        PlaneType = planeType;
        DepartureAirport = departureAirport;
        DepartureTime = departureTime;
        ArrivalAirport = arrivalAirport;
        ArrivalATime = arrivalATime;
    }
}