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
        
        Add(flightLabel, btnDelete, btnEdit);
    }
}