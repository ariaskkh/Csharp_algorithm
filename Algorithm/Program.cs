using System;
using System.Text;

namespace Algorithm;

public class Program
{
    static void Main(string[] args)
    {
        int N = int.Parse(Console.ReadLine());
        Solve(N);
    }

    static void Solve(int N)
    {
        int[][] studentsData = new int[N][];
        for (var i = 0; i < N; i++)
        {
            int[] classData = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
            studentsData[i] = classData;
        }

        var classPresident = new ClassPresident(studentsData);
        classPresident.SetPresident();
        Console.WriteLine(classPresident.GetPresident());
    }

    class ClassPresident
    {
        static int gradeCount = 5;
        int[][] _studentsData;
        int[] _numberOfSameClassList;
        public ClassPresident(int[][] studentsData)
        {
            _studentsData = studentsData;
            _numberOfSameClassList = new int[_studentsData.Length];
        }

        public void SetPresident()
        {
            for (var student1 = 0; student1 < _studentsData.Length; student1++)
            {
                List<int> sameClassmates = new List<int>();
                for (var student2 = 0; student2 < _studentsData.Length; student2++)
                {
                    if (student1 == student2)
                    {
                        continue;
                    }
                    for (var grade = 0; grade < gradeCount; grade++)
                    {
                        if (_studentsData[student1][grade] == _studentsData[student2][grade])
                        {
                            if (!sameClassmates.Contains(student2))
                            {
                                sameClassmates.Add(student2);
                            }
                        }
                    }
                }
                _numberOfSameClassList[student1] = sameClassmates.Count;
            }
        }

        public int GetPresident()
        {
            var max = _numberOfSameClassList.Max();
            var president = Array.IndexOf(_numberOfSameClassList, max) + 1;
            return president;
        }
    }

    static Dictionary<char, int> SetDictionary(Dictionary<char, int > charCounts, string inputString)
    {
        foreach (var character in inputString)
        {
            if (charCounts.ContainsKey(character))
            {
                charCounts[character]++;
            }
            else
            {
                charCounts[character] = 1;
            }
        }
        return charCounts;
    }
       

    static IEnumerable<(T1, T2)> IteratonFunction<T1, T2>(IEnumerable<T1> arr1, IEnumerable<T2> arr2)
    {
        return arr1.Join<T1, T2, bool, (T1, T2)>(arr2, item1 => true, item2 => true, (item1, item2) => (item1, item2));
    }
    static IEnumerable<(T1, T2, T3)> IteratonFunction<T1, T2, T3>(IEnumerable<T1> arr1, IEnumerable<T2> arr2, IEnumerable<T3> arr3)
    {
        return arr1.Join(arr2, item1 => true, item2 => true, (item1, item2) => (item1, item2)).Join(arr3, item => true, item3 => true, (item, item3) => (item.item1, item.item2, item3));
    }

    static char[,] Get2dArrayCopy(char[,] square)
    {
        int rows = square.GetLength(0);
        int cols = square.GetLength(1);
        char[,] copyArr = new char[rows, cols];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                copyArr[i, j] = square[i, j];
            }
        }
        return copyArr;
    }

    static char[,] GetSquareArr(int N)
    {
        char[,] resultArr = new char[N, N];
        for (var i = 0; i < N; i++)
        {
            var tmpRowArr = Console.ReadLine().ToCharArray();
            for (var j = 0; j < N; j++)
            {
                resultArr[i, j] = tmpRowArr[j];
            }
        }
        return resultArr;
    }

    static int[,] GetArr(int N, int M)
    {
        int[,] resultArr = new int[N, M];
        for (var i = 0; i < N; i++)
        {
            var tmpRowArr = Console.ReadLine().Split(' ');
            for (var j = 0; j < M; j++)
            {
                resultArr[i, j] = int.Parse(tmpRowArr[j]);
            }
        }
        return resultArr;
    }

    static int[] ChangeCharToNum(char[] numsInChar)
    {
        return numsInChar.Select(x => int.Parse(x.ToString())).ToArray();
    }

    static int[] ChangeStrToNum(string[] numsInStr)
    {
        return numsInStr.Select(x => int.Parse(x)).ToArray();
    }

    static void Print(int[,] result, int N, int K)
    {
        for (var i = 0; i < N; i++)
        {
            for (var j = 0; j < K; j++)
            {
                Console.Write($"{result[i, j]} ");
            }
            Console.WriteLine();
        }
    }
}