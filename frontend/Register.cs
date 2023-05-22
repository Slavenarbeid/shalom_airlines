﻿using backend.Controllers;
using Terminal.Gui;

namespace shalom_airlines;

public class Register : Window
{
    public Register()
    {
        Title = "Example App (Ctrl+Q to quit)";

        // Create input components and labels
        var emailLabel = new Label()
        {
            Text = "Email:",
            Y = Pos.Center()
        };

        var emailText = new TextField("")
        {
            // Position text field adjacent to the label
            X = Pos.Right(emailLabel) + 1,
            Y = Pos.Center(),

            // Fill remaining horizontal space
            Width = Dim.Fill(),
        };
        
        var firstNameLabel = new Label()
        {
            Text = "First Name:",
            X = Pos.Left(emailLabel),
            Y = Pos.Bottom(emailLabel) + 1
        };

        var firstNameText = new TextField("")
        {
            X = Pos.Left(emailText),
            Y = Pos.Top(firstNameLabel),
            Width = Dim.Fill(),
        };
        
        var lastNameLabel = new Label()
        {
            Text = "Last Name:",
            X = Pos.Left(firstNameLabel),
            Y = Pos.Bottom(firstNameLabel) + 1
        };

        var lastNameText = new TextField("")
        {
            X = Pos.Left(firstNameText),
            Y = Pos.Top(lastNameLabel),
            Width = Dim.Fill(),
        };

        var passwordLabel = new Label()
        {
            Text = "Password:",
            X = Pos.Left(lastNameLabel),
            Y = Pos.Bottom(lastNameLabel) + 1
        };

        var passwordText = new TextField("")
        {
            Secret = true,
            // align with the text box above
            X = Pos.Left(lastNameText),
            Y = Pos.Top(passwordLabel),
            Width = Dim.Fill(),
        };
        
        var passwordAuthLabel = new Label()
        {
            Text = "Password:",
            X = Pos.Left(passwordLabel),
            Y = Pos.Bottom(passwordLabel) + 1
        };

        var passwordAuthText = new TextField("")
        {
            Secret = true,
            // align with the text box above
            X = Pos.Left(passwordText),
            Y = Pos.Top(passwordAuthLabel),
            Width = Dim.Fill(),
        };
        
        // Create login button
        var btnRegister = new Button()
        {
            Text = "Register",
            Y = Pos.Bottom(passwordAuthText) + 1,
            // center the login button horizontally
            X = Pos.Center(),
            IsDefault = true,
        };

        // Field validation
        if (backend.Models.User.EmailUsedBefore((string)emailText.Text))
        {
            MessageBox.ErrorQuery("Creating User", "Email used before", "Ok");
            return;
        }
        
        // When login button is clicked display a message popup
        btnRegister.Clicked += () =>
        {
            if (passwordText.Text != passwordAuthText.Text)
            {
                MessageBox.ErrorQuery("Creating User", "Passwords do not match", "Ok");
                return;
            }
            // create user
            UserController.Create((string)emailText.Text, (string)firstNameText.Text, (string)lastNameText.Text, (string)passwordText.Text);
            MessageBox.Query("Creating User", "User Created", "Ok");
            Application.Run<MainMenu>();
        };

        // Add the views to the Window
        Add(emailLabel, emailText, firstNameLabel, firstNameText, lastNameLabel, lastNameText, passwordLabel, passwordText, passwordAuthLabel, passwordAuthText, btnRegister);
    }
}