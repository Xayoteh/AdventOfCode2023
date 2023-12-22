using System.Data;
using System.IO;
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        var engine = new List<char[]>();
        var filename = "input.txt";
        int sum;
        using (var file = new StreamReader(filename))
        {
            string? line;
            while((line = file.ReadLine()) != null)
                engine.Add(line.ToCharArray());
        }
        sum = Evaluate(engine);
        Console.WriteLine(sum);
    }

    private static int Evaluate(List<char[]> engine)
    {
        int sum = 0;

        for(int row = 0; row < engine.Count; ++row)
        {
            for(int col = 0; col < engine[row].Length; ++col)
            {
                if(engine[row][col] == '*')
                    sum += GetParts(engine, row, col);
            }
        }

        return sum;
    }

    private static int GetParts(List<char[]> engine, int row, int col)
    {
        int parts = 1;
        int count = 0;

        for(int i = row - 1; i <= row + 1; i++) 
        {
            for(int j = col - 1; j <= col + 1; j++)
            {
                if(i == row && j == col) continue;
                if(i < 0 || i >= engine.Count) continue;
                if(j < 0 || j >= engine[row].Length) continue;
                if(GetNumber(engine[i], j, out int num))
                {
                    if(count++ == 2) return 0;
                    parts *= num;
                }
            }
        }

        return count == 2 ? parts : 0;
    }

    private static bool GetNumber(char[] line, int col, out int number) 
    {
        if(!char.IsDigit(line[col])) 
        {
            number = 0;
            return false;
        }

        var num = new StringBuilder();

        for(int i = col; i < line.Length && char.IsDigit(line[i]); ++i)
        {
            num.Append(line[i]);
            line[i] = '.';
        }

        for(int i = col - 1; i >= 0 && char.IsDigit(line[i]); --i)
        {
            num.Insert(0, line[i]);
            line[i] = '.';
        }

        number = int.Parse(num.ToString());
        return true;
    }
}