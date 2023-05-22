using backend.Controllers;
using backend.Models;
using Terminal.Gui;

namespace shalom_airlines.User.Flights;

public class Show : Window
{
    public Show(Flight flight)
    {
        Title = $"Viewing flight {flight.FlightNumber}";

        var flightLabel = new Label(flight.ToString());
        
        Add(flightLabel);
        
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

        Dictionary<string,int> availableSeats = FlightController.AvailableSeatsPerClass(flight);
        string availableSeatsString = "";
        foreach (var seat in availableSeats)
        {
            availableSeatsString += $"{seat.Key}: {seat.Value}, ";
        }
        var availableSeatsLabel = new Label()
        {
            Text = availableSeatsString,
            Y = Pos.Bottom(lastSeat) + 1,
            X = 0,
        };
        Add(availableSeatsLabel);
        if (lastSeat != null) {
            var confirmReservationButton = new Button()
            {
                Text = "reserve",
                Y = Pos.Bottom(lastSeat) + 2,
                X = 0,
            };
            
            confirmReservationButton.Clicked += () =>
            {
                var SearchArrivalFieldText = new TextField("")
                {
                    Y = Pos.Bottom(lastSeat) + 2,
                    X = 0,
                    Width = Dim.Percent(30),
                };
                Add(SearchArrivalFieldText);
                Remove(confirmReservationButton);
            };
            Add(confirmReservationButton);
        }
    }
}