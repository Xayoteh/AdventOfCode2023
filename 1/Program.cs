using System;
using System.IO;
using System.Text;

internal class Program
{
    private static readonly Dictionary<string, string> map = new Dictionary<string, string>() {
        {"one", "1"}, {"two", "2"}, {"three", "3"}, {"four", "4"}, {"five", "5"},
        {"six", "6"}, {"seven", "7"}, {"eight", "8"}, {"nine", "9"}
    };
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

    private static string GetNumber(string s) {
        int firstIdx = s.IndexOfAny(digits), 
            lastIdx = s.LastIndexOfAny(digits);
        string firstDigit = $"{(firstIdx != -1 ? s[firstIdx]:"")}", 
            lastDigit = $"{(lastIdx != -1 ? s[lastIdx]:"")}";
        
        foreach(var kvp in map) {
            int idx = s.IndexOf(kvp.Key);
            if(firstIdx == -1 || (idx != -1 && idx < firstIdx)) {
                firstIdx = idx;
                firstDigit = kvp.Value;
            }
            idx = s.LastIndexOf(kvp.Key);
            if(idx > lastIdx) {
                lastIdx = idx;
                lastDigit = kvp.Value;
            }
        }

        return firstDigit + lastDigit;
    }
}