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
            IsDefault = true,
        };
        
        var btnRegister = new Button()
        {
            Text = "Register",
            X = Pos.Right(btnLogin) + 1,
            IsDefault = true,
        };

        var btns = new FrameView()
        {
            Y = Pos.Bottom(welcomeMessage) + 1,
            X = Pos.Center(),
            Width = Dim.Width(btnLogin) + Dim.Width(btnRegister) + 1,
            Height = Dim.Height(btnLogin),
            Border = new Border()
            {
                BorderStyle = BorderStyle.None
            }
        };
        btns.Add(btnLogin, btnRegister);

        void BtnLoginClickedHandler()
        {
            btnLogin.Clicked -= BtnLoginClickedHandler;
            Application.RequestStop();
            Application.Top.RemoveAll();
            Application.Run<Login>();
        }
        btnLogin.Clicked += BtnLoginClickedHandler;

        void BtnRegisterClickedHandler()
        {
            btnRegister.Clicked -= BtnRegisterClickedHandler;
            Application.RequestStop();
            Application.Top.RemoveAll();
            Application.Run<Register>();
        }
        btnRegister.Clicked += BtnRegisterClickedHandler;

        Add(welcomeMessage, btns);
    }
}