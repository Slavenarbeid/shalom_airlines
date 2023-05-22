using NStack;
using backend.Controllers;
using Terminal.Gui;

namespace shalom_airlines.Admin.Flights;

public partial class Create : Window
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
            X = Pos.Right(flightNumberLabel) + 2,
            Width = Dim.Percent(75),
        };
        
        var planeTypeLabel = new Label("Select Planetype:  ")
        {
            X = Pos.Left(flightNumberLabel),
            Y = Pos.Bottom(flightNumberLabel) + 2
        };
        
        ustring[] planeTypeRadioGroup = PlaneController.Planes
            .Select(plane => ustring.Make(plane.Model))
            .ToArray();
        
        var planeType = new RadioGroup(planeTypeRadioGroup)
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
            X = Pos.Left(arrivalDateText),
            Y = Pos.Top(arrivalTimeLabel),
            Width = Dim.Percent(75),
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
            int flightNumberValue = Convert.ToInt32(flightNumberText.Text);
            
            var selectedPlane = PlaneController.Planes[planeType.SelectedItem];
            var planeTypeValue = PlaneController.Create(selectedPlane.Model, selectedPlane.SeatsLayout, selectedPlane.Info);

            string departureAirportValue = (string)departureAirportText.Text;
            DateTime departureDateTimeValue = departureDateText.Date + departureTimeText.Time;
            
            string arrivalAirportValue = (string)arrivalAirportText.Text;
            DateTime arrivalDateTimeValue = arrivalDateText.Date + arrivalTimeText.Time;
            
            // validate values

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
        
        btnBack.Clicked += () =>
        {
            {
                Layout.OpenWindow<Admin.Flights.Index>();
            }
        };

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