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
        
        // show admin fields
        var fullName = new Label
        {
            Text = $"Name: {user.FirstName} {user.Lastname}",
            Y = 1,
            X = 0,
        };
        
        var email = new Label
        {
            Text = $"Email: {user.Email}",
            Y = Pos.Bottom(fullName) + 1,
            X = 0,
        };

        var level = new Label
        {
            Text = $"Permission level: {user.Level}",
            Y = Pos.Bottom(email) + 1,
            X = 0,
        };

        Dictionary<string, object>? filters = null;
        
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
            Y = Pos.Bottom(level) + 2,
            Width = Width,
            Height = Dim.Fill(),
        };
        
        list.OpenSelectedItem += f => { Layout.OpenWindow<Show>(f.Value); };
        
        Add(fullName, email, level, list);
    }
}