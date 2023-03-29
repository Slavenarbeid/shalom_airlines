using Terminal.Gui;

namespace shalom_airlines;

public class Layout : Toplevel
{
    public Layout()
    {
        X = 0;
        Y = 0;
        Width = Dim.Fill();
        Height = Dim.Fill() - 1;
    }
}