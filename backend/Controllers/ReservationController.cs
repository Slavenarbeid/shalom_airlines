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
    
    public static Flight FillReservation(Flight flight, List<int[]> groupToFill, User user, int amountToFill)
    {

        foreach (var seats in groupToFill)
        {
            if (amountToFill == 0) break;
            if (flight.PlaneType.SeatsLayout[seats[0]][seats[1]].Reservation != null) continue;
            flight.PlaneType.SeatsLayout[seats[0]][seats[1]].Reservation = user;
            amountToFill--;
        }
        
        return flight;
    }
    
}