using backend.Controllers;
using Terminal.Gui;

namespace shalom_airlines;

public class Register : Window
{
    public Register()
    {
        Title = "Register";

        // Create input components and labels
        var emailLabel = new Label()
        {
            Text = "Email:",
        };

        var emailText = new TextField("")
        {
            Y = Pos.Bottom(emailLabel),
            Width = Dim.Fill(),
        };
        
        var firstNameLabel = new Label()
        {
            Text = "First Name:",
            X = Pos.Left(emailLabel),
            Y = Pos.Bottom(emailLabel) + 2
        };

        var firstNameText = new TextField("")
        {
            Y = Pos.Bottom(firstNameLabel),
            Width = Dim.Fill(),
        };
        
        var lastNameLabel = new Label()
        {
            Text = "Last Name:",
            X = Pos.Left(firstNameLabel),
            Y = Pos.Bottom(firstNameLabel) + 2
        };

        var lastNameText = new TextField("")
        {
            Y = Pos.Bottom(lastNameLabel),
            Width = Dim.Fill(),
        };

        var passwordLabel = new Label()
        {
            Text = "Password:",
            X = Pos.Left(lastNameLabel),
            Y = Pos.Bottom(lastNameLabel) + 2
        };

        var passwordText = new TextField("")
        {
            Secret = true,
            Y = Pos.Bottom(passwordLabel),
            Width = Dim.Fill(),
        };
        
        var passwordAuthLabel = new Label()
        {
            Text = "Password:",
            X = Pos.Left(passwordLabel),
            Y = Pos.Bottom(passwordLabel) + 2
        };

        var passwordAuthText = new TextField("")
        {
            Secret = true,
            Y = Pos.Bottom(passwordAuthLabel),
            Width = Dim.Fill(),
        };
        
        // Create login button
        var btnRegister = new Button()
        {
            Text = "Register",
            IsDefault = true,
        };

        // When login button is clicked display a message popup
        void BtnRegisterClickedHandler()
        {
            // Field validation
            if (backend.Models.User.EmailUsedBefore((string)emailText.Text))
            {
                MessageBox.ErrorQuery("Creating User", "Email used before", "Ok");
                return;
            }
            
            if (passwordText.Text != passwordAuthText.Text)
            {
                MessageBox.ErrorQuery("Creating User", "Passwords do not match", "Ok");
                return;
            }

            btnRegister.Clicked -= BtnRegisterClickedHandler;
            // create user
            UserController.Create((string)emailText.Text, (string)firstNameText.Text, (string)lastNameText.Text, (string)passwordText.Text);
            MessageBox.Query("Creating User", "User Created", "Ok");
            Application.RequestStop();
            Application.Top.RemoveAll();
            Application.Run<MainMenu>();
        }
        btnRegister.Clicked += BtnRegisterClickedHandler;
        
        var btnBack = new Button()
        {
            Text = "Back",
            X = Pos.Right(btnRegister),
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
            Y = Pos.Bottom(passwordAuthText) + 1,
            X = Pos.Center(),
            Width = Dim.Width(btnRegister) + Dim.Width(btnBack) + 1,
            Height = Dim.Height(btnRegister),
            Border = new Border()
            {
                BorderStyle = BorderStyle.None
            }
        };
        btns.Add(btnRegister, btnBack);

        // Add the views to the Window
        Add(emailLabel, emailText, firstNameLabel, firstNameText, lastNameLabel, lastNameText, passwordLabel, passwordText, passwordAuthLabel, passwordAuthText, btns);
    }
}