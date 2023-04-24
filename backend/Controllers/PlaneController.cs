using System.Data;
using backend.Models;

namespace backend.Controllers;

public static class PlaneController
{
    // bool will become reservation?
    private static DataTable _boeing737Layout =
        CreateSeatingLayout(4, 5, 10, 15);
    private static DataTable _airbus330Layout =
        CreateSeatingLayout(4, 5, 10, 15);
    private static DataTable _boeing787Layout = 
        CreateSeatingLayout(4, 5, 10, 15);

    public static List<Plane> Planes { get; } = new()
    {
        new Plane("Boeing 737", _boeing737Layout, "A small Boeing 737"),
        new Plane("Airbus 330", _airbus330Layout, "An small Airbus 330"),
        new Plane("Boeing 787", _boeing787Layout, "A big Boeing 787"),
    };

    public static Plane Create(string model, DataTable seatsLayout, string info = "")
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
    public static DataTable CreateSeatingLayout(int numFirstClassRows, int numFirstClassColumns, int numBusinessClassColumns, int numEconomyClassColumns, string[]? rowLetters = null)
    {
        // Create the DataTable to store the seating layout
        DataTable dtSeatingLayout = new DataTable();
        dtSeatingLayout.TableName = "SeatingLayout";
        dtSeatingLayout.Columns.Add("Class", typeof(string));
        dtSeatingLayout.Columns.Add("Row", typeof(string));
        dtSeatingLayout.Columns.Add("SeatNumber", typeof(int));
        dtSeatingLayout.Columns.Add("IsOccupied", typeof(bool));

        // If rowLetters is not provided, use default row letters A-F
        rowLetters ??= new[] { "A", "B", "C", "D", "E", "F" };
        int numBusinessClassRows = rowLetters.Length;
        int numEconomyClassRows = rowLetters.Length;

        // Create seating layout for first class
        if (numFirstClassRows > 0)
        {
            for (int i = 1; i <= numFirstClassRows; i++)
            {
                for (int j = 1; j <= numFirstClassColumns; j++)
                {
                    // Initialize all seats as available
                    DataRow dr = dtSeatingLayout.NewRow();
                    dr["Class"] = "First class";
                    dr["Row"] = "Row " + i;
                    dr["SeatNumber"] = j;
                    dr["IsOccupied"] = false;
                    dtSeatingLayout.Rows.Add(dr);
                }
            }
        }

        // Create seating layout for business class
        if (numBusinessClassColumns > 0 && numBusinessClassRows > 0)
        {
            for (int i = 0; i < numBusinessClassRows; i++)
            {
                string rowLetter = rowLetters[i];
                for (int j = 1; j <= numBusinessClassColumns; j++)
                {
                    // Initialize all seats as available
                    DataRow dr = dtSeatingLayout.NewRow();
                    dr["Class"] = "Business class";
                    dr["Row"] = rowLetter;
                    dr["SeatNumber"] = j;
                    dr["IsOccupied"] = false;
                    dtSeatingLayout.Rows.Add(dr);
                }
            }
        }

        // Create seating layout for economy class
        if (numEconomyClassColumns > 0 && numEconomyClassRows > 0)
        {
            for (int i = 0; i < numEconomyClassRows; i++)
            {
                string rowLetter = rowLetters[i];
                for (int j = 1; j <= numEconomyClassColumns; j++)
                {
                    // Initialize all seats as available
                    DataRow dr = dtSeatingLayout.NewRow();
                    dr["Class"] = "Economy class";
                    dr["Row"] = rowLetter;
                    dr["SeatNumber"] = j;
                    dr["IsOccupied"] = false;
                    dtSeatingLayout.Rows.Add(dr);
                }
            }
        }

        return dtSeatingLayout;
    }
}