using System.Collections;
using System.Linq;

public class BinarySearchTree : IEnumerable
{
    private Node? _root;

    ///
    /// Insert a new node in the BST.
    ///
    public void Insert(int value)
    {
        Node newNode = new(value);

        if (_root is null)
            _root = newNode;
        else
            _root.Insert(value);
    }

    ///
    /// Check to see if the tree contains a certain value
    ///
    public bool Contains(int value)
    {
        return _root != null && _root.Contains(value);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    ///
    /// Iterate forward through the BST
    ///
    public IEnumerator GetEnumerator()
    {
        var numbers = new List<int>();
        TraverseForward(_root, numbers);

        foreach (var number in numbers)
            yield return number;
    }

    private void TraverseForward(Node? node, List<int> values)
    {
        if (node is not null)
        {
            TraverseForward(node.Left, values);
            values.Add(node.Data);
            TraverseForward(node.Right, values);
        }
    }

    ///
    /// Iterate backward through the BST.
    ///
    public IEnumerable Reverse()
    {
        var numbers = new List<int>();
        TraverseBackward(_root, numbers);

        foreach (var number in numbers)
            yield return number;
    }

    private void TraverseBackward(Node? node, List<int> values)
    {
        // Problem 3: Traverse backwards (right, node, left)
        if (node is not null)
        {
            TraverseBackward(node.Right, values);
            values.Add(node.Data);
            TraverseBackward(node.Left, values);
        }
    }

    ///
    /// Get the height of the tree
    ///
    public int GetHeight()
    {
        if (_root is null)
            return 0;

        return _root.GetHeight();
    }

    public override string ToString()
    {
        // Include the prefix expected by the tests: "<Bst>"
        // Use Cast<int>() to avoid the recursive string.Join overload problem.
        return "<Bst>{" + string.Join(", ", this.Cast<int>()) + "}";
    }
}

// Extension methods must be in a top-level static class (not nested)
public static class IntArrayExtensionMethods
{
    public static string AsString(this IEnumerable array)
    {
        // Include the prefix expected by the tests: "<IEnumerable>"
        return "<IEnumerable>{" + string.Join(", ", array.Cast<int>()) + "}";
    }
}
