// See https://aka.ms/new-console-template for more information
var data = new Data();
var firstQuery = data.students
    .Where(x => x.Age > 18)
    .OrderBy(x => x.Age)
    .Select(x=>x.Name.ToUpper());
var secondQuery = data.students
    .GroupJoin(data.grades, x => x.Name, y => y.StudentName,
    (x, group) => (Name:x.Name,Count: group.Count()));
var thirdQuery = data.grades
    .GroupBy(x => x.Course)
    .Select(x => (Course: x.Key, Avg: Math.Round(x.Average(y => y.Score),2)))
    .MaxBy(x => x.Avg);
var fourthQuery = data.grades
    .GroupBy(g => g.StudentName)
    .Where(g => g.All(x => x.Score == 5))
    .Select(g => g.Key);
Console.WriteLine(string.Join(", ", firstQuery));
foreach (var s in secondQuery)
    Console.WriteLine($"{s.Name}: {s.Count}");
Console.WriteLine($"{thirdQuery.Course}: {thirdQuery.Avg}");
foreach (var name in fourthQuery)
    Console.WriteLine(name);

