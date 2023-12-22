using System;
using System.IO;
using System.Text;

internal class Program
{
    private static readonly char[] digits = "123456789".ToArray();
    private static void Main(string[] args)
    {
        string filename = @"input.txt";
        using (StreamReader file = new StreamReader(filename))
        {
            var sum = 0;
            string? line;
            while ((line = file.ReadLine()) != null)
            {
                string number = GetNumber(line);
                sum += int.Parse(number);
            }
            Console.WriteLine(sum);
        }
    }

    private static string GetNumber(string line) {
        int firstIdx = line.IndexOfAny(digits), 
            lastIdx = line.LastIndexOfAny(digits);

        return $"{line[firstIdx]}{line[lastIdx]}";
    }
}