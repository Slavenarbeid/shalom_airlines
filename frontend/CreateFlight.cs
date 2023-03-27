using backend.Controllers;
using Terminal.Gui;

namespace shalom_airlines;

public class CreateFlight : Window
{
    public CreateFlight()
    {
        Title = "Create a flight";
        
        var flightNumberLabel = new Label()
        {
            Text = "Flight Number:",
        };

        var flightNumberText = new TextField("")
        {
            X = Pos.Right(flightNumberLabel) + 2,
            Width = Dim.Percent(75),
        };

        var planeTypeLabel = new Label()
        {
            Text = "Plane type:",
            X = Pos.Left(flightNumberLabel),
            Y = Pos.Bottom(flightNumberLabel) + 2
        };

        var planeTypeText = new TextField("")
        {
            X = Pos.Left(flightNumberText),
            Y = Pos.Top(planeTypeLabel),
            Width = Dim.Percent(75),
        };

        var departureAirportLabel = new Label()
        {
            Text = "Departure Airport:",
            X = Pos.Left(planeTypeLabel),
            Y = Pos.Bottom(planeTypeLabel) + 2
        };

        var departureAirportText = new TextField("")
        {
            X = Pos.Left(planeTypeText),
            Y = Pos.Top(departureAirportLabel),
            Width = Dim.Percent(75),
        };

        var departureDateLabel = new Label()
        {
            Text = "Departure Date:",
            X = Pos.Left(departureAirportLabel),
            Y = Pos.Bottom(departureAirportLabel) + 2
        };

        var departureDateText = new DateField(DateTime.Today)
        {
            X = Pos.Left(departureAirportText),
            Y = Pos.Top(departureDateLabel),
            Width = Dim.Percent(75),
        };
        
        var departureTimeLabel = new Label()
        {
            Text = "Departure Time:",
            X = Pos.Left(departureAirportLabel),
            Y = Pos.Bottom(departureAirportLabel) + 2
        };

        var departureTimeText = new TimeField(new TimeSpan(0,0, 0))
        {
            X = Pos.Left(departureAirportText),
            Y = Pos.Top(departureTimeLabel),
            Width = Dim.Percent(75),
        };

        var arrivalAirportLabel = new Label()
        {
            Text = "Arrival Airport:",
            X = Pos.Left(departureDateLabel),
            Y = Pos.Bottom(departureDateLabel) + 2
        };

        var arrivalAirportText = new TextField()
        {
            X = Pos.Left(departureDateText),
            Y = Pos.Top(arrivalAirportLabel),
            Width = Dim.Percent(75),
        };

        var arrivalAtDateLabel = new Label()
        {
            Text = "Arrival Date:",
            X = Pos.Left(arrivalAirportLabel),
            Y = Pos.Bottom(arrivalAirportLabel) + 2
        };

        var arrivalAtDateText = new DateField(DateTime.Today)
        {
            X = Pos.Left(arrivalAirportText),
            Y = Pos.Top(arrivalAtDateLabel),
            Width = Dim.Percent(75),
        };
        
        var btnCreate = new Button()
        {
            Text = "Create",
            Y = Pos.Bottom(arrivalAtDateText) + 2,
            X = Pos.Center(),
            IsDefault = true,
        };
        
        btnCreate.Clicked += () =>
        {
            // add validation func

            // parse types
            int flightNumberValue = Convert.ToInt32(flightNumberText.Text);
            var planeTypeValue = PlaneController.Create((string)planeTypeText.Text, 30, 10);
            string departureAirportValue = (string)departureAirportText.Text;
            DateTime departureDateValue = departureDateText.Date;
            string arrivalAirportValue = (string)arrivalAirportText.Text;
            DateTime arrivalAtDateValue = arrivalAtDateText.Date;

            FlightController.Create(
                flightNumberValue, 
                planeTypeValue, 
                departureAirportValue, 
                departureDateValue, 
                arrivalAirportValue, 
                arrivalAtDateValue);
            
            MessageBox.Query("Creating Flight", "Flight Created", "Ok");
            Application.RequestStop();
            Application.Run<AdminOverview>();
        };

        Add(
            flightNumberLabel, flightNumberText, 
            planeTypeLabel, planeTypeText, 
            departureAirportLabel, departureAirportText, 
            departureDateLabel ,departureDateText, 
            departureTimeLabel ,departureTimeText, 
            arrivalAirportLabel, arrivalAirportText,
            arrivalAtDateLabel, arrivalAtDateText, 
            btnCreate);
    }
}