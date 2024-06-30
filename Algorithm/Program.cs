using static System.Console;

class Program
{
    static void Main(string[] args)
    {
        var peopleNumber = int.Parse(ReadLine());
        var studentLIst = Enumerable.Range(0, peopleNumber).Select(_ => ReadLine().Split(' '))
            .Select(data => new Student(data[0], data[1], data[2], data[3])) // 이름, 일, 달, 년 
            .ToList();
        var answer = Solve(studentLIst);
        WriteLine(answer.youngest.Name);
        WriteLine(answer.oldest.Name);
    }

    static (Student youngest, Student oldest) Solve(List<Student> studentLIst)
    {
        (Student youngest, Student oldest) students =
            (BirthdayRankingCalculator.GetYoungestBDStudent(studentLIst), BirthdayRankingCalculator.GetOldestBDStudnet(studentLIst));
        return students;
    }

    static class BirthdayRankingCalculator
    {
        public static Student? GetYoungestBDStudent(List<Student> studentList)
        {
            return studentList.OrderByDescending(student => student.BirthDay)
                .FirstOrDefault();
        }

        public static Student? GetOldestBDStudnet(List<Student> studentList)
        {
            return studentList.OrderBy(student => student.BirthDay)
                .FirstOrDefault();
        }
    }

    class Student
    {
        public string Name { get; }
        public DateTime BirthDay { get; }
        public Student(string name, string BDDay, string BDMonth, string BDYear)
        {
            Name = name;
            BirthDay = DateTime.Parse($"{BDMonth} {BDDay} {BDYear}");
        }
    }
}