using FluentAssertions;
using Xunit.Abstractions;

namespace CalculatorLibrary.Tests.UnitTest;

public class CalculatorTests : IDisposable, IAsyncLifetime
{
    #region Arrange
    //Arrange
    private readonly Calculator _calculator = new();
    private readonly Guid _guid = Guid.NewGuid();
    private readonly ITestOutputHelper _outputHelper;
    #endregion

    #region Constructor
    public CalculatorTests(ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;
    }
    #endregion

    [Fact]
    public void Add_ShouldAddTwoNumbers_WhenTwoNumbersAreIntegger()
    {
        //Act
        var result = _calculator.Add(2, 7);
        //Assert
        //Assert.Equal(9, result);
        result.Should().Be(9);
        result.Should().NotBe(7);
    }

    [Fact]
    public void Subtract_ShouldSubtractTwoNumbers_WhenTwoNumbersAreIntegger()
    {
        //Act
        var result = _calculator.Subtract(7, 2);
        //Assert
        //Assert.Equal(5, result);
        result.Should().Be(5);
        result.Should().NotBe(7);
    }

    [Fact]
    public void Multiply_ShouldMultiplyTwoNumbers_WhenTwoNumbersAreIntegger()
    {
        //Act
        var result = _calculator.Multiply(5, 5);
        //Assert
        //Assert.Equal(25, result);
        result.Should().Be(25);
        result.Should().NotBe(5);
    }

    [Fact]
    public void Divide_ShouldDivideTwoNumbers_WhenTwoNumbersAreIntegger()
    {
        //Act
        var result = _calculator.Divide(10, 2);
        //Assert
        //Assert.Equal(5, result);
        result.Should().Be(5);
        result.Should().NotBe(2);
    }

    //Testi atlamak için kullanýlan yöntem
    //Method used to skip the test
    #region Fact=Skip
    [Fact(Skip = "Bu method artýk kullanýlmýyor!")]
    public void Test1()
    {
        _outputHelper.WriteLine(_guid.ToString());
    }
    #endregion

    // genelde integration testlerde yazýlýr
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