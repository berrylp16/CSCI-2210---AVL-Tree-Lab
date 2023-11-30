using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CSCI_2210___AVL_Tree_Lab
{
    internal class AvlTreeNode<T> where T : IComparable<T>
    {
        internal T Data { get; set; }
        internal AvlTreeNode<T>? Left { get; set; }
        internal AvlTreeNode<T>? Right { get; set; }
        internal int Height { get; set; }

        internal AvlTreeNode(T data)
        {
            this.Data = data;
            this.Height = 0;
        }

        internal void UpdateHeight()
        {
            // Recall that the height of a node in a tree 
            // is exactly one higher than its highest child node.
            int leftChildHeight = this.Left?.Height ?? -1;
            int rightchildHeight = this.Right?.Height ?? -1;
            int highestChild = Math.Max(leftChildHeight, rightchildHeight);
            this.Height = highestChild + 1;
        }

        internal int GetBalanceFactor()
        {
            int rightHeight = 0;
            int leftHeight = 0;
            if (this.Right != null)
                rightHeight = this.Right.Height;
            if (this.Left != null)
                leftHeight = this.Left.Height;
            int BalanceFactor = rightHeight - leftHeight;
            return BalanceFactor;
        }

        public AvlTreeNode<T> Add(AvlTreeNode<T> root, T data)
        {
            if (root == null)
            {
                root = new AvlTreeNode<T>(data);
            }
            else if (data.CompareTo(root.Data) < 0)
            {
                root.Left = Add(root.Left, data);
            }
            else if (data.CompareTo(root.Data) > 0)
            {
                root.Right = Add(root.Right, data);
            }
            return root;
            var node = Add(root, data);
            node.UpdateHeight();
            return Rebalance(node);
        }

        public AvlTreeNode<T>? Remove(AvlTreeNode<T> root, T data)
        {
            // Is the root node null?
            // we've reached the bottom of the tree
            var node = Remove(root, data);
            if (root == null) return null;

            if (data.CompareTo(root.Data) < 0)
            {
                // Take a left
                root.Left = Remove(root.Left, data);
            }
            else if (data.CompareTo(root.Data) > 0)
            {
                // Take a right
                root.Right = Remove(root.Right, data);
            }
            // We found the data
            else
            {
                // Determine which of the 3 cases has occurred
                // leaf node
                if (root.Left == null && root.Right == null)
                {
                    root = null;
                }
                // Has right child
                // OR Has two children
                else if (root.Right != null)
                {
                    // Deal with the child on the right
                    var successor = SuccessorSearch(root);
                    root.Data = successor.Data;
                    root.Right = Remove(root.Right, successor.Data);
                }
                // Has left child
                else if (root.Left != null)
                {
                    // Deal with the child on the left
                    var predecessor = PredecessorSearch(root);
                    root.Data = predecessor.Data;
                    root.Left = Remove(root.Left, predecessor.Data);
                }
            }

            return root;
            node.UpdateHeight();
            return Rebalance(node);
        }

        public AvlTreeNode<T>? SuccessorSearch(AvlTreeNode<T> root)
        {
            var currentNode = root.Right;
            while (currentNode.Left != null)
                currentNode = currentNode.Left;
            return currentNode;
        }

        public AvlTreeNode<T>? PredecessorSearch(AvlTreeNode<T> root)
        {
            var currentNode = root.Left;
            while (currentNode.Right != null)
                currentNode = currentNode.Right;
            return currentNode;
        }

        public AvlTreeNode<T>? RotateLeft(AvlTreeNode<T> currentNode)
        {
            var node = currentNode.Left;
            var rightNode = currentNode.Right;
            currentNode.Right = currentNode.Right.Left;
            currentNode.UpdateHeight();
            rightNode.UpdateHeight();
            return rightNode;
        }

        public AvlTreeNode<T>?RotateRight(AvlTreeNode<T> currentNode)
        {
            var node = currentNode.Right;
            var leftNode = currentNode.Left;
            currentNode.Left = currentNode.Left.Right;
            currentNode.UpdateHeight();
            leftNode.UpdateHeight();
            return leftNode;

        }

        public AvlTreeNode<T>? Rebalance(AvlTreeNode<T> currentNode)
        {
            if(GetBalanceFactor() < -1)
            {
                if(Left.GetBalanceFactor() <= 0)
                {
                    currentNode = RotateRight(currentNode);
                }
                else
                {
                   currentNode.Left = RotateLeft(currentNode.Left);
                   currentNode =  RotateRight(currentNode);
                }
            }
            if(GetBalanceFactor() > 1)
            {
                if(Right.GetBalanceFactor() >= 0)
                {
                    currentNode = RotateLeft(currentNode);
                }
                else
                {
                   currentNode.Right = RotateRight(currentNode.Right);
                   currentNode.Right = RotateLeft(currentNode.Right);
                }
            }
            return currentNode;
        }
    }
}
