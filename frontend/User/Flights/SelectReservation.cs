using backend.Controllers;
using backend.Models;
using Terminal.Gui;

namespace shalom_airlines.User.Flights;

public class SelectReservation : Window
{
    public SelectReservation(Flight flight, string type, int amount)
    {
        Title = $"Viewing flight {flight.FlightNumber}";

        var flightLabel = new Label(flight.ToString());

        Add(flightLabel);
        List<string> avaibleSeatTypes = new List<string>();
        View? lastSeat = null;
        for (int rowInt = 0; rowInt < flight.PlaneType.SeatsLayout.Count; rowInt++)
        {
            Pos xCord = 0;
            lastSeat = null;
            if (flight.PlaneType.SeatsLayout[rowInt] == null) continue;
            for (int seatInt = 0; seatInt < flight.PlaneType.SeatsLayout[rowInt].Count; seatInt++)
            {
                String seatType = flight.PlaneType.SeatsLayout[rowInt][seatInt].Type;
                if (lastSeat != null)
                {
                    xCord = Pos.Right(lastSeat) + 1;
                }

                int rowInt1 = rowInt;
                int seatInt1 = seatInt;
                if (flight.PlaneType.SeatsLayout[rowInt][seatInt].Reservation == null)
                {
                    var seatButton = new Label()
                    {
                        Text = $"( {seatType[0]}: {rowInt}-{seatInt} )",
                        Y = Pos.Bottom(flightLabel) + 2 + rowInt,
                        X = xCord,
                    };

                    Add(seatButton);
                    lastSeat = seatButton;
                    if (avaibleSeatTypes.Contains(seatType)) continue;
                    avaibleSeatTypes.Add(seatType);
                }
                else
                {
                    var seatDisplay = new Label()
                    {
                        Text = $"( xxxxxx )",

                        Y = Pos.Bottom(flightLabel) + 2 + rowInt,
                        X = xCord,
                    };
                    Add(seatDisplay);
                    lastSeat = seatDisplay;
                }
            }
        }
    }
}