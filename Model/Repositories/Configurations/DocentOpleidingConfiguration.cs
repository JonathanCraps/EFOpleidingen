using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model.Entities;

namespace Model.Repositories.Configurations;
class DocentOpleidingConfiguration : IEntityTypeConfiguration<DocentOpleiding>
{
    public void Configure(EntityTypeBuilder<DocentOpleiding> builder)
    {
        builder.ToTable("DocentOpleidingen");

        builder.HasKey(docentOpleiding => new {docentOpleiding.OpleidingId, docentOpleiding.DocentId});
        builder.Property(docentOpleiding => docentOpleiding.Expertise)
            .IsRequired();
        
    }
}
