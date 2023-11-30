using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSCI_2210___AVL_Tree_Lab
{
    internal class AVLTree<T> where T : IComparable<T>
    {
        AvlTreeNode<T> root;
        public AVLTree() { }

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
    }
}
