using Bogus;
using WebApiSystem.Models;

namespace WebApiSystem.FakeData
{
    public class DataGenerator
    {
        Faker<Employee> employeeModelFake;

        public DataGenerator()
        {
            Randomizer.Seed = new Random(8675309);

            employeeModelFake = new Faker<Employee>()
                .RuleFor(e => e.Id, f => f.IndexFaker + 1)
                .RuleFor(e => e.FirstName, f => f.Name.FirstName())
                .RuleFor(e => e.LastName, f => f.Name.LastName())
                .RuleFor(e => e.Title, f => f.Name.JobTitle())
                .RuleFor(e => e.Email, (f, e) => f.Internet.Email(e.FirstName, e.LastName))
                .RuleFor(e => e.Password, f => f.Internet.Password())
                .RuleFor(e => e.Phone, f => f.Phone.PhoneNumber())
                .RuleFor(e => e.Gender, f => f.PickRandom(new[] { "Male", "Female" }))
                .RuleFor(e => e.Address, f => f.Address.FullAddress())
                .RuleFor(e => e.Joined, f => f.Date.Past(10))
                .RuleFor(e => e.PositionId, f => 1)
                .RuleFor(e => e.IdNumber, f =>
                {
                    int randomInt = f.Random.Int(1000, 9999);
                    string randomString = randomInt.ToString();
                    return randomString;
                });


        }
        public Employee GenerateEmployee()
        {
            return employeeModelFake.Generate();
        }
        public  List<Employee> GenerateEmployees(int count)
        { 
            return  employeeModelFake.Generate(count);
        }
    }
}
