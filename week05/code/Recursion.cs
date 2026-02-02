using System.Collections;
using System.Collections.Generic;

public static class Recursion
{
    // ======================
    // Problem 1
    // ======================
    public static int SumSquaresRecursive(int n)
    {
        if (n <= 0)
            return 0;

        return n * n + SumSquaresRecursive(n - 1);
    }

    // ======================
    // Problem 2
    // ======================
    public static void PermutationsChoose(List<string> results, string letters, int size, string word = "")
    {
        if (word.Length == size)
        {
            results.Add(word);
            return;
        }

        for (int i = 0; i < letters.Length; i++)
        {
            string remaining = letters.Remove(i, 1);
            PermutationsChoose(results, remaining, size, word + letters[i]);
        }
    }

    // ======================
    // Problem 3
    // ======================
    public static decimal CountWaysToClimb(int s, Dictionary<int, decimal>? remember = null)
    {
        if (remember == null)
            remember = new Dictionary<int, decimal>();

        if (s < 0)
            return 0;

        if (s == 0)
            return 1;

        if (remember.ContainsKey(s))
            return remember[s];

        decimal ways =
            CountWaysToClimb(s - 1, remember) +
            CountWaysToClimb(s - 2, remember) +
            CountWaysToClimb(s - 3, remember);

        remember[s] = ways;
        return ways;
    }

    // ======================
    // Problem 4
    // ======================
    public static void WildcardBinary(string pattern, List<string> results)
    {
        int index = pattern.IndexOf('*');

        if (index == -1)
        {
            results.Add(pattern);
            return;
        }

        WildcardBinary(pattern[..index] + "0" + pattern[(index + 1)..], results);
        WildcardBinary(pattern[..index] + "1" + pattern[(index + 1)..], results);
    }

// ======================
// Problem 5
// ======================
public static void SolveMaze(
    List<string> results,
    Maze maze,
    int x = 0,
    int y = 0,
    List<(int, int)>? currPath = null)
{
    if (currPath == null)
        currPath = new List<(int, int)>();

    // Check if move is valid
    if (!maze.IsValidMove(currPath, x, y))
        return;

    // Add current position
    currPath.Add((x, y));

    // Check if this is the end
    if (maze.IsEnd(x, y))
    {
        results.Add(currPath.AsString());
        currPath.RemoveAt(currPath.Count - 1);
        return;
    }

    // Explore all four directions
    SolveMaze(results, maze, x + 1, y, currPath);
    SolveMaze(results, maze, x - 1, y, currPath);
    SolveMaze(results, maze, x, y + 1, currPath);
    SolveMaze(results, maze, x, y - 1, currPath);

    // Backtrack
       currPath.RemoveAt(currPath.Count - 1);
}
}
 