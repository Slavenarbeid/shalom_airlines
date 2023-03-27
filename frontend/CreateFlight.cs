using Terminal.Gui;

namespace shalom_airlines;

public class CreateFlight : Window
{
    public CreateFlight()
    {
        Title = "Create a flight";

        // Create input components and labels
        var FlightNumberLabel = new Label()
        {
            Text = "Flight Number:",
        };

        var FlightNumberText = new TextField("")
        {
            // Position text field adjacent to the label
            X = Pos.Right(FlightNumberLabel) + 2,

            // Fill remaining horizontal space
            Width = Dim.Percent(75),
        };

        var PlaneTypeLabel = new Label()
        {
            Text = "Plane type:",
            X = Pos.Left(FlightNumberLabel),
            Y = Pos.Bottom(FlightNumberLabel) + 2
        };

        var PlaneTypeText = new TextField("")
        {
            // align with the text box above
            X = Pos.Left(FlightNumberText),
            Y = Pos.Top(PlaneTypeLabel),
            Width = Dim.Percent(75),
        };

        var DepartureAirportLabel = new Label()
        {
            Text = "Departure Airport:",
            X = Pos.Left(PlaneTypeLabel),
            Y = Pos.Bottom(PlaneTypeLabel) + 2
        };

        var DepartureAirportText = new TextField("")
        {
            // align with the text box above
            X = Pos.Left(FlightNumberText),
            Y = Pos.Top(DepartureAirportLabel),
            Width = Dim.Percent(75),
        };

        var DepartureTimeLabel = new Label()
        {
            Text = "Departure Time:",
            X = Pos.Left(DepartureAirportLabel),
            Y = Pos.Bottom(DepartureAirportLabel) + 2
        };

        var DepartureTimeText = new TextField("")
        {
            // align with the text box above
            X = Pos.Left(DepartureAirportText),
            Y = Pos.Top(DepartureTimeLabel),
            Width = Dim.Percent(75),
        };

        var ArrivalAirportLabel = new Label()
        {
            Text = "Arrival Airport:",
            X = Pos.Left(DepartureTimeLabel),
            Y = Pos.Bottom(DepartureTimeLabel) + 2
        };

        var ArrivalAirportText = new TextField("")
        {
            // align with the text box above
            X = Pos.Left(DepartureTimeText),
            Y = Pos.Top(ArrivalAirportLabel),
            Width = Dim.Percent(75),
        };

        var ArrivalATimeLabel = new Label()
        {
            Text = "Arrival Airport:",
            X = Pos.Left(ArrivalAirportLabel),
            Y = Pos.Bottom(ArrivalAirportLabel) + 2
        };

        var ArrivalATimeText = new TextField("")
        {
            // align with the text box above
            X = Pos.Left(ArrivalAirportText),
            Y = Pos.Top(ArrivalATimeLabel),
            Width = Dim.Percent(75),
        };

        // Create login button
        var btnCreate = new Button()
        {
            Text = "Create",
            Y = Pos.Bottom(ArrivalATimeText) + 2,
            // center the login button horizontally
            X = Pos.Center(),
            IsDefault = true,
        };

        // When login button is clicked display a message popup
        btnCreate.Clicked += () => { };

        Add(FlightNumberLabel, FlightNumberText, PlaneTypeLabel, PlaneTypeText, DepartureAirportLabel,
            DepartureAirportText, DepartureTimeLabel, DepartureTimeText, ArrivalAirportLabel, ArrivalAirportText,
            ArrivalATimeLabel, ArrivalATimeText, btnCreate);
    }
}