// Johan Vandaele - Versie: 20230315

namespace UIConsole;

public partial class Program
{
	// -----
	// Enums
	// -----
	public enum Direction { Horizontal, Vertical }
	public enum MenuItemActive { Enabled, Disabled }
	public enum MenuItemVisible { Visible, Hidden }

	// --------
	// MenuItem
	// --------
	public class MenuItem
	{
		public MenuItem() { }
		public MenuItem(int id) => (Id) = (id);

		public MenuItem(int id, string label, MenuItemActive active, MenuItemVisible visible)
			=> (Id, Label, Active, Visible, LabelOrg, ActiveOrg, VisibleOrg) = (id, label, active, visible, label, active, visible);

		public MenuItem(int id, string label, string titel, MenuItemActive active, MenuItemVisible visible)
			=> (Id, Label, SubMenuTitel, Active, Visible, LabelOrg, ActiveOrg, VisibleOrg) = (id, label, titel, active, visible, label, active, visible);

		// ----------
		// Properties
		// ----------
		private string label { get; set; } = null!;
		public int Id { get; set; }         // Unique : To enable/disable menuitem

		public string Label
		{
			get { return label; }
			set
			{
				label = value;
				if (this is SubMenu && label != null) label += "...";
			}
		}

		public string ActualLabel { get; set; } = null!;
		public string SubMenuTitel { get; set; } = null!;
		public MenuItemActive Active { get; set; }
		public MenuItemVisible Visible { get; set; }
		public string LabelOrg { get; set; } = null!;
		public MenuItemActive ActiveOrg { get; set; }
		public MenuItemVisible VisibleOrg { get; set; }
	}

	// MenuItem
	// --------
	public class MenuLijn : MenuItem
	{
		// -----------
		// Constructor
		// -----------
		public MenuLijn() { }
		public MenuLijn(int id) : base(id) { }

		// ----------
		// Properties
		// ----------
	}

	public class SubMenu : MenuItem
	{
		// -----------
		// Constructor
		// -----------
		public SubMenu(int id, string? label, string titel, MenuItemActive active, MenuItemVisible visible, List<MenuItem> menuItems) : base(id, label!, active, visible)
			=> (SubMenuTitel, Richting, MenuItems) = (titel, Direction.Vertical, menuItems);

		public SubMenu(int id, string? label, string titel, MenuItemActive active, MenuItemVisible visible, Direction richting, List<MenuItem> menuItems) : base(id, label!, active, visible)
			=> (SubMenuTitel, Richting, MenuItems) = (titel, richting, menuItems);

		// ----------
		// Properties
		// ----------
		public List<MenuItem> MenuItems { get; set; }
		public Direction Richting { get; set; }
		public string Input { get; set; } = null!;
	}

	public class MenuAction : MenuItem
	{
		// -----------
		// Constructor
		// -----------
		public MenuAction(int id, string label, string titel, MenuItemActive active, MenuItemVisible visible, Action menuItemAction00) : base(id, label, titel, active, visible)
			=> (MenuItemAction00) = (menuItemAction00);

		// ----------
		// Properties
		// ----------
		public Action MenuItemAction00 { get; set; } = null!;
		public Action<object> MenuItemAction01 { get; set; } = null!;
		public object Par01 { get; set; } = null!;
	}

	// -------
	// Methods
	// -------
	private static bool FirstTime = true;
	private static ConsoleColor menuTextForegroundActive;
	private static ConsoleColor menuTextForegroundNotActive;
	private static ConsoleColor menuTextBackground = ConsoleColor.Blue;
	private static string MenuPath = string.Empty;
	private static string AppTitel = string.Empty;
	private static readonly string exitTextSubMenu = "E<x>it";
	private static readonly string exitTextHoofdMenu = "E<x>it toepassing";
	private static readonly string versie = " - Johan Vandaele (VDAB) - Versie 20230315";

	// ---------
	// SetActive
	// ---------
	public static void SetActive(SubMenu sm, List<int> menuIds, MenuItemActive active)
		=> menuIds.ForEach(i => SetActive(sm, i, active));

