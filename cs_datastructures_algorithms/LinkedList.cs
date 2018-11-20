namespace LinkedList
{
	public class LinkedList<T> :
	System.Collections.Generic.ICollection<T>
	{
		public LinkedListNode<T> Head {
			get;
			private set;
		}

		public LinkedListNode<T> Tail {
			get;
			private set;
		}

		#region Add
		public void AddFirst(T value)
		{
			AddFirst (new LinkedListNode<T> (value));
		}
		public void AddFirst(LinkedListNode<T> node)
		{
			// Save off the head node so we don't lose it
			LinkedListNode<T> temp = Head;

			// Point head to new node
			Head = node;

			// Insert the rest of the list behind the head
			Head.Next = temp;

			Count++;

			if (Count == 1) {
			// if the list was empty then Head and Tail should both point to new node
				Tail = Head;
			}
		}

		public void AddLast(T value)
		{
			AddLast (new LinkedListNode<T> (value));
		}

		public void AddLast(LinkedListNode<T> node)
		{
			if (Count == 0) {
				Head = node;
			} else {
				Tail.Next = node;
			}
			Tail = node;
			Count++;
			#endregion
		}

		#region Remove

		public void RemoveFirst()
		{
			if (Count != 0)
			{
				Head = Head.Next;
				Count--;
				if (Count == 0)
				{
					Tail = null;
				}
			}
		}

		public void RemoveLast()
		{
			if (Count != 0) {
				Head = null;
				Tail = null;
			}
		}


		#endregion





	}
}

