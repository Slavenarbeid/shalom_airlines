using shalom_airlines.Admin;
using shalom_airlines.Admin.Flights;
using shalom_airlines.User;
using Terminal.Gui;

namespace shalom_airlines;

public class Layout : Toplevel
{
    private Window _win;

    public static backend.Models.User LoggedInUser;

    public Layout(backend.Models.User user)
    {
        LoggedInUser = user;
        X = 0;
        Y = 0;
        Width = Dim.Fill();
        Height = Dim.Fill() - 1;

        MenuBar menu;
        switch (LoggedInUser.Level)
        {
            case "admin":
                _win = new AdminOverview();
                menu = new MenuBar(new[]
                {
                    new MenuBarItem("Menu", new[]
                    {
                        new MenuItem("Dashboard", "Main Overview", OpenWindow<AdminOverview>),
                    }),
                    new MenuBarItem("Flights", new[]
                    {
                        new MenuItem("Overview", "See all Flights", OpenWindow<Admin.Flights.Index>),
                        new MenuItem("Create", "Create a Flight", OpenWindow<Create>),
                    }),
                    new MenuBarItem("Account", new[]
                    {
                        new MenuItem("Profile", "See Profile", OpenWindow<User.Account.Profile>),
                    }),
                    new MenuBarItem("Logout", new[]
                    {
                        new MenuItem("Logout", "Logout Account", () =>
                        {
                            Application.Top.RequestStop();
                            Application.Run<MainMenu>();
                        })
                    })
                });
                    break;
            default:
                _win = new UserOverview();
                menu = new MenuBar(new[]
                {
                    new MenuBarItem("Menu", new[]
                    {
                        new MenuItem("Dashboard", "Main Overview", OpenWindow<UserOverview>),
                    }),
                    new MenuBarItem("Flights", new[]
                    {
                        new MenuItem("Overview", "See all Flights", OpenWindow<User.Flights.Index>),
                    }),
                    new MenuBarItem("Account", new[]
                    {
                        new MenuItem("Profile", "See Profile", OpenWindow<User.Account.Profile>),
                    }),
                    new MenuBarItem("Logout", new[]
                    {
                        new MenuItem("Logout", "Logout Account", () =>
                        {
                            Application.Top.RequestStop();
                            Application.Run<MainMenu>();
                        })
                    })
                });
                break;
        }
        Add(menu, _win);
    }

    public static void OpenWindow(Window window)
    {
        if (Application.Top is Layout layout)
        {
            layout.Remove(layout._win);
            layout._win = window;
            layout.Add(layout._win);
        }
        else
        {
            throw new Exception("Toplevel is not of class `Layout`.");
        }
    }

    public static void OpenWindow<TWindow>(params object?[]? args) where TWindow : Window
    {
        var window = (TWindow)Activator.CreateInstance(typeof(TWindow), args)!;
        if (window == null)
            throw new Exception($"Class `{typeof(TWindow)}` is not of class `Window`.");

        OpenWindow(window);
    }

    public static void OpenWindow<TWindow>() where TWindow : Window
    {
        OpenWindow<TWindow>(null);
    }
}