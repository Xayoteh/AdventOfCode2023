using System.Text;
internal class Program
{
    private static void Main(string[] args)
    {
        var filename = "input.txt";
        var sum = 0;
        using(var file = new StreamReader(filename))
        {
            string? card;
            while((card = file.ReadLine()) != null) 
            {
                sum += EvaluateCard(card);
            }
        }
        Console.WriteLine(sum);
    }

    private static int EvaluateCard(string _card) 
    {
        var card = new StringReader(_card.Replace("Card ", ""));
        var cardNumber = GetNumber(card);
        var winningNumbers = GetNumbers(card);
        var myNumbers = new HashSet<int>(GetNumbers(card));
        int points = 0;

        foreach(var number in winningNumbers) 
        {
            if(!myNumbers.Contains(number)) continue;
            if (points == 0) ++points;
            else points *= 2;
        }

        return points;
    }

    private static string GetNumber(StringReader card)
    {
        var number = new StringBuilder();
        char nextChar;

        while((nextChar = (char)card.Read()) != ':') 
            number.Append(nextChar);

        return number.ToString();
    }

    private static List<int> GetNumbers(StringReader card)
    {
        var numbers = new List<int>();
        var curr = new StringBuilder();
        int next;

        while(true) 
        {
            char nextChar = '.';
            if((next = card.Read()) == -1 || !char.IsDigit(nextChar = (char)next)) 
            {
                if(curr.Length > 0) 
                {
                    numbers.Add(int.Parse(curr.ToString()));
                    curr.Clear();
                }
                if(next == -1 || nextChar == '|') break;
            }
            else curr.Append(nextChar);
        }

        return numbers;
    }
}