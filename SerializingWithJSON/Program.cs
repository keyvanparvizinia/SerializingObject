using SerializingWithJSON;
using NewJson = System.Text.Json.JsonSerializer;
using static System.Console;
using static System.Environment;
using static System.IO.Path;

// create an object graph
List<Person> people = new()
{
    new(30000M)
    {
        FirstName = "Keyvan",
        LastName = "Parvizinia",
        DateOfBirth = new(1990, 10, 26)
    },
    new(40000M)
    {
        FirstName = "Saman",
        LastName = "Mohammadi",
        DateOfBirth = new(1969, 11, 23)
    },
    new(20000M)
    {
        FirstName = "Reza",
        LastName = "Ahmadi",
        DateOfBirth = new(1984, 5, 4),
        Children = new()
        {
            new(0M)
            {
                FirstName = "Arwin",
                LastName = "Ahmadi",
                DateOfBirth = new(2000, 7, 12)
            }
        }
    }
};

// create a file to write to
string jsonPath = Combine(CurrentDirectory, "people.json");

using (StreamWriter jsonStream = File.CreateText(jsonPath))
{
    // create an object that will format as JSON
    Newtonsoft.Json.JsonSerializer jss = new();

    // serialize the object graph into a string
    jss.Serialize(jsonStream, people);
}

WriteLine();
WriteLine($"Written {new FileInfo(jsonPath).Length} bytes of JSON to: {jsonPath}");

// Display the serialized object graph
WriteLine(File.ReadAllText(jsonPath));

//Deserializing a JSON file
using (FileStream jsonLoad = File.Open(jsonPath, FileMode.Open))
{
    // deserialize object graph into a List of Person
    List<Person>? loadedPeople = await NewJson.DeserializeAsync(
        utf8Json: jsonLoad,
        returnType: typeof(List<Person>)) as List<Person>;
    
    if (loadedPeople is not null)
    {
        foreach (Person p in loadedPeople)
        {
            WriteLine($"{p.LastName} has {p.Children?.Count ?? 0} children.");
        }
    }
}