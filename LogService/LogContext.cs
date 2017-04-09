using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using LogService.Migrations;

namespace LogService {
    public class LogContext : DbContext {
        public LogContext() {

        }
        public LogContext(SqlConnection connection):base (connection,true) {}

        //public LogContext(string name) : base("name=DefaultConnection") { }

        public DbSet<LogService.Entities.Log> Logs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            //var logs = modelBuilder.Entity<LogService.Entities.Log>().ToTable("Logs");
        }
        public static void ConfigureInitializer() {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<LogContext, Configuration>());
        }
    }
}
