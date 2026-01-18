public class PersonQueue
{
    private readonly List<Person> _items = new();

    public int Length => _items.Count;

    public void Enqueue(Person person)
    {
        // add to the back
        _items.Add(person);
    }

    public Person Dequeue()
    {
        if (IsEmpty())
        {
            throw new InvalidOperationException("Queue is empty.");
        }

        // remove from the front (index 0)
        var person = _items[0];
        _items.RemoveAt(0);
        return person;
    }

    public bool IsEmpty()
    {
        return _items.Count == 0;
    }

    public override string ToString()
    {
        return $"[{string.Join(", ", _items)}]";
    }
}
