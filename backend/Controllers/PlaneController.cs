using System.Data;
using backend.Models;

namespace backend.Controllers;

public static class PlaneController
{
    private static Dictionary<string, DataTable> _boeing737Layout =
        CreateSeatingLayout(
            new []{"A", "B", "C", "D"}, 
            5, 
            new []{"A", "B", "C", "D", "E", "F"},
            10,
            new []{"A", "B", "C", "D", "E", "F"},
            15);
    private static Dictionary<string, DataTable> _airbus330Layout =
        CreateSeatingLayout(
            new []{"A", "B", "C", "D"}, 
            5, 
            new []{"A", "B", "C", "D", "E", "F"},
            10,
            new []{"A", "B", "C", "D", "E", "F"},
            15);    private static Dictionary<string, DataTable> _boeing787Layout = 
        CreateSeatingLayout(
            new []{"A", "B", "C", "D"}, 
            5, 
            new []{"A", "B", "C", "D", "E", "F"},
            10,
            new []{"A", "B", "C", "D", "E", "F"},
            15);
    
    public static List<Plane> Planes { get; } = new()
    {
        new Plane("Boeing 737", _boeing737Layout, "A small Boeing 737"),
        new Plane("Airbus 330", _airbus330Layout, "An small Airbus 330"),
        new Plane("Boeing 787", _boeing787Layout, "A big Boeing 787"),
    };

    public static Plane Create(string model, Dictionary<string, DataTable> seatsLayout, string info = "")
    {
        Plane plane = new Plane(model, seatsLayout, info);

        JsonHandle<Plane> jsonHandle = new JsonHandle<Plane>("Planes");
        jsonHandle.AddToJson(plane);
        
        return plane;
    }

    public static Plane? Update(string modelToUpdate, string info)
    {
        Plane planeToEdit = Planes.Find(plane => plane.Model == modelToUpdate);
        if (planeToEdit == null) return null;
        planeToEdit.Info = info;

        return planeToEdit;
    }

    public static void Delete(string modelToDelete)
    {
        Planes.Remove(Planes.Find(plane => plane.Model == modelToDelete));
    }
    
    public static Dictionary<string, DataTable> CreateSeatingLayout(
        string[] firstClassRowLetters,
        int numFirstClassColumns,
        string[] businessClassRowLetters,
        int numBusinessClassColumns,
        string[] economyClassRowLetters,
        int numEconomyClassColumns)
    {
        // Create data tables for each seating class
        var firstClassTable = new DataTable();
        var businessClassTable = new DataTable();
        var economyClassTable = new DataTable();

        // Add columns to the data tables
        firstClassTable.Columns.Add("Row", typeof(string));
        for (int i = 1; i <= numFirstClassColumns; i++)
        {
            firstClassTable.Columns.Add(i.ToString(), typeof(bool));
        }
        businessClassTable.Columns.Add("Row", typeof(string));
        for (int i = 1; i <= numBusinessClassColumns; i++)
        {
            businessClassTable.Columns.Add(i.ToString(), typeof(bool));
        }
        economyClassTable.Columns.Add("Row", typeof(string));
        for (int i = 1; i <= numEconomyClassColumns; i++)
        {
            economyClassTable.Columns.Add(i.ToString(), typeof(bool));
        }

        // Add rows to the data tables for each seating class
        foreach (string letter in firstClassRowLetters)
        {
            var row = firstClassTable.NewRow();
            row["Row"] = letter;
            for (int j = 1; j <= numFirstClassColumns; j++)
            {
                row[j.ToString()] = false;
            }
            firstClassTable.Rows.Add(row);
        }

        foreach (string letter in businessClassRowLetters)
        {
            var row = businessClassTable.NewRow();
            row["Row"] = letter;
            for (int j = 1; j <= numBusinessClassColumns; j++)
            {
                row[j.ToString()] = false;
            }
            businessClassTable.Rows.Add(row);
        }

        foreach (string letter in economyClassRowLetters)
        {
            var row = economyClassTable.NewRow();
            row["Row"] = letter;
            for (int j = 1; j <= numEconomyClassColumns; j++)
            {
                row[j.ToString()] = false;
            }
            economyClassTable.Rows.Add(row);
        }

        // Create the dictionary and add the data tables
        var seatingLayout = new Dictionary<string, DataTable>();
        seatingLayout.Add("First Class", firstClassTable);
        seatingLayout.Add("Business Class", businessClassTable);
        seatingLayout.Add("Economy Class", economyClassTable);

        return seatingLayout;
    }
}