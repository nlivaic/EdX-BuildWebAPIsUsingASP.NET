using System.Collections.Generic;
using System.Linq;

namespace Demo.Models
{
    public class Repository
    {
        private static int counter = 0;
        public static IList<Person> People { get; } = new List<Person>();

        public static Person GetPersonById(int id)
        {
            Person target = People.SingleOrDefault(p => p.Id == id);
            return target;
        }
        public static void RemovePersonById(int id)
        {
            Person target = People.SingleOrDefault(p => p.Id == id);
            if (target != null)
            {
                People.Remove(target);
            }
        }
        public static void ReplacePersonById(int id, Person person)
        {
            Person target = People.SingleOrDefault(p => p.Id == id);
            if (target != null)
            {
                People.Remove(target);
                People.Add(person);
            }
        }
        public static void AddPerson(Person person)
        {
            person.Id = counter++;
            People.Add(person);
        }
    }
}