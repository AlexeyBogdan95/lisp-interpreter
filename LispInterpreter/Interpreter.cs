namespace LispInterpreter;

public class Interpreter
{
    private readonly ICalculator _calculator = new Calculator();
    private readonly IExpressionParser _expressionParser = new ExpressionParser();

    public string Call(string command)
    {
        var root = _expressionParser.GetTree(command);
        var result = _calculator.Calculate(root);
        return result.Value;
    }

}