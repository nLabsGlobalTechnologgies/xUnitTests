namespace CalculatorLibrary;
public sealed class User
{
    public User()
    {
        FullName = "Cuma KÖSE";
        Age = 37;
        DateOfBirth = new(198, 08, 29);
    }
    public string FullName { get; set; }
    public int Age { get; set; }
    public DateOnly DateOfBirth { get; set; }
}
