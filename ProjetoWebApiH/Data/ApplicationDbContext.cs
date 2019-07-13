using ProjetoWebApiH.Models;
using System.Data.Entity;

namespace ProjetoWebApiH.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() :
            base("ProjetoH")
        {

        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        public DbSet<Tarefa> Tarefas { get; set; }
    }
}