using Labirint.Models;

namespace Labirint;

public class Generator
{
    public void Generiraj(LabirintModel labirint)
    {
        NarediPot(labirint, 1, 1);
        labirint.Zacetek = labirint.Grid[1, 1];
        labirint.Zacetek.Vrsta = VrstaCelice.Zacetek;

        labirint.Konec = labirint.Grid[labirint.Sirina - 2, labirint.Visina - 2];
        labirint.Konec.Vrsta = VrstaCelice.Konec;
    }

    private void NarediPot(LabirintModel labirint, int x, int y)
    {
        labirint.Grid[x, y].Vrsta = VrstaCelice.Pot;
        labirint.Grid[x, y].Obiskana = true;

        Izris.Animacija(labirint.Grid, 30);

        Smer[] smeri =
        {
            new Smer(0, -2),
            new Smer(0,  2),
            new Smer(-2, 0),
            new Smer(2,  0)
        };

        smeri.Shuffle();

        foreach (Smer smer in smeri)
        {
            int nx = x + smer.SmerX;
            int ny = y + smer.SmerY;

            if (AliJeVeljavno(labirint, nx, ny) && !labirint.Grid[nx, ny].Obiskana)
            {
                labirint.Grid[x + smer.SmerX / 2, y + smer.SmerY / 2].Vrsta = VrstaCelice.Pot;
                Izris.Animacija(labirint.Grid, 30);
                NarediPot(labirint, nx, ny);
            }
        }
    }

    private bool AliJeVeljavno(LabirintModel labirint, int x, int y)
    {
        return x > 0 && x < labirint.Sirina - 1 && y > 0 && y < labirint.Visina - 1;
    }
}

class Smer
{
    public int SmerX { get; }
    public int SmerY { get; }

    public Smer(int smerX, int smerY)
    {
        SmerX = smerX;
        SmerY = smerY;
    }
}

public static class GeneratorRazsiritve
{
    private static readonly Random _random = new();

    public static void Shuffle<T>(this T[] polje)
    {
        for (int i = polje.Length - 1; i > 0; i--)
        {
            int j = _random.Next(i + 1);
            (polje[i], polje[j]) = (polje[j], polje[i]);
        }
    }
}
