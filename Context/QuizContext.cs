using Microsoft.EntityFrameworkCore;
using QUIZ_API_REACT.Entities;

namespace QUIZ_API_REACT.Context
{
    public class QuizContext : DbContext
    {
       public DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>().ToTable("usuarios");
    }
    }}
