using backend.Controllers;
using backend.Models;
using NStack;
using Terminal.Gui;

namespace shalom_airlines.User.Flights;

public class Index : Window
{
    public Index() : this(null)
    {
    }

    public Index(Dictionary<string, object>? filters)
    {
        Title = "Flights";

        var SearchIdlLabel = new Label()
        {
            Text = "ID: ",
        };

        string text = "";
        if (filters != null && filters.ContainsKey("FlightNumber"))
        {
            text = Convert.ToString(filters["FlightNumber"]);
        }

        var SearchIdFieldText = new TextField("")
        {
            Text = text,
            X = Pos.Right(SearchIdlLabel) + 1,
            Width = Dim.Percent(30),
        };

        // Field  Departure -> Arrival
        var SearchDeparturelLabel = new Label()
        {
            Text = "Flight: ",
            Y = Pos.Bottom(SearchIdlLabel) + 1,
        };
        text = "";
        if (filters != null && filters.ContainsKey("DepartureAirport"))
        {
            text = Convert.ToString(filters["DepartureAirport"]);
        }

        var SearchDepartureFieldText = new TextField("")
        {
            Text = text,
            Y = Pos.Bottom(SearchIdlLabel) + 1,
            X = Pos.Right(SearchDeparturelLabel) + 1,
            Width = Dim.Percent(30),
        };
        var SearchArrivallLabel = new Label()
        {
            Text = "->",
            Y = Pos.Bottom(SearchIdlLabel) + 1,
            X = Pos.Right(SearchDepartureFieldText) + 1,
        };
        text = "";
        if (filters != null && filters.ContainsKey("ArrivalAirport"))
        {
            text = Convert.ToString(filters["ArrivalAirport"]);
        }

        var SearchArrivalFieldText = new TextField("")
        {
            Text = text,
            Y = Pos.Bottom(SearchIdlLabel) + 1,
            X = Pos.Right(SearchArrivallLabel) + 1,
            Width = Dim.Percent(30),
        };

        // Button Search
        var SearchFieldButton = new Button()
        {
            Y = Pos.Bottom(SearchDeparturelLabel) + 1,
            Text = "Search",
        };
        SearchFieldButton.Clicked += () =>
        {
            Dictionary<string, object> newFilter = new Dictionary<string, object>
            {
            };
            if (SearchIdFieldText.Text != "")
            {
                int a;
                if (int.TryParse((string)SearchIdFieldText.Text, out a))
                {
                    int SearchIdValue = Convert.ToInt32(a);
                    newFilter.Add("FlightNumber", SearchIdValue);
                }
            }

            if (SearchDepartureFieldText.Text != "")
            {
                string SearchDepartureValue = (string)SearchDepartureFieldText.Text;
                newFilter.Add("DepartureAirport", SearchDepartureValue);
            }

            if (SearchArrivalFieldText.Text != "")
            {
                string SearchArrivalValue = (string)SearchArrivalFieldText.Text;
                newFilter.Add("ArrivalAirport", SearchArrivalValue);
            }

            Layout.OpenWindow<Index>(newFilter);
        };

        // Button Reset
        var SearchResetButton = new Button()
        {
            Y = Pos.Bottom(SearchDeparturelLabel) + 1,
            X = Pos.Right(SearchFieldButton) + 1,
            Text = "Reset",
        };
        SearchResetButton.Clicked += () => { Layout.OpenWindow<Index>(null); };

        List<Flight> flightView = new List<Flight>();
        flightView = filters == null ? Flight.All() : Flight.Search(filters);

        var list = new ListView(flightView)
        {
            Y = Pos.Bottom(SearchFieldButton) + 2,
            Width = Width,
            Height = Dim.Fill(),
        };

        list.OpenSelectedItem += f => { Layout.OpenWindow<Show>(f.Value); };

        Add(SearchIdlLabel, SearchIdFieldText, // ID search
            SearchDeparturelLabel, SearchDepartureFieldText, SearchArrivalFieldText,
            SearchArrivallLabel, // Departure -> Arrival search
            SearchFieldButton, SearchResetButton, // Search buttons
            list);
    }
}