using backend.Controllers;
using backend.Models;
using Terminal.Gui;

namespace shalom_airlines.Admin.Flights;

public class Index : Window
{
    public Index()
    {
        Layout.OpenWindow<Admin.Flights.Index>(null);
    }
    
    public Index(Dictionary<string, object>? filters)
    {

        Title = "Flights";

        var SearchFieldText = new TextField("")
        {
            Width = Dim.Percent(30),
        };
 
        var SearchFieldButton = new Button()
        {
            X = Pos.Right(SearchFieldText) + 2,
            Text = "Search",
        };
        
        SearchFieldButton.Clicked += () =>
        {
            string SearchValue = (string)SearchFieldText.Text;
            Dictionary<string, object> newFilter = new Dictionary<string, object>
            {
                { "FlightNumber", SearchValue },
            };
            Layout.OpenWindow<Admin.Flights.Index>(newFilter);
        };

        List<Flight> flightView = new List<Flight>();
        flightView = filters == null ? Flight.All() : Flight.Search(filters);
        
        var list = new ListView(flightView)
        {
            Y = Pos.Bottom(SearchFieldText) + 2,
            Width = Width,
            Height = Dim.Fill(),
        };
        
        list.OpenSelectedItem += f =>
        {
            Layout.OpenWindow<Show>(f.Value);
        };
        
        Add(SearchFieldText, SearchFieldButton, list);
    }
}