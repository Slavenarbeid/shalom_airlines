using backend.Controllers;
using backend.Models;
using Terminal.Gui;
using Attribute = Terminal.Gui.Attribute;

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
        bool hasReservation = false;
        for (int rowInt = 0; rowInt < flight.PlaneType.SeatsLayout.Count; rowInt++)
        {
            Pos xCord = 0;
            if (flight.PlaneType.SeatsLayout[rowInt] == null) continue;
            lastSeat = null;

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
                else if (flight.PlaneType.SeatsLayout[rowInt][seatInt].Reservation?.ID == Layout.LoggedInUser.ID)
                {
                    hasReservation = true;
                    var colorScheme = new ColorScheme();
                    colorScheme.Normal = new Attribute(Color.Black, Color.White);
                    var seatDisplay = new Label()
                    {
                        Text = $"( Yours  )",

                        Y = Pos.Bottom(flightLabel) + 2 + rowInt,
                        X = xCord,

                        ColorScheme = colorScheme,
                    };
                    seatDisplay.Clicked += () => { Layout.OpenWindow<ShowReservation>(flight, rowInt1, seatInt1); };
                    Add(seatDisplay);
                    lastSeat = seatDisplay;
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

        if (lastSeat != null)
        {
            var cancelReserverdSeats = new Button("")
            {
                Text = "Cancel reserved seats",
                Y = Pos.Bottom(lastSeat) + 1,
                X = 0,
            };
            cancelReserverdSeats.Clicked += () =>
            {
                var n = MessageBox.Query ("Cancel reservation", "Are you sure you want cancel the reservation?", "Yes", "No");
                if (n == 0){
                    flight = ReservationController.CancelReservation(flight, Layout.LoggedInUser);
                    Layout.OpenWindow<Show>(flight);
                }
            };
            if (hasReservation)
            {
                Add(cancelReserverdSeats);
            }
            
            
            var availableSeatsLabel = new Label()
            {
                Text = availableSeatsString,
                Y = Pos.Bottom(lastSeat) + 3,
                X = 0,
            };
            Add(availableSeatsLabel);

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
                    Y = Pos.Bottom(availableSeatsLabel) + 1,
                    X = xCord,
                };
                lastButton = confirmReservationButton;
                var i1 = i;
                confirmReservationButton.Clicked += () =>
                {
                    var AmountOfReservation = new TextField("")
                    {
                        Y = Pos.Bottom(availableSeatsLabel) + 1,
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
                            if (availableSeats[avaibleSeatTypes[i2]] >= a)
                            {
                                Layout.OpenWindow<SelectReservation>(flight, seatType, a);
                            }
                            else
                            {
                                MessageBox.ErrorQuery("Error", $"Selected amount is too big", "Ok");
                            }
                        }
                        else
                        {
                            MessageBox.ErrorQuery("Error", $"Please enter a integer", "Ok");
                        }
                    };

                    var CancelReservation = new Button()
                    {
                        Text = "Cancel",
                        Y = Pos.Bottom(AmountOfReservation),
                        X = 0,
                    };
                    CancelReservation.Clicked += () => { Layout.OpenWindow<Show>(flight); };


                    Add(AmountOfReservation, StartReservation, CancelReservation);
                    Remove(confirmReservationButton);
                };
                Add(confirmReservationButton);
            }
        }
    }
}