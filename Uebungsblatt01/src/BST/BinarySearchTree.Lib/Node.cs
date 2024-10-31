namespace BinarySearchTree.Lib;

/// <summary>
/// Internal Node class representing a single node in the Binary Search Tree.
/// </summary>
internal class Node
{
    public int Value;                       // Value stored in the node
    public Node? Left, Right = default;     // Left and right children
    public int Count;                       // Count for handling duplicates

    /// <summary>
    /// Initializes a new node with the specified value.
    /// </summary>
    public Node(int value)
    {
        Value = value;
        Count = 1;
    }
}