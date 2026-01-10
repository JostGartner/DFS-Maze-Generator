using Labirint.Models;

namespace Labirint;

public class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.CursorVisible = false;
        Console.Title = "DFS Labirint";

        IResitev dfsResitev = new DfsResitev();

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("GENERATOR LABIRINTA\n");
        Console.ResetColor();

        var (sirina, visina) = IzberiVelikost();

        if (!PreveriVelikost(sirina, visina))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Napaka: Izbrana velikost labirinta je prevelika za trenutno okno. Prosim maksimizirajte konzolo.");
            Console.ResetColor();

            Console.WriteLine("\nPritisnite katerokoli tipko za nadaljevanje...");
            Console.ReadKey();
            Console.Clear();
            Main(args);
            return;
        }

        Console.Clear();

        var labirint = new LabirintModel(sirina, visina);
        bool jeResen = false;

        Izris.Inicializiraj(visina);
        Izris.Status($"Generiranje labirinta ({sirina} x {visina})...");

        var generator = new Generator();
        generator.Generiraj(labirint);
        Izris.Animacija(labirint.Grid, 10);

        Izris.Status("Labirint uspešno generiran!");
        Izris.Meni("[D] Reši z DFS    [M] Meni");

        while (true)
        {
            var key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.D)
            {
                if (jeResen)
                {
                    Izris.Status("Labirint je že rešen.");
                    continue;
                }

                Izris.Status($"Reševanje labirinta z algoritmom {dfsResitev.Ime}...");
                Izris.Meni("");
                dfsResitev.Resi(labirint.Grid, grid => Izris.Animacija(grid, 30));
                jeResen = true;

                Izris.Status("Reševanje končano!");
                Izris.Meni("[O] Odstrani obiskano    [M] Meni");
            }
            else if (key == ConsoleKey.O)
            {
                labirint.Grid.PocistiObiskano();
                Izris.Animacija(labirint.Grid, 10);
            }

            else if (key == ConsoleKey.M)
            {
                Console.Clear();
                Main(args);
                return;
            }
        }
    }

    static (int sirina, int visina) IzberiVelikost()
    {
        int startLine = Console.CursorTop;

        while (true)
        {
            PocistiVrstico(startLine);
            PocistiVrstico(startLine + 1);
            PocistiVrstico(startLine + 2);

            Console.SetCursorPosition(0, startLine);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("[M] Majhen    [S] Srednji    [V] Velik    [C] Po meri\n");
            Console.ResetColor();

            Console.Write("VAŠA IZBIRA: ");
            string? izbira = Console.ReadLine()?.Trim().ToUpper();

            switch (izbira)
            {
                case "M":
                    return (21, 21);

                case "S":
                    return (31, 31);

                case "V":
                    return (41, 41);

                case "C":
                    int sirina = PreberiLihoStevilo("Vnesite širino labirinta: ");
                    int visina = PreberiLihoStevilo("Vnesite višino labirinta: ");
                    return (sirina, visina);

                default:
                    IzpisiNapako(startLine + 3, "Napaka: Izberite M, S, V ali C.");
                    continue;
            }
        }
    }

    static bool PreveriVelikost(int sirina, int visina)
    {
        int nujnaSirina = sirina * 2;
        int nujnaVisina = visina + 2;

        return Console.WindowWidth >= nujnaSirina && Console.WindowHeight > nujnaVisina;
    }

    static int PreberiLihoStevilo(string sporocilo)
    {
        int zacetnaVrstica = Console.CursorTop;
        int vrednost;

        while (true)
        {
            PocistiVrstico(zacetnaVrstica);
            Console.SetCursorPosition(0, zacetnaVrstica);
            Console.Write(sporocilo);

            string? vnos = Console.ReadLine();

            if (!int.TryParse(vnos, out vrednost))
            {
                IzpisiNapako(zacetnaVrstica + 1, "Napaka: Vnesite veljavno celo število.");
                continue;
            }

            if (vrednost < 5)
            {
                IzpisiNapako(zacetnaVrstica + 1, "Napaka: Število naj bo vsaj 5.");
                continue;
            }

            if (vrednost % 2 == 0)
            {
                IzpisiNapako(zacetnaVrstica + 1, "Napaka: Vnesite liho število.");
                continue;
            }

            PocistiVrstico(zacetnaVrstica + 1);
            Console.SetCursorPosition(0, zacetnaVrstica + 1);

            return vrednost;
        }
    }

    static void PocistiVrstico(int vrstica)
    {
        Console.SetCursorPosition(0, vrstica);
        Console.Write(new string(' ', Console.WindowWidth));
    }

    static void IzpisiNapako(int vrstica, string sporocilo)
    {
        PocistiVrstico(vrstica);
        Console.SetCursorPosition(0, vrstica);

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(sporocilo);
        Console.ResetColor();
    }
}
