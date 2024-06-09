namespace LispInterpreter;

public class ExpressionNode
{
    public string Value { get; private set; } = "";
    public List<ExpressionNode> Children { get; private set; } = [];

    public ExpressionNode()
    {
    }

    public ExpressionNode(string value)
    {
        Value = value;
    }

    public void SetValue(string value)
    {
        Value = value;
    }

    public void SetChildren(List<ExpressionNode> children)
    {
        Children = children;
    }

    public bool IsFinite()
    {
        return !string.IsNullOrEmpty(Value) && Children.Count > 0;
    }
}