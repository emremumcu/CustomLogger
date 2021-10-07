namespace SGKWeb.Lib.EFCore
{
    using AppLib.Entities;
    using Microsoft.EntityFrameworkCore;

    public class DbLoggerContext : DbContext
    {
        public DbLoggerContext()
        {
            // bool b = Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // SQLite requires an absolute path since SQLite use the process current working directory,
            // which if launched in IIS or other servers, can be a different folder than your source code directory.            
            //var path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location.Substring(0, System.Reflection.Assembly.GetEntryAssembly().Location.IndexOf("bin\\")));
            optionsBuilder.UseSqlite($"Data Source={System.IO.Path.GetTempPath()}\\CustomLogger.sqlite;");            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // .HasDefaultValueSql("getdate()");
            // .HasComputedColumnSql("[LastName] + ', ' + [FirstName]");
            // .HasComputedColumnSql("LEN([LastName]) + LEN([FirstName])", stored: true);
            // modelBuilder.Entity<InternalLog>().Property(b => b.RowTimeStamp).HasDefaultValueSql("getdate()"); // SQL Server
            modelBuilder.Entity<InternalLog>().Property(b => b.RowTimeStamp).HasDefaultValueSql("datetime()"); // SQLite
        }

        /// Entities
        public DbSet<InternalLog> InternalLogs { get; set; }
    }
}
