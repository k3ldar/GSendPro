global using GCAAnalyser;
global using GCAAnalyser.Internal;
global using GCAAnalyser.Abstractions;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

string finish = File.ReadAllText(@"C:\Carveco Projects\Candle Files\Birch Bird_toolpath2_3d relief_Ball Nose 0.75 Finishing.tap");
GCodeParser sut = new GCodeParser();

IGCodeAnalyses analysis = sut.Parse(finish);
analysis.Analyse();

List<GCodeCommand> ZMovement = analysis
    .Commands.Where(c => c.Code == 1 && c.Z > 0)
    .OrderBy(o => o.Index).ToList();

foreach (GCodeCommand z in analysis.Commands)
{
    if (z.Attributes.HasFlag(CommandAttributes.SafeZ))
        Console.WriteLine(z.ToString());
}

Console.WriteLine("Safe Z: {0}", analysis.SafeZ);
Console.WriteLine("Contains Duplicates: {0}", analysis.ContainsDuplicates ? "yes" : "no");

for (int i = 165003; i < analysis.Commands.Count; i++)
{
    Console.WriteLine(analysis.Commands[i].ToString());
}
