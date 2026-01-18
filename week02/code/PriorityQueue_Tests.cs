using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;


// TODO Problem 2 - Write and run test cases and fix the code to match requirements.


[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Enqueue three items with different priorities and then dequeue all of them.
    // Expected Result: Items are returned in order of highest priority first: "B" (5), "C" (3), "A" (1).
    // Defect(s) Found: Dequeue originally did not remove items from the internal list and skipped checking the last element.
    public void TestPriorityQueue_1()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 1);
        priorityQueue.Enqueue("B", 5);
        priorityQueue.Enqueue("C", 3);

        Assert.AreEqual("B", priorityQueue.Dequeue());
        Assert.AreEqual("C", priorityQueue.Dequeue());
        Assert.AreEqual("A", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Enqueue multiple items with the same highest priority and then dequeue all of them.
    // Expected Result: Items with equal highest priority are dequeued in FIFO order: "A", "B", then "C".
    // Defect(s) Found: Dequeue originally did not remove the item from the list, so repeated calls returned the same value instead of moving to the next.
    public void TestPriorityQueue_2()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 10);
        priorityQueue.Enqueue("B", 10);
        priorityQueue.Enqueue("C", 10);

        Assert.AreEqual("A", priorityQueue.Dequeue());
        Assert.AreEqual("B", priorityQueue.Dequeue());
        Assert.AreEqual("C", priorityQueue.Dequeue());
    }

    // Add more test cases as needed below.

    [TestMethod]
    // Scenario: Call Dequeue on an empty queue.
    // Expected Result: InvalidOperationException is thrown with message "The queue is empty.".
    // Defect(s) Found: None (exception type and message were already correct).
    public void TestPriorityQueue_Empty()
    {
        var priorityQueue = new PriorityQueue();

        var ex = Assert.ThrowsException<InvalidOperationException>(() => priorityQueue.Dequeue());
        Assert.AreEqual("The queue is empty.", ex.Message);
    }
}
