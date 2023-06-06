using backend.Controllers;
using backend.Models;
using NStack;
using Terminal.Gui;

namespace shalom_airlines.Admin.Account;

public class Index : Window
{
    public Index() : this(null)
    {
    }

    public Index(Dictionary<string, object>? filters)
    {
        Title = "Users";

        var SearchEmaillLabel = new Label()
        {
            Text = "Email: ",
        };

        string text = "";
        if (filters != null && filters.ContainsKey("Email"))
        {
            text = Convert.ToString(filters["Email"]);
        }

        var SearchEmailFieldText = new TextField("")
        {
            Text = text,
            X = Pos.Right(SearchEmaillLabel) + 1,
            Width = Dim.Percent(30),
        };

        // Field  Departure -> Arrival
        var SearchFirstNamelLabel = new Label()
        {
            Text = "Firstname: ",
            Y = Pos.Bottom(SearchEmaillLabel) + 1,
        };
        text = "";
        if (filters != null && filters.ContainsKey("FirstName"))
        {
            text = Convert.ToString(filters["FirstName"]);
        }

        var SearchFirstNameFieldText = new TextField("")
        {
            Text = text,
            Y = Pos.Bottom(SearchEmaillLabel) + 1,
            X = Pos.Right(SearchFirstNamelLabel) + 1,
            Width = Dim.Percent(30),
        };
        var SearchLastNameLabel = new Label()
        {
            Text = "Lastname: ",
            Y = Pos.Bottom(SearchEmaillLabel) + 1,
            X = Pos.Right(SearchFirstNameFieldText) + 1,
        };
        text = "";
        if (filters != null && filters.ContainsKey("LastName"))
        {
            text = Convert.ToString(filters["LastName"]);
        }

        var SearchLastNameFieldText = new TextField("")
        {
            Text = text,
            Y = Pos.Bottom(SearchEmaillLabel) + 1,
            X = Pos.Right(SearchLastNameLabel) + 1,
            Width = Dim.Percent(30),
        };

        // Button Reset
        var SearchResetButton = new Button()
        {
            Y = Pos.Bottom(SearchFirstNamelLabel) + 1,
            Text = "Reset",
        };
        SearchResetButton.Clicked += Layout.OpenWindow<Index>;

        List<backend.Models.User> userView = new List<backend.Models.User>();
        userView = filters == null ? backend.Models.User.All() : backend.Models.User.Search(filters);

        var list = new ListView(userView)
        {
            Y = Pos.Bottom(SearchResetButton) + 2,
            Width = Width,
            Height = Dim.Fill(),
        };

        // Search on text input
        void SearchAction()
        {
            Dictionary<string, object> newFilter = new();
            if (SearchEmailFieldText.Text != "")
            {
                if (int.TryParse((string)SearchEmailFieldText.Text, out int a))
                {
                    int SearchEmailValue = Convert.ToInt32(a);
                    newFilter.Add("Email", SearchEmailValue);
                }
            }

            if (SearchFirstNameFieldText.Text != "")
            {
                string SearchFirstNameValue = (string)SearchFirstNameFieldText.Text;
                newFilter.Add("FirstName", SearchFirstNameValue);
            }

            if (SearchLastNameFieldText.Text != "")
            {
                string SearchLastNameValue = (string)SearchLastNameFieldText.Text;
                newFilter.Add("LastName", SearchLastNameValue);
            }

            // Layout.OpenWindow<Index>(newFilter);
            var users = newFilter.Count > 0 ? backend.Models.User.Search(newFilter) : backend.Models.User.All();
            list.SetSource(users);
            list.Redraw(new Rect(new Point(list.GetAutoSize()), list.GetAutoSize()));
        }

        SearchEmailFieldText.TextChanged += _ => SearchAction();
        SearchFirstNameFieldText.TextChanged += _ => SearchAction();
        SearchLastNameFieldText.TextChanged += _ => SearchAction();

        list.OpenSelectedItem += f => { Layout.OpenWindow<Show>(f.Value); };

        Add(SearchEmaillLabel, SearchEmailFieldText, // ID search
            SearchFirstNamelLabel, SearchFirstNameFieldText, SearchLastNameFieldText,
            SearchLastNameLabel, // Departure -> Arrival search
            SearchResetButton, // Search buttons
            list);
    }
}