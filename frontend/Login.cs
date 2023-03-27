using backend.Controllers;
using Terminal.Gui;

namespace shalom_airlines;

public class Login : Window
{
    public TextField usernameText;

    public Login()
    {
        Title = "Example App (Ctrl+Q to quit)";

        // Create input components and labels
        var usernameLabel = new Label()
        {
            Text = "Username:",
            Y = Pos.Center()
        };

        usernameText = new TextField("")
        {
            // Position text field adjacent to the label
            X = Pos.Right(usernameLabel) + 1,
            Y = Pos.Center(),

            // Fill remaining horizontal space
            Width = Dim.Fill(),
        };

        var passwordLabel = new Label()
        {
            Text = "Password:",
            X = Pos.Left(usernameLabel),
            Y = Pos.Bottom(usernameLabel) + 1
        };

        var passwordText = new TextField("")
        {
            Secret = true,
            // align with the text box above
            X = Pos.Left(usernameText),
            Y = Pos.Top(passwordLabel),
            Width = Dim.Fill(),
        };

        // Create login button
        var btnLogin = new Button()
        {
            Text = "Login",
            Y = Pos.Bottom(passwordLabel) + 1,
            // center the login button horizontally
            X = Pos.Center(),
            IsDefault = true,
        };

        // When login button is clicked display a message popup
        btnLogin.Clicked += () =>
        {
            if (AuthController.Login(usernameText.Text, passwordText.Text))
            {
                MessageBox.Query("Logging In", "Login Successful", "Ok");
                Application.RequestStop();
                Application.Run<AdminOverview>();
            }
            else
            {
                MessageBox.ErrorQuery("Logging In", "Incorrect username or password", "Ok");
            }
        };

        // Add the views to the Window
        Add(usernameLabel, usernameText, passwordLabel, passwordText, btnLogin);
    }
}