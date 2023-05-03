namespace SerializingAsXML;

public class Person
{
    public Person()
    {
    }

    public Person(decimal salary)
    {
        Salary = salary;
    }

    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public HashSet<Person>? Children { get; set; }
    public decimal Salary { get; set; }
}