using System.Data.Entity;

namespace BookReader.Data.Models
{
    public class BookReaderContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, add the following
        // code to the Application_Start method in your Global.asax file.
        // Note: this will destroy and re-create your database with every model change.
        // 
        // System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<BookReader.Data.Models.BookReaderContext>());

        public BookReaderContext()
        {
            Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<BookCollection> BookCollections { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Chapter> Chapters { get; set; }
        public DbSet<Verse> Verses { get; set; }
        public DbSet<Reference> References { get; set; }


    }
}
