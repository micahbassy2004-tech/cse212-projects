using System.Collections;
using System.Linq;
public class LinkedList : IEnumerable<int>
{
    private Node? _head;
    private Node? _tail;

    /// <summary>
    /// Insert a new node at the front (i.e. the head) of the linked list.
    /// </summary>
    public void InsertHead(int value)
    {
        Node newNode = new(value);
        if (_head is null)
        {
            _head = newNode;
            _tail = newNode;
        }
        else
        {
            newNode.Next = _head;
            _head.Prev = newNode;
            _head = newNode;
        }
    }

    /// <summary>
    /// Insert a new node at the back (i.e. the tail) of the linked list.
    /// </summary>
    public void InsertTail(int value)
    {
        if (_head is null)
        {
            InsertHead(value);
            return;
        }

        Node newNode = new(value);
        newNode.Prev = _tail;
        _tail!.Next = newNode;
        _tail = newNode;
    }

    /// <summary>
    /// Remove the first node (i.e. the head) of the linked list.
    /// </summary>
    public void RemoveHead()
    {
        if (_head == _tail)
        {
            _head = null;
            _tail = null;
        }
        else if (_head is not null)
        {
            _head.Next!.Prev = null;
            _head = _head.Next;
        }
    }

    /// <summary>
    /// Remove the last node (i.e. the tail) of the linked list.
    /// </summary>
    public void RemoveTail()
    {
        if (_tail is null)
            return;

        if (_head == _tail)
        {
            RemoveHead();
            return;
        }

        _tail = _tail.Prev;
        _tail!.Next = null;
    }

    /// <summary>
    /// Insert 'newValue' after the first occurrence of 'value' in the linked list.
    /// </summary>
    public void InsertAfter(int value, int newValue)
    {
        Node? curr = _head;
        while (curr is not null)
        {
            if (curr.Data == value)
            {
                if (curr == _tail)
                {
                    InsertTail(newValue);
                }
                else
                {
                    Node newNode = new(newValue);
                    newNode.Prev = curr;
                    newNode.Next = curr.Next;
                    curr.Next!.Prev = newNode;
                    curr.Next = newNode;
                }

                return;
            }
            curr = curr.Next;
        }
    }

    /// <summary>
    /// Remove the first node that contains 'value'.
    /// </summary>
    public void Remove(int value)
    {
        Node? current = _head;

        while (current is not null)
        {
            if (current.Data == value)
            {
                if (current == _head)
                    RemoveHead();
                else if (current == _tail)
                    RemoveTail();
                else
                {
                    current.Prev!.Next = current.Next;
                    current.Next!.Prev = current.Prev;
                }
                return;
            }
            current = current.Next;
        }
    }

    /// <summary>
    /// Search for all instances of 'oldValue' and replace the value to 'newValue'.
    /// </summary>
    public void Replace(int oldValue, int newValue)
    {
        Node? current = _head;

        while (current is not null)
        {
            if (current.Data == oldValue)
                current.Data = newValue;

            current = current.Next;
        }
    }

    /// <summary>
    /// Yields all values in the linked list
    /// </summary>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }

    /// <summary>
    /// Iterate forward through the Linked List
    /// </summary>
    public IEnumerator<int> GetEnumerator()
    {
        var curr = _head;
        while (curr is not null)
        {
            yield return curr.Data;
            curr = curr.Next;
        }
    }

    /// <summary>
    /// Iterate backward through the Linked List
    /// </summary>
    public IEnumerable Reverse()
    {
        Node? curr = _tail;
        while (curr is not null)
        {
            yield return curr.Data;
            curr = curr.Prev;
        }
    }

    public override string ToString()
    {
        return "<LinkedList>{" + string.Join(", ", this) + "}";
    }

    // Just for testing.
    public bool HeadAndTailAreNull()
    {
        return _head is null && _tail is null;
    }

    // Just for testing.
    public bool HeadAndTailAreNotNull()
    {
        return _head is not null && _tail is not null;
    }
}