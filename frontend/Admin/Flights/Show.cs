using backend.Models;
using Terminal.Gui;

namespace shalom_airlines.Admin.Flights;

public class Show : Window
{
    public Show(Flight flight)
    {
        Add(new Label(flight.ToString()));
    }
}