using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model.Entities;

namespace Model.Repositories.Configurations;
class DocentConfiguration : IEntityTypeConfiguration<Docent>
{
    public void Configure(EntityTypeBuilder<Docent> builder)
    {
        builder.ToTable("Docenten");

        builder.HasKey(docent => docent.Id);
        builder.Property(docent => docent.Id).ValueGeneratedOnAdd();

        builder.Property(docent => docent.Voornaam)
            .IsRequired()
            .HasMaxLength(30);
        builder.Property(docent => docent.Familienaam)
            .IsRequired()
            .HasMaxLength(30);
        builder.Property(docent => docent.Wedde)
            .IsRequired();
    }
}
