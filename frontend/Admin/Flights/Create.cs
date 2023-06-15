using NStack;
using backend.Controllers;
using backend.Models;
using Terminal.Gui;

namespace shalom_airlines.Admin.Flights;

public class Create : Window
{
    public Create()
    {
        Title = "Create a flight";
        
        var flightNumberLabel = new Label()
        {
            Text = "Flight Number:  ",
        };

        var flightNumberText = new TextField("")
        {
            Y = Pos.Bottom(flightNumberLabel),
            Width = Dim.Fill(),
        };
        
        var planeTypeLabel = new Label("Select Planetype:  ")
        {
            Y = Pos.Bottom(flightNumberLabel) + 2
        };
        
        ustring[] planeTypeRadioGroup = PlaneController.Planes
            .Select(plane => ustring.Make(plane.Model))
            .ToArray();
        
        var planeType = new RadioGroup(planeTypeRadioGroup)
        {
            Y = Pos.Bottom(planeTypeLabel)
        };

        var departureAirportLabel = new Label()
        {
            Text = "Departure Airport:  ",
            X = Pos.Left(planeTypeLabel),
            Y = Pos.Bottom(planeTypeLabel) + 4
        };

        var departureAirportText = new TextField("")
        {
            Y = Pos.Bottom(departureAirportLabel),
            Width = Dim.Fill(),
        };
        
        var departureDateLabel = new Label()
        {
            Text = "Departure Date:",
            X = Pos.Left(departureAirportLabel),
            Y = Pos.Bottom(departureAirportLabel) + 2
        };

        var departureDateText = new DateField(DateTime.Today)
        {
            Y = Pos.Bottom(departureDateLabel),
            Width = Dim.Fill(),
        };

        var departureTimeLabel = new Label()
        {
            Text = "Departure Time:  ",
            X = Pos.Left(departureDateLabel),
            Y = Pos.Bottom(departureDateLabel) + 2
        };

        var departureTimeText = new TimeField(new TimeSpan(0,0, 0))
        {
            Y = Pos.Bottom(departureTimeLabel),
            Width = Dim.Fill(),
        };

        var arrivalAirportLabel = new Label()
        {
            Text = "Arrival Airport:  ",
            X = Pos.Left(departureTimeLabel),
            Y = Pos.Bottom(departureTimeLabel) + 2
        };

        var arrivalAirportText = new TextField("")
        {
            Y = Pos.Bottom(arrivalAirportLabel),
            Width = Dim.Fill(),
        };

        var arrivalDateLabel = new Label()
        {
            Text = "Arrival Date:",
            X = Pos.Left(arrivalAirportLabel),
            Y = Pos.Bottom(arrivalAirportLabel) + 2
        };

        var arrivalDateText = new DateField(DateTime.Today)
        {
            Y = Pos.Bottom(arrivalDateLabel),
            Width = Dim.Fill(),
        };
        
        var arrivalTimeLabel = new Label()
        {
            Text = "Arrival Time:  ",
            X = Pos.Left(arrivalDateLabel),
            Y = Pos.Bottom(arrivalDateLabel) + 2
        };

        var arrivalTimeText = new TimeField(new TimeSpan(0,0, 0))
        {
            Y = Pos.Bottom(arrivalTimeLabel),
            Width = Dim.Fill(),
        };
        
        var btnCreate = new Button()
        {
            Text = "Create",
            Y = Pos.Bottom(arrivalTimeText) + 2,
            X = Pos.Center(),
            IsDefault = true,
        };
        
        btnCreate.Clicked += () =>
        {
            // extract values
            int flightNumberValue;
            if (int.TryParse((string)flightNumberText.Text, out int fn))
            {
                flightNumberValue = Convert.ToInt32(fn);
            }
            else
            {
                MessageBox.ErrorQuery("Creating Flight", "Flight number must be integer", "Ok");
                return;
            }

            var selectedPlane = PlaneController.Planes[planeType.SelectedItem];
            var planeTypeValue = PlaneController.Create(selectedPlane.Model, selectedPlane.SeatsLayout, selectedPlane.Info);

            string departureAirportValue = (string)departureAirportText.Text;
            DateTime departureDateTimeValue = departureDateText.Date + departureTimeText.Time;
            
            string arrivalAirportValue = (string)arrivalAirportText.Text;
            DateTime arrivalDateTimeValue = arrivalDateText.Date + arrivalTimeText.Time;
            
            // Field validation
            if (Flight.FlightNumberUsedBefore(flightNumberValue))
            {
                MessageBox.ErrorQuery("Creating Flight", "Flight number used before", "Ok");
                return;
            }

            // create flight
            FlightController.Create(
                flightNumberValue, 
                planeTypeValue,
                departureAirportValue, 
                departureDateTimeValue, 
                arrivalAirportValue, 
                arrivalDateTimeValue);
            
            MessageBox.Query("Creating Flight", "Flight Created", "Ok");
            Layout.OpenWindow<Index>();
        };
        
        var btnBack = new Button()
        {
            Text = "Back",
            Y = Pos.Bottom(btnCreate) + 2,
            X = Pos.Center(),
        };
        btnBack.Clicked += Layout.Back;

        Add(flightNumberLabel, flightNumberText, 
            planeTypeLabel, planeType, 
            departureAirportLabel, departureAirportText, 
            departureDateLabel, departureDateText, 
            departureTimeLabel ,departureTimeText, 
            arrivalAirportLabel, arrivalAirportText,
            arrivalDateLabel, arrivalDateText, 
            arrivalTimeLabel, arrivalTimeText, 
            btnCreate, btnBack);
    }
}