using backend.Controllers;
using backend.Models;
using Terminal.Gui;
using Attribute = Terminal.Gui.Attribute;

namespace shalom_airlines.User.Flights;

public class SelectReservation : Window
{
    public SelectReservation(Flight flight, string type, int amount)
    {
        var seats = flight.PlaneType.SeatsLayout;

        // seats.Insert(0, null); // add null row to front of the list
        if (seats.Last() != null) seats.Add(null); // add null row to end of the list
         
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
        Dictionary<int, List<int[]>> groups = new Dictionary<int, List<int[]>>();
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
                    groups.Add(groupCounter, new List<int[]>());
                }
                groups[groupCounter].Add(new []{seatInt, rowInt});
                if (seats[seatInt][rowInt].Reservation == null)
                {
                    groupCounterAvailableSeats++;
                }
            }
        }


        // Calculate optimal seat size
        int optimalgroupsize = amount;

        if (maxSeatSize < amount) optimalgroupsize = maxSeatSize;
        int startgroupsize = optimalgroupsize;
        
        while (true)
        {
            if (optimalgroupsize <= 0)
            {
                optimalgroupsize = startgroupsize;
                while (true)
                {
                    
                    optimalgroupsize++;
                    if (groupsAvailableSeats.ContainsKey(optimalgroupsize)) break;
                }
                break;
            }
            if (groupsAvailableSeats.ContainsKey(optimalgroupsize)) break;
            optimalgroupsize--;
        }
        
        // debug info
        string text = $"max:{maxSeatSize}\noptimal:{optimalgroupsize}\n";
        // foreach (var amountofseats in groupsAvailableSeats)
        // {
        //     text += $"{amountofseats.Key} {string.Join(",", amountofseats.Value)}\n";
        // }
        foreach (var amountofseats in groups)
        {
            text += $"{amountofseats.Key}: ";
            foreach (var val in amountofseats.Value)
            {
                text += $"[ {val[0]}-{val[1]} ]";
            }
            text += $"\n";
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
                var colorScheme = new ColorScheme();
                string seatTaken = "O";
                if (seats[seatInt][rowInt].Reservation != null) seatTaken = "X";

                if (groupsAvailableSeats.ContainsKey(optimalgroupsize) &&
                    groupsAvailableSeats[optimalgroupsize].Contains(groupviewcount) &&
                    amount > 0)
                {   // if seat is available
                    
                    colorScheme.Normal = new Attribute(Color.Green, Color.Black); // Give Green Color to available seats
                    if (seats[seatInt][rowInt].Reservation == Layout.LoggedInUser) {
                        colorScheme.Normal = new Attribute(Color.Blue, Color.Black); // Give Blue color to seat user selected
                    }
                    var seatDisplay = new Label()
                    {
                        Text = $"[ {groupviewcount}-{seatTaken} ]",
                        Y = Pos.Bottom(infoDisplay) + seatInt,
                        X = xCord,
                        ColorScheme = colorScheme,
                    };

                    var groupviewcount1 = groupviewcount;
                    seatDisplay.Clicked += () =>
                    {
                        MessageBox.Query("Test", $"Group: {groupviewcount1}", "Ok");
                        int reservationAmount = optimalgroupsize;
                        if (optimalgroupsize > amount) reservationAmount = amount;
                        flight = ReservationController.FillReservation(flight, groups[groupviewcount1], Layout.LoggedInUser, reservationAmount);
                        amount -= optimalgroupsize;

                        Layout.OpenWindow<SelectReservation>(flight, type, amount);
                    };
                    
                    Add(seatDisplay);
                    lastSeat = seatDisplay;
                }
                else
                {   // if seat is not available
                    if (seats[seatInt][rowInt].Reservation == Layout.LoggedInUser) {
                        colorScheme.Normal = new Attribute(Color.Blue, Color.Black); // Give Blue color to seat user selected
                    }
                    var seatDisplay = new Label()
                    {
                        Text = $"( {groupviewcount}-{seatTaken} )",
                        Y = Pos.Bottom(infoDisplay) + seatInt,
                        X = xCord,
                        ColorScheme = colorScheme,
                    };
                    Add(seatDisplay);
                    lastSeat = seatDisplay;
                }
            }
        }
        if (amount <= 0) // Only show button when user selected all seats
        {
            var confirmationButton = new Button()
            {
                Text = $"Confirm",
                Y = Pos.Bottom(lastSeat),
                X = 0,
            };
            confirmationButton.Clicked += () =>
            {
                FlightController.UpdateFlightByFlightNumber(flight.FlightNumber, flight);
                Layout.OpenWindow<Show>(flight);
            };
            Add(confirmationButton);
        }
    }



}