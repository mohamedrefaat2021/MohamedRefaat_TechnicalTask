using System;
using System.Collections.Generic;
using System.Linq;
using MohamedRefaat_TechnicalTask.Models;
using Moq;
using Xunit;

public class PersonServiceTests
{
    private readonly Mock<CsvRepository> _mockCsvRepository;
    private readonly Mock<MongoRepository> _mockMongoRepository;
    private readonly PersonService _personService;

    public PersonServiceTests()
    {
        _mockCsvRepository = new Mock<CsvRepository>();
        _mockMongoRepository = new Mock<MongoRepository>();
        _personService = new PersonService(_mockCsvRepository.Object, _mockMongoRepository.Object);
    }

    [Fact]
    public void GetPersons_ShouldReturnAllPersons_WhenNoFilterIsProvided()
    {
        // Arrange
        var csvPersons = new List<CsvPerson>
        {
            new CsvPerson { FirstName = "Ahmed", LastName = "Mohamed", CountryCode = "02", Number = "12345", FullAddress = "123 Street" }
        };
        var mongoPersons = new List<MongoPerson>
        {
            new MongoPerson { Name = "Ahmed Mohamed", Country = "Egypt", TelephoneNumber = "67890", Address = "456 New" }
        };

        _mockCsvRepository.Setup(repo => repo.GetPersonsFromCsv()).Returns(csvPersons);
        _mockMongoRepository.Setup(repo => repo.GetPersonsFromMongo()).Returns(mongoPersons);

        // Act
        var result = _personService.GetPersons();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.Contains(result, p => p.FirstName == "Sar" && p.LastName == "Ali");
        Assert.Contains(result, p => p.FirstName == "Mustafa" && p.LastName == "Jaber");
    }

    [Fact]
    public void GetPersons_ShouldApplyFilter_WhenFilterIsProvided()
    {
        // Arrange
        var csvPersons = new List<CsvPerson>
        {
            new CsvPerson { FirstName = "Ahmed", LastName = "Mohamed", CountryCode = "02", Number = "12345", FullAddress = "123 Street" }
        };
        var mongoPersons = new List<MongoPerson>
        {
            new MongoPerson { Name = "Ahmed Mohamed", Country = "Egypt", TelephoneNumber = "67890", Address = "456 New" }
        };

        _mockCsvRepository.Setup(repo => repo.GetPersonsFromCsv()).Returns(csvPersons);
        _mockMongoRepository.Setup(repo => repo.GetPersonsFromMongo()).Returns(mongoPersons);

        // Act
        var result = _personService.GetPersons("Ahmed");

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Contains(result, p => p.FirstName == "Ahmed" && p.LastName == "Mohamed");
    }

    [Fact]
    public void GetPersons_ShouldReturnEmptyList_WhenNoDataExists()
    {
        // Arrange
        _mockCsvRepository.Setup(repo => repo.GetPersonsFromCsv()).Returns(new List<CsvPerson>());
        _mockMongoRepository.Setup(repo => repo.GetPersonsFromMongo()).Returns(new List<MongoPerson>());

        // Act
        var result = _personService.GetPersons();

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }
}
