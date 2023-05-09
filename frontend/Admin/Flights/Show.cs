using backend.Controllers;
using backend.Models;
using Terminal.Gui;

namespace shalom_airlines.Admin.Flights;

public class Show : Window
{
    public Show(Flight flight)
    {
        Title = $"Viewing flight {flight.FlightNumber}";

        var flightLabel = new Label(flight.ToString());
        var btnDelete = new Button()
        {
            Text = "Delete",
            Y = Pos.Bottom(flightLabel) + 2,
            X = 0,
        };

        btnDelete.Clicked += () =>
        {
            if (FlightController.Delete(flight))
            {
                MessageBox.Query("Deleting Flight", "Flight Deleted", "Ok");
                Layout.OpenWindow<Index>();
                return;
            }
            MessageBox.Query("Deleting Flight Failed", "Flight not Deleted", "Ok");
        };

        var btnEdit = new Button()
        {
            Text = "Edit",
            Y = Pos.Bottom(btnDelete) + 2,
            X = 0,
            IsDefault = true,
        };

        btnEdit.Clicked += () =>
        {
            Layout.OpenWindow<EditFlight>(flight);
        };
        
        var firstclass = flight.PlaneType.SeatsLayout["First Class"];
        var businessClass = flight.PlaneType.SeatsLayout["Business Class"];
        var economyClass = flight.PlaneType.SeatsLayout["Economy Class"];
        
        var firstClassSeatsView = new TableView () {
            X = 0,
            Y = Pos.Bottom(btnEdit) + 2,
            Width = 50,
            Height = 10,
        };
        var firstClassSeatsLabel = new Label("First class overview")
        {
            X = Pos.Left(firstClassSeatsView),
            Y = Pos.Top(firstClassSeatsView) - 1,
        };
        firstClassSeatsView.Table = firstclass;
        
        var businessClassSeatsView = new TableView () {
            X = 0,
            Y = Pos.Bottom(firstClassSeatsView) + 2,
            Width = 50,
            Height = 10,
        };
        var businessClassSeatsLabel = new Label("Business class overview")
        {
            X = Pos.Left(businessClassSeatsView),
            Y = Pos.Top(businessClassSeatsView) - 1,
        };
        businessClassSeatsView.Table = businessClass;     
        
        var economyClassSeatsView = new TableView () {
            X = 0,
            Y = Pos.Bottom(businessClassSeatsView) + 2,
            Width = 50,
            Height = 10,
        };
        var economyClassSeatsLabel = new Label("Economy class overview")
        {
            X = Pos.Left(economyClassSeatsView),
            Y = Pos.Top(economyClassSeatsView) - 1,
        };
        economyClassSeatsView.Table = economyClass;
        
        Add(flightLabel, btnDelete, btnEdit, 
            firstClassSeatsLabel, firstClassSeatsView,
            businessClassSeatsLabel, businessClassSeatsView,
            economyClassSeatsLabel, economyClassSeatsView);
    }
}