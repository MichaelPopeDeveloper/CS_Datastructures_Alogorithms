using System;
using System.Collections.Generic;

namespace HashTable
{
    // The hash table data chain
    class HashTableArrayNode<TKey, TValue>
    {
        // This list contains the actual data in the hash table. It chains together
        // data collisions. It would be possible to use a list only when there is a collision
        // to avoid allocating the list unneccessarily but this approach makes the
        // implementation easier to follow for this sample.
        LinkedList<HashTableNodePair<TKey, TValue>> _items;

        // Adds the key/value pair to the node. If the key already exists in the
        // list an ArgumentException will be thrown
        public void Add(TKey key, TValue value)
        {
            // lazy init the linked list
            if (_items == null)
            {
                _items = new LinkedList<HashTableNodePair<TKey, TValue>>();
            }
            else
            {
                // Multiple items might collide and exist in this list - but each
                // key should only be in the list once.
                foreach (HashTableNodePair<TKey, TValue> pair in _items)
                {
                    if (pair.Key.Equals(key))
                    {
                        throw new ArgumentException("The collection already contains the key");
                    }
                }
            }
            // if we made it this far - add the item
            _items.AddFirst(new HashTableNodePair<TKey, TValue>(key, value));
        }

        // Updates the value of the existing key/value pair in the list.
        // If the key does not exist in the list an ArgumentException
        // will be thrown.
        public void Update(TKey key, TValue value)
        {
            boll updated = false;
            if (_items != null)
            {
                // check each item in the list for the specified key
                foreach(HashTableNodePair<TKey, TValue> pair in _items)
                {
                    if (pair.Key.Equals(key))
                    {
                        //Update the value
                        pair.Value = value;
                        updated = true;
                        break;
                    }
                }
            }

            if (!updated)
            {
                throw new ArgumentException("The collection does not contain the key");
            }
        }

        // Finds and returns the value for the specified key.
        public bool  TryGetValue(TKey key, out TValue value)
        {
            value = default(TValue);

            boll found = false;

            if (_items != null)
            {
                foreach (HashTableNodePair<TKey, TValue> pair in _items)
                {
                    if (pair.Key.Equals(key))
                    {
                        value = pair.Value;
                        found = true;
                        break;
                    }
                }
            }

            return found;
        }

        // Removes the item from the list whose key matches 
        // the specified key.
        public bool Remove(TKey key)
        {
            bool removed = false;
            if (_items != null)
            {
                LinkedListNode<HashTableNodePair<TKey, TValue>> current = _items.First;
                while (current != null)
                {
                    if (current.Value.Key.Equals(key))
                    {
                        _items.Remove(current);
                        removed = true;
                        break;
                    }
                    current = current.Next;
                }
            }
            return removed;
        }

        // Removes all the items from the list
        public void Clear()
        {
            if (_items != null)
            {
                _items.Clear();
            }
        }

        // Returns an enumerator for all of the values in the list
        public IEnumerable<TValue> Values
        {
            get
            {
                if (_items != null)
                {
                    foreach (HashTableNodePair<TKey, TValue> node in _items)
                    {
                        yield return node.Value;
                    }
            }
        }
    }

        // Returns an enumerator for all of the keys in the list
        public IEnumerable<TKey> Keys
        {
            get
            {
                if (_items != null)
                {
                    foreach (HashTableNodePair<TKey, TValue> node in _items)
                {
                        yield return node.Key;
                }
            }
        }
}