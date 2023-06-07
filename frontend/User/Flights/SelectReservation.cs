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
            Text = $"- {amountOfRows} -",
            Y = 0,
            X = 0,
        };
        Add(infoDisplay);


        // calculating the group sizes
        Dictionary<int, List<View>> groups = new Dictionary<int, List<View>>();
        Dictionary<int, List<int>> groupsAvailableSeats = new Dictionary<int, List<int>>();
        int groupCounter = 0;
        int groupCounterAvailableSeats = 0;
        int maxSeatSize = 0;
        for (int rowInt = 0; rowInt < amountOfRows; rowInt++)
        {
            for (int seatInt = 0; seatInt < seats.Count; seatInt++)
            {
                if (seats[seatInt] == null)
                {
                    if (!groupsAvailableSeats.ContainsKey(groupCounterAvailableSeats))
                    {
                        groupsAvailableSeats.Add(groupCounterAvailableSeats, new List<int>());
                    }

                    if (groupCounterAvailableSeats > maxSeatSize) maxSeatSize = groupCounterAvailableSeats;
                    groupsAvailableSeats[groupCounterAvailableSeats].Add(groupCounter);
                    groupCounterAvailableSeats = 0;
                    groupCounter++;
                    continue;
                }

                if (seats[seatInt][rowInt].Type != type) continue;
                if (!groups.ContainsKey(groupCounter))
                {
                    groups.Add(groupCounter, new List<View>());
                }


                if (seats[seatInt][rowInt].Reservation == null)
                {
                    groupCounterAvailableSeats++;
                }
            }
        }


        // Calculate optimal seat size
        int optimalgroupsize = amount;
        if (maxSeatSize < amount) optimalgroupsize = maxSeatSize;
        // debug info
        string text = $"max:{maxSeatSize}\noptimal:{optimalgroupsize}\n";
        foreach (var amountofseats in groupsAvailableSeats)
        {
            text += $"{amountofseats.Key} {string.Join(",", amountofseats.Value)}\n";
        }

        infoDisplay.Text = text;


        // Display the seats
        Pos xCord = 0;
        View? lastSeat = null;
        int groupviewcount = 0;
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
                    groupviewcount++;
                    continue;
                }

                string seatTaken = "O";
                if (seats[seatInt][rowInt].Reservation != null) seatTaken = "X";
                if (groupsAvailableSeats.ContainsKey(optimalgroupsize) &&
                    groupsAvailableSeats[optimalgroupsize].Contains(groupviewcount))
                {
                    var seatDisplay = new Label()
                    {
                        Text = $"[ {groupviewcount}-{seatTaken} ]",
                        Y = Pos.Bottom(infoDisplay) + seatInt,
                        X = xCord,
                    };
                    Add(seatDisplay);
                    lastSeat = seatDisplay;
                }
                else
                {
                    var seatDisplay = new Label()
                    {
                        Text = $"( {groupviewcount}-{seatTaken} )",
                        Y = Pos.Bottom(infoDisplay) + seatInt,
                        X = xCord,
                    };
                    Add(seatDisplay);
                    lastSeat = seatDisplay;
                }
            }
        }
    }
}