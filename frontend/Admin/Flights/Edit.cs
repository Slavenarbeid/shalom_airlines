using backend.Models;

namespace shalom_airlines.Admin.Flights;
using NStack;
using backend.Controllers;
using Terminal.Gui;


public class EditFlight : Window
{
    public EditFlight(Flight flight)
    {
        Title = $"Edit flight: {flight.FlightNumber}";
        
        var flightNumberLabel = new Label()
        {
            Text = "Flight Number:  ",
        };

        var flightNumberText = new TextField("")
        {
            Text = flight.FlightNumber.ToString(),
            X = Pos.Right(flightNumberLabel) + 2,
            Width = Dim.Percent(75),
        };
        
        var planeTypeLabel = new Label("Select Planetype:  ")
        {
            X = Pos.Left(flightNumberLabel),
            Y = Pos.Bottom(flightNumberLabel) + 2
        };
        
        var planeType = new RadioGroup(new ustring[] {"Boeing 737", "Airbus 330 ", "Boeing 787"})
        {
            X = Pos.Left(flightNumberText),
            Y = Pos.Top(planeTypeLabel)
        };

        var departureAirportLabel = new Label()
        {
            Text = "Departure Airport:  ",
            X = Pos.Left(planeTypeLabel),
            Y = Pos.Bottom(planeTypeLabel) + 4
        };

        var departureAirportText = new TextField("")
        {
            Text = flight.DepartureAirport,
            X = Pos.Left(planeType),
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
            Date = flight.DepartureTime.Date,
            X = Pos.Left(departureAirportText),
            Y = Pos.Top(departureDateLabel),
            Width = Dim.Percent(75),
        };

        var departureTimeLabel = new Label()
        {
            Text = "Departure Time:  ",
            X = Pos.Left(departureDateLabel),
            Y = Pos.Bottom(departureDateLabel) + 2
        };

        var departureTimeText = new TimeField(new TimeSpan(0,0, 0))
        {
            Time = flight.DepartureTime.TimeOfDay,
            X = Pos.Left(departureDateText),
            Y = Pos.Top(departureTimeLabel),
            Width = Dim.Percent(75),
        };

        var arrivalAirportLabel = new Label()
        {
            Text = "Arrival Airport:  ",
            X = Pos.Left(departureTimeLabel),
            Y = Pos.Bottom(departureTimeLabel) + 2
        };

        var arrivalAirportText = new TextField("")
        {
            Text = flight.ArrivalAirport,
            X = Pos.Left(departureTimeText),
            Y = Pos.Top(arrivalAirportLabel),
            Width = Dim.Percent(75),
        };

        var arrivalDateLabel = new Label()
        {
            Text = "Arrival Date:",
            X = Pos.Left(arrivalAirportLabel),
            Y = Pos.Bottom(arrivalAirportLabel) + 2
        };

        var arrivalDateText = new DateField(DateTime.Today)
        {
            Date = flight.ArrivalTime.Date,
            X = Pos.Left(arrivalAirportText),
            Y = Pos.Top(arrivalDateLabel),
            Width = Dim.Percent(75),
        };
        
        var arrivalTimeLabel = new Label()
        {
            Text = "Arrival Time:  ",
            X = Pos.Left(arrivalDateLabel),
            Y = Pos.Bottom(arrivalDateLabel) + 2
        };

        var arrivalTimeText = new TimeField(new TimeSpan(0,0, 0))
        {
            Time = flight.ArrivalTime.TimeOfDay,
            X = Pos.Left(arrivalDateText),
            Y = Pos.Top(arrivalTimeLabel),
            Width = Dim.Percent(75),
        };
        
        var btnEdit = new Button()
        {
            Text = "Edit",
            Y = Pos.Bottom(arrivalTimeText) + 2,
            X = Pos.Center(),
            IsDefault = true,
        };
        
        btnEdit.Clicked += () =>
        {
            // add validation func

            // parse types
            int flightNumberValue = Convert.ToInt32(flightNumberText.Text);
            var planeTypeValue = PlaneController.Create((string)planeType.Text, 30, 10);
            
            string departureAirportValue = (string)departureAirportText.Text;
            DateTime departureDateValue = departureDateText.Date;
            TimeSpan departureTimeValue = departureTimeText.Time;

            DateTime departureDateTimeValue = departureDateValue + departureTimeValue;
            
            string arrivalAirportValue = (string)arrivalAirportText.Text;
            DateTime arrivalDateValue = arrivalDateText.Date;
            TimeSpan arrivalTimeValue = arrivalTimeText.Time;

            DateTime arrivalDateTimeValue = arrivalDateValue + arrivalTimeValue;

            FlightController.Update(
                flight,
                flightNumberValue, 
                planeTypeValue, 
                departureAirportValue, 
                departureDateTimeValue, 
                arrivalAirportValue, 
                arrivalDateTimeValue);
            
            MessageBox.Query("Editing Flight", "Flight Edited", "Ok");
            Layout.OpenWindow<Show>(flight);
        };

        Add(flightNumberLabel, flightNumberText, 
            planeTypeLabel, planeType, 
            departureAirportLabel, departureAirportText, 
            departureDateLabel, departureDateText, 
            departureTimeLabel ,departureTimeText, 
            arrivalAirportLabel, arrivalAirportText,
            arrivalDateLabel, arrivalDateText, 
            arrivalTimeLabel, arrivalTimeText, 
            btnEdit);
    }
}