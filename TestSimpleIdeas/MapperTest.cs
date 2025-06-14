using TestSimpleIdeas.Models;
using Generated;
using CrazyMapper;

namespace TestSimpleIdeas
{
    public class MapperTest
    {
        [Fact]
        public void TestMapping()
        {

            HelloWorld.SayHello();
            var person = new Person()
            {
                FirstName = "Test",
                LastName = "Test",
                Age = 34,
                Children = 3
            };

            var map = new Mapper();

            PersonDTO personDTO = map.Map<PersonDTO, Person>(person);

            personDTO.FirstName = person.FirstName;
            personDTO.LastName = person.LastName;  
            personDTO.Age = person.Age;
        }
    }
}