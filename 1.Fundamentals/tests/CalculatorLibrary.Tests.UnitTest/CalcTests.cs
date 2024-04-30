using FluentAssertions;
using Xunit.Abstractions;

namespace CalculatorLibrary.Tests.UnitTest;
public class CalcTests : IDisposable, IAsyncLifetime
{
    #region Arrange
    //Arrange
    private readonly Calculator _calculator = new();
    private readonly Guid _guid = Guid.NewGuid();
    private readonly ITestOutputHelper _outputHelper;
    #endregion

    #region Constructor
    public CalcTests(ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;
    }
    #endregion
    [Theory]
    [InlineData(6, 2, 8)]
    [InlineData(8, 2, 10)]
    [InlineData(0, 0, 0, Skip = "Addition with zero cannot be done!")]
    public void Add_ShouldAddTwoNumbers_WhenTwoNumbersAreIntegger(int a, int b, int expected)
    {
        //Act
        var result = _calculator.Add(a, b);
        //Assert
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData(6, 2, 4)]
    [InlineData(8, 2, 8)]
    [InlineData(0, 0, 0, Skip = "It is not possible to subtract zero with zero!")]
    public void Subtract_ShouldSubtractTwoNumbers_WhenTwoNumbersAreIntegger(int a, int b, int expected)
    {
        //Act
        var result = _calculator.Subtract(a, b);
        //Assert
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData(4, 2, 8)]
    [InlineData(3, 2, 7)]
    [InlineData(0, 0, 0, Skip = "Multiplication by zero cannot be done!")]
    public void Multiply_ShouldMultiplyTwoNumbers_WhenTwoNumbersAreIntegger(int a, int b, int expected)
    {
        //Act
        var result = _calculator.Multiply(a, b);
        //Assert
        result.Should().Be(expected);
    }


    [Theory]
    [InlineData(6, 2, 3)]
    [InlineData(8, 2, 4)]
    [InlineData(0, 0, 0, Skip = "Dividing by zero cannot be done!")]
    public void Divide_ShouldDivideTwoNumbers_WhenTwoNumbersAreIntegger(int a, int b, int expected)
    {
        //Act
        var result = _calculator.Divide(a, b);
        //Assert
        result.Should().Be(expected);
    }

    //Testi atlamak için kullanılan yöntem
    //Method used to skip the test
    #region Fact=Skip
    [Fact(Skip = "Bu method artık kullanılmıyor!")]
    public void Test1()
    {
        _outputHelper.WriteLine(_guid.ToString());
    }
    #endregion

    // genelde integration testlerde yazılır
    // usually written in integration tests
    #region Dispose
    public void Dispose()
    {
        _outputHelper.WriteLine("Dispose is working...");
    }
    #endregion

    #region InitializeAsync
    public async Task InitializeAsync()
    {
        _outputHelper.WriteLine("InitializeAsync is working...");
        await Task.Delay(1);
        await Task.CompletedTask;
    }
    #endregion

    #region DisposeAsync
    public async Task DisposeAsync()
    {
        _outputHelper.WriteLine("DisposeAsync is working...");
        await Task.Delay(1);
        await Task.CompletedTask;
    }
    #endregion
}
