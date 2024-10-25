﻿

namespace Model.Entities;
public class Opleiding
{
    public int Id { get; set; }
    public required string Naam { get; set; } = null!;
    public required int AantalDagen { get; set; }
    public virtual ICollection<DocentOpleiding> docentOpleidingen { get; set; } = new List<DocentOpleiding>();
}
