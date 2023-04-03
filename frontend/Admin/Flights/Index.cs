using backend.Controllers;
using backend.Models;
using Terminal.Gui;

namespace shalom_airlines.Admin.Flights;

public class Index : Window
{
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
            Layout.OpenWindow<Show>(f.Value);
        };
        
        Add(list);
    }
}