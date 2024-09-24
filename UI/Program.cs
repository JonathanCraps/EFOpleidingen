using Model.Entities;
using Model.Repositories;
using Model.Services;

namespace UI;
public partial class Program
{
    //Context
    private static readonly EFOpleidingenContext _context = new EFOpleidingenContext();
    //Repositories
    private static IDocentRepository _docentRepository = new SQLDocentRepository(_context);
    //Services
    private static readonly DocentService _docentService = new DocentService(_docentRepository);

    public static string MenuGegevens => $"";
    public static SubMenu menu = new SubMenu(01, null, "MuziekMenu", MenuItemActive.Enabled, MenuItemVisible.Visible, new List<MenuItem> {
        new MenuAction(02, "<1> Docenten", "Geeft de Docenten weer op naam", MenuItemActive.Enabled, MenuItemVisible.Visible, Docenten),
        new MenuAction(03, "<2> Docent met wedde vanaf", "Geeft de Docenten met Weddes die groter zijn dan (getal) weer", MenuItemActive.Enabled, MenuItemVisible.Visible, WeddesGroterDan),
        new MenuAction(04, "<3> Opleidingen Docent", "Geeft de Opleidingen weer van een Docent", MenuItemActive.Enabled, MenuItemVisible.Visible, OpleidingenDocent),
        new MenuAction(05, "<4> Toevoegen Docent", "Maak een nieuwe Docent aan", MenuItemActive.Enabled, MenuItemVisible.Visible, NieuweDocent)

    });
    private static void Main(string[] args)
    {
        ToonMenu("EFOpleidingen", menu);
    }
    //Menu Onderdelen
    public static void Docenten()
    {
        var docentenLijst = GetAllDocenten();
        if (docentenLijst.Count() > 0)
        {
        LeesLijst("Docenten", docentenLijst, docentenLijst.OrderBy(docent => docent.Familienaam).ThenBy(docent => docent.Voornaam).Select(docent => "(" + docent.Id + ") " + docent.Familienaam + " " + docent.Voornaam).ToList(), SelectionMode.None);
        }
        else
        {
            ToonFoutBoodschap("Er zijn geen Docenten");
        }
        ToonMenuLijn(20);
    }
    public static void WeddesGroterDan()
    {
        List<Docent> docentenLijst = GetAllDocenten();
        decimal gegevenWedde = (decimal)LeesDecimal("Vanaf welke wedde wilt u de docenten zien?", 0, decimal.MaxValue, OptionMode.Mandatory)!;
        LeesLijst($"Docenten waarvan de Wedde groter is dan {gegevenWedde} euro per maand",
            docentenLijst,
            docentenLijst
            .Where(docent => docent.Wedde >= gegevenWedde)
            .OrderBy(docent => docent.Wedde)
            .Select(docent => "[" + docent.Wedde + " euro/maand]" + docent.Voornaam + " " + docent.Familienaam)
            .ToList(),
            SelectionMode.None);
    }
    public static void OpleidingenDocent()
    {
        throw new NotImplementedException();
    }
    public static void NieuweDocent()
    {
        throw new NotImplementedException();
    }
    //Functies
    public static List<Docent> GetAllDocenten()
    {
        return (List<Docent>)_docentService.GetAllDocentenAsync().Result;
    }

}