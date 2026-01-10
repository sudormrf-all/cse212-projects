public static class Arrays
{
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'.  For
    /// example, MultiplesOf(7, 5) will result in: {7, 14, 21, 28, 35}.  Assume that length is a positive
    /// integer greater than 0.
    /// </summary>
    /// <returns>array of doubles that are the multiples of the supplied number</returns>
    public static double[] MultiplesOf(double number, int length)
    {
        // PLAN (Problem 1: MultiplesOf)
        // 1) Create a new double array with exactly 'length' slots.
        // 2) Use a loop from i = 0 up to i < length to fill each slot.
        // 3) For each position i, compute the (i+1)-th multiple of 'number':
        //      - The 1st multiple should be number * 1 (this goes in index 0)
        //      - The 2nd multiple should be number * 2 (this goes in index 1)
        //      - In general: result[i] = number * (i + 1)
        // 4) Return the filled array.

        double[] result = new double[length];

        for (int i = 0; i < length; i++)
        {
            result[i] = number * (i + 1);
        }

        return result;
    }

    /// <summary>
    /// Rotate the 'data' to the right by the 'amount'.  For example, if the data is
    /// List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9} and an amount is 3 then the list after the function runs should be
    /// List<int>{7, 8, 9, 1, 2, 3, 4, 5, 6}.  The value of amount will be in the range of 1 to data.Count, inclusive.
    ///
    /// Because a list is dynamic, this function will modify the existing data list rather than returning a new list.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {
        // PLAN (Problem 2: RotateListRight)
        // Goal: Modify the existing list so the last 'amount' items move to the front,
        //       while keeping their original order.
        //
        // 1) Identify where the "split" happens:
        //      - If data.Count is N, then the last 'amount' items start at index (N - amount).
        // 2) Use GetRange to slice the list into two parts:
        //      - rightPart = data.GetRange(N - amount, amount)  (the items moving to the front)
        //      - leftPart  = data.GetRange(0, N - amount)       (the remaining items)
        //    GetRange creates a new list containing that range (a shallow copy). [page:2]
        // 3) Clear out the original list content using RemoveRange(0, N).
        //    RemoveRange removes a contiguous range starting at an index. [page:1]
        // 4) Rebuild the original list by adding rightPart first, then leftPart:
        //      - data.AddRange(rightPart)
        //      - data.AddRange(leftPart)

        int n = data.Count;
        int splitIndex = n - amount;

        List<int> rightPart = data.GetRange(splitIndex, amount);
        List<int> leftPart = data.GetRange(0, splitIndex);

        data.RemoveRange(0, n);

        data.AddRange(rightPart);
        data.AddRange(leftPart);
    }
}
