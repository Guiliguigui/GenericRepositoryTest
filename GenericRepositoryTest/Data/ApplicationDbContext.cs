using GenericRepositoryTest.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericRepositoryTest.Data
{
    public class ApplicationDbContext : DbContext
    {
        //sur un projet qui n'utilise pas d'injection de services, utiliser le constructeur par défaut sans options et la méthode OnConfiguring pour pouvoir utiliser add-migration
        //https://docs.microsoft.com/fr-fr/ef/core/cli/dbcontext-creation?tabs=dotnet-core-cli#using-a-constructor-with-no-parameters
        public ApplicationDbContext() : base()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source = np:\\.\pipe\LOCALDB#363A1ABA\tsql\query;Integrated Security=True");
        }

        DbSet<Student> Students { get; set; }

    }
}
