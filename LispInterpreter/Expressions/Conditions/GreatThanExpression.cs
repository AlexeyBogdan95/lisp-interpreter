using LispInterpreter.Exception;

namespace LispInterpreter.Expressions.Conditions;

public class GreatThanExpression: IExpression
{
    public string Operator => ">";

    public ExpressionResult Execute(params string[] operands)
    {
        if (operands.Length != 2)
        {
            throw new InvalidExpressionException("GreatThan expression requires two operands");
        }

        var leftOperand = operands[0];
        var rightOperand = operands[1];

        if (int.TryParse(leftOperand, out var left) && int.TryParse(rightOperand, out var right))
        {
            return ExpressionResult.CreateValueResult(
                left > right ? ConditionResultReturnValues.True : ConditionResultReturnValues.False);
        }

        throw new InvalidExpressionException("Operands of GreanThan expression should be numbers");
    }
}