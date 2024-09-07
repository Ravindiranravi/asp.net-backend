
//using Institute.Models;
//using Microsoft.EntityFrameworkCore;

//namespace Institute.Data
//{
//    public class ApplicationDbContext : DbContext
//    {
//        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
//            : base(options)
//        {
//        }

//        public DbSet<User> Users { get; set; }
//        public DbSet<Student> Students { get; set; }
//        public DbSet<Trainee> trainees { get; set; }

//    }
//}


using Institute.Models;
using Microsoft.EntityFrameworkCore;

namespace Institute.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Trainee> Trainees { get; set; } // Ensure this is included
    }
}
