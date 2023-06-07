namespace shalom_airlines;
using Terminal.Gui;

public class Info : Window
{
    public Info()
    {
        Title = "About us";
        
        var AirportInfo = new Label
        {
            Text = "Location:\nAirport: Rotterdam Airport\nAddress: Rotterdam Airportplein 60, 3045 AP Rotterdam\nPhone: +31 10 446 3444",
            Y = 1,
            X = 0,
        };
        
        var ContactInfo = new Label
        {
            Text = "Contact us:\nEmail: airlines@airlines.nl\nPhone: +31 10 666 9994\nAs an entrepreneur Jake Darcy is going to get involved in this business",
            Y = Pos.Bottom(AirportInfo) + 1,
            X = 0,
        };

        Add(AirportInfo, ContactInfo);
    }
}