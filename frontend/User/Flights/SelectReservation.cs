using backend.Controllers;
using backend.Models;
using Terminal.Gui;
using Attribute = Terminal.Gui.Attribute;

namespace shalom_airlines.User.Flights;

public class SelectReservation : Window
{
    public SelectReservation(Flight flight, string type, int amount)
    {
        Title = $"{flight.FlightNumber} - Amount of selections left {amount}";

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
                if (flight.PlaneType.SeatsLayout[rowInt][seatInt].Reservation == null && flight.PlaneType.SeatsLayout[rowInt][seatInt].Type == type && amount != 0)
                {
                    var seatButton = new Button()
                    {
                        Text = $"{seatType[0]}: {rowInt1}-{seatInt1}",
                        Y = Pos.Bottom(flightLabel) + 2 + rowInt,
                        X = xCord,
                    };

                    seatButton.Clicked += () =>
                    {
                        ReservationController.ReserveSeat(flight, rowInt1, seatInt1, Layout.LoggedInUser);
                        MessageBox.Query("Seat", $"{seatType[0]}: {rowInt1}-{seatInt1}", "Ok");
                        amount--;
                        Layout.OpenWindow<SelectReservation>(flight, type, amount);
                    };
                    Add(seatButton);
                    lastSeat = seatButton;
                }
                else
                {
                    var colorScheme = new ColorScheme();
                    string LabelText = "( xxxxxx )";
                    if (flight.PlaneType.SeatsLayout[rowInt][seatInt].Reservation?.ID == Layout.LoggedInUser.ID)
                    {
                        LabelText = "( Yours  )";
                        
                        colorScheme.Normal = new Attribute(Color.Black, Color.White);
                    }
                    
                    var seatDisplay = new Label()
                    {
                        Text = LabelText,

                        Y = Pos.Bottom(flightLabel) + 2 + rowInt,
                        X = xCord,
                        
                        ColorScheme = colorScheme,
                    };
                    Add(seatDisplay);
                    lastSeat = seatDisplay;
                }
            }
        }
        
        if (lastSeat != null) {
            var confirmReservationButton = new Button()
            {
                Text = "Confirm",
                Y = Pos.Bottom(lastSeat) + 2,
                X = 0,
            };
            
            confirmReservationButton.Clicked += () =>
            {
                FlightController.UpdateFlightByFlightNumber(flight.FlightNumber, flight);
                MessageBox.Query("Saved", $"Reservations Saved", "Ok");
                Layout.OpenWindow<Show>(flight);
            };
            Add(confirmReservationButton);
            
            var CancelReservationButton = new Button()
            {
                Text = "Cancel",
                Y = Pos.Bottom(lastSeat) + 2,
                X = Pos.Right(confirmReservationButton),
            };
            
            CancelReservationButton.Clicked += () =>
            {
                Layout.OpenWindow<Show>(flight);
            };
            Add(CancelReservationButton);
        }
    }
}