	private static void SetActive(MenuItem menuItem, int id, MenuItemActive active)
	{
		if (menuItem is SubMenu)
			foreach (var item in ((SubMenu)menuItem).MenuItems)
			{
				if (item.Id == id) item.Active = active;
				SetActive(item, id, active);
			}
	}

	// ---------
	// Set Label
	// ---------
	public static void SetLabel(SubMenu sm, List<int> menuIds, string label)
		=> menuIds.ForEach(i => SetLabel(sm, i, label));

	private static void SetLabel(MenuItem menuItem, int id, string label)
	{
		if (menuItem is SubMenu)
			foreach (var item in ((SubMenu)menuItem).MenuItems)
			{
				if (item.Id == id) item.Label = label;
				SetLabel(item, id, label);
			}
	}

	// ----------
	// SetVisible
	// ----------
	public static void SetVisible(SubMenu sm, List<int> menuIds, MenuItemVisible visible)
		=> menuIds.ForEach(i => SetVisible(sm, i, visible));

	private static void SetVisible(MenuItem menuItem, int id, MenuItemVisible visible)
	{
		if (menuItem is SubMenu)
			foreach (var item in ((SubMenu)menuItem).MenuItems)
			{
				if (item.Id == id) item.Visible = visible;
				SetVisible(item, id, visible);
			}
	}

	// -----
	// Reset
	// -----
	public static void ResetMenu(SubMenu subMenu)
	{
		foreach (MenuItem item in subMenu.MenuItems)
		{
			item.Active = item.ActiveOrg;
			item.Visible = item.VisibleOrg;
			item.Label = item.LabelOrg;

			if (item is SubMenu) ResetMenu((SubMenu)item);
		}
	}

	// ------------------
	// ToonMenuHorizontal
	// ------------------
	private static void ToonMenuHorizontal(SubMenu menuItem)
	{
		char? keuze = null;

		menuItem.Input = string.Empty;
		var menuHorizontal = string.Empty;

		foreach (var item in menuItem.MenuItems)
		{
			var label = item.Label;

			if (!(item is MenuLijn))
			{
				int idx1 = label.IndexOf('<');
				int idx2 = label.IndexOf('>');

				if (idx1 >= 0 && idx2 >= 0 && idx2 == idx1 + 2)
					if (item.Active == MenuItemActive.Enabled)
						menuItem.Input += label.Substring(idx1 + 1, 1).ToUpper();
					else
					{
						label = label.Remove(idx1, 1);
						label = label.Remove(idx2 - 1, 1);
					}

				menuHorizontal += $"{label} - ";
			}
		}

		Console.WriteLine($"{menuHorizontal}{exitTextSubMenu}");
		menuItem.Input += "X";

		keuze = LeesString($"Geef uw keuze ({menuItem.Input})", 1, 1, OptionMode.Mandatory)!.ToUpper().ToCharArray()[0];

		while (!menuItem.Input.Contains((char)keuze))
		{
			ToonFoutBoodschap($"Verkeerde keuze ({menuItem.Input}): ");
			keuze = LeesString($"Geef uw keuze ({menuItem.Input})", 1, 1, OptionMode.Mandatory)!.ToUpper().ToCharArray()[0];
		}

		var selectedItem = menuItem.MenuItems.Where(i => !(i is MenuLijn) && i.Label.Contains($"<{keuze}>")).FirstOrDefault();

		if (selectedItem is MenuAction)                     // Execute method
		{
			var fgcSaved = Console.ForegroundColor;
			Console.ForegroundColor = ConsoleColor.Blue;

			if (((MenuAction)selectedItem).MenuItemAction00 != null)
			{
				if (selectedItem.SubMenuTitel != String.Empty)
					ToonMenuHoofding(selectedItem.SubMenuTitel, '-', false);

				((MenuAction)selectedItem).MenuItemAction00();
			}

			Console.ForegroundColor = fgcSaved;
		}
	}

