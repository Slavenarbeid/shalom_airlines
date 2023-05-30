namespace shalom_airlines.User.Account;
using Terminal.Gui;

public class Profile : Window
{
    public Profile()
    {
        var user = Layout.LoggedInUser;
        Title = "Profile";
        
        // show admin fields
        var fullName = new Label
        {
            Text = $"Name: {user.FirstName} {user.Lastname}",
            Y = 1,
            X = 0,
        };
        
        var email = new Label
        {
            Text = $"Email: {user.Email}",
            Y = Pos.Bottom(fullName) + 1,
            X = 0,
        };
        
        var btnEdit = new Button("Edit")
        {
            Y = Pos.Bottom(email) + 1,
            X = 0,
        };
        
        btnEdit.Clicked += () =>
        {
            Layout.OpenWindow<EditUser>();
        };
        
        Add(fullName, email, btnEdit);
    }
}