
public class Data
{
    public record Student(string Name, string Group, int Age);
    public record Grade(string StudentName, string Course, int Score);
    
    public List<Student> students =
    [
        new("Анна", "25.Б41", 19), new("Борис", "25.Б41", 20),
        new("Вера", "25.Б42", 18), new("Глеб", "25.Б42", 19),
        new("Дина", "25.Б43", 20), new("Егор", "25.Б43", 18),
    ];

    public List<Grade> grades =
    [
        new("Анна", "C#", 5), new("Анна", "Матан", 4), new("Борис", "C#", 3),
        new("Вера", "C#", 5), new("Вера", "Матан", 5), new("Глеб", "C#", 4),
        new("Дина", "Матан", 4),
    ];
}
