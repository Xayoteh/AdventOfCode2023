using System.Xml;

internal class Program
{
    private static void Main(string[] args)
    {
        string filename = "input.txt";
        long location;

        using(var almanac = new StreamReader(filename))
        {
            location = Evaluate(almanac);
        }

        Console.WriteLine(location);
    }

    private static long Evaluate(StreamReader almanac)
    {
        const int mapsCount = 7;
        var values = GetSeeds(almanac);

        for(int i = 0; i < mapsCount; ++i) 
            values = MapValues(almanac, values); // Mapping sequentially

        return values.Min();
    }

    private static List<long> GetSeeds(StreamReader almanac)
    {
        var seeds = new List<long>();
        var line = almanac.ReadLine();
        
        almanac.ReadLine(); // read empty line
        if(line != null) // not necesary, just to not get warnings
            foreach(var num in line.Replace("seeds: ", "").Split(' '))
                seeds.Add(long.Parse(num));

        return seeds;
    }

    private static List<long> MapValues(StreamReader almanac, List<long> values)
    {
        almanac.ReadLine(); // ignore first line (x-to-x map)
        var newValues = new List<long>();
        string? line;

        while((line = almanac.ReadLine()) != null && line.Length > 0)
        {
            var convertionValues = line.Split(' ');
            var destStart = long.Parse(convertionValues[0]);
            var sourceStart = long.Parse(convertionValues[1]);
            var length = long.Parse(convertionValues[2]);

            var temp = new List<long>(values);
            foreach(var value in temp)
                if(value >= sourceStart && value < sourceStart + length)
                {
                    // map value and remove from original list
                    newValues.Add(destStart + (value - sourceStart));
                    values.Remove(value);
                }
        }

        // add remaining values
        foreach(var value in values)
            newValues.Add(value);

        return newValues;
    }
}