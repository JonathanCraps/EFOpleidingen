
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model.Entities;

namespace Model.Repositories.Seedings;
class DocentOpleidingSeeding : IEntityTypeConfiguration<DocentOpleiding>
{
    public void Configure(EntityTypeBuilder<DocentOpleiding> builder) 
    {
        builder.HasData(
            //Programmeren
            new DocentOpleiding
            {
                DocentId = 1,
                OpleidingId = 1,
                Expertise = 7
            }, new DocentOpleiding
            {
                DocentId = 1,
                OpleidingId = 3,
                Expertise = 6
            }, new DocentOpleiding
            {
                DocentId = 8,
                OpleidingId = 3,
                Expertise = 7
            }, new DocentOpleiding
            {
                DocentId = 1,
                OpleidingId = 6,
                Expertise = 4
            },
            //Talen
            new DocentOpleiding
            {
                DocentId = 2,
                OpleidingId = 5,
                Expertise = 9
            }, new DocentOpleiding
            {
                DocentId = 2,
                OpleidingId = 8,
                Expertise = 7
            }, new DocentOpleiding
            {
                DocentId = 1,
                OpleidingId = 8,
                Expertise = 4
            },
            //Koken
            new DocentOpleiding
            {
                DocentId = 3,
                OpleidingId = 2,
                Expertise = 9
            }, new DocentOpleiding
            {
                DocentId = 4,
                OpleidingId = 2,
                Expertise = 7
            }, new DocentOpleiding
            {
                DocentId = 5,
                OpleidingId = 2,
                Expertise = 5
            },
            //Communicatie
            new DocentOpleiding
            {
                DocentId = 5,
                OpleidingId = 7,
                Expertise = 4
            }, new DocentOpleiding
            {
                DocentId = 6,
                OpleidingId = 7,
                Expertise = 7
            },
            //Wiskunde
            new DocentOpleiding
            {
                DocentId = 7,
                OpleidingId = 4,
                Expertise = 9
            }, new DocentOpleiding
            {
                DocentId = 8,
                OpleidingId = 4,
                Expertise = 6
            }
        );
    }
}
