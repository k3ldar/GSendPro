global using GCAAnalyser;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

string finish = File.ReadAllText(@"C:\Carveco Projects\Candle Files\Birch Bird_toolpath2_3d relief_Ball Nose 0.75 Finishing.tap");
GCodeParser sut = new GCodeParser();

for (int i = 0; i < 20; i++)
{
    sut.Parse(finish);
    Console.WriteLine(i.ToString());

    if (sut.Commands.Count != 165617)
    {
        throw new Exception("oops");
    }
}
