using CsvHelper;
using MeterReadingAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MeterReadingAPI.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            string resourceName = "MeterReadingAPI.Resources.Test_Accounts.csv";
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    CsvReader csvReader = new CsvReader(reader, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB"));
                    var accounts = csvReader.GetRecords<Account>().ToArray();

                    foreach (var item in accounts)
                    {
                        modelBuilder.Entity<Account>().HasData(new Account { AccountId = item.AccountId, FirstName = item.FirstName, LastName = item.LastName });
                    }
                }
            }
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Reading> Readings { get; set; }
    }
}
