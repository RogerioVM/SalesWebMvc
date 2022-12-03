using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class SellerMap : IEntityTypeConfiguration<Seller>
{
    public void Configure(EntityTypeBuilder<Seller> builder)
    {
        builder.ToTable("Seller");

        builder.Property(p => p.Id)
            .ValueGeneratedNever()
            .IsRequired();

        builder.Property(p => p.Name)
            .HasColumnType("varchar(150)")
            .IsRequired();

        builder.Property(p => p.Email)
            .HasColumnType("varchar(150)")
            .IsRequired();

        builder.Property(p => p.BaseSalary)
            .HasColumnType("numeric(38,2)")
            .IsRequired();

        builder.Property(p => p.BirthDate)
            .HasColumnType("datetime")
            .IsRequired();

        builder.HasOne(p => p.Department)
            .WithMany(p => p.Sellers)
            .OnDelete(DeleteBehavior.NoAction);

    }
}
