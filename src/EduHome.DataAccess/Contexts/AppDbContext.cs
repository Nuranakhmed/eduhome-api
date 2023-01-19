using EduHome.Core.Entities;
using EduHome.DataAccess.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.DataAccess.Contexts;

public class AppDbContext:DbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
	{

	}
	public DbSet<Course> Courses { get; set; } = null!;
    public DbSet<CourseCategory> Categories { get; set; }=null!;


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CourseConfigurations).Assembly);
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Course>()
            .HasOne<CourseCategory>(s => s.CourseCategory)
            .WithMany(g => g.Courses)
            .HasForeignKey(s => s.CategoryId);
    }
}