	// --------
	// ToonMenu
	// --------
	public static void ToonMenu(string appTitel, SubMenu menuItem)
	{
		AppTitel = appTitel;
		Console.Title = $"{AppTitel}{versie}";

		if (FirstTime)      // Eerste oproep van een menu (Start application)
		{
			FirstTime = false;
			InitConsole();

			menuTextForegroundActive = darkMode ? ConsoleColor.White : ConsoleColor.Black;
			menuTextForegroundNotActive = ConsoleColor.Gray;

			ToonMenuHoofding(appTitel, '=', false, 0);
			Console.WriteLine();

			Console.WriteLine(@"oooooo    oooo  oooooooooo.         .o.       oooooooooo.");
			Console.WriteLine(@"`888.     .8'  `888'   `Y8b       .888.      `888'   `Y8b ");
			Console.WriteLine(@"`888.   .8'    888      888     .8`888.      888     888");
			Console.WriteLine(@"`888. .8'     888      888    .8' `888.     888oooo888' ");
			Console.WriteLine(@"`888.8'      888      888   .88ooo8888.    888    `88b ");
			Console.WriteLine(@"`888'       888     d88'  .8'     `888.   888    .88P ");
			Console.WriteLine(@"`8'        o888bood8P'  o88o     o8888o o888bood8P'  ");
			Console.WriteLine($"\n\n{$".NET Versie: {Environment.Version}"}");

			Console.ReadKey();
			ToonMenu(menuItem);
			Console.WriteLine("\nWij danken u voor onze prettige samenwerking. Tot de volgend keer....");
			Console.ReadKey();
		}
		else
		{
			if (menuItem.Richting == Direction.Vertical) ToonMenu(menuItem);
			else ToonMenuHorizontal(menuItem);
		}
	}

	// --------
	// ToonMenu
	// --------
	private static void ToonMenu(SubMenu menuItem)
	{
		// I n i t i a l i s a t i e s
		MenuPath += $"\\{menuItem.SubMenuTitel}";
		char? keuze = null;

		while (keuze != 'X')
		{
			// Bepaal minimum lengte van een menuitemlabel
			var labelLength = exitTextSubMenu.Length;
			if (string.IsNullOrWhiteSpace(menuItem.Label)) labelLength = exitTextHoofdMenu.Length;

			menuItem.Input = string.Empty;

			// Bepaal de geactualiseerde label en verzamel de input tekens
			foreach (var item in menuItem.MenuItems)
				if (!(item is MenuLijn))
				{
					var label = item.Label;

					// Bepaal het input character van een menuitem
					int idx1 = label.IndexOf('<');
					int idx2 = label.IndexOf('>');

					if (idx1 >= 0 && idx2 >= 0 && idx2 == idx1 + 2)
						if (item.Active == MenuItemActive.Enabled && item.Visible == MenuItemVisible.Visible)
							// Indien menuitem enabled : verzamel de mogelijke tekens voor input
							menuItem.Input += label.Substring(idx1 + 1, 1).ToUpper();
						else
						{
							// Indien menuitem disabled verwijder de '<' en '>' : kan niet gekozen worden
							label = label.Remove(idx2, 1);
							label = label.Remove(idx1, 1);
						}

					// Bepaal de lengte van het langste menuitem
					if (!(item is MenuLijn) && item.Visible == MenuItemVisible.Visible)
						if (item.Label.Length > labelLength) labelLength = item.Label.Length;

					// Bewaar de geactualiseerde label
					item.ActualLabel = label;
				}

			// Voeg E<X>it toe
			menuItem.Input += "X";

			// T o o n   M e n u
			ToonSubMenu(menuItem, labelLength);

			// M e n u I n p u t
			Console.Beep();
			keuze = LeesString($"Geef uw keuze ({menuItem.Input})", 1, 1, OptionMode.Mandatory)!.ToUpper().ToCharArray()[0];

			while (!menuItem.Input.Contains((char)keuze))
			{
				ToonFoutBoodschap($"Verkeerde keuze ({menuItem.Input}): ");
				Console.Beep();
				keuze = LeesString($"Geef uw keuze ({menuItem.Input})", 1, 1, OptionMode.Mandatory)!.ToUpper().ToCharArray()[0];
			}

			if (keuze != 'X')
			{
				var selectedItem = menuItem.MenuItems.Where(i => !(i is MenuLijn) && i.Label.ToUpper().Contains($"<{keuze}>")).FirstOrDefault();

				if (selectedItem is MenuAction)                     // Execute method
				{
					var fgcSaved = Console.ForegroundColor;
					Console.ForegroundColor = ConsoleColor.Blue;

					if (((MenuAction)selectedItem).MenuItemAction00 != null)
					{
						if (selectedItem.SubMenuTitel != String.Empty)
							ToonMenuHoofding(selectedItem.SubMenuTitel, '-', false);

						((MenuAction)selectedItem).MenuItemAction00();
					}

					//else if (((MenuAction)selectedItem).MenuItemAction01 != null) ((MenuAction)selectedItem).MenuItemAction01(((MenuAction)selectedItem).Par01);

					Console.ForegroundColor = fgcSaved;
					Console.Write("Druk een toets om verder te gaan.");
					Console.ReadKey();
				}
				else if (selectedItem is SubMenu)                   // Toon Submenu
					ToonMenu((SubMenu)selectedItem);
			}
			else
			{
				if (string.IsNullOrWhiteSpace(menuItem.Label))
				{
					if (!(bool)LeesBool("Weet u zeker dat u de toepassing wenst te verlaten ?", OptionMode.Mandatory)!) keuze = null;
				}
				else
				{
					var idx = MenuPath.LastIndexOf('\\');
					MenuPath = MenuPath.Remove(idx, MenuPath.Length - idx);
				}
			}
		}
	}

