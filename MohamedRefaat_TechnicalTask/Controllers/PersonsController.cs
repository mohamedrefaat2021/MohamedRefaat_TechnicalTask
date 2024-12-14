using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class PersonsController : ControllerBase
{
    private readonly IPersonService _personService;

    public PersonsController(IPersonService personService)
    {
        _personService = personService;
    }

    [HttpGet]
    public IActionResult GetPersons([FromQuery] string? name)
    {
        var persons = _personService.GetPersons(name);
        return Ok(persons);
    }
}
