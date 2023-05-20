namespace backend.Models;

public class Flight : Model<Flight>
{
    public int FlightNumber { get; }
    public Plane PlaneType { get; }
    public string DepartureAirport { get; set; }
    public DateTime DepartureTime { get; set; }
    public string ArrivalAirport { get; set; }
    public DateTime ArrivalTime { get; set; }

    public Flight(int flightNumber, Plane planeType, string departureAirport, DateTime departureTime, string arrivalAirport, DateTime arrivalTime)
    {
        FlightNumber = flightNumber;
        PlaneType = planeType;
        DepartureAirport = departureAirport;
        DepartureTime = departureTime;
        ArrivalAirport = arrivalAirport;
        ArrivalTime = arrivalTime;
    }

    public override string ToString() => $"[{FlightNumber}] {DepartureAirport} -> {ArrivalAirport} | {DepartureTime.ToString()}";
}