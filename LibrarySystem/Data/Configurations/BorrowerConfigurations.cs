using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LibrarySystem.Entities;

namespace LibrarySystem.Data.Configurations
{
    public class BorrowerConfigurations : IEntityTypeConfiguration<Borrower>
    {
        public void Configure(EntityTypeBuilder<Borrower> builder)
        {
            builder.Property(b => b.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(b => b.MembershipDate)
                .IsRequired();

            builder.HasQueryFilter(b => !b.IsDeleted);
        }
    }
}
