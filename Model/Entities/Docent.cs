﻿
namespace Model.Entities;
public class Docent
{
    public int Id { get; set; }
    public required string Voornaam { get; set; } = null!;
    public required string Familienaam { get; set; } = null!;
    public required decimal Wedde { get; set; }
    public virtual ICollection<Opleiding> opleidingen { get; set; } = new List<Opleiding>();
}