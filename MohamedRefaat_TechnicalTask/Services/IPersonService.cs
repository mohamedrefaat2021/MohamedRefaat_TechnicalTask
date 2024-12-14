using MohamedRefaat_TechnicalTask.Models;


    public interface IPersonService
    {
    List<Person> GetPersons(string filter = null);
}
