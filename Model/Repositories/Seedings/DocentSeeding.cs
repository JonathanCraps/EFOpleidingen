using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model.Entities;

namespace Model.Repositories.Seedings;
class DocentSeeding : IEntityTypeConfiguration<Docent>
{
    public void Configure(EntityTypeBuilder<Docent> builder)
    {
        builder.HasData(
            new Docent
            {
                Id = 1,
                Voornaam = "Frederik",
                Familienaam = "Michelle",
                Wedde = 5300

            }, new Docent
            {
                Id = 2,
                Voornaam = "Anatov",
                Familienaam = "Varaco",
                Wedde = 6200

            }, new Docent
            {
                Id = 3,
                Voornaam = "Pieter",
                Familienaam = "Von Pieterson",
                Wedde = 7900

            }, new Docent
            {
                Id = 4,
                Voornaam = "Sarah",
                Familienaam = "De Slaeger",
                Wedde = 3900

            }, new Docent
            {
                Id = 5,
                Voornaam = "Karel",
                Familienaam = "Vandenheuvel",
                Wedde = 6600

            }, new Docent
            {
                Id = 6,
                Voornaam = "Irma",
                Familienaam = "Desmet",
                Wedde = 3850

            }, new Docent
            {
                Id = 7,
                Voornaam = "Bart",
                Familienaam = "Copain",
                Wedde = 4200

            }, new Docent
            {
                Id = 8,
                Voornaam = "Valentino",
                Familienaam = "La Montana",
                Wedde = 7300

            }
            );
    }
}
