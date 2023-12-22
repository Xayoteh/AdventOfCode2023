using System.Reflection;
using System.Text;
internal class Program
{
    private static void Main(string[] args)
    {
        var cardCount = new Dictionary<int, int>();
        var filename = "input.txt";
        var sum = 0;
        using(var file = new StreamReader(filename))
        {
            string? card;
            while((card = file.ReadLine()) != null) 
            {
                var (number, matches) = EvaluateCard(card);
                
                cardCount.TryGetValue(number, out int count);
                cardCount[number] = count + 1;
                for(int i = 1; i <= matches; ++i) 
                {
                    cardCount.TryGetValue(number + i, out count);
                    cardCount[number + i] = count + cardCount[number];
                }
            }
        }
        foreach(var count in cardCount)
        {
            sum += count.Value;
        }
        Console.WriteLine(sum);
    }

    private static (int number, int matches) EvaluateCard(string _card) 
    {
        var card = new StringReader(_card.Replace("Card ", ""));
        var cardNumber = int.Parse(GetNumber(card));
        var winningNumbers = GetNumbers(card);
        var myNumbers = new HashSet<int>(GetNumbers(card));
        int matches = 0;

        foreach(var number in winningNumbers) 
        {
            if(myNumbers.Contains(number)) ++matches;
        }

        return (cardNumber, matches);
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