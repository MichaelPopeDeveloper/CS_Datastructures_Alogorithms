using System;
using System.Collections.Generic;

namespace HashTable
{
    // A key/value associative collection

    public class HashTable<TKey, TValue>
    {
        // if the array array exceeds this fill percentage it will grow
        // in this example the fill factor is the total number of items
        // regardless of wether they are collisions or not.

        // the maximum number of items to store before growing.
        // This is just a cached value of the fill factor calculation
        int _maxItemsAtCurrentSize;

        // the number of items in the hash table
        int _count;

        // The array where the items are stored.
        HashTableArray<TKey, TValue> _array;

        // Constructs a hash table with the default capacity
        public HashTable()
            : this(1000)
        {

        }

        // Constructs a hash table with the specified capacity
        public HashTable(int initialCapacity)
        {
            if (initialCapacity < 1)
            {
                throw new ArgumentOutOfRangeException("initialCapacity");
            }

            _array = new HashTableArray<TKey, TValue>(initialCapacity);

            // when the count exceeds this value, the enxt Add will cause the
            // array to grow
            _maxItemsAtCurrentSize = (int)(initialCapacity * _fillFactor) + 1;
        }

        // Adds the key/value pair to the hash table. If the key already exists in the
        // hash table an ArgumentException will be thrown
        public void Add(TKey key, TValue value)
        {
            // if we are at capacity, the array needs to grow
            if (_count >= _maxItemsAtCurrentSize)
            {
                // allocate a larger array
                HashTableArray<TKey, TValue> largerArray = new HashTableArray<TKey, TValue>(_array.Capacity * 2);

                // and re-add each item to the new array
                foreach (HashTableNodePair<TKey, TValue> node in _array.Items)
                {
                    largerArray.Add(node.Key, node.Value);
                }

                // the larger array is now the hash table storage
                _array = largerArray;

                // update the new max items cached value
                _maxItemsAtCurrentSize = (int)(_array.Capacity * _fillFactor) + 1;

            }

            _array.Add(key, value);
            _count++;
        }

        //Removes the item from the hash table whose key matches
        // the specified key.
        public bool Remove(TKey key)
        {
            boll removed = _array.Remove(key);
            if (removed)
            {
                _count--;
            }

            return removed;
        }

        //Gets and sets the value with the specified key. ArgumentException is
        // throw if the key does not already exist in the hash table.
        public TValue this[TKey key]
        {
            get
            {
                TValue value;
                if (!_array.TryGetValue(key, out value))
                {
                    throw new ArgumentException("key");
                }

                return value;
            }
            set
            {
                _array.Update(key, value);
            }
        }

        // Finds and returns the value for the specified key.
        public bool TryGetValue(TKey key, out TValue value)
        {
            return _array.TryGetValue(key, out value);
        }


        // Returns a boolean indicating if the hash table contains the specified value.
        public bool ContainsValue(TValue value)
        {
            foreach (TValue foundValue in _array.Values)
            {
                if (value.Equals(foundValue))
                {
                    return true;
                }
            }

            return false;
        }

        // Returns an enumerator for all of the keys in the hash table
        public IEnumerable<TKey> Keys
        {
            get
            {
                foreach (TKey key in _array.Keys)
                {
                    yield return key;
                }
            }
        }

        // Returns an enumerator for all of the keys in the hash table
        public IEnumerable<TValue> Values
        {
            get
            {
                foreach (TValue value in _array.Values)
                {
                    yield return value;
                }
            }
        }

        // Removes all items from the hash table
        public void Clear()
        {
            _array.Clear();
            _count = 0;
        }

        // The number of items currently in the hash table
        public int Count
        {
            get
            {
                return _count;
            }

        }
    }
}