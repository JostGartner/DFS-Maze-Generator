namespace Labirint.Models;

public enum VrstaCelice
{
    Zid,
    Pot,
    Zacetek,
    Konec,
    Obiskana,
    Resitev
}

public class Celica
{
    public int X { get; set; }
    public int Y { get; set; }
    public bool Obiskana { get; set; }
    public VrstaCelice Vrsta { get; set; }

    public Celica(int x, int y)
    {
        X = x;
        Y = y;
        Vrsta = VrstaCelice.Zid;
        Obiskana = false;
    }
}
