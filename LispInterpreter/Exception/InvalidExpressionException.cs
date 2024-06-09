namespace LispInterpreter.Exception;

public class InvalidExpressionException(string message): System.Exception(message);