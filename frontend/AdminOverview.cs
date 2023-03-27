using Terminal.Gui;
namespace shalom_airlines;

public class AdminOverview : Window
{
    public AdminOverview()
    {
        Title = "Admin Overview";
        
        var menu = new MenuBar(new[]
        {
            new MenuBarItem("Menu", new[]
            {
                new MenuItem("Create", "Create a Flight", () => Application.Run<CreateFlight>())
            })
        });
        
        
        
        Add(menu);
    }
}