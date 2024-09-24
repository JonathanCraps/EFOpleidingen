namespace Model.Entities;
public class DocentOpleiding
{
    public int DocentId { get; set; }
    public int OpleidingId { get; set; }
    public int Expertise {  get; set; }

    public virtual Docent docent { get; set; } = null!;
    public virtual Opleiding opleiding{ get; set; } = null!;
}
