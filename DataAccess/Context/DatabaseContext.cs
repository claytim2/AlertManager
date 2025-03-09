using Model.DatabaseContext;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;


namespace DataAccess.Context
{
    public class DatabaseContext : DbContext
    {
        static DatabaseContext()
        {
            Database.SetInitializer<DatabaseContext>(null);

        }

        public DatabaseContext()
                : base("Name=DatabaseContext")
        {
            Configuration.LazyLoadingEnabled = true;
        }

        #region Entidades
        /// <summary>
        /// Representa o contexto do banco de dados para a aplicação.
        /// </summary>
        public DbSet<AboutConfiguration> AboutConfiguration { get; set; }
        public DbSet<SystemConfiguration> SystemConfiguration { get; set; }
        public DbSet<Log> Log { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Workday> Workday { get; set; }
        public DbSet<GapAnalysis> GapAnalysis { get; set; }
        public DbSet<Jos> Jos { get; set; }
        public DbSet<LpaBel> LpaBel { get; set; }
        public DbSet<LpaVls> LpaVls { get; set; }
        public DbSet<Rh> Rh { get; set; }
        public DbSet<Notification> Notification { get; set; }
        public DbSet<Location> Location { get; set; }


        #endregion

        /// <summary>
        /// Carrega as propriedades das entidades
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

        }
    }
}
