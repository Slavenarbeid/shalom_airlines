﻿using NStack;
using backend.Models;
using backend.Controllers;
using Terminal.Gui;

namespace shalom_airlines.Admin.Flights;

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
            Text = flight.DepartureAirport,
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
            Date = flight.DepartureTime.Date,
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
            Time = flight.DepartureTime.TimeOfDay,
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
            Text = flight.ArrivalAirport,
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
            Date = flight.ArrivalTime.Date,
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
            Time = flight.ArrivalTime.TimeOfDay,
            Y = Pos.Bottom(arrivalTimeLabel),
            Width = Dim.Fill(),
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
            // extract values
            int flightNumberValue = Convert.ToInt32(flightNumberText.Text);
            
            var selectedPlane = PlaneController.Planes[planeType.SelectedItem];
            var planeTypeValue = PlaneController.Create(selectedPlane.Model, selectedPlane.SeatsLayout, selectedPlane.Info);

            string departureAirportValue = (string)departureAirportText.Text;
            DateTime departureDateTimeValue = departureDateText.Date + departureTimeText.Time;
            
            string arrivalAirportValue = (string)arrivalAirportText.Text;
            DateTime arrivalDateTimeValue = arrivalDateText.Date + arrivalTimeText.Time;
            
            // validate values

            // update flight
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