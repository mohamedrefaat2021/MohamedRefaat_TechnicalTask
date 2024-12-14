using MohamedRefaat_TechnicalTask.Models;

public class PersonService :IPersonService
{
    private readonly CsvRepository _csvRepository;
    private readonly MongoRepository _mongoRepository;

    public PersonService(CsvRepository csvRepository, MongoRepository mongoRepository)
    {
        _csvRepository = csvRepository;
        _mongoRepository = mongoRepository;
    }

    public List<Person> GetPersons(string filter = null)
    {
        // Fetch CSV and MongoDB persons
        var csvPersons = _csvRepository.GetPersonsFromCsv() ?? new List<CsvPerson>();
        var mongoPersons = _mongoRepository.GetPersonsFromMongo() ?? new List<MongoPerson>();

        // Convert them to unified Person type
        var allPersons = csvPersons.Select(ConvertToPersonFromCsv)
                                   .Concat(mongoPersons.Select(ConvertToPersonFromMongo))
                                   .ToList();

        // Apply filter if provided
        if (!string.IsNullOrEmpty(filter))
        {
            allPersons = allPersons
                .Where(p => p.FirstName.Contains(filter, StringComparison.OrdinalIgnoreCase) ||
                           p.LastName.Contains(filter, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        return allPersons;
    }

    public Person ConvertToPersonFromCsv(CsvPerson csvPerson)
    {
        return new Person
        {
            FirstName = csvPerson.FirstName,
            LastName = csvPerson.LastName,
            Country = csvPerson.CountryCode, // Map CountryCode from CSV to Country
            TelephoneNumber = csvPerson.Number, // Map Number from CSV to TelephoneNumber
            FullAddress = csvPerson.FullAddress
        };
    }
    public Person ConvertToPersonFromMongo(MongoPerson mongoPerson)
    {
        return new Person
        {
            FirstName = mongoPerson.Name.Split(' ')[0], // Assume Name is "First Last"
            LastName = mongoPerson.Name.Split(' ').Last(),
            Country = mongoPerson.Country,
            TelephoneNumber = mongoPerson.TelephoneNumber,
            FullAddress = mongoPerson.Address
        };
    }

}
