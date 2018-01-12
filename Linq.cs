using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice
{
    class Linq
    {
        public static void Test()
        {
            List<Student> students = new List<Student> { 
                new Student
                {
                    ID = 1,
                    Name = "Kapil"
                },new Student
                {
                    ID = 2,
                    Name = "James"
                }
                ,new Student
                {
                    ID = 2,
                    Name = "Sujith"
                },new Student
                {
                    ID = 3,
                    Name = "Michael"
                }
            };

            var result = from s in students
                         where s.Name.Contains("J")
                         select s.Name;
            Console.WriteLine(result.First());
            result = students.Where(s => s.Name.Contains("J")).Select(s => s.Name);
            Console.WriteLine(result.First());

            var r = students.OrderBy(s => s.ID).ThenByDescending(s=> s.Name);
            foreach (var s in r)
                Console.WriteLine(s.Name);

            r = from s in students
                orderby s.ID, s.Name descending
                select s;
                

            // SelectMany
            PetOwner[] petOwners = 
        { new PetOwner { Name="Higa, Sidney", 
              Pets = new List<string>{ "Scruffy", "Sam" } },
          new PetOwner { Name="Ashkenazi, Ronen", 
              Pets = new List<string>{ "Walker", "Sugar" } },
          new PetOwner { Name="Price, Vernette", 
              Pets = new List<string>{ "Scratches", "Diesel" } } };
            IEnumerable<string> query1 = petOwners.SelectMany(p => p.Pets);
            foreach (string pet in query1)
            {
                Console.WriteLine(pet);
            }

            IEnumerable<List<string>> query2 = petOwners.Select(p => p.Pets);
            foreach (var list in query2)
                foreach (var pet in list)
                    Console.WriteLine(pet);
            var p1 = from s in students.OfType<Student>()
                        select s;
            p1 = students.OfType<Student>();

            var query3 = petOwners.SelectMany(petOwner => petOwner.Pets, (petOwner, petName) => new { petOwner, petName });
            query3 = query3.Where(ownerAndPet => ownerAndPet.petName.StartsWith("S"));
            var query4 = query3.Select(ownerAndPet => new { Owner = ownerAndPet.petOwner.Name, Pet = ownerAndPet.petName });
            foreach (var item in query3)
            {
                //Console.WriteLine("{0},  {1}", item.petOwner.Name, item.petName);
                Console.WriteLine(item.petOwner);
            }
            foreach (var item in query4)
            {
                //{Owner=Higa, Pet=Scruffy}
                Console.WriteLine(item);
            }
            
        }
    }
    internal class Student
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
    class PetOwner
    {
        public string Name { get; set; }
        public List<String> Pets { get; set; }
    }
}
