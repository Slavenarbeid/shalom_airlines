using Terminal.Gui;

namespace shalom_airlines;

public class MainMenu : Window
{
    public MainMenu()
    {
        Colors.Base.Normal = Application.Driver.MakeAttribute(Color.White, Color.Black);

        Title = "Main Menu";

        var welcomeMessage = new Label()
        {
            Text = @"____________       __________                    
___    |__(_)_________  /__(_)___________________
__  /| |_  /__  ___/_  /__  /__  __ \  _ \_  ___/
_  ___ |  / _  /   _  / _  / _  / / /  __/(__  ) 
/_/  |_/_/  /_/    /_/  /_/  /_/ /_/\___//____/  
                                                 ",
            Y = Pos.Center(),
            X = Pos.Center()
        };

        var btnLogin = new Button()
        {
            Text = "Login",
            Y = Pos.Bottom(welcomeMessage) + 1,
            // center the login button horizontally
            X = Pos.Percent(38),
            IsDefault = true,
        };
        
        var btnRegister = new Button()
        {
            Text = "Register",
            Y = Pos.Bottom(welcomeMessage) + 1,
            X = Pos.Right(btnLogin) + 1,
            IsDefault = true,
        };

        btnLogin.Clicked += () =>
        {
            Application.RequestStop();
            Application.Run<Login>();
        };
        
        btnRegister.Clicked += () =>
        {
            Application.RequestStop();
            Application.Run<Register>();
        };

        Add(welcomeMessage, btnLogin, btnRegister);
    }
}