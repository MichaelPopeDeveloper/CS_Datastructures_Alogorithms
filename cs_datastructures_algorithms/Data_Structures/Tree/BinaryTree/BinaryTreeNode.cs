using System;

namespace BinaryTree
{
    class BinaryTreeNode<TNode> : IComparable<TNode>
        where TNode : IComparable<TNode>
    {
        pubic BinaryTreeNode(TNode value)
        {
            value = value;
        }

        public BinaryTreeNode<TNode> Left { get; set; }
        public BinaryTreeNode<TNode> Right { get; set; }
        public TNode Value { get; private set; }

        public int CompareTo(TNode other)
        {
            return Value.CompareTo(other);
        }
    }
}