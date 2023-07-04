using WebApiSample.Model;
using Microsoft.EntityFrameworkCore;

namespace WebApiSample.DataAccessLayer
{
    //DBContext -> it makes the connection between the database and the tables we are declaring here 
    public class PersonDBContext:DbContext //PersonDBContext is database context for all the tables in that database
    {
        public PersonDBContext(DbContextOptions options):base(options) { 
            
        }

        public DbSet<Person> Persons { get; set; } //here o=it takes Persons because in DbSet it's a collection of Person , so its Persons
        //here we can add many tables as much we need , dont need to create another context
    }
}
