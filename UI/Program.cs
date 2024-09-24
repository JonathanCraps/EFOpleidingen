using Model.Repositories;

namespace UI;
public partial class Program
{
    private static readonly EFOpleidingenContext context = new EFOpleidingenContext();


    public static string MenuGegevens => $"";
    public static SubMenu menu = new SubMenu(01, null, "MuziekMenu", MenuItemActive.Enabled, MenuItemVisible.Visible, new List<MenuItem> {
        new MenuAction(02, "<1> Artiesten", "Geeft de Docenten weer", MenuItemActive.Enabled, MenuItemVisible.Visible, Docenten),
        new MenuAction(03, "<2> Artiesten", "Geeft de Weddes weer die groter zijn dan (getal)", MenuItemActive.Enabled, MenuItemVisible.Visible, WeddesGroterDan),
        new MenuAction(04, "<3> Artiesten", "Geeft de Opleidingen weer van een Docent", MenuItemActive.Enabled, MenuItemVisible.Visible, OpleidingenDocent),
        new MenuAction(05, "<4> Artiesten", "Maak een nieuwe Docent aan", MenuItemActive.Enabled, MenuItemVisible.Visible, NieuweDocent)

    });
    private static void Main(string[] args)
    {
        ToonMenu("EFOpleidingen", menu);
    }
    public static void Docenten()
    {
        throw new NotImplementedException();
    }
    public static void WeddesGroterDan()
    {
        throw new NotImplementedException();
    }
    public static void OpleidingenDocent()
    {
        throw new NotImplementedException();
    }
    public static void NieuweDocent()
    {
        throw new NotImplementedException();
    }

}