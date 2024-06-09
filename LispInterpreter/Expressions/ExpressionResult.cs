namespace LispInterpreter.Expressions;

public class ExpressionResult
{
    public ExpressionResultType Type { get; private init; }
    public string Result { get; private init; } = null!;
    public string Variable { get; private init; } = null!;

    public static ExpressionResult CreateValueResult(string result)
    {
        return new ExpressionResult()
        {
            Type = ExpressionResultType.Value,
            Result = result
        };
    }

    public static ExpressionResult CreateSetVariableResult(string variable, string result)
    {
        return new ExpressionResult
        {
            Type = ExpressionResultType.SetVariable,
            Variable = variable,
            Result = result
        };
    }
}

public enum ExpressionResultType
{
    Value = 0,
    SetVariable = 1
}