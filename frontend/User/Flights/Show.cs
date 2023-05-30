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

        Dictionary<string, int> availableSeats = FlightController.AvailableSeatsPerClass(flight);
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
        if (lastSeat != null)
        {
            View? lastButton = null;
            Pos xCord = 0;
            for (int i = 0; i < avaibleSeatTypes.Count; i++)
            {
                if (lastButton != null)
                {
                    xCord = Pos.Right(lastButton) + 1;
                }

                var confirmReservationButton = new Button()
                {
                    Text = $"{avaibleSeatTypes[i]}",
                    Y = Pos.Bottom(lastSeat) + 2,
                    X = xCord,
                };
                lastButton = confirmReservationButton;
                var i1 = i;
                confirmReservationButton.Clicked += () =>
                {
                    var AmountOfReservation = new TextField("")
                    {
                        Y = Pos.Bottom(lastSeat) + 2,
                        X = 0,
                        Width = Dim.Percent(10),
                    };

                    var StartReservation = new Button("")
                    {
                        Text = "Start Reservation",
                        Y = Pos.Y(AmountOfReservation),
                        X = Pos.Right(AmountOfReservation),
                    };

                    var i2 = i1;
                    StartReservation.Clicked += () =>
                    {
                        int a;
                        string seatType = avaibleSeatTypes[i2];
                        if (int.TryParse((string)AmountOfReservation.Text, out a))
                        {
                            
                            Layout.OpenWindow<SelectReservation>(flight, seatType, a);
                        }
                    };
                    Add(AmountOfReservation, StartReservation);
                    Remove(confirmReservationButton);
                };
                Add(confirmReservationButton);
            }
        }
    }
}