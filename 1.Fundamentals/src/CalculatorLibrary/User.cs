namespace CalculatorLibrary;
public sealed class User
{
    public string NameSurName { get; set; } = string.Empty;
    public int Age { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public User()
    {
        NameSurName = "Cuma Kose";
        Age = 37;
        DateOfBirth = new(1987, 8, 29);
    }
}
