using Terminal.Gui;
namespace shalom_airlines;

public class AdminOverview : Window
{
    public AdminOverview(backend.Models.User admin)
    {
        Title = "Admin Overview";
        
        // show admin fields
        var fullName = new Label
        {
            Text = $"Name: {admin.FirstName} {admin.Lastname}",
            Y = 1,
            X = 0,
        };
        
        var email = new Label
        {
            Text = $"Email: {admin.Email}",
            Y = Pos.Bottom(fullName) + 1,
            X = 0,
        };

        var level = new Label
        {
            Text = $"Permission level: {admin.Level}",
            Y = Pos.Bottom(email) + 1,
            X = 0,
        };
        
        Add(fullName, email, level);
        
        // show user flights
    }
}