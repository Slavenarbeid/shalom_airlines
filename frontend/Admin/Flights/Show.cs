using System.CodeDom;
using backend.Controllers;
using backend.Models;
using Terminal.Gui;

namespace shalom_airlines.Admin.Flights;

public class Show : Window
{
    public Show(Flight flight)
    {
        Title = $"Viewing flight details of flight {flight.FlightNumber}";

        var flightLabel = new Label()
        {
            Text = $"From {flight.DepartureAirport} to {flight.ArrivalAirport}\nDeparture date and time:{flight.DepartureTime.Date}\nArrival date and time: {flight.ArrivalTime}",
             
            
        };


        var btnEdit = new Button()
        {
            Text = "Edit",
            Y = Pos.Bottom(flightLabel) + 1,
            X = 0,
            IsDefault = true,
        };

        btnEdit.Clicked += () =>
        {
            Layout.OpenWindow<EditFlight>(flight);
        };

        
        var btnDelete = new Button()
        {
            Text = "Delete",
            Y = Pos.Bottom(flightLabel) + 1,
            X = Pos.Bottom(btnEdit) + 6,
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
        
        
        var btnBack = new Button()
        {
            Text = "Back",
            Y = Pos.Bottom(btnEdit) + 1,
            X = 0,
        };
        
        btnBack.Clicked += () =>
        {
            {
                Layout.OpenWindow<Admin.Flights.Index>();
            }
        };
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
                        Layout.OpenWindow<ShowReservation>(flight, rowInt1, seatInt1);
                    };
                    Add(seatDisplay);
                    lastSeat = seatDisplay;
                }
            }
        }
        


        Add(flightLabel, btnDelete, btnEdit, btnBack);
        
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
        }
    }
}