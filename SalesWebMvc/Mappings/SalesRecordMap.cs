using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class SalesRecordMap : IEntityTypeConfiguration<SalesRecord>
{
    public void Configure(EntityTypeBuilder<SalesRecord> builder)
    {
        builder.ToTable("SalesRecord");

        builder.Property(p => p.Date)
               .HasColumnType("datetime")
               .IsRequired();

        builder.Property(p => p.Amount)
               .HasColumnType("numeric(5,2)")
               .IsRequired();

        builder.HasOne(p => p.Seller)
               .WithMany(p => p.Sales)
               .OnDelete(DeleteBehavior.NoAction);
    }
}
