using EduHome.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.DataAccess.Configurations;

public class CourseConfigurations : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.Property(x => x.Name).IsRequired(true).HasMaxLength(150);
        builder.Property(x=>x.Description).IsRequired(false).HasMaxLength(400);
        builder.Property(x => x.Image).IsRequired(false).HasMaxLength(400);
        
    }
}
