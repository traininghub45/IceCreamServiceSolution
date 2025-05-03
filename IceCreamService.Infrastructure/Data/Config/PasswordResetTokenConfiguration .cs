using IceCreamService.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IceCreamService.Infrastructure.Data.Config
{
    public class PasswordResetTokenConfiguration : IEntityTypeConfiguration<PasswordResetToken>
    {
        public void Configure(EntityTypeBuilder<PasswordResetToken> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                   .ValueGeneratedOnAdd();  // Automatically generates values for Id

            builder.Property(e => e.UserId)
                   .IsRequired();

            builder.Property(e => e.Token)
                   .IsRequired()
                   .HasMaxLength(256);

            builder.Property(e => e.ExpirationTime)
                   .IsRequired();
        }
    }
}
