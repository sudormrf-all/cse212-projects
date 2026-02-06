using System.Collections;

public static class Recursion
{
    ///
    /// #############
    /// # Problem 1 #
    /// #############
    /// Using recursion, find the sum of 1^2 + 2^2 + 3^2 + ... + n^2
    /// and return it. Remember to both express the solution
    /// in terms of recursive call on a smaller problem and
    /// to identify a base case (terminating case). If the value of
    /// n <= 0, just return 0. A loop should not be used.
    ///
    public static int SumSquaresRecursive(int n)
    {
        if (n <= 0) return 0;
        return (n * n) + SumSquaresRecursive(n - 1);
    }

    ///
    /// #############
    /// # Problem 2 #
    /// #############
    /// Using recursion, insert permutations of length
    /// 'size' from a list of 'letters' into the results list. This function
    /// should assume that each letter is unique (i.e. the
    /// function does not need to find unique permutations).
    ///
    /// In mathematics, we can calculate the number of permutations
    /// using the formula: len(letters)! / (len(letters) - size)!
    ///
    /// For example, if letters was [A,B,C] and size was 2 then
    /// the following would the contents of the results array after the function ran:
    /// AB, AC, BA, BC, CA, CB (might be in a different order).
    ///
    /// You can assume that the size specified is always valid (between 1
    /// and the length of the letters list).
    ///
    public static void PermutationsChoose(List<string> results, string letters, int size, string word = "")
    {
        if (word.Length == size)
        {
            results.Add(word);
            return;
        }

        for (int i = 0; i < letters.Length; i++)
        {
            char c = letters[i];
            string remaining = letters.Remove(i, 1);
            PermutationsChoose(results, remaining, size, word + c);
        }
    }

    ///
    /// #############
    /// # Problem 3 #
    /// #############
    /// Imagine that there was a staircase with 's' stairs.
    /// We want to count how many ways there are to climb
    /// the stairs. If the person could only climb one
    /// stair at a time, then the total would be just one.
    /// However, if the person could choose to climb either
    /// one, two, or three stairs at a time (in any order),
    /// then the total possibilities become much more
    /// complicated. If there were just three stairs,
    /// the possible ways to climb would be four as follows:
    ///
    /// 1 step, 1 step, 1 step
    /// 1 step, 2 step
    /// 2 step, 1 step
    /// 3 step
    ///
    /// With just one step to go, the ways to get
    /// to the top of 's' stairs is to either:
    ///
    /// - take a single step from the second to last step,
    /// - take a double step from the third to last step,
    /// - take a triple step from the fourth to last step
    ///
    /// These final leaps give us a sum:
    ///
    /// CountWaysToClimb(s) = CountWaysToClimb(s-1) +
    ///                       CountWaysToClimb(s-2) +
    ///                       CountWaysToClimb(s-3)
    ///
    /// To run this function for larger values of 's', you will need
    /// to update this function to use memoization. The parameter
    /// 'remember' has already been added as an input parameter to
    /// the function for you to complete this task.
    ///
    public static decimal CountWaysToClimb(int s, Dictionary<int, decimal>? remember = null)
    {
        // Base Cases
        if (s == 0) return 0;
        if (s == 1) return 1;
        if (s == 2) return 2;
        if (s == 3) return 4;

        remember ??= new Dictionary<int, decimal>();

        if (remember.TryGetValue(s, out decimal cached))
            return cached;

        decimal ways =
            CountWaysToClimb(s - 1, remember) +
            CountWaysToClimb(s - 2, remember) +
            CountWaysToClimb(s - 3, remember);

        remember[s] = ways;
        return ways;
    }

    ///
    /// #############
    /// # Problem 4 #
    /// #############
    /// A binary string is a string consisting of just 1's and 0's.
    /// If we introduce a wildcard symbol * into the string, we can say that
    /// this is now a pattern for multiple binary strings.
    ///
    /// Using recursion, insert all possible binary strings for a given pattern into the results list.
    /// You might find some of the string functions like IndexOf and [..X] / [X..] to be useful.
    ///
    public static void WildcardBinary(string pattern, List<string> results)
{
    // Base case: empty pattern expands to one result: the empty string
    if (pattern.Length == 0)
    {
        results.Add("");
        return;
    }

    int star = pattern.IndexOf('*');
    if (star < 0)
    {
        results.Add(pattern);
        return;
    }

    string prefix = pattern[..star];
    string suffix = pattern[(star + 1)..];

    WildcardBinary(prefix + "0" + suffix, results);
    WildcardBinary(prefix + "1" + suffix, results);
}

    ///
    /// Use recursion to insert all paths that start at (0,0) and end at the
    /// 'end' square into the results list.
    ///
    public static void SolveMaze(List<string> results, Maze maze, int x = 0, int y = 0, List<(int, int)>? currPath = null)
    {
        // If this is the first time running the function, then we need
        // to initialize the currPath list.
        if (currPath == null)
            currPath = new List<(int, int)>();

        // Current position must be valid (including the start)
        if (!maze.IsValidMove(currPath, x, y))
            return;

        // Choose
        currPath.Add((x, y));

        // If at the end, record the solution path
        if (maze.IsEnd(x, y))
        {
            results.Add(currPath.AsString());
            currPath.RemoveAt(currPath.Count - 1); // backtrack
            return;
        }

        // Explore neighbors
        SolveMaze(results, maze, x + 1, y, currPath);
        SolveMaze(results, maze, x - 1, y, currPath);
        SolveMaze(results, maze, x, y + 1, currPath);
        SolveMaze(results, maze, x, y - 1, currPath);

        // Un-choose (backtrack)
        currPath.RemoveAt(currPath.Count - 1);
    }
}
