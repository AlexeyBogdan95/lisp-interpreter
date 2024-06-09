using LispInterpreter.Exception;

namespace LispInterpreter.Expressions.Conditions;

public class ConditionExpression: IExpression
{
    public string Operator => "if";

    public ExpressionResult Execute(params string[] operands)
    {
        if (operands.Length != 3)
        {
            throw new InvalidExpressionException("Condition expression requires three operands");
        }

        var booleanOperand = operands[0];
        var leftOperand = operands[1];
        var rightOperand = operands[2];

        if (booleanOperand is not (ConditionResultReturnValues.True or ConditionResultReturnValues.False))
        {
            throw new InvalidExpressionException("First operand of condition expression should be a boolean value");
        }

        if (!int.TryParse(leftOperand, out _) || !int.TryParse(rightOperand, out _))
        {
            throw new InvalidExpressionException("Second and third operands of condition expression should be numbers");
        }

        return ExpressionResult.CreateValueResult(booleanOperand == "True" ? leftOperand : rightOperand);
    }
}