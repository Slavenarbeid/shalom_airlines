using backend.Models;
using shalom_airlines.Admin.Flights;
using Terminal.Gui;

namespace shalom_airlines.Admin;

public class AdminOverview : Window
{
    public AdminOverview()
    {
        var user = Layout.LoggedInUser;
        Title = "Admin Overview";
        
        var flightsLabel = new Label
        {
            Text = "Your flights:",
            Y = 1,
            X = 0,
        };

        List<Flight> flightView = new List<Flight>();

        foreach (var flight in Flight.All())
        {
            if (flight.FlightHasUser(user))
            {
                flightView.Add(flight);
            }
        }

        var list = new ListView(flightView)
        {
            Y = Pos.Bottom(flightsLabel) + 1,
            Width = Width,
            Height = Dim.Fill(),
        };
        
        list.OpenSelectedItem += f => { Layout.OpenWindow<Show>(f.Value); };
        
        Add(flightsLabel, list);
    }
}