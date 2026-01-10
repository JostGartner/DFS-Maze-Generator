using Labirint.Models;

namespace Labirint;

public class Izris
{
    private static int _statusLine = 0;
    private static int _menuLine = 0;

    public static void Inicializiraj(int visinaLabirinta)
    {
        _statusLine = visinaLabirinta + 1;
        _menuLine = visinaLabirinta + 2;
    }

    public static void Izrisi(Celica[,] grid)
    {
        int sirina = grid.GetLength(0);
        int visina = grid.GetLength(1);

        Console.SetCursorPosition(0, 0);

        var stringbilder = new System.Text.StringBuilder();

        for (int y = 0; y < visina; y++)
        {
            for (int x = 0; x < sirina; x++)
            {
                stringbilder.Append(ZnakStringUnicode(grid[x, y]));
            }
            stringbilder.AppendLine();
        }

        Console.Write(stringbilder.ToString());
    }

    public static void Animacija(Celica[,] grid, int ms = 30)
    {
        Izrisi(grid);
        Thread.Sleep(ms);
    }

    public static void Status(string status)
    {
        Console.SetCursorPosition(0, _statusLine);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write(new string(' ', Console.WindowWidth));
        Console.SetCursorPosition(0, _statusLine);
        Console.Write(status);
        Console.ResetColor();
    }
    public static void Meni(string meni)
    {
        Console.SetCursorPosition(0, _menuLine);
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write(new string(' ', Console.WindowWidth));
        Console.SetCursorPosition(0, _menuLine);
        Console.Write(meni);
        Console.ResetColor();
    }

    private static string ZnakString(Celica celica)
    {
        return celica.Vrsta switch
        {
            VrstaCelice.Zid => "██",
            VrstaCelice.Pot => "  ",
            VrstaCelice.Zacetek => "ZZ",
            VrstaCelice.Konec => "KK",
            VrstaCelice.Obiskana => "..",
            VrstaCelice.Resitev => "**",
            _ => "??"
        };
    }

    private static string ZnakStringUnicode(Celica celica)
    {
        return celica.Vrsta switch
        {
            VrstaCelice.Zid => "▒▒",
            VrstaCelice.Pot => "  ",
            VrstaCelice.Zacetek => "🐭",
            VrstaCelice.Konec => "🧀‍",
            VrstaCelice.Obiskana => "⭘ ",
            VrstaCelice.Resitev => "██",
            _ => "??"
        };
    }
}
