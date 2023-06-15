using backend.Controllers;
using backend.Models;
using NStack;
using Terminal.Gui;

namespace shalom_airlines.Admin.Flights;

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

        var SearchIdFieldText = new TextField("")
        {
            X = Pos.Right(SearchIdlLabel) + 1,
            Width = Dim.Percent(30),
        };
        if (filters != null && filters.TryGetValue("FlightNumber", out var filterFlightNumber))
        {
            SearchIdFieldText.Text = Convert.ToString(filterFlightNumber)!;
        }

        // Field  Departure -> Arrival
        var SearchDeparturelLabel = new Label()
        {
            Text = "Flight: ",
            Y = Pos.Bottom(SearchIdlLabel) + 1,
        };

        var SearchDepartureFieldText = new TextField("")
        {
            Y = Pos.Bottom(SearchIdlLabel) + 1,
            X = Pos.Right(SearchDeparturelLabel) + 1,
            Width = Dim.Percent(30),
        };
        if (filters != null && filters.TryGetValue("DepartureAirport", out var filterDepartureAirport))
        {
            SearchDepartureFieldText.Text = Convert.ToString(filterDepartureAirport);
        }

        var SearchArrivallLabel = new Label()
        {
            Text = "->",
            Y = Pos.Bottom(SearchIdlLabel) + 1,
            X = Pos.Right(SearchDepartureFieldText) + 1,
        };

        var SearchArrivalFieldText = new TextField("")
        {
            Y = Pos.Bottom(SearchIdlLabel) + 1,
            X = Pos.Right(SearchArrivallLabel) + 1,
            Width = Dim.Percent(30),
        };
        if (filters != null && filters.TryGetValue("ArrivalAirport", out var filterArrivalAirport))
        {
            SearchArrivalFieldText.Text = Convert.ToString(filterArrivalAirport);
        }

        // Button Reset
        var SearchResetButton = new Button()
        {
            Y = Pos.Bottom(SearchDeparturelLabel) + 1,
            Text = "Reset",
        };
        SearchResetButton.Clicked += Layout.OpenWindow<Index>;

        List<Flight> flightView = filters == null ? Flight.All() : Flight.Search(filters);

        var list = new ListView(flightView)
        {
            Y = Pos.Bottom(SearchResetButton) + 2,
            Width = Width,
            Height = Dim.Fill(),
        };

        list.OpenSelectedItem += f => Layout.OpenWindow<Show>(f.Value);

        // Search on text input
        void SearchAction()
        {
            Dictionary<string, object> newFilter = new();
            if (SearchIdFieldText.Text != "")
            {
                if (int.TryParse((string)SearchIdFieldText.Text, out int a))
                {
                    int searchIdValue = Convert.ToInt32(a);
                    newFilter.Add("FlightNumber", searchIdValue);
                }
            }

            if (SearchDepartureFieldText.Text != "")
            {
                string searchDepartureValue = (string)SearchDepartureFieldText.Text;
                newFilter.Add("DepartureAirport", searchDepartureValue);
            }

            if (SearchArrivalFieldText.Text != "")
            {
                string searchArrivalValue = (string)SearchArrivalFieldText.Text;
                newFilter.Add("ArrivalAirport", searchArrivalValue);
            }

            var flights = newFilter.Count > 0 ? Flight.Search(newFilter) : Flight.All();
            list.SetSource(flights);
            list.Redraw(new Rect(new Point(list.GetAutoSize()), list.GetAutoSize()));
        }

        ;
        SearchIdFieldText.TextChanged += _ => SearchAction();
        SearchDepartureFieldText.TextChanged += _ => SearchAction();
        SearchArrivalFieldText.TextChanged += _ => SearchAction();

        Add(SearchIdlLabel, SearchIdFieldText, // ID search
            SearchDeparturelLabel, SearchDepartureFieldText, SearchArrivalFieldText,
            SearchArrivallLabel, // Departure -> Arrival search
            SearchResetButton, // Search buttons
            list);
    }
}