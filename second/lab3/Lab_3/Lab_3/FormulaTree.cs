using System.Text;

namespace Lab_3;

public class FormulaTree
{
    public Node Root { get; private set; }

    public FormulaTree(object value) => Root = new Node(value);

    public FormulaTree(Node node) => Root = node;

    public void Insert(object value) => InsertHelper(value, Root);

    public void Insert(Node node) => InsertHelper(node, Root);

    public string LevelTraverse() => LevelTraverseHelper(Root, new StringBuilder(Root.ToString().PadLeft(5) + "\n"));

    private void InsertHelper(object value, Node root)
    {
        if (root.Right == null)
            root.Right = new Node(value);
        else if (root.Left == null)
            root.Left = new Node(value);
        else
            InsertHelper(value, root.Left);
    }

    private void InsertHelper(Node node, Node root)
    {
        if (root.Right == null)
            root.Right = node;
        else if (root.Left == null)
            root.Left = node;
        else
            InsertHelper(node, root.Left);
    }

    private string LevelTraverseHelper(Node root, StringBuilder result)
    {
        if (root.Left != null)
            result.Append(root.Left);
        if (root.Right != null)
            result.Append(root.Right + "\n");
        if (root.Left != null)
            LevelTraverseHelper(root.Left, result);
        return result.ToString();
    }
}
