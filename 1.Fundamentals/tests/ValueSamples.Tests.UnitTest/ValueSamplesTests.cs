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
        fullName.Should().Be("Cuma KÖSE");
        fullName.Should().NotBeEmpty();
        fullName.Should().StartWith("Cuma");
        fullName.Should().EndWith("KÖSE");
    }

    [Fact]
    public void InteggerAssertionExample()
    {
        //Act
        var age = _valueSample.Age;

        //Assert
        age.Should().Be(38);
        age.Should().BePositive();
        age.Should().BeGreaterThan(35);
        age.Should().BeLessThanOrEqualTo(40);
        age.Should().BeInRange(30, 40);
    }

    [Fact]
    public void ObjectAssertionExample()
    {
        //Act

        var exitingUser = new User()
        {
            FullName = "Cuma KÖSE",
            Age = 37,
            DateOfBirth = new(198, 08, 29)
        };
        var user = _user;

        //Assert
        _user.Should().BeEquivalentTo(exitingUser);
    }
}
