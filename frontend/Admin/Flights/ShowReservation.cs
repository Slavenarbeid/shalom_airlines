using backend.Controllers;
using backend.Models;
using Terminal.Gui;
using Attribute = Terminal.Gui.Attribute;

namespace shalom_airlines.Admin.Flights;

public class ShowReservation : Window
{
    public ShowReservation(Flight flight, int rowInt, int seatInt)
    {
        backend.Models.User? currentReservation = flight.PlaneType.SeatsLayout[rowInt][seatInt].Reservation;
        if (currentReservation == null) Layout.OpenWindow<Show>(flight);
        
        var btnBack = new Button()
        {
            Text = "Return",
            Y = 0,
            X = 0,
        };
        btnBack.Clicked += Layout.Back;
        Add(btnBack);
        
        var seatDisplay = new Label()
        {
            Text = $"FirstName:{currentReservation.FirstName}\nLastName:{currentReservation.Lastname}\nEmail:{currentReservation.Email}",
            Y = Pos.Bottom(btnBack) ,
            X = 0,
        };
        Add(seatDisplay);
        
        var btnDelete = new Button()
        {
            Text = "Delete",
            Y = Pos.Bottom(seatDisplay),
            X = 0,
        };
        btnDelete.Clicked += () =>
        {
            ReservationController.RemoveReservation(flight, rowInt, seatInt);
            Layout.OpenWindow<Show>(flight);
        };
        Add(btnDelete);
    }
}