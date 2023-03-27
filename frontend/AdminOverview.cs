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
                new MenuItem("Create", "Create a Flight", () => Application.Run<CreateFlight>()),
                new MenuItem("test", "test", () => Application.Run<Test>())
                
            })
        });
        Add(menu);
    }
}