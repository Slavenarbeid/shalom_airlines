using backend.Models;

namespace backend.Controllers;

public static class FlightController
{
    public static Flight Create(int flightNumber, Plane planeType, string departureAirport, DateTime departureTime, string arrivalAirport, DateTime arrivalATime)
    {
        return new Flight(flightNumber, planeType, departureAirport, departureTime, arrivalAirport, arrivalATime);
    }
}