using LispInterpreter.Exception;
using Shouldly;
using Xunit;

namespace LispInterpreter.Tests;

public class Tests
{
    [Theory]
    [InlineData("(* (+ 2 3) (* 4 5)))")]
    [InlineData("(* 2 3))")]
    [InlineData(")(* 2 3)")]
    [InlineData(")(")]
    [InlineData("((* 2 3 )")]
    [InlineData("()")]
    [InlineData("")]
    public void Call_InvalidCommand_ThrowsInvalidCommandExpression(string command)
    {
        //Arrange
        var interpreter = new Interpreter();

        //Act && Assert
        Assert.Throws<InvalidCommandException>(() => { interpreter.Call(command); });
    }

    [Theory]
    [InlineData("(+ 2 3)", "5")]
    [InlineData("(* 2 6)", "12")]
    [InlineData("(+ 2 3 5)", "10")]
    [InlineData("(* 2 6 8)", "96")]
    [InlineData("(- 3 2)", "1")]
    [InlineData("(/ 6 2)", "3")]
    [InlineData("(/ 2 6)", "0")]
    [InlineData("(- 2 3 5)", "-6")]
    [InlineData("(/ 12 2 6)", "1")]
    public void Call_BasicArithmeticOperations_ReturnsResult(string command, string expectedResult)
    {
        //Arrange
        var interpreter = new Interpreter();

        //Act && Assert
        var result = interpreter.Call(command);

        //Assert
        result.ShouldBe(expectedResult);
    }

    [Theory]
    [InlineData("(+ 2 3 (* 2 2))", "9")]
    [InlineData("(* (+ 2 3) (- 7 5))", "10")]
    [InlineData("(* 2 (* 2 (* 2 (* 2 (* 2 2)))))", "64")]
    [InlineData("(* (+ (* 2 3) (* 7 1)) (- (- 3 2) (- 4 12)))", "117")]
    public void Call_NestedArithmeticOperations_ReturnsResult(string command, string expectedResult)
    {
        //Arrange
        var interpreter = new Interpreter();

        //Act && Assert
        var result = interpreter.Call(command);

        //Assert
        result.ShouldBe(expectedResult);
    }

    [Theory]
    [InlineData("(/ 2 0)")]
    [InlineData("(/ 12 6 0)")]
    [InlineData("(/ 12 (- 5 5))")]
    public void Call_DivideByZero_ThrowException(string command)
    {
        //Arrange
        var interpreter = new Interpreter();

        //Act && Assert
        Assert.Throws<DivideByZeroException>(() => interpreter.Call(command));
    }

    [Fact]
    public void Call_DefineExpressionsAndArithmeticOperation_ReturnsResult()
    {
        //Arrange
        var interpreter = new Interpreter();

        //Act
        interpreter.Call("(define a 10)");
        interpreter.Call("(define b 15) ");
        var result = interpreter.Call("(+ a b)");

        //Assert
        result.ShouldBe("25");
    }

    [Fact]
    public void Call_DefineExpressionsAndNestedArithmeticOperation_ReturnsResult()
    {
        //Arrange
        var interpreter = new Interpreter();

        //Act
        interpreter.Call("(define a 10)");
        interpreter.Call("(define b 15)");
        interpreter.Call("(define c 22)");
        var result = interpreter.Call("(/ (* a b) (+ 3 c))");

        //Assert
        result.ShouldBe("6");
    }

    [Theory]
    [InlineData("(if (> 5 2) (+ 2 3) (- 5 4))", "5")]
    [InlineData("(if (< 5 2) (+ 2 3) (- 5 3))", "2")]
    [InlineData("(* (if (> 5 2) (+ 2 3) (- 5 4)) (if (< 5 2) (+ 2 3) (- 5 3)))", "10")]
    public void Call_BasicConditionCommands_ReturnsResult(string command, string expectedResult)
    {
        //Arrange
        var interpreter = new Interpreter();

        //Act && Assert
        var result = interpreter.Call(command);

        //Assert
        result.ShouldBe(expectedResult);
    }


    [Fact]
    public void Call_DefineExpressionsAndConditionCall_ReturnsResult()
    {
        //Arrange
        var interpreter = new Interpreter();

        //Act
        interpreter.Call("(define a 10)");
        interpreter.Call("(define b 15)");
        interpreter.Call("(define c 22)");
        var result = interpreter.Call("(if (> a b) (* 2 5) (* 3 c))");

        //Assert
        result.ShouldBe("66");
    }
}