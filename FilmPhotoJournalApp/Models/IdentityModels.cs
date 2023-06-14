using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;


/*
 ----------------------NOTES-------------
-enable migration in package manager console by typing "enable-migrations"
-create Models
    -create DbSets that corresponds to the Models to create the database and its tables
    -create migration for photo class by typing "add-migration 'class(plural)' into the 'package manager console (e.g. : add-migration photos)
    -update database by typing "update-databse" in the "PM console" (check if table is created or updated in the 'SQL Server Object Explorer')

 */


namespace FilmPhotoJournalApp.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        //Add 'photo' entity into the system
        public DbSet<Photo> Photos { get; set; }    //DbSet naming convention - DB<Class(singular)> Class(plural)

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}