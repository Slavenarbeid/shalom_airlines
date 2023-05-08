using System.Data;
using backend.Models;

namespace backend.Controllers;

public static class PlaneController
{
    // bool will become reservation?
    private static Dictionary<string, DataTable> _boeing737Layout =
        CreateSeatingLayout(4, 5, 10, 15);
    private static Dictionary<string, DataTable> _airbus330Layout =
        CreateSeatingLayout(4, 5, 10, 15);
    private static Dictionary<string, DataTable> _boeing787Layout = 
        CreateSeatingLayout(4, 5, 10, 15);

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
    
    public static Dictionary<string, DataTable> CreateSeatingLayout(int numFirstClassRows, int numFirstClassColumns, int numBusinessClassColumns, int numEconomyClassColumns, string[]? rowLetters = null)
    {
        var seatLayout = new Dictionary<string, DataTable>();
        var firstClassTable = new DataTable("FirstClass");
        var businessClassTable = new DataTable("BusinessClass");
        var economyClassTable = new DataTable("EconomyClass");
        var allTables = new DataTable[] { firstClassTable, businessClassTable, economyClassTable };
        var numColumns = new int[] { numFirstClassColumns, numBusinessClassColumns, numEconomyClassColumns };
        var rowLettersPerClass = rowLetters ?? new string[] { "A", "B", "C", "D", "E", "F" };

        for (var i = 0; i < allTables.Length; i++)
        {
            var table = allTables[i];
            var columns = numColumns[i];
            var rowLetter = rowLettersPerClass[i];

            table.Columns.Add("Row", typeof(string));
            for (var j = 1; j <= columns; j++)
            {
                table.Columns.Add($"Seat{j}", typeof(bool));
            }

            for (var k = 1; k <= (i == 0 ? numFirstClassRows : 20); k++) // separate loop for first class rows
            {
                var newRow = table.NewRow();
                newRow["Row"] = rowLetter;
                for (var l = 1; l <= columns; l++)
                {
                    newRow[$"Seat{l}"] = false;
                }
                table.Rows.Add(newRow);
                rowLetter = NextChar(rowLetter);
            }
        }

        seatLayout.Add("FirstClass", firstClassTable);
        seatLayout.Add("BusinessClass", businessClassTable);
        seatLayout.Add("EconomyClass", economyClassTable);
        return seatLayout;
    }

    private static string NextChar(string currentChar)
    {
        var currentAscii = Convert.ToInt32(currentChar[0]);
        var nextAscii = currentAscii + 1;
        return Convert.ToChar(nextAscii).ToString();
    }
}