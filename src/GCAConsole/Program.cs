global using GCAAnalyser;
global using GCAAnalyser.Abstractions;
global using GCAAnalyser.Internal;

string finish = File.ReadAllText(@"C:\Carveco Projects\Candle Files\Birch Bird_toolpath2_3d relief_Ball Nose 0.75 Finishing.tap");
GCodeParser sut = new();

IGCodeAnalyses analysis = sut.Parse(finish);
analysis.Analyse();

//List<GCodeCommand> ZMovement = analysis
//    .Commands.Where(c => c.Code == 1 && c.Z > 0)
//    .OrderBy(o => o.Index).ToList();

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
