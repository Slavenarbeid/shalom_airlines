using backend.Models;

namespace backend.Controllers;

public static class FlightController
{
    private static List<Flight> _flights = new List<Flight>();
        
    public static Flight Create(int flightNumber, Plane planeType, string departureAirport, DateTime departureTime, string arrivalAirport, DateTime arrivalTime)
    {
        Flight flight = new(flightNumber, planeType, departureAirport, departureTime, arrivalAirport, arrivalTime);

        JsonHandle<Flight> jsonHandle = new JsonHandle<Flight>("Flights");
        jsonHandle.AddToJson(flight);
        _flights.Add(flight);
        
        return flight;
    }

    public static bool Delete(Flight flight)
    {
        JsonHandle<Flight> jsonHandle = new JsonHandle<Flight>("Flights");
        return jsonHandle.RemoveFromJson(flight);
    }
    

}