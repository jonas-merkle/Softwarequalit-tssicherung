using FsCheck.Xunit;

namespace BinarySearchTree.Tests;

//using Lib;
using Lib.Mutants.M10;

/// <summary>
/// Unit tests for the BinarySearchTree class using property-based testing.
/// </summary>
public class PropertyTests
{
    /// <summary>
    /// Tests that adding a value increases the count and sum as expected.
    /// </summary>
    /// <param name="value">The value to add to the tree.</param>
    [Property]
    public void Add_IncreasesCountAndSum(int value)
    {
        // Arrange
        var tree = new BinarySearchTree();
        var initialCount = tree.Count(value);               // Get initial count of the value
        var initialSum = tree.Sum();                        // Get initial sum of all values

        // Act
        tree.Add(value);                                    // Add the value to the tree

        // Assert
        Assert.Equal(initialCount + 1, tree.Count(value));  // Verify that the count of the value has increased by 1
        Assert.Equal(initialSum + value, tree.Sum());       // Verify that the sum of all values has increased by the added value
    }

    /// <summary>
    /// Tests that removing a value decreases the count and sum as expected.
    /// </summary>
    /// <param name="value">The value to remove from the tree.</param>
    [Property]
    public void Remove_DecreasesCountAndSum(int value)
    {
        // Arrange
        var tree = new BinarySearchTree();
        tree.Add(value);                                    // Add the value to ensure it exists in the tree
        var initialCount = tree.Count(value);               // Get initial count of the value
        var initialSum = tree.Sum();                        // Get initial sum of all values

        // Act
        tree.Remove(value);                                 // Remove the value from the tree

        // Assert
        Assert.Equal(initialCount - 1, tree.Count(value));  // Verify that the count of the value has decreased by 1
        Assert.Equal(initialSum - value, tree.Sum());       // Verify that the sum of all values has decreased by the removed value
    }

    /// <summary>
    /// Tests that the ToList method contains all added values in sorted order.
    /// </summary>
    /// <param name="values">The list of values to add to the tree.</param>
    [Property]
    public void ToList_ContainsAllAddedValues(List<int> values)
    {
        // Arrange
        var tree = new BinarySearchTree();
        var sortedValues = new List<int>(values);
        sortedValues.Sort();                                // Sort the original list of values
        
        // Act
        foreach (var value in values)
        {
            tree.Add(value);                                // Add each value to the tree
        }

        // Assert
        Assert.Equal(sortedValues, tree.ToList());          // Verify that the tree's ToList method returns the values in sorted order
    }

    /// <summary>
    /// Tests that the Count method reflects the number of occurrences of each value.
    /// </summary>
    /// <param name="values">The list of values to add to the tree.</param>
    [Property]
    public void Count_ReflectsNumberOfOccurrences(List<int> values)
    {
        // Arrage
        var tree = new BinarySearchTree();
        var valueCounts = new Dictionary<int, int>();

        // Act
        foreach (var value in values)
        {
            tree.Add(value);                                // Add each value to the tree
            if (!valueCounts.TryAdd(value, 1))
            {
                valueCounts[value]++;                       // Track the count of each value
            }
        }

        // Assert
        foreach (var kvp in valueCounts)
        {
            Assert.Equal(kvp.Value, tree.Count(kvp.Key));   // Verify that the tree's Count method returns the correct number of occurrences for each value
        }
    }

    /// <summary>
    /// Tests that the Sum method reflects the sum of all values in the tree.
    /// </summary>
    /// <param name="values">The list of values to add to the tree.</param>
    [Property]
    public void Sum_ReflectsSumOfAllValues(List<int> values)
    {
        // Arrange
        var tree = new BinarySearchTree();
        var expectedSum = 0;

        // Act
        foreach (var value in values)
        {
            tree.Add(value);                                // Add each value to the tree
            expectedSum += value;                           // Calculate the expected sum
        }

        // Asset
        Assert.Equal(expectedSum, tree.Sum());              // Verify that the tree's Sum method returns the correct sum of all values
    }

    /// <summary>
    /// Tests that merging two trees results in the same tree regardless of the order of merging.
    /// </summary>
    /// <param name="valuesA">The list of values to add to the first tree.</param>
    /// <param name="valuesB">The list of values to add to the second tree.</param>
    [Property]
    public void MergingTwoTrees_ResultsInSameTree(List<int> valuesA, List<int> valuesB)
    {
        // Arrange
        var treeA = new BinarySearchTree();
        var treeB = new BinarySearchTree();
        foreach (var value in valuesA)
        {
            treeA.Add(value);                               // Add each value to the first tree
        }
        foreach (var value in valuesB)
        {
            treeB.Add(value);                               // Add each value to the second tree
        }

        // Act
        var mergedTree1 = new BinarySearchTree();           // Merge A then B
        foreach (var value in treeA.ToList())
        {
            mergedTree1.Add(value);                         // Merge values from the first tree
        }
        foreach (var value in treeB.ToList())
        {
            mergedTree1.Add(value);                         // Merge values from the second tree
        }

        var mergedTree2 = new BinarySearchTree();           // Merge B then A
        foreach (var value in treeB.ToList())
        {
            mergedTree2.Add(value);                         // Merge values from the second tree
        }
        foreach (var value in treeA.ToList())
        {
            mergedTree2.Add(value);                         // Merge values from the first tree
        }

        // Assert
        Assert.Equal(mergedTree1.ToList(), mergedTree2.ToList()); // Verify that both merged trees contain the same values in the same order
    }
}