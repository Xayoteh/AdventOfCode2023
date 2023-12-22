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
                EvaluateCard(card, cardCount);
            }
        }
        foreach(var count in cardCount)
        {
            sum += count.Value;
        }
        Console.WriteLine(sum);
    }

    private static void EvaluateCard(string _card, Dictionary<int, int> cardsCount) 
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

        cardsCount.TryGetValue(cardNumber, out int cardCount);
        cardsCount[cardNumber] = ++cardCount;
        for(int i = 1; i <= matches; ++i) 
        {
            cardsCount.TryGetValue(cardNumber + i, out int count);
            cardsCount[cardNumber + i] = count + cardCount;
        }
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