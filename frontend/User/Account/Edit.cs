using backend.Models;
using backend.Controllers;
using Terminal.Gui;

namespace shalom_airlines.User.Account;

public class EditUser : Window
{
    public EditUser()
    {
        var user = Layout.LoggedInUser;
        Title = $"Edit User: {user.FirstName} {user.Lastname}";

        var emailLabel = new Label()
        {
            Text = "Email:",
        };

        var emailText = new TextField("")
        {
            Text = user.Email,
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
            Text = user.FirstName,
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
            Text = user.Lastname,
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
            Text = "Password Auth:",
            X = Pos.Left(passwordLabel),
            Y = Pos.Bottom(passwordLabel) + 2
        };

        var passwordAuthText = new TextField("")
        {
            Secret = true,
            Y = Pos.Bottom(passwordAuthLabel),
            Width = Dim.Fill(),
        };

        // Create edit button
        var btnEdit = new Button()
        {
            Text = "Edit",
            Y = Pos.Bottom(passwordAuthText) + 2,
            X = Pos.Center(),
            IsDefault = true,
        };

        btnEdit.Clicked += () =>
        {
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

            backend.Models.User newUser = UserController.Update(user, (string)emailText.Text, (string)firstNameText.Text, (string)lastNameText.Text,
                (string)passwordText.Text);
            
            MessageBox.Query("Editing User", "User Edited", "Ok");
            Layout.LoggedInUser = newUser;
            Layout.OpenWindow<Profile>();
        };

        Add(emailLabel, emailText, firstNameLabel, firstNameText, lastNameLabel, lastNameText, passwordLabel,
            passwordText, passwordAuthLabel, passwordAuthText, btnEdit);
    }
}