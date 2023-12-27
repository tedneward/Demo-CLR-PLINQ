using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapReduce
{
    class Person
    {
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public int Age { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            // {{## BEGIN data ##}}
            var people = new Person[]
            {
                new Person() { FirstName = "Ted", LastName = "Neward", Age = 45 },
                new Person() { FirstName = "Charlotte", LastName = "Neward", Age = 45 },
                new Person() { FirstName = "Michael", LastName = "Neward", Age = 23 },
                new Person() { FirstName = "Matthew", LastName = "Neward", Age = 17 },
                new Person() { FirstName = "Joanie", LastName = "Cunningham", Age = 16 },
                new Person() { FirstName = "Richie", LastName = "Cunningham", Age = 18 }
            };
            // {{## END data ##}}

            // {{## BEGIN average-age ##}}
            // Print out average age of all the people
            var avgAge = (people
                .Select(p => p.Age)
                .Aggregate(0, (initial, a) => initial + a))
              / people.Count();
            Console.WriteLine("Average age = {0}", avgAge);
            // {{## END average-age ##}}

            // {{## BEGIN parallel-average-age ##}}
            // Print out average age of all the people
            var parAvgAge = (people
                .AsParallel()
                .Select(p => p.Age)
                .Aggregate(0, (initial, a) => initial + a))
              / people.Count();
            Console.WriteLine("Average age = {0}", parAvgAge);
            // {{## END parallel-average-age ##}}

            // {{## BEGIN people-as-xml ##}}
            var xml = people
                .AsParallel()
                .Select(p => String.Format("<person><firstName>{0}</firstName></person>", p.FirstName))
                .Aggregate("<people>", (init, pxml) => init + pxml) + "</people>";
            Console.WriteLine("XML = {0}", xml);
            // {{## END people-as-xml ##}}
        }
    }
}
