using LispInterpreter.Exception;

namespace LispInterpreter;

public interface IExpressionParser
{
    ExpressionNode GetTree(string command);
}

public class ExpressionParser: IExpressionParser
{
    public ExpressionNode GetTree(string command)
    {
        var tokens = GetTokens(command);
        var (node, endIndex) = GetTree(tokens, 0);
        if (endIndex != tokens.Length)
        {
            throw new InvalidCommandException();
        }

        return node;
    }

    private string[] GetTokens(string command)
    {
        return command
            .Replace("(", " ( ")
            .Replace(")", " ) ")
            .Split()
            .Where(x => !string.IsNullOrEmpty(x))
            .ToArray();
    }

    private (ExpressionNode node, int endIndex) GetTree(string[] tokens, int index)
    {
        ExpressionNode? node = null;
        while (index < tokens.Length)
        {
            var token = tokens[index];
            if (token == ")")
            {
                break;
            }

            if (token == "(" && node == null)
            {
                node = new ExpressionNode();
                index++;
                continue;
            }

            if (token == "(" && node != null)
            {
                var nodeResult = GetTree(tokens, index);
                node.Children.Add(nodeResult.node);
                index = nodeResult.endIndex;
                continue;
            }

            if (string.IsNullOrEmpty(node.Value))
            {
                node.SetValue(token);
                index++;
                continue;
            }

            node.Children.Add(new ExpressionNode(token));
            index++;
        }

        if (node == null || !node.IsFinite())
        {
            throw new InvalidCommandException();
        }

        return (node, index + 1);
    }
}