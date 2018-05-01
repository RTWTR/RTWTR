using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace RTWTR.Data
{
   // Needed by the CLI
   public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<RTWTRDbContext>
   {
       public RTWTRDbContext CreateDbContext(string[] args)
       {
           IConfigurationRoot configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .Build();

           var builder = new DbContextOptionsBuilder<RTWTRDbContext>();

           builder.UseSqlServer(this.GetDbConnectionString());       

           return new RTWTRDbContext(builder.Options);
       }

       private string GetDbConnectionString()
       {
           if (System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT").Equals("Development"))
           {
               return System.Environment.GetEnvironmentVariable("rtwtr_dev").Replace(@"\\", @"\");                
           }

           return System.Environment.GetEnvironmentVariable("rtwtr").Replace(@"\\", @"\");
       }
   }
}