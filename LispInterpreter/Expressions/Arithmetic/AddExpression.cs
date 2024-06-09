using System.Data;

namespace LispInterpreter.Expressions.Arithmetic;

public class AddExpression: IExpression
{
    public string Operator => "+";

    public ExpressionResult Execute(params string[] operands)
    {
        if (operands.Length < 2)
        {
            throw new InvalidExpressionException("Add expression should have at least two operands");
        }

        return ExpressionResult.CreateValueResult(operands.Sum(int.Parse).ToString());
    }
}