using LispInterpreter.Exception;
using LispInterpreter.Expressions.Arithmetic;
using LispInterpreter.Expressions.Conditions;

namespace LispInterpreter.Expressions;

public interface IExpressionFactory
{
    IExpression GetExpression(string operand);
}

public class ExpressionFactory: IExpressionFactory
{
    private readonly Dictionary<string, IExpression> _expressions = new();

    public ExpressionFactory()
    {
        IExpression[] expressions = [
            new AddExpression(),
            new SubtractExpression(),
            new MultiplyExpression(),
            new DivideExpression(),
            new DefineExpression(),
            new ConditionExpression(),
            new LessThanExpression(),
            new GreatThanExpression()
        ];

        foreach (var exp in expressions)
        {
            _expressions.Add(exp.Operator, exp);
        }
    }


    public IExpression GetExpression(string operand)
    {
        var result = _expressions.GetValueOrDefault(operand);
        if (result == null)
        {
            throw new UnknownOperationException(operand);
        }

        return result;
    }
}