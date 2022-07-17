using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGSX.CqrsTemp.Domain.Models;

namespace SGSX.CqrsTemp.Persistence.Cats.Command;
internal class CatModelConfiguration : IEntityTypeConfiguration<Cat>
{
    public void Configure(EntityTypeBuilder<Cat> builder)
    {
        builder.Property(c => c.Id);

        builder.Property(c => c.MouseBuddyId);

        builder.Property(c => c.Name)
            .HasMaxLength(30);

        builder.Property(c => c.Description)
            .HasMaxLength(300)
            .IsUnicode();

        builder.Property(c => c.CatBreed);

        builder.Property(c => c.CreateDate);

        builder.HasKey(c => c.Id);

        builder.HasOne(c => c.MouseBuddy)
            .WithOne(c => c.Owner)
            .HasForeignKey<MouseBuddy>(c => c.CatId)
            .OnDelete(DeleteBehavior.ClientSetNull);

    }
}

