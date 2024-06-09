using System.ComponentModel;
using LispInterpreter.Expressions;

namespace LispInterpreter;

public interface ICalculator
{
    ExpressionNode Calculate(ExpressionNode node);
}

public class Calculator: ICalculator
{
    private readonly IExpressionFactory _factory = new ExpressionFactory();
    private readonly Dictionary<string, string> _variablesStore = new();

    public ExpressionNode Calculate(ExpressionNode node)
    {
        if (node.Children.Count == 0)
        {
            return node;
        }

        if (node.Children.Count > 0)
        {
            node.SetChildren(node.Children.Select(Calculate).ToList());
        }

        var expression = _factory.GetExpression(node.Value);
        var operands = node.Children
            .Select(n => _variablesStore.TryGetValue(n.Value, value: out var result) ? result : n.Value)
            .ToArray();

        var expressionResult = expression.Execute(operands);
        switch (expressionResult.Type)
        {
            case ExpressionResultType.Value:
                return new ExpressionNode(expressionResult.Result);

            case ExpressionResultType.SetVariable:
                _variablesStore[expressionResult.Variable] = expressionResult.Result;
                return new ExpressionNode(expressionResult.Variable);

            default:
                throw new InvalidEnumArgumentException();
        }
    }
}