using Labirint.Models;

namespace Labirint;

public interface IResitev
{
    string Ime { get; }
    bool Resi(Celica[,] grid, Action<Celica[,]>? obisk = null);
}

public class DfsResitev : IResitev
{
    public string Ime => "Depth-First Search (DFS)";

    public static (int x, int y) NajdiZacetek(Celica[,] grid)
    {
        int sirina = grid.GetLength(0);
        int visina = grid.GetLength(1);

        for (int y = 0; y < visina; y++)
            for (int x = 0; x < sirina; x++)
                if (grid[x, y].Vrsta == VrstaCelice.Zacetek)
                    return (x, y);

        throw new Exception("Začetek ni najden!");
    }

    public bool Resi(Celica[,] grid, Action<Celica[,]>? obisk = null)
    {
        foreach (var c in grid)
            c.Obiskana = false;

        var (startX, startY) = NajdiZacetek(grid);

        return Dfs(grid, startX, startY, obisk);
    }

    public bool Dfs(Celica[,] grid, int x, int y, Action<Celica[,]>? obisk)
    {
        int sirina = grid.GetLength(0);
        int visina = grid.GetLength(1);

        if (x < 0 || y < 0 || x >= sirina || y >= visina)
            return false;

        Celica c = grid[x, y];

        if (c.Vrsta == VrstaCelice.Zid || c.Obiskana)
            return false;

        if (c.Vrsta == VrstaCelice.Pot)
            c.Vrsta = VrstaCelice.Obiskana;
        
        obisk?.Invoke(grid);

        if (c.Vrsta == VrstaCelice.Konec)
            return true;

        c.Obiskana = true;

        int[] smerX = { 0, 1, 0, -1 };
        int[] smerY = { -1, 0, 1, 0 };

        for (int i = 0; i < 4; i++)
        {
            if (Dfs(grid, x + smerX[i], y + smerY[i], obisk))
            {
                if (c.Vrsta != VrstaCelice.Zacetek &&
                c.Vrsta != VrstaCelice.Konec)
                {
                    c.Vrsta = VrstaCelice.Resitev;
                    obisk?.Invoke(grid);
                }

                return true;
            }
        }

        return false;
    }
}

public static class DfsRazsiritve
{
    public static void PocistiObiskano(this Celica[,] grid)
    {
        foreach (var c in grid)
        {
            if (c.Vrsta == VrstaCelice.Obiskana)
            {
                c.Vrsta = VrstaCelice.Pot;
            }
        }
    }
}
