using backend.Controllers;
using Terminal.Gui;

namespace shalom_airlines;

public class Login : Window
{
    public TextField emailText;

    public Login()
    {
        Title = "Login";

        // Create input components and labels
        var emailLabel = new Label()
        {
            Text = "Username:",
            Y = Pos.Center()
        };

        emailText = new TextField("")
        {
            // Position text field adjacent to the label
            X = Pos.Right(emailLabel) + 1,
            Y = Pos.Center(),

            // Fill remaining horizontal space
            Width = Dim.Fill(),
        };

        var passwordLabel = new Label()
        {
            Text = "Password:",
            X = Pos.Left(emailLabel),
            Y = Pos.Bottom(emailLabel) + 1
        };

        var passwordText = new TextField("")
        {
            Secret = true,
            // align with the text box above
            X = Pos.Left(emailText),
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
        void BtnLoginClickedHandler()
        {
            var login = AuthController.Login(emailText.Text, passwordText.Text);
            // check if login successful
            if (login.Item1)
            {
                btnLogin.Clicked -= BtnLoginClickedHandler;
                MessageBox.Query("Logging In", "Login Successful", "Ok");
                Application.Top.RemoveAll();
                Application.RequestStop();

                Application.Run(new Layout(login.Item2));
            }
            MessageBox.ErrorQuery("Logging In", "Incorrect username or password", "Ok");
        }
        btnLogin.Clicked += BtnLoginClickedHandler;

        // Add the views to the Window
        Add(emailLabel, emailText, passwordLabel, passwordText, btnLogin);
    }
}