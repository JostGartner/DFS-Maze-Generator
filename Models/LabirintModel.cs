namespace Labirint.Models;

public class LabirintModel
{
    public int Sirina { get; set; }
    public int Visina { get; set; }
    public Celica[,] Grid { get; set; }
    public Celica Zacetek { get; set; }
    public Celica Konec { get; set; }

    public LabirintModel(int sirina, int visina)
    {
        Sirina = sirina;
        Visina = visina;
        Grid = new Celica[sirina, visina];

        for(int x = 0; x < sirina; x++)
        {
            for (int y = 0; y < visina; y++)
            {
                Grid[x, y] = new Celica(x, y);
            }
        }
        Zacetek = Grid[0, 0];
        Konec = Grid[sirina - 1, visina - 1];
    }
}