namespace tanciskola;

public class Tanciskola
{
    public string Tanc { get; set; }
    private string[] par = new string[2];
    public string[] Par
    {
        get { return par; }
        set { par = value; }
    }
}
public class Program
{
    static List<Tanciskola> list = new List<Tanciskola>();
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Feladat1();
        Feladat2();
        Feladat3();
        Feladat4();
        Feladat5();
        Feladat6();
        Feladat7();

        Console.ReadKey();
    }
    private static void Feladat1()
    {
        string[] data = File.ReadAllLines(@"tancrend.txt");

        for (int i = 0; i < data.Length - 3; i += 3)
        {
            Tanciskola uj = new Tanciskola();

            uj.Tanc = data[i];
            uj.Par[0] = data[i + 1];
            uj.Par[1] = data[i + 2];

            list.Add(uj);
        }
    }
    private static void Feladat2()
    {
        Console.WriteLine("2. feladat");
        Console.WriteLine("Az elsőként bemutatott tánc neve: {0}", list.First().Tanc);
        Console.WriteLine("Az utolsóként bemutatott tánc neve: {0}", list.Last().Tanc);
        Console.WriteLine();
    }
    private static void Feladat3()
    {
        Console.WriteLine("3. feladat");
        Console.WriteLine("Sambát {0} pár mutatott be.", list.Where(x => x.Tanc == "samba").Count());
        Console.WriteLine();
    }
    private static void Feladat4()
    {
        Console.WriteLine("4. feladat");

        var szures = list.Where(x => x.Par.Contains("Vilma")).ToList();

        foreach (var item in szures)
        {
            Console.WriteLine(item.Tanc);
        }

        Console.WriteLine();
    }
    private static void Feladat5()
    {
        Console.WriteLine("5. feladat");

        Console.Write("Adja meg a tánc nevét: ");
        string bTanc = Console.ReadLine();

        var szures = list.Where(x => x.Par.Contains("Vilma") && x.Tanc == bTanc).ToList();

        if (szures.Count == 0) Console.WriteLine($"Vilma nem táncolt {bTanc}-t.");
        else Console.WriteLine($"A {bTanc} bemutatóján Vilma párja {szures[0].Par[1]} volt.");

        Console.WriteLine();
    }
    private static void Feladat6()
    {
        StreamWriter sw = new StreamWriter(@"szereplok.txt");

        var lanyok = list.GroupBy(x => x.Par[0]).OrderBy(g => g.Key).ToList();
        var fiuk = list.GroupBy(x => x.Par[1]).OrderBy(g => g.Key).ToList();

        sw.Write("Lányok: ");
        foreach (var item in lanyok)
        {
            if(lanyok.Last() == item) sw.Write($"{item.Key}");
            else sw.Write($"{item.Key}, ");
        }

        sw.Write("\nFiúk: ");
        foreach (var item in fiuk)
        {
            if (fiuk.Last() == item) sw.Write($"{item.Key}");
            else sw.Write($"{item.Key}, ");
        }

        sw.Close();
    }
    private static void Feladat7()
    {
        Console.WriteLine("7. feladat");

        int maxL = 0, maxF = 0;
        var lanyok = list.GroupBy(x => x.Par[0]).OrderByDescending(g => g.Count()).ToList();
        var fiuk = list.GroupBy(x => x.Par[1]).OrderByDescending(g => g.Count()).ToList();

        Console.WriteLine("A legtöbbet szerepelt lányok: ");
        for (int i = 0; i < lanyok.Count; i++)
        {
            if (lanyok[i].Count() >= maxL)
            {
                maxL = lanyok[i].Count();
                Console.WriteLine(lanyok[i].Key);
            }
        }

        Console.WriteLine("\nA legtöbbet szerepelt fiúk: ");
        for (int i = 0; i < fiuk.Count; i++)
        {
            if (fiuk[i].Count() >= maxF)
            {
                maxF = fiuk[i].Count();
                Console.WriteLine(fiuk[i].Key);
            }
        }
    }
}