namespace CalculatorLibrary;
public class ValueSample
{
    public string FullName = "Cuma Kose";
    public int Age = 37;

    public User user = new()
    {
        NameSurName = "Cuma Kose",
        Age = 37,
        DateOfBirth = new(1987, 8, 29)
    };

    public IEnumerable<User> Users = new[]
    {
        new User()
        {
            NameSurName = "Cuma Kose",
            Age = 37,
            DateOfBirth = new(1987, 8, 29)
        },
        new User()
        {
            NameSurName = "Veli Kose",
            Age = 7,
            DateOfBirth = new(2017,2, 2)
        },
        new User()
        {
            NameSurName = "Mehmet KÖKoseSE",
            Age = 4,
            DateOfBirth = new(2020,2, 2)
        }
    };

    public IEnumerable<int> numbers = new[] { 1, 2, 3, 4, 5, 6, 7, 8 };

    public float Divide(int a, int b)
    {
        if (a == 0 || b == 0)
        {
            throw new DivideByZeroException();
        }
        return a / b;
    }

    public event EventHandler ExampleEvent;
    public virtual void RaiseExampleEvent()
    {
        ExampleEvent(this, EventArgs.Empty);
    }

    internal int InternalSecretNumber = 42;
}
