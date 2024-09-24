using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model.Entities;

namespace Model.Repositories.Seedings;
class OpleidingSeeding : IEntityTypeConfiguration<Opleiding>
{
    public void Configure(EntityTypeBuilder<Opleiding> builder)
    {
        builder.HasData(
            new Opleiding
            {
                Id = 1,
                Naam = "C# Development",
                AantalDagen = 90
            }, new Opleiding
            {
                Id = 2,
                Naam = "Professioneel Koken",
                AantalDagen = 60
            }, new Opleiding
            {
                Id = 3,
                Naam = "Java Development",
                AantalDagen = 90
            }, new Opleiding
            {
                Id = 4,
                Naam = "Geavanceerde Algebra",
                AantalDagen = 30
            }, new Opleiding
            {
                Id = 5,
                Naam = "Italiaans voor beginners",
                AantalDagen = 15
            }, new Opleiding
            {
                Id = 6,
                Naam = "Cybersecurity in webapplicaties",
                AantalDagen = 30
            }, new Opleiding
            {
                Id = 7,
                Naam = "Communicatie in professionele setting",
                AantalDagen = 10
            }, new Opleiding
            {
                Id = 8,
                Naam = "Frans voor gevorderden",
                AantalDagen = 40
            }
            );
    }
}
