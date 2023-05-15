using backend.Controllers;
using backend.Models;
using Terminal.Gui;

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

        btnEdit.Clicked += () =>
        {
            Layout.OpenWindow<EditFlight>(flight);
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
                    
                    Text = flight.PlaneType.SeatsLayout[rowInt][seatInt].Type,
                    Y = Pos.Bottom(btnEdit) + 2 + rowInt,
                    X = xCord,
                };
                lastSeat = seatDisplay;
                Add(seatDisplay);
            }
        }
        
        Add(flightLabel, btnDelete, btnEdit);
    }
}