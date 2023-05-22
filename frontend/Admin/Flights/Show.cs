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
        
        
        for (int rowInt = 0; rowInt < flight.PlaneType.SeatsLayout.Count; rowInt++)
        {
            Pos xCord = 0;
            Button? lastSeat = null;
            if (flight.PlaneType.SeatsLayout[rowInt] == null) continue;
            for (int seatInt = 0; seatInt < flight.PlaneType.SeatsLayout[rowInt].Count; seatInt++)
            {
                if (lastSeat != null)
                {
                    xCord = Pos.Right(lastSeat) + 1; 
                }
                
                var seatDisplay = new Button()
                {
                    Id = $"{rowInt}-{seatInt}",
                    Text = flight.PlaneType.SeatsLayout[rowInt][seatInt].Type+$":{rowInt}-{seatInt}",
                    Y = Pos.Bottom(btnEdit) + 2 + rowInt,
                    X = xCord,
                };
                seatDisplay.Clicked += () =>
                {
                    MessageBox.Query("Seat", $"Seat {seatDisplay.Id}", "Ok");
                };
                Add(seatDisplay);
                lastSeat = seatDisplay;
            }
        }
        
        

        
        Add(flightLabel, btnDelete, btnEdit, btnBack);
    }
}