using System;
using System.Collections.Generic;

namespace HashTable
{
    // The fixed size array of the nodes in the hash table

    class HashTableArray<TKey, TValue>
    {
        HashTableArrayNode<TKey, TValue>[] _array;

        // Constructs a new hash table with the specified capacity

        public HashTableArray(int capacity)
        {
            _array = new HashTableArrayNode<TKey, TValue>[capacity];
            for (int i = 0; i < capacity; i++)
            {
                _array[i] = new HashTableArrayNode<TKey, TValue>();
            }
        }

        // Adds the key/value pair to the node. If the key already exists in the
        // node array an ArgumentException will be thrown
        public void Add(TKey key, TValue value)
        {
            _array[GetIndex(key)].Add(key, value);
        }

        //Updates the value of the existing key/value pair in the node array.
        // If the key does not exist in the array an ArgumentException
        // will be thrown.
        public void Update(TKey key, TValue value)
        {
            _array[GetIndex(key)].Update(key, value);
        }

        // Removes the item from the node array whose key matches
        // the specified key.
        public bool Remove(TKey key)
        {
            return _array[GetIndex(key)].Remove(key);
        }

        // Finds and returns the value for the specified key.
        public bool TryGetValue(TKey key, out TValue value)
        {
            return _array[GetIndex(key)].TryGetValue(key, out value);
        }

        // The capacity of the hash table array
        public int Capacity
        {
            get
            {
                return _array.Length;
            }
        }

        //Removes every item from the hash table array
        public void Clear()
        {
            foreach (HashTableArrayNode<TKey, TValue> node in _array)
            {
                node.Clear();
            }
        }

        //Returns an enumerator for all of the values in the node array
        public IEnumerable<TValue> Values
        {
            get
            {
                foreach (HashTableArrayNode<TKey, TValue> node in _array)
                {
                    foreach (TValue value in node.Values)
                    {
                        yield return value;
                    }
                }
            }
        }
    }
}