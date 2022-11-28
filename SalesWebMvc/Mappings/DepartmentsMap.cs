using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesWebMvc.Models.ViewModels;

public class DepartmentsMap : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.ToTable("Department");

        builder.Property(p => p.Id)
            .HasColumnType("numeric(5,2)")
            .IsRequired();

        builder.Property(p => p.Name)
            .HasColumnType("varchar(100)")
            .IsRequired();
    }
}
