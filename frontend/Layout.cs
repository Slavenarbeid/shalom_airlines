using shalom_airlines.Admin.Flights;
using Terminal.Gui;

namespace shalom_airlines;

public class Layout : Toplevel
{
    private Window _win = new AdminOverview();

    public Layout()
    {
        X = 0;
        Y = 0;
        Width = Dim.Fill();
        Height = Dim.Fill() - 1;

        
        var menu = new MenuBar(new[]
        {
            new MenuBarItem("Menu", new[]
            {
                new MenuItem("Dashboard", "Main Overview", () => OpenWindow(new AdminOverview())),
            }),
            new MenuBarItem("Flights", new[]
            {
                new MenuItem("Overview", "See all Flights", () => OpenWindow(new Admin.Flights.Index())),
                new MenuItem("Create", "Create a Flight", () => OpenWindow(new Create())),
            })
        });
        
        Add(menu, _win);
    }

    private void OpenWindow(Window window)
    {
        Remove(_win);
        _win = window;
        Add(_win);
    }
}