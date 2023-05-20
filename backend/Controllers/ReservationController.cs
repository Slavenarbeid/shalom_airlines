using backend.Models;

namespace backend.Controllers;

public static class ReservationController
{
    public static Flight ReserveSeat(Flight flight, int rowInt, int seatInt, User user)
    {
        if (flight.PlaneType.SeatsLayout[rowInt][seatInt].Reservation != null) return flight;
        
        Flight oldFlight = flight;
        flight.PlaneType.SeatsLayout[rowInt][seatInt].Reservation = user;

        return flight;
    }
    
    
    public static Flight RemoveReservation(Flight flight, int rowInt, int seatInt)
    {
        if (flight.PlaneType.SeatsLayout[rowInt][seatInt].Reservation == null) return flight;
        
        Flight oldFlight = flight;
        flight.PlaneType.SeatsLayout[rowInt][seatInt].Reservation = null;
        
        return flight;
    }
}