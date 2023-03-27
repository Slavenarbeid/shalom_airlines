using Terminal.Gui;

namespace shalom_airlines;

public class MainMenu : Window
{

    public MainMenu()
    {
        Colors.Base.Normal = Application.Driver.MakeAttribute (Color.White, Color.Black);
        
        Title = "Main Menu";
        
        var welcomeMessage = new Label () { 
            Text = @"___       __    ______                                 _____            _____________        ______                      ____________       __________                   ______   
__ |     / /_______  /__________________ ________      __  /______      __  ___/__  /_______ ___  /____________ ___      ___    |__(_)_________  /__(_)_____________________  /   
__ | /| / /_  _ \_  /_  ___/  __ \_  __ `__ \  _ \     _  __/  __ \     _____ \__  __ \  __ `/_  /_  __ \_  __ `__ \     __  /| |_  /__  ___/_  /__  /__  __ \  _ \_  ___/_  /    
__ |/ |/ / /  __/  / / /__ / /_/ /  / / / / /  __/     / /_ / /_/ /     ____/ /_  / / / /_/ /_  / / /_/ /  / / / / /     _  ___ |  / _  /   _  / _  / _  / / /  __/(__  ) /_/     
____/|__/  \___//_/  \___/ \____//_/ /_/ /_/\___/      \__/ \____/      /____/ /_/ /_/\__,_/ /_/  \____//_/ /_/ /_/      /_/  |_/_/  /_/    /_/  /_/  /_/ /_/\___//____/ (_)      
                                                                                                                                                                                  ", 
            Y = Pos.Center (),
            X = Pos.Center ()
        };
        
        var btnLogin = new Button () {
            Text = "Login",
            Y = Pos.Bottom(welcomeMessage) + 1,
            // center the login button horizontally
            X = Pos.Center (),
            IsDefault = true,
        };
        
        btnLogin.Clicked += () =>
        {
            Application.RequestStop();
            Application.Run<Login>();
        };

        Add(welcomeMessage, btnLogin);
    }
}