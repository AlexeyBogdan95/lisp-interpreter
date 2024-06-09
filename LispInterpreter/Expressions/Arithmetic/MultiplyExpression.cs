using LispInterpreter.Exception;

namespace LispInterpreter.Expressions.Arithmetic;

public class MultiplyExpression: IExpression
{
    public string Operator => "*";

    public ExpressionResult Execute(params string[] operands)
    {
        if (operands.Length < 2)
        {
            throw new InvalidExpressionException("Add expression should have at least two operands");
        }

        var result = operands.Aggregate(1, (current, operand) => current * int.Parse(operand));
        return ExpressionResult.CreateValueResult(result.ToString());
    }
}