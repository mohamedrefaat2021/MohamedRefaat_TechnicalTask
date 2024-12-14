using CsvHelper;
using MohamedRefaat_TechnicalTask.Models;
using System.Globalization;

public class CsvRepository
{
    private readonly string _filePath;

    public CsvRepository(string filePath)
    {
        _filePath = filePath;
    }

    public List<CsvPerson> GetPersonsFromCsv()
    {
        using var reader = new StreamReader(_filePath);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        return csv.GetRecords<CsvPerson>().ToList();
    }
}
