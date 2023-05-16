using backend.Controllers;
using backend.Models;
using Terminal.Gui;
using Attribute = Terminal.Gui.Attribute;

namespace shalom_airlines.Admin.Flights;

public class Show : Window
{
    public Show(Flight flight)
    {

        Title = $"Viewing flight {flight.FlightNumber}";

        var flightLabel = new Label(flight.ToString());
        var btnDelete = new Button()
        {
            Text = "Delete",
            Y = Pos.Bottom(flightLabel) + 2,
            X = 0,
        };

        btnDelete.Clicked += () =>
        {
            if (FlightController.Delete(flight))
            {
                MessageBox.Query("Deleting Flight", "Flight Deleted", "Ok");
                Layout.OpenWindow<Index>();
                return;
            }

            MessageBox.Query("Deleting Flight Failed", "Flight not Deleted", "Ok");
        };

        var btnEdit = new Button()
        {
            Text = "Edit",
            Y = Pos.Bottom(btnDelete) + 2,
            X = 0,
            IsDefault = true,
        };

        btnEdit.Clicked += () => { Layout.OpenWindow<EditFlight>(flight); };

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

                if (flight.PlaneType.SeatsLayout[rowInt][seatInt].Reservation == null)
                {
                    int rowInt1 = rowInt;
                    int seatInt1 = seatInt;
                    var seatButton = new Button()
                    {
                        Text = $"{seatType[0]}: {rowInt1}-{seatInt1}",
                        Y = Pos.Bottom(btnEdit) + 2 + rowInt,
                        X = xCord,
                    };

                    seatButton.Clicked += () =>
                    {
                        ReservationController.ReserveSeat(flight, rowInt1, seatInt1, Layout.LoggedInUser);
                        MessageBox.Query("Seat", $"{seatType[0]}: {rowInt1}-{seatInt1}", "Ok");
                        Layout.OpenWindow<Show>(flight);
                    };
                    Add(seatButton);
                    lastSeat = seatButton;
                }
                else
                {
                    
                    var myColor = Application.Driver.MakeAttribute (Color.Blue, Color.Red);
                    var seatDisplay = new Label()
                    {

                        Text = $"( {seatType[0]}: {rowInt}-{seatInt} )",
                        
                        Y = Pos.Bottom(btnEdit) + 2 + rowInt,
                        X = xCord,
                    };
                    // seatDisplay.ColorScheme.Normal = new Attribute(Color.Red, Color.Black);
                    seatDisplay.Clicked += () =>
                    {

                        MessageBox.Query("test", $"test", "Ok");

                    };
                    Add(seatDisplay);
                    lastSeat = seatDisplay;
                }
            }
        }


        Add(flightLabel, btnDelete, btnEdit);
        
        if (lastSeat != null) {
            // var confirmReservationButton = new Button()
            // {
            //     Text = "Confirm",
            //     Y = Pos.Bottom(lastSeat) + 2,
            //     X = 0,
            // };
            //
            // confirmReservationButton.Clicked += () =>
            // {
            //     JsonHandle<Flight> jsonHandle = new JsonHandle<Flight>("Flights");
            //     jsonHandle.UpdateJson(oldFlight,newFlight);
            //     Layout.OpenWindow<Show>(flight);
            // };
        }
    }
}