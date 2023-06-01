using backend.Controllers;
using backend.Models;
using Terminal.Gui;

namespace shalom_airlines.User.Flights;

public class SelectReservation : Window
{
    public SelectReservation(Flight flight, string type, int amount)
    {
        var seats = flight.PlaneType.SeatsLayout;

        // seats.Insert(0, null); // add null row to front of the list
        seats.Add(null); // add null row to end of the list
        int amountOfRows = 0;
        for (int seatInt = 0; seatInt < seats.Count; seatInt++)
        {
            if (seats[seatInt] == null) continue;
            amountOfRows = seats[seatInt].Count;
            break;
        }

        var infoDisplay = new Label()
        {
            Text = $"( {amountOfRows} )",
            Y = 0,
            X = 0,
        };
        Add(infoDisplay);

        Dictionary<int, List<Seat>> groups = new Dictionary<int, List<Seat>>();
        int groupCounter = 0;
        Pos xCord = 0;
        View? lastSeat = null;
        for (int rowInt = 0; rowInt < amountOfRows; rowInt++)
        {
            if (lastSeat != null)
            {
                xCord = Pos.Right(lastSeat) + 1;
            }
            for (int seatInt = 0; seatInt < seats.Count; seatInt++)
            {
                if (seats[seatInt] == null)
                {
                    groupCounter++;
                    continue;
                }


                if (!groups.ContainsKey(groupCounter))
                {
                    groups.Add(groupCounter, new List<Seat>());
                }

                groups[groupCounter].Add(seats[seatInt][rowInt]);
                var seatDisplay = new Label()
                {
                    Text = $"( {groupCounter} )",
                    Y = Pos.Bottom(infoDisplay) + seatInt,
                    X = xCord,
                };
                Add(seatDisplay);
                lastSeat = seatDisplay;
            }
        }
    }
}