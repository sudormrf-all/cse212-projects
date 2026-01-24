using System.Text.Json;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Linq;

public static class SetsAndMaps
{
    ///
    /// The words parameter contains a list of two character
    /// words (lower case, no duplicates). Using sets, find an O(n)
    /// solution for returning all symmetric pairs of words.
    ///
    /// For example, if words was: [am, at, ma, if, fi], we would return :
    /// ["am & ma", "if & fi"]
    ///
    /// The order of the array does not matter, nor does the order of the specific words in each string in the array.
    /// at would not be returned because ta is not in the list of words.
    ///
    /// As a special case, if the letters are the same (example: 'aa') then
    /// it would not match anything else (remember the assumption above
    /// that there were no duplicates) and therefore should not be returned.
    ///
    /// <param name="words">An array of 2-character words (lowercase, no duplicates)</param>
   public static string[] FindPairs(string[] words)
{
    var charPairs = new Dictionary<(char, char), string>();
    var result = new List<string>();

    // Single pass: populate reverse lookup
    foreach (var w in words)
    {
        if (w.Length != 2) continue;
        char a = w[0], b = w[1];
        if (a == b) continue;
        charPairs[(b, a)] = w;
    }

    // Second pass: find matches
    foreach (var kvp in charPairs)
    {
        var revKey = kvp.Key;
        if (charPairs.TryGetValue((revKey.Item2, revKey.Item1), out string match))
        {
            string first = string.Compare(kvp.Value, match) < 0 ? kvp.Value : match;
            string second = string.Compare(kvp.Value, match) < 0 ? match : kvp.Value;
            result.Add($"{first} & {second}");
        }
    }

    return result.Distinct().ToArray();  
}

    ///
    /// Read a census file and summarize the degrees (education)
    /// earned by those contained in the file. The summary
    /// should be stored in a dictionary where the key is the
    /// degree earned and the value is the number of people that
    /// have earned that degree. The degree information is in the
    /// 4th column of the file. There is no header row in the
    /// file.
    ///
    /// <param name="filename">The name of the file to read</param>
    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var degrees = new Dictionary<string, int>();
        foreach (var line in File.ReadLines(filename))
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            var fields = line.Split(',');
            if (fields.Length <= 3) continue;
            string degree = fields[3].Trim();
            if (degree.Length == 0) continue;
            if (!degrees.ContainsKey(degree))
                degrees[degree] = 0;
            degrees[degree]++;
        }
        return degrees;
    }

    ///
    /// Determine if 'word1' and 'word2' are anagrams. An anagram
    /// is when the same letters in a word are re-organized into
    /// a new word. A dictionary (here, a fixed array of counts) is used
    /// to solve the problem.
    ///
    /// Important Note: When determining if two words are anagrams, you
    /// should ignore any spaces. You should also ignore cases. For
    /// example, 'Ab' and 'Ba' should be considered anagrams.
    ///
    public static bool IsAnagram(string word1, string word2)
    {
        if (word1 == null || word2 == null)
            return false;

        // Fixed-size counts for all possible byte values (0-255)
        int[] counts = new int[256];
        int len1 = word1.Length;
        int len2 = word2.Length;

        // Count chars from word1 (ignoring whitespace, case-insensitive)
        for (int i = 0; i < len1; i++)
        {
            char c = word1[i];
            if (char.IsWhiteSpace(c)) continue;
            char lower = char.ToLowerInvariant(c);
            if (lower >= 256) return false;
            counts[(int)lower]++;
        }

        // Subtract chars from word2
        for (int i = 0; i < len2; i++)
        {
            char c = word2[i];
            if (char.IsWhiteSpace(c)) continue;
            char lower = char.ToLowerInvariant(c);
            if (lower >= 256) return false;
            counts[(int)lower]--;
            if (counts[(int)lower] < 0) return false;
        }

        // Ensure all counts zero (same multiset of characters)
        for (int i = 0; i < 256; i++)
        {
            if (counts[i] != 0) return false;
        }
        return true;
    }

    ///
    /// This function will read JSON (Javascript Object Notation) data from the
    /// United States Geological Service (USGS) consisting of earthquake data.
    /// The data will include all earthquakes in the current day.
    ///
    /// JSON data is organized into a dictionary. After reading the data using
    /// the built-in HTTP client library, this function will return a list of all
    /// earthquake locations ('place' attribute) and magnitudes ('mag' attribute).
    ///
    public static string[] EarthquakeDailySummary()
    {
        const string uri = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";
        using var client = new HttpClient();
        using var getRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
        using var jsonStream = client.Send(getRequestMessage).Content.ReadAsStream();
        using var reader = new StreamReader(jsonStream);
        var json = reader.ReadToEnd();
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json, options);
        var list = new List<string>();
        if (featureCollection?.Features != null)
        {
            foreach (var f in featureCollection.Features)
            {
                var props = f?.Properties;
                if (props == null) continue;
                if (string.IsNullOrWhiteSpace(props.Place) || !props.Mag.HasValue) continue;
                list.Add($"{props.Place} - Mag {props.Mag.Value}");
            }
        }
        return list.ToArray();
    }
}
