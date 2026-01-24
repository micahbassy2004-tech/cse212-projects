using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Collections.Generic;

// JSON classes for Problem 5
public class FeatureCollection
{
    public List<Feature> Features { get; set; }
}

public class Feature
{
    public Properties Properties { get; set; }
}

public class Properties
{
    public string Place { get; set; }
    public double Mag { get; set; }
}

public static class SetsAndMaps
{
    // Problem 1: Find symmetric pairs in O(n)
    public static string[] FindPairs(string[] words)
    {
        var seen = new HashSet<string>();
        var result = new List<string>();

        foreach (var word in words)
        {
            if (word.Length != 2) continue;  // just in case
            string reversed = word[1].ToString() + word[0].ToString();

            if (word == reversed) continue;  // ignore "aa"
            if (seen.Contains(reversed)) result.Add($"{reversed} & {word}");
            seen.Add(word);
        }

        return result.ToArray();
    }

    // Problem 2: Summarize degrees from file
    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var degrees = new Dictionary<string, int>();
        foreach (var line in File.ReadLines(filename))
        {
            var fields = line.Split(",");
            if (fields.Length > 3)
            {
                var degree = fields[3].Trim();
                if (degrees.ContainsKey(degree)) degrees[degree]++;
                else degrees[degree] = 1;
            }
        }
        return degrees;
    }

    // Problem 3: Check if two words are anagrams
    public static bool IsAnagram(string word1, string word2)
    {
        word1 = word1.Replace(" ", "").ToLower();
        word2 = word2.Replace(" ", "").ToLower();

        if (word1.Length != word2.Length) return false;

        var count = new Dictionary<char, int>();
        foreach (var c in word1) count[c] = count.ContainsKey(c) ? count[c] + 1 : 1;
        foreach (var c in word2)
        {
            if (!count.ContainsKey(c) || count[c] == 0) return false;
            count[c]--;
        }

        return true;
    }

    // Problem 5: Get earthquakes from USGS JSON
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
        var results = new List<string>();

        if (featureCollection?.Features != null)
        {
            foreach (var feature in featureCollection.Features)
            {
                var place = feature.Properties.Place;
                var mag = feature.Properties.Mag;
                results.Add($"{place} - Mag {mag}");
            }
        }

        return results.ToArray();
    }
}
