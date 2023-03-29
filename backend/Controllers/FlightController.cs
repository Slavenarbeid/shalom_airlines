using backend.Models;

namespace backend.Controllers;

public static class FlightController
{
    private static List<Flight> _flights = new List<Flight>();
        
    public static Flight Create(int flightNumber, Plane planeType, string departureAirport, DateTime departureTime, string arrivalAirport, DateTime arrivalATime)
    {
        Flight flight = new(flightNumber, planeType, departureAirport, departureTime, arrivalAirport, arrivalATime);

        JsonHandle<Flight> jsonHandle = new JsonHandle<Flight>("Flights");
        jsonHandle.SaveToJson(flight);

        _flights.Add(flight);

        return flight;
    }
}