using backend.Models;

namespace backend.Controllers;

public static class FlightController
{
    public static List<Flight> Index()
    {
        return Flight.All();
    }
    
    
    
    public static Flight Create(int flightNumber, Plane planeType, string departureAirport, DateTime departureTime, string arrivalAirport, DateTime arrivalTime)
    {
        Flight flight = new(flightNumber, planeType, departureAirport, departureTime, arrivalAirport, arrivalTime);

        JsonHandle<Flight> jsonHandle = new JsonHandle<Flight>("Flights");
        jsonHandle.AddToJson(flight);
        
        return flight;
    }

    public static bool Delete(Flight flight)
    {
        JsonHandle<Flight> jsonHandle = new JsonHandle<Flight>("Flights");
        return jsonHandle.RemoveFromJson(flight);
    }

    public static void Update(Flight oldFlight, int flightNumber, Plane planeType, string departureAirport, DateTime departureTime, string arrivalAirport, DateTime arrivalTime)
    {
        Flight newFlight = new(flightNumber, planeType, departureAirport, departureTime, arrivalAirport, arrivalTime);
        JsonHandle<Flight> jsonHandle = new JsonHandle<Flight>("Flights");
        jsonHandle.UpdateJson(oldFlight,newFlight);
    }
    
    public static void UpdateFlightByFlightNumber(int flightNumber, Flight newFLight)
    {
        JsonHandle<Flight> jsonHandle = new JsonHandle<Flight>("Flights");
        List<Flight> flights = Flight.All();
        Flight flightToUpdate = flights.Find(obj => obj.FlightNumber == flightNumber);
        var index = flights.IndexOf(flightToUpdate);
        if(index != -1)
            flights[index] = newFLight;
        jsonHandle.SaveJsonFile(flights);
    }
}