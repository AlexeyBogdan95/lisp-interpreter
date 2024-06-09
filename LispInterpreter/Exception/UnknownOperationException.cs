namespace LispInterpreter.Exception;

public class UnknownOperationException(string operation) : System.Exception($"Unknown operation ${operation}");