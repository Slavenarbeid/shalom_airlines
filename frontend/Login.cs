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
            Text = "Email:",
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
            IsDefault = true,
        };

        var btnBack = new Button()
        {
            Text = "Back",
            X = Pos.Right(btnLogin),
        };

        void BackAction()
        {
            btnBack.Clicked -= BackAction;
            Application.RequestStop();
            Application.Top.RemoveAll();
            Application.Run<MainMenu>();
        }

        btnBack.Clicked += BackAction;
        
        var btns = new FrameView()
        {
            Y = Pos.Bottom(passwordLabel) + 1,
            X = Pos.Center(),
            Width = Dim.Width(btnLogin) + Dim.Width(btnBack) + 1,
            Height = Dim.Height(btnLogin),
            Border = new Border()
            {
                BorderStyle = BorderStyle.None
            }
        };
        btns.Add(btnLogin, btnBack);

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
        Add(emailLabel, emailText, passwordLabel, passwordText, btns);
    }
}