	// ------------
	// Toon SubMenu
	// ------------
	private static void ToonSubMenu(SubMenu subMenu, int labelLength)
	{
		Console.Clear();

		ToonMenuHoofding(subMenu.SubMenuTitel, '-', true);

		foreach (var item in subMenu.MenuItems)
			if (item is MenuLijn) ToonMenuLijn(labelLength);
			else if (item.Visible == MenuItemVisible.Visible) WriteMenuItem(item.ActualLabel, item.Active, labelLength);

		ToonMenuLijn(labelLength, 0);

		if (string.IsNullOrWhiteSpace(subMenu.Label)) WriteMenuItem(exitTextHoofdMenu, MenuItemActive.Enabled, labelLength);
		else WriteMenuItem(exitTextSubMenu, MenuItemActive.Enabled, labelLength);

		ToonMenuLijn(labelLength);
	}

	// -------------
	// Toon MenuLijn
	// -------------
	private static bool lijn = false;

	private static void ToonMenuLijn(int lengte, int level = 0)
	{
		var bgSaved = Console.BackgroundColor;
		Console.BackgroundColor = menuTextBackground;

		if (!lijn) Console.WriteLine($"{new String('\t', level + 1)}+{new String('-', lengte)}+");
		Console.BackgroundColor = bgSaved;
		lijn = true;
	}

	// -------------
	// ToonMenuTitel
	// -------------
	private static void ToonMenuHoofding(string text, char lineChar, bool toonGegevens, int level = 0)
	{
		if (toonGegevens)
		{
			//Console.WriteLine($"MenuPath: {MenuPath}");
			Console.Title = $"{AppTitel} - {MenuPath}{versie}";
			Console.WriteLine($"    Info: {MenuGegevens}");
		}

		var line = new String(lineChar, text.Length * 2);
		var tabs = new String('\t', level + 1);

		var bgSaved = Console.BackgroundColor;

		Console.WriteLine($"\n{tabs}+{line}+");

		Console.Write($"{tabs}|");
		foreach (char c in text) Console.Write(c.ToString().ToUpper() + " ");
		Console.Write("|");

		Console.WriteLine($"\n{tabs}+{line}+");
		Console.BackgroundColor = bgSaved;
	}

