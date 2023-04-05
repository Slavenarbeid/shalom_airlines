using backend.Controllers;
using Terminal.Gui;
using Index = shalom_airlines.Admin.Flights.Index;

namespace shalom_airlines;

public class CreatePlane : Window
{
    public CreatePlane()
    {
        Title = "Create a Plane";
        
        var planeModelLabel = new Label()
        {
            Text = "Plane Model: ",
        };

        var planeModelText = new TextField("")
        {
            X = Pos.Right(planeModelLabel) + 2,
            Width = Dim.Percent(75),
        };

        var businessSeatsLabel = new Label()
        {
            Text = "Business Seats: ",
            X = Pos.Left(planeModelLabel),
            Y = Pos.Bottom(planeModelLabel) + 2
        };

        var businessSeatsText = new TextField()
        {
            X = Pos.Left(planeModelText),
            Y = Pos.Top(businessSeatsLabel),
            Width = Dim.Percent(75),
        };
        
        var firstClassSeatsLabel = new Label()
        {
            Text = "First Class Seats: ",
            X = Pos.Left(businessSeatsLabel),
            Y = Pos.Bottom(businessSeatsLabel) + 2
        };

        var firstClassSeatsText = new TextField()
        {
            X = Pos.Left(businessSeatsText),
            Y = Pos.Top(firstClassSeatsLabel),
            Width = Dim.Percent(75),
        };
        
        var btnCreate = new Button()
        {
            Text = "Create",
            Y = Pos.Bottom(firstClassSeatsText) + 2,
            X = Pos.Center(),
            IsDefault = true,
        };
        
        btnCreate.Clicked += () =>
        {
            // extract values
            string planeModelValue = (string)planeModelText.Text;
            int businessSeatsValue = Convert.ToInt32(businessSeatsText.Text);
            int firstClassSeatsValue = Convert.ToInt32(firstClassSeatsText.Text);
            
            // validate values

            // create plane
            PlaneController.Create(
                planeModelValue, 
                businessSeatsValue,
                firstClassSeatsValue);
            
            MessageBox.Query("Creating Plane", "Plane Created", "Ok");
            Layout.OpenWindow<Index>();
        };

        Add(planeModelLabel, planeModelText, 
            businessSeatsLabel, businessSeatsText, 
            firstClassSeatsLabel, firstClassSeatsText,
            btnCreate);
    }
}