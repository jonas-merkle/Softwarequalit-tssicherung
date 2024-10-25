namespace BinarySearchTree.Lib.Mutants.M10;

/// <summary>
/// This class represents a Binary Search Tree.
/// </summary>
public class BinarySearchTree
{
    #region Mutant

    /// <summary>
    /// Mutation 10: Return the sum of the root node only.
    /// </summary>
    private static int SumValues(Node? node)
    {
        return node?.Value * node?.Count ?? 0;
    }

    #endregion
    
    #region private members

    private Node? _root;

    #endregion

    #region public methods
    
    /// <summary>
    /// Adds a value to the tree.
    /// </summary>
    public void Add(int value)
    {
        if (_root is null)
        {
            _root = new Node(value);
        }
        else
        {
            AddNode(_root, value);
        }
    }

    /// <summary>
    /// Removes a value from the tree.
    /// </summary>
    public void Remove(int value)
    {
        _root = RemoveNode(_root, value);
    }
    
    /// <summary>
    /// Converts the tree to a sorted list of values.
    /// </summary>
    public List<int> ToList()
    {
        var result = new List<int>();
        InOrderTraversal(_root, result);
        return result;
    }
    
    /// <summary>
    /// Counts the number of occurrences of a value in the tree.
    /// </summary>
    public int Count(int value)
    {
        return CountValue(_root, value);
    }
    
    /// <summary>
    /// Calculates the sum of all values in the tree.
    /// </summary>
    public int Sum()
    {
        return SumValues(_root);
    }
    
    #endregion

    #region private static methods

    /// <summary>
    /// Helper method to add a value to the correct position in the tree.
    /// </summary>
    private static void AddNode(Node node, int value)
    {
        while (true)
        {
            if (value < node.Value)
            {
                if (node.Left == null)
                {
                    node.Left = new Node(value);
                    return;
                }
                node = node.Left;
            }
            else if (value > node.Value)
            {
                if (node.Right == null)
                {
                    node.Right = new Node(value);
                    return;
                }
                node = node.Right;
            }
            else
            {
                node.Count++; // If value is already present, increase the count
                return;
            }
        }
    }
    
    /// <summary>
    /// Helper method to remove a value from the tree recursively.
    /// </summary>
    private static Node? RemoveNode(Node? node, int value)
    {
        if (node is null) return null;

        if (value < node.Value)
        {
            node.Left = RemoveNode(node.Left, value);
        }
        else if (value > node.Value)
        {
            node.Right = RemoveNode(node.Right, value);
        }
        else
        {
            if (node.Count > 1)
            {
                node.Count--; // If duplicates exist, decrement the count
            }
            else
            {
                // Node has no or one child
                if (node.Left == null) return node.Right;
                if (node.Right == null) return node.Left;

                // Node with two children: find the smallest node in the right subtree
                var minNode = GetMinNode(node.Right);
                node.Value = minNode.Value;
                node.Count = minNode.Count;
                node.Right = RemoveNode(node.Right, minNode.Value);
                minNode.Count = 1; // Reset successor node's count after transfer
            }
        }
        return node;
    }

    /// <summary>
    /// Finds the node with the minimum value starting from the given node.
    /// </summary>
    private static Node GetMinNode(Node node)
    {
        while (node.Left != null)
        {
            node = node.Left;
        }
        return node;
    }

    /// <summary>
    /// Performs an in-order traversal to collect the values into a list.
    /// </summary>
    private static void InOrderTraversal(Node? node, List<int> result)
    {
        if (node is null) return;

        InOrderTraversal(node.Left, result);

        for (var i = 0; i < node.Count; i++) // Add the value as many times as its count
        {
            result.Add(node.Value);
        }

        InOrderTraversal(node.Right, result);
    }
    
    /// <summary>
    /// Helper method to count the occurrences of a value.
    /// </summary>
    private static int CountValue(Node? node, int value)
    {
        while (node != null)
        {
            if (value < node.Value)
            {
                node = node.Left;
            }
            else if (value > node.Value)
            {
                node = node.Right;
            }
            else
            {
                return node.Count;
            }
        }
        return 0; // Value not found
    }
    
    #endregion
}
