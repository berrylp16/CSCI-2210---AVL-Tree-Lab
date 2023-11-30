namespace CSCI_2210___AVL_Tree_Lab
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            AVLTree<int> tree = new AVLTree<int>();
            AvlTreeNode<int> yay = new AvlTreeNode<int>(40);
            AvlTreeNode<int> yay1 = new AvlTreeNode<int>(23);
            AvlTreeNode<int> yay2 = new AvlTreeNode<int>(102);
            AvlTreeNode<int> yay3 = new AvlTreeNode<int>(39);
            AvlTreeNode<int> yay4 = new AvlTreeNode<int>(12);
            AvlTreeNode<int> yay5 = new AvlTreeNode<int>(2);
            AvlTreeNode<int> yay6 = new AvlTreeNode<int>(38);
            tree.Add(yay, 0);
            tree.Add(yay1, 1);
            tree.Add(yay2, 2);
            tree.Add(yay3, 3);
            tree.Add(yay4, 4);
            tree.Add(yay5, 5);
            Console.WriteLine(tree);
        }
    }
}
