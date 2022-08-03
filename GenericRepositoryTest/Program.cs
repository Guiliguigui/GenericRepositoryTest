using GenericRepositoryTest.Data;
using GenericRepositoryTest.Models;
using GenericRepositoryTest.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GenericRepositoryTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MainAsync().Wait();
        }
        static async Task MainAsync()
        {
            using var context = new ApplicationDbContext();

            Student student = new Student()
            {
                Firstname = "Guillaume",
                Lastname = "Mairesse",
                Phone = "0607080910",
                Email = "guillaume@exemple.com",
            };

            var studentRepository = new GenericRepository<Student>(context);

            var createdStudent = await studentRepository.Create(student);
            Console.WriteLine($"Created student :\n{createdStudent}");

            Console.WriteLine($"Find with Id '0' :\n{await studentRepository.Find(0)}");
            Console.WriteLine($"Find with student's Id :\n{await studentRepository.Find(createdStudent.Id)}");
            Console.WriteLine($"FindAll :\n[\n\t{string.Join(",\n\t",(await studentRepository.FindAll()))}\n]");

            Console.WriteLine($"Update with null :\n{await studentRepository.Update(null)}");
            student.Firstname = "Pierre";
            Console.WriteLine($"Update with new FirstName :\n{await studentRepository.Update(student)}");


            Console.WriteLine($"Find with predicate on FirstName :\n{await studentRepository.Find(e => e.Firstname == "Pierre")}");
            Console.WriteLine($"FindAll with predicate on FirstName :\n[\n\t{string.Join(",\n\t", (await studentRepository.FindAll(e => e.Firstname == "Guillaume")))}\n]");


            Console.WriteLine($"Delete with student's Id :\n{await studentRepository.Delete(createdStudent.Id)}");
        }
    }
}
