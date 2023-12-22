using System;
using System.IO;
using System.Text;
internal class Program
{
    private static readonly Dictionary<string, int> Bag = new Dictionary<string, int>() 
    {
        {"red", 12},
        {"green", 13},
        {"blue", 14}
    };
    private static void Main(string[] args)
    {
        var filename = "input.txt";
        var sum = 0;
        using(var file = new StreamReader(filename))
        {
            string? game;
            while((game = file.ReadLine()) != null) 
            {
                sum += EvaluateGame(game);
            }
        }
        Console.WriteLine(sum);
    }

    private static int EvaluateGame(string _game) 
    {
        var game = new StringReader(_game.Replace("Game ", ""));
        Dictionary<string, int> subset;
        var id = GetID(game);

        while((subset = GetSubset(game)).Count > 0)
        {
            foreach(var cube in subset) 
            {
                if(Bag[cube.Key] < cube.Value) return 0;
            }
        }
        
        return int.Parse(id);
    }

    private static string GetID(StringReader game)
    {
        var id = new StringBuilder();
        int nextChar;

        while((nextChar = game.Read()) != (int)':') 
            id.Append((char)nextChar);

        return id.ToString();
    }
    private static Dictionary<string, int> GetSubset(StringReader game)
    {
        var subset = new Dictionary<string, int>();
        var quantity = new StringBuilder();
        var color = new StringBuilder();
        int nextChar;

        while(true)
        {
            char next;
            if ((nextChar = game.Read()) == -1 || (next = (char)nextChar) == ';')
            {
                if(color.Length != 0) 
                {
                    subset[color.ToString()] = int.Parse(quantity.ToString());
                    color.Clear(); quantity.Clear();
                }
                break;
            }

            if(next == ' ') continue;
            if(char.IsDigit(next)) quantity.Append(next);
            else if(char.IsLetter(next)) color.Append(next);
            else if(next == ',')
            {
                subset[color.ToString()] = int.Parse(quantity.ToString());
                color.Clear(); quantity.Clear();
            }
        }

        return subset;
    }

}