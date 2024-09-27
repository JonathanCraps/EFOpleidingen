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
    private static IDocentOpleidingRepository _docentOpleidingRepository = new SQLDocentOpleidingRepository(_context);
    //Services
    private static readonly DocentService _docentService = new DocentService(_docentRepository);
    private static readonly DocentOpleidingService _docentOpleidingService = new DocentOpleidingService(_docentOpleidingRepository);

    public static string MenuGegevens => $"";
    public static SubMenu menu = new SubMenu(01, null, "MuziekMenu", MenuItemActive.Enabled, MenuItemVisible.Visible, new List<MenuItem> {
        new MenuAction(02, "<1> Docenten", "Geeft de Docenten weer op naam", MenuItemActive.Enabled, MenuItemVisible.Visible, Docenten),
        new MenuAction(03, "<2> Docent met wedde vanaf", "Geeft de Docenten weer met Weddes die groter zijn dan (getal)", MenuItemActive.Enabled, MenuItemVisible.Visible, WeddesGroterDan),
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
        var docentenLijst = _docentService.GetAllDocentenOrderedByNameAsync().Result;
        if (docentenLijst.Count() > 0)
        {
            LeesLijst("Docenten", docentenLijst, docentenLijst.Select(docent => "(" + docent.Id + ") " + docent.Familienaam + " " + docent.Voornaam).ToList(), SelectionMode.None);
        }
        else
        {
            ToonFoutBoodschap("Er zijn geen Docenten");
        }
        ToonMenuLijn(20);
    }
    public static void WeddesGroterDan()
    {
        decimal gegevenWedde = (decimal)LeesDecimal($"Vanaf welke wedde wilt u de docenten zien? liggen)", 0, decimal.MaxValue, OptionMode.Mandatory)!;
        var weddeLijst = _docentService.GetDocentenByMinWeddeAsync(gegevenWedde).Result;
        if (weddeLijst.Count() > 0)
        {
            LeesLijst($"Docenten waarvan de Wedde groter is dan {gegevenWedde} euro per maand",
                weddeLijst,
                weddeLijst
                .Select(docent => "[" + docent.Wedde + " euro/maand] " + docent.Voornaam + " " + docent.Familienaam)
                .ToList(),
                SelectionMode.None);
        }
        else
        {
            ToonFoutBoodschap("Er zijn geen Docenten met die minimum wedde.");
        }
    }


    public static void OpleidingenDocent()
    {
        List<Docent> docentenLijst = (List<Docent>)_docentService.GetAllDocentenOrderedById().Result;
        if (docentenLijst.Count > 0)
        {
            LeesLijst("Docenten", docentenLijst, docentenLijst.Select(docent => $"[{docent.Id}] {docent.Familienaam} {docent.Voornaam}").ToList(), SelectionMode.None);
            int gegevenId = (int)LeesInt("Welke Docent hun Opleidingen wilt u zien?", 0, int.MaxValue, OptionMode.Mandatory)!;
            List<DocentOpleiding> docentOpleidingLijst = (List<DocentOpleiding>)_docentOpleidingService.GetAllDocentOpleidingenByDocentIdAsync(gegevenId).Result;
            Docent? gegevenDocent = docentOpleidingLijst.FirstOrDefault()?.docent;
            if (gegevenDocent != null)
            {
                if (docentOpleidingLijst.Count > 0)
                {
                    LeesLijst($"{gegevenDocent.Familienaam} {gegevenDocent.Voornaam} 's Opleidingen", docentOpleidingLijst, docentOpleidingLijst.Select(docentOpleiding => docentOpleiding.OpleidingId + ") " + docentOpleiding.opleiding.Naam + ", Expertise : " + docentOpleiding.Expertise + "/10").ToList(), SelectionMode.None);
                }
                else
                {
                    ToonFoutBoodschap($"De geselecteerde Docent heeft geen Opleidingen.");
                }
            }
            else
            {
                ToonFoutBoodschap("Deze leerkracht bestaat niet.");
            }
        }
        else
        {
            ToonFoutBoodschap("Er zijn geen Docenten");
        }
        

    }
    public static void NieuweDocent()
    {
        string dVoornaam = LeesString("Wat is de Voornaam van de Docent?", 0, 40, OptionMode.Mandatory)!;
        string dFamilienaam = LeesString("Wat is de Familienaam van de Docent?", 0, 40, OptionMode.Mandatory)!;
        decimal dWedde = (decimal)LeesDecimal("Wat is de Wedde van de Docent?", 0, decimal.MaxValue, OptionMode.Mandatory)!;
        _ = _docentService.AddDocentAsync(new Docent { Voornaam = dVoornaam, Familienaam = dFamilienaam, Wedde = dWedde });
    }

}