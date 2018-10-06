using System.Linq;
using Demo.Models;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers
{
    [Route("api/[controller]")]
    public class PeopleController : Controller
    {
        [HttpGet]
        public Person[] Get()
        {
            return Repository.People.ToArray();
        }
        [HttpGet("{id}")]
        public Person Get(int id)
        {
            return Repository.GetPersonById(id);
        }
        [HttpPost]
        public void Post([FromBody]Person person)
        {
            Repository.AddPerson(person);
        }
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]Person person)
        {
            Repository.ReplacePersonById(id, person);
        }
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Repository.RemovePersonById(id);
        }
    }
}