using Microsoft.EntityFrameworkCore;
using zalohovaci_system_api.Models;

namespace zalohovaci_system_api
{
    public class MyContext : DbContext
    {
        public DbSet<BackupJob> BackupJobs { get; set; }
        public DbSet<BackupMethod> BackupMethod { get; set; }
        public DbSet<BackupRetention> BackupRetention { get; set; }
        public DbSet<FilePath> FilePaths { get; set; }
        public DbSet<FilePathType> FilePathType { get; set; }
        public DbSet<PathToJobCombo> PathToJobs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=mysqlstudenti.litv.sssvt.cz;database=3b1_swientekmatej_db2;user=swientekmatej;password=123456");
        }
    }
}
