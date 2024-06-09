using System.Text.RegularExpressions;
using LispInterpreter.Exception;

namespace LispInterpreter.Expressions;

public class DefineExpression: IExpression
{
    private readonly Regex _variableRegex = new("^[a-z]$");

    public string Operator => "define";

    public ExpressionResult Execute(params string[] operands)
    {
        if (operands.Length != 2)
        {
            throw new InvalidExpressionException("Define expression requires two operands");
        }

        var variableName = operands[0];
        var variableValue = operands[1];

        if (!_variableRegex.IsMatch(variableName))
        {
            throw new InvalidExpressionException("First operand of define expression must be a letter");
        }

        if (!int.TryParse(variableValue, out _))
        {
            throw new InvalidExpressionException("Second operand of define expression must be a number");
        }

        return ExpressionResult.CreateSetVariableResult(variableName, variableValue);
    }
}