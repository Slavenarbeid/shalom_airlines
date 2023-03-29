using backend.Controllers;
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
        JsonHandle<Flight> jsonHandle = new JsonHandle<Flight>("Flights");
        Flights = jsonHandle.LoadJson();


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