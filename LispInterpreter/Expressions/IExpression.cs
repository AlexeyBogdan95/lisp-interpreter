namespace LispInterpreter.Expressions;

public interface IExpression
{
    public string Operator { get; }
    public ExpressionResult Execute(params string[] operands);
}