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
        
        var list = new ListView(Flight.All())
        {
            Width = Width,
            Height = Dim.Fill(),
        };

        list.OpenSelectedItem += f =>
        {
            Application.Run(new Show((Flight)f.Value));
        };
        
        Add(list);
    }
}