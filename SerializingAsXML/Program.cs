using SerializingAsXML;
using System.Xml.Serialization;
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

// create object that will format a List of Persons as XML
XmlSerializer xs = new(people.GetType());

// create a file to write to
string path = Combine(GetFolderPath(SpecialFolder.Personal), "people.xml");

using (FileStream stream = File.Create(path))
{
    // serialize the object graph to the stream
    xs.Serialize(stream, people);
}

WriteLine($"Written {new FileInfo(path).Length} bytes of XML to {path}");
WriteLine();

// Display the serialized object graph
WriteLine(File.ReadAllText(path));

////Deserializing XML files
//using (FileStream xmlLoad = File.Open(path, FileMode.Open))
//{
//    // deserialize and cast the object graph into a List of Person
//    List<Person>? loadedPeople = xs.Deserialize(xmlLoad) as List<Person>;

//    if (loadedPeople is not null)
//    {
//        foreach (Person p in loadedPeople)
//        {
//            WriteLine($"{p.LastName} has {p.Children?.Count ?? 0} children.");
//        }
//    }
//}