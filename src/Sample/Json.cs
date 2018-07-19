using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Sample
{
    public class Json
    {
        // A demo of JSON serialization
        public string Result { get; set; }

        public string JsonObject()
        {
            Result = JsonConvert.SerializeObject(GetList());
            return Result;
        }

        private List<Person> GetList()
        {
            var person1 = new Person()
            {
                DateOfBirth = DateTime.Now.AddYears(-20),
                FirstName = "Don",
                LastName = "Julio"
            };
            var person2 = new Person()
            {
                DateOfBirth = DateTime.Now.AddYears(-30),
                FirstName = "Don",
                LastName = "Carleon"
            };

            var persons = new List<Person>();
            persons.Add(person1);
            persons.Add(person2);
            return persons;
        }
    }
}
