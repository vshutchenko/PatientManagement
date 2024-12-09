using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Bogus;

class Program
{
    private static readonly HttpClient client = new HttpClient();

    static async Task Main(string[] args)
    {
        if (args.Length < 1)
        {
            Console.WriteLine("Please provide the API URL.");
            return;
        }

        var apiUrl = args[0];

        Console.WriteLine($"Sending patients to API at {apiUrl}");

        var patients = GeneratePatients(100);

        foreach (var patient in patients)
        {
            var content = JsonContent.Create(patient);

            var response = await client.PostAsync(apiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Successfully added patient: {patient.Name.Family}");
            }
            else
            {
                Console.WriteLine($"Failed to add patient: {patient.Name.Family}");
            }
        }

        Console.WriteLine("All patients processed.");
    }

    static PatientViewModel GeneratePatient()
    {
        var faker = new Faker();
        var givenNames = new[] { faker.Name.FirstName(), faker.Name.LastName() };

        return new PatientViewModel
        {
            Name = new PatientNameViewModel
            {
                Id = Guid.NewGuid(),
                Use = "official",
                Family = faker.Name.LastName(),
                Given = new List<string> { givenNames[0], givenNames[1] }
            },
            Gender = faker.PickRandom("male", "female", "other", "unknown"),
            BirthDate = faker.Date.Past(10),
            Active = true
        };
    }

    static List<PatientViewModel> GeneratePatients(int count)
    {
        var patients = new List<PatientViewModel>();

        for (int i = 0; i < count; i++)
        {
            patients.Add(GeneratePatient());
        }

        return patients;
    }
}

public class PatientNameViewModel
{
    public Guid Id { get; set; }
    public string Use { get; set; } = string.Empty;
    public string Family { get; set; } = string.Empty;
    public List<string> Given { get; set; } = new();
}

public class PatientViewModel
{
    public PatientNameViewModel Name { get; set; } = new();
    public string Gender { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public bool Active { get; set; }
}
