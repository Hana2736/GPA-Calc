using System.Globalization;

Dictionary<string, double> gradeScale = new();

var totalCreditsTaken = 0d;
var totalGradePoints = 0d;
Console.WriteLine("\n<Scale format>\n" +
                  "A  4\n" +
                  "A- 3.66666\n...\n\n" +
                  "Enter file path to scale: ");
var filePathScale = Console.ReadLine();
Console.WriteLine("\n\n<Grades format>\n" +
                  "Discrete Math   3 B-\nIntro Biol      2 A\n...\n\n" +
                  "Enter file path to grades: ");
var filePathGrades = Console.ReadLine();

filePathScale = filePathScale?.Replace('\"', ' ').Trim();
filePathGrades = filePathGrades?.Replace('\"', ' ').Trim();

string[] linesIn;
try
{
    linesIn = File.ReadAllLines(filePathScale!);
    foreach (var item in linesIn)
    {
        var parts = item.Split(' ');
        gradeScale.Add(parts[0].Trim(), double.Parse(parts[^1].Trim()));
        //Console.WriteLine(parts[0].Trim() +" is worth "+gradeScale[parts[0].Trim()]);
    }
}
catch
{
    Console.WriteLine("Scale could not be read, check path and permissions.");
    return;
}

try
{
    linesIn = File.ReadAllLines(filePathGrades!);
    foreach (var item in linesIn)
    {
        var parts = item.Split(' ');
        var credits = double.Parse(parts[^2]);
        var gradeSymbol = parts[^1];
        totalCreditsTaken += credits;
        totalGradePoints += gradeScale[gradeSymbol] * credits;
    }
}
catch
{
    Console.WriteLine("Grades not be read, check path, file format, and permissions.");
    return;
}

Console.WriteLine("\n\n" + totalGradePoints + " grade points / " + totalCreditsTaken + " credits taken = \n");
Console.ForegroundColor = ConsoleColor.Green;
var gpa = (totalGradePoints / totalCreditsTaken).ToString(CultureInfo.CurrentCulture);
gpa = gpa[..int.Min(5, gpa.Length)];
Console.Write(gpa);
Console.ResetColor();
Console.WriteLine(" GPA according to the scale.");