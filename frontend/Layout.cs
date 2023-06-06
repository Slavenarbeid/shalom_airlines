using shalom_airlines.Admin;
using shalom_airlines.Admin.Flights;
using shalom_airlines.User;
using Terminal.Gui;

namespace shalom_airlines;

public class Layout : Toplevel
{
    private Window _win;

    public static backend.Models.User LoggedInUser;

    private static readonly List<Window> WindowHistory = new();

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
                        new MenuItem("Profile", "See Profile", OpenWindow<Admin.Account.Profile>),
                        new MenuItem("Overview", "See All Users", OpenWindow<Admin.Account.Index>),
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

    internal static void OpenWindow(Window window, bool silent = false)
    {
        if (Application.Top is not Layout layout)
        {
            throw new Exception("Toplevel is not of class `Layout`.");
        }
        
        if (!silent) WindowHistory.Add(layout._win);
        
        layout.Remove(layout._win);
        layout._win = window;
        layout.Add(layout._win);
    }

    internal static void OpenWindow<TWindow>(params object?[]? args) where TWindow : Window
    {
        var window = (TWindow)Activator.CreateInstance(typeof(TWindow), args)!;
        window.Height = Dim.Fill();
        if (window == null)
            throw new Exception($"Class `{typeof(TWindow)}` is not of class `Window`.");

        OpenWindow(window);
    }

    internal static void OpenWindow<TWindow>() where TWindow : Window
    {
        OpenWindow<TWindow>(null);
    }

    internal static void Back()
    {
        OpenWindow(WindowHistory.Last(), true);
        WindowHistory.RemoveAt(WindowHistory.Count - 1);
    }
}