	// -------------
	// WriteMenuItem
	// -------------
	private static void WriteMenuItem(string text, MenuItemActive isActive, int lengte, int level = 0)
	{
		lijn = false;

		var fgSaved = Console.ForegroundColor;
		var bgSaved = Console.BackgroundColor;

		if (isActive == MenuItemActive.Enabled) Console.ForegroundColor = menuTextForegroundActive;
		else Console.ForegroundColor = menuTextForegroundNotActive;

		Console.BackgroundColor = menuTextBackground;

		Console.WriteLine($"{new String('\t', level + 1)}|{text}{new String(' ', lengte - text.Length)}|");

		Console.ForegroundColor = fgSaved;
		Console.BackgroundColor = bgSaved;
	}

	// ---------
	// PrintMenu
	// ---------
	public static void PrintMenu(string appTitel, SubMenu menuItem)
	{
		if (FirstTime)      // Eerste oproep van een menu (Start application)
		{
			FirstTime = false;
			InitConsole();

			menuTextForegroundActive = darkMode ? ConsoleColor.White : ConsoleColor.Black;
			menuTextForegroundNotActive = ConsoleColor.Gray;
			menuTextBackground = darkMode ? ConsoleColor.Black : ConsoleColor.White;

			ToonMenuHoofding(appTitel, '=', false);
		}

		if (menuItem.Richting == Direction.Vertical) PrintMenu(menuItem);
		else PrintMenuHorizontal(menuItem, 0);
	}

	// ---------
	// PrintMenu
	// ---------
	private static int PrintMenu(SubMenu menuItem, int level = 0)
	{
		level++;

		ToonMenuHoofding(menuItem.SubMenuTitel, '-', false, level);

		// Bepaal minimum lengte van een menuitemlabel
		var labelLength = exitTextSubMenu.Length;
		if (string.IsNullOrWhiteSpace(menuItem.Label)) labelLength = exitTextHoofdMenu.Length;

		foreach (var mi in menuItem.MenuItems)
		{
			foreach (var item in menuItem.MenuItems)
				if (!(item is MenuLijn))
					if (item.Label.Length > labelLength) labelLength = item.Label.Length;

			if (mi is MenuLijn) ToonMenuLijn(labelLength, level);
			else
			{
				WriteMenuItem($"{mi.Label}", MenuItemActive.Enabled, labelLength, level);

				if (mi is SubMenu) PrintMenu((SubMenu)mi, level);
			}
		}

		ToonMenuLijn(labelLength, level);

		if (menuItem.Label is null) WriteMenuItem(exitTextHoofdMenu, MenuItemActive.Enabled, labelLength, level);
		else WriteMenuItem(exitTextSubMenu, MenuItemActive.Enabled, labelLength, level);

		ToonMenuLijn(labelLength, level);
		Console.WriteLine();

		return level;
	}

	// -------------------
	// PrintMenuHorizontal
	// -------------------
	private static int PrintMenuHorizontal(SubMenu menuItem, int level)
	{
		level++;

		ToonMenuHoofding(menuItem.SubMenuTitel, '-', false, level);

		// Bepaal minimum lengte van een menuitemlabel
		var labelLength = exitTextSubMenu.Length;
		if (string.IsNullOrWhiteSpace(menuItem.Label)) labelLength = exitTextHoofdMenu.Length;

		foreach (var mi in menuItem.MenuItems)
		{
			foreach (var item in menuItem.MenuItems)
				if (!(item is MenuLijn))
					if (item.Label.Length > labelLength) labelLength = item.Label.Length;

			if (mi is MenuLijn) ToonMenuLijn(labelLength, level);
			else
			{
				WriteMenuItem($"{mi.Label}", MenuItemActive.Enabled, labelLength, level);

				if (mi is SubMenu) PrintMenu((SubMenu)mi, level);
			}
		}

		ToonMenuLijn(labelLength, level);

		if (menuItem.Label is null) WriteMenuItem(exitTextHoofdMenu, MenuItemActive.Enabled, labelLength, level);
		else WriteMenuItem(exitTextSubMenu, MenuItemActive.Enabled, labelLength, level);

		ToonMenuLijn(labelLength, level);
		Console.WriteLine();

		return level;
	}
}
