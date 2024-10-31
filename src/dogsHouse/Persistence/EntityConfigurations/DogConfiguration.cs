using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class DogConfiguration : IEntityTypeConfiguration<Dog>
{
    public void Configure(EntityTypeBuilder<Dog> builder)
    {
        builder.ToTable("Dogs").HasKey(d => d.Id);

        builder.Property(d => d.Id).HasColumnName("Id").IsRequired();
        builder.Property(d => d.Name).HasColumnName("Name").IsRequired();
        builder.Property(d => d.Color).HasColumnName("Color").IsRequired();
        builder.Property(d => d.TailLength).HasColumnName("TailLength").IsRequired();
        builder.Property(d => d.Weight).HasColumnName("Weight").IsRequired();
        builder.Property(d => d.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(d => d.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(d => d.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(d => !d.DeletedDate.HasValue);
    }
}