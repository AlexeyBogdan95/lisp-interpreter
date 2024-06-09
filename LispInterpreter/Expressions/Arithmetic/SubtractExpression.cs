using System.Data;

namespace LispInterpreter.Expressions.Arithmetic;

public class SubtractExpression: IExpression
{
    public string Operator => "-";

    public ExpressionResult Execute(params string[] operands)
    {
        if (operands.Length < 2)
        {
            throw new InvalidExpressionException("Add expression should have at least two operands");
        }

        var result = int.Parse(operands[0]);

        for (int i = 1; i < operands.Length; i++)
        {
            result -= int.Parse(operands[i]);
        }

        return ExpressionResult.CreateValueResult(result.ToString());
    }
}