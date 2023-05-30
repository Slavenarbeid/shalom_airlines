using Terminal.Gui;

namespace shalom_airlines.User;

public class UserOverview : Window
{
    public UserOverview()
    {
        var user = Layout.LoggedInUser;
        Title = "User Overview";
        
        // show user fields
        var fullName = new Label($"{user.FirstName} {user.Lastname}");
        var email = new Label(user.Email); 
        
        Add(fullName, email);
        
        // show user flights
        
    }
}