using backend.Models;
using Terminal.Gui;

namespace shalom_airlines.Admin.Flights;

public class Index : Window
{
    public List<Flight> Flights;
    
    public Index()
    {
        Title = "Flights";
        
        var menu = new MenuBar(new[]
        {
            new MenuBarItem("Menu", new[]
            {
                new MenuItem("Overview", "See all Flights", () => Application.Run<shalom_airlines.Admin.Flights.Index>()),
                new MenuItem("Create", "Create a Flight", () => Application.Run<CreateFlight>()),
            })
        });
        
        Flights = new List<Flight>()
        {
            new(1, new Plane("plane 1", 15, 15), "airport 1", new DateTime(), "airport 2", new DateTime()),
            new(2, new Plane("plane 2", 15, 15), "airport 2", new DateTime(), "airport 3", new DateTime()),
            new(3, new Plane("plane 3", 15, 15), "airport 3", new DateTime(), "airport 1", new DateTime()),
        };


        var list = new ListView(Flights)
        {
            Width = Width,
            Height = Dim.Fill(),
            Y = Pos.Bottom(menu)
        };

        list.OpenSelectedItem += f =>
        {
            Application.Run(new Show((Flight)f.Value));
        };
        Add(menu, list);
    }
}