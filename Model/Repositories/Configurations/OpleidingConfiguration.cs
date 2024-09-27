using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model.Entities;

namespace Model.Repositories.Configurations;
class OpleidingConfiguration : IEntityTypeConfiguration<Opleiding>
{
    public void Configure(EntityTypeBuilder<Opleiding> builder)
    {
        builder.ToTable("Opleidingen");

        builder.HasKey(opleiding => opleiding.Id);
        builder.Property(opleiding => opleiding.Id).ValueGeneratedOnAdd();

        builder.Property(opleiding => opleiding.Naam)
            .IsRequired()
            .HasMaxLength(50);
        builder.Property(opleiding => opleiding.AantalDagen)
            .IsRequired();
    }
}
