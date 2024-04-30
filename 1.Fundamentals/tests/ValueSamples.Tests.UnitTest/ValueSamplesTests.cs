using CalculatorLibrary;
using FluentAssertions;

namespace ValueSamples.Tests.UnitTest;
public class ValueSamplesTests
{
    //Arrange
    private readonly ValueSample _valueSample = new();
    private readonly User _user = new();

    [Fact]
    public void StringAssertionExample()
    {
        //Act
        var fullName = _valueSample.FullName;

        //Assert
        fullName.Should().Be("Cuma Kose");
        fullName.Should().NotBeEmpty();
        fullName.Should().StartWith("Cuma");
        fullName.Should().EndWith("Kose");
    }

    [Fact]
    public void InteggerAssertionExample()
    {
        //Act
        var age = _valueSample.Age;

        //Assert
        age.Should().Be(37);
        age.Should().BePositive();
        age.Should().BeGreaterThan(36);
        age.Should().BeLessThanOrEqualTo(40);
        age.Should().BeInRange(30, 40);
    }

    [Fact]
    public void ObjectAssertionExample()
    {
        //Act

        var exitingUser = new User()
        {
            NameSurName = "Cuma Kose",
            Age = 37,
            DateOfBirth = new(1987, 08, 29)
        };
        var user = _user;

        //Assert
        user.Should().BeEquivalentTo(exitingUser);
    }

    [Fact]
    public void EnumerableObjectAssertionExample()
    {
        //Act
        var isExistsUser = new User
        {
            NameSurName = "Cuma Kose",
            Age = 37,
            DateOfBirth = new(1987, 8, 29)
        };

        var users = _valueSample.Users.As<User[]>();

        //Assert
        users.Should().ContainEquivalentOf(isExistsUser);
        users.Should().HaveCount(3);
        users.Should().Contain(p => p.NameSurName.StartsWith("Veli") && p.Age > 5);
    }

    [Fact]
    public void EnumerableAssertionExample()
    {
        //Act
        var numbers = _valueSample.numbers.As<int[]>();
        numbers.Should().Contain(5);
    }

    [Fact]
    public void ExceptionThrowASsertionExample()
    {
        //Act
        Action result = () => _valueSample.Divide(1, 0);
        
        //Assert
        result.Should().Throw<DivideByZeroException>();
        //result.Should().Throw<DivideByZeroException>().WithMessage("Attempted to divide by zero.");
    }

    [Fact]
    public void EventRaisedAssertionExample()
    {
        var monitorSubcject = _valueSample.Monitor();
        _valueSample.RaiseExampleEvent();

        //Assert
        monitorSubcject.Should().Raise("ExampleEvent");
    }

    [Fact]
    public void TestingInternalMembersExample()
    {
        //Act
        var number = _valueSample.InternalSecretNumber;

        //Assert
        number.Should().Be(42);
    }


}
