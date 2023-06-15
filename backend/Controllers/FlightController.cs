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

    public static Dictionary<string, int> AvailableSeatsPerClass(Flight flight)
    {
        Dictionary<string, int> availableSeats = new Dictionary<string, int>();
        for (int rowInt = 0; rowInt < flight.PlaneType.SeatsLayout.Count; rowInt++)
        {
            if (flight.PlaneType.SeatsLayout[rowInt] == null) continue;
            for (int seatInt = 0; seatInt < flight.PlaneType.SeatsLayout[rowInt].Count; seatInt++)
            {
                String seatType = flight.PlaneType.SeatsLayout[rowInt][seatInt].Type;
                if (!availableSeats.ContainsKey(seatType)) availableSeats.Add(seatType, 0);
                if (flight.PlaneType.SeatsLayout[rowInt][seatInt].Reservation != null) continue;
                availableSeats[seatType]++;
            }
        }

        return availableSeats;
    }
    
    public static Flight GetFlightByFlightNumber(int flightNumber)
    {
        JsonHandle<Flight> jsonHandle = new JsonHandle<Flight>("Flights");
        List<Flight> flights = Flight.All();
        return flights.Find(obj => obj.FlightNumber == flightNumber);
    }
}