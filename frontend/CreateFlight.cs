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

        var departureTimeLabel = new Label()
        {
            Text = "Departure Time:",
            X = Pos.Left(departureAirportLabel),
            Y = Pos.Bottom(departureAirportLabel) + 2
        };

        var departureTimeText = new TextField("")
        {
            X = Pos.Left(departureAirportText),
            Y = Pos.Top(departureTimeLabel),
            Width = Dim.Percent(75),
        };

        var arrivalAirportLabel = new Label()
        {
            Text = "Arrival Airport:",
            X = Pos.Left(departureTimeLabel),
            Y = Pos.Bottom(departureTimeLabel) + 2
        };

        var arrivalAirportText = new TextField("")
        {
            X = Pos.Left(departureTimeText),
            Y = Pos.Top(arrivalAirportLabel),
            Width = Dim.Percent(75),
        };

        var arrivalATimeLabel = new Label()
        {
            Text = "Arrival Airport:",
            X = Pos.Left(arrivalAirportLabel),
            Y = Pos.Bottom(arrivalAirportLabel) + 2
        };

        var arrivalATimeText = new TextField("")
        {
            X = Pos.Left(arrivalAirportText),
            Y = Pos.Top(arrivalATimeLabel),
            Width = Dim.Percent(75),
        };
        
        var btnCreate = new Button()
        {
            Text = "Create",
            Y = Pos.Bottom(arrivalATimeText) + 2,
            X = Pos.Center(),
            IsDefault = true,
        };
        
        btnCreate.Clicked += () =>
        {
            MessageBox.Query("Creating Flight", "Flight Created", "Ok");
            Application.RequestStop();
            Application.Run<AdminOverview>();
        };

        Add(flightNumberLabel, flightNumberText, planeTypeLabel, planeTypeText, departureAirportLabel,
            departureAirportText, departureTimeLabel ,departureTimeText, arrivalAirportLabel, arrivalAirportText,
            arrivalATimeLabel, arrivalATimeText, btnCreate);
    }
}