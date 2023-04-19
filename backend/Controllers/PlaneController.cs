using backend.Models;

namespace backend.Controllers;

public static class PlaneController
{
    // bool will become reservation?
    private static Dictionary<string, Dictionary<string, Dictionary<int, bool>>> _boeing737Layout =
        CreateSeatingLayout(4, 5, 10, 15);
    private static Dictionary<string, Dictionary<string, Dictionary<int, bool>>> _airbus330Layout =
        CreateSeatingLayout(4, 5, 10, 15);
    private static Dictionary<string, Dictionary<string, Dictionary<int, bool>>> _boeing787Layout = 
        CreateSeatingLayout(4, 5, 10, 15);

    public static List<Plane> Planes { get; } = new()
    {
        new Plane("Boeing 737", _boeing737Layout, "A small Boeing 737"),
        new Plane("Airbus 330", _airbus330Layout, "An small Airbus 330"),
        new Plane("Boeing 787", _boeing787Layout, "A big Boeing 787"),
    };

    public static Plane Create(string model, Dictionary<string, Dictionary<string, Dictionary<int, bool>>> seatsLayout, string info = "")
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
    
    /// <summary>
    /// Creates a seating layout for an airplane with specified number of rows and columns in different classes.
    /// </summary>
    /// <param name="numFirstClassRows">Number of rows in first class.</param>
    /// <param name="numFirstClassColumns">Number of columns in first class.</param>
    /// <param name="numBusinessClassColumns">Number of columns in business class.</param>
    /// <param name="numEconomyClassColumns">Number of columns in economy class.</param>
    /// <param name="rowLetters">Optional array of row letters. If not provided, default row letters A, B, C, D, E, F will be used.</param>
    /// <returns>A dictionary representing the seating layout with information about occupied/unoccupied seats.</returns>
    public static Dictionary<string, Dictionary<string, Dictionary<int, bool>>> CreateSeatingLayout(int numFirstClassRows, int numFirstClassColumns, int numBusinessClassColumns, int numEconomyClassColumns, string[]? rowLetters = null)
    {
        // Create the top-level dictionary to store the seating layout
        Dictionary<string, Dictionary<string, Dictionary<int, bool>>> seatingLayout = new Dictionary<string, Dictionary<string, Dictionary<int, bool>>>();

        // If rowLetters is not provided, use default row letters A-F
        rowLetters ??= new[] { "A", "B", "C", "D", "E", "F" };
        int numBusinessClassRows = rowLetters.Length;
        int numEconomyClassRows = rowLetters.Length;

        // Create seating layout for first class
        if (numFirstClassRows > 0)
        {
            Dictionary<string, Dictionary<int, bool>> firstClassRows = new Dictionary<string, Dictionary<int, bool>>();
            for (int i = 1; i <= numFirstClassRows; i++)
            {
                Dictionary<int, bool> rowSeats = new Dictionary<int, bool>();
                for (int j = 1; j <= numFirstClassColumns; j++)
                {
                    // Initialize all seats as available
                    rowSeats.Add(j, false); 
                }
                // Add the row with seat dictionary to the first class rows
                firstClassRows.Add("Row " + i, rowSeats); 
            }
            // Add the first class with rows to the seating layout
            seatingLayout.Add("First class", firstClassRows);
        }

        // Create seating layout for business class
        if (numBusinessClassColumns > 0 && numBusinessClassRows > 0)
        {
            Dictionary<string, Dictionary<int, bool>> businessClassRows = new Dictionary<string, Dictionary<int, bool>>();
            for (int i = 0; i < numBusinessClassRows; i++)
            {
                string rowLetter = rowLetters[i];
                Dictionary<int, bool> rowSeats = new Dictionary<int, bool>();
                for (int j = 1; j <= numBusinessClassColumns; j++)
                {
                    // Initialize all seats as available
                    rowSeats.Add(j, false);
                }
                // Add the row with seat dictionary to the business class rows
                businessClassRows.Add(rowLetter, rowSeats);
            }
            // Add the business class with rows to the seating layout
            seatingLayout.Add("Business class", businessClassRows);
        }

        // Create seating layout for economy class
        if (numEconomyClassColumns > 0 && numEconomyClassRows > 0)
        {
            Dictionary<string, Dictionary<int, bool>> economyClassRows = new Dictionary<string, Dictionary<int, bool>>();
            for (int i = 0; i < numEconomyClassRows; i++)
            {
                string rowLetter = rowLetters[i];
                Dictionary<int, bool> rowSeats = new Dictionary<int, bool>();
                for (int j = 1; j <= numEconomyClassColumns; j++)
                {
                    // Initialize all seats as available
                    rowSeats.Add(j, false);
                }
                // Add the row with seat dictionary to the economy class rows
                economyClassRows.Add(rowLetter, rowSeats);
            }
            // Add the economy class with rows to the seating layout
            seatingLayout.Add("Economy class", economyClassRows);
        }

        return seatingLayout;
    }
}