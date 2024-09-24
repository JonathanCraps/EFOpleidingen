// Johan Vandaele - Versie: 20230315

using System.Globalization;
using System.Numerics;
using System.Text.RegularExpressions;

namespace UIConsole;

public enum OptionMode { Optional = 0, Mandatory = 1 }
public enum SelectionMode { None = 0, Single = 1, Multiple = 2 }
public enum ReturnNullOrEmpty { Null = 0, Empty = 1 }

public static partial class Program
{
	private const int LengthInputLabel = 32;
	//private static int LengthInputLabel;
	private static bool darkMode = false;
	private static TimeSpan paswoordDuurtijd = new TimeSpan(10, 0, 0, 1);

	// ===========================================
	// - - - - - - - - C O N S O L E - - - - - - - 
	// ===========================================

	// ------------
	// InitConsole
	// ------------
	public static void InitConsole()
	{
#pragma warning disable CA1416 // Validate platform compatibility
		Console.SetWindowSize(160, 50);
		Console.SetBufferSize(160, 250);
#pragma warning restore CA1416 // Validate platform compatibility

		darkMode = ((string)LeesKeuzeUitLijst("Dark Mode", new List<object> { "J", "N", "j", "n" }, OptionMode.Mandatory)!).ToUpper() == "J";
		//LengthInputLabel = 32;
		ResetConsole();
	}

	// ------------
	// ResetConsole
	// ------------
	public static void ResetConsole()
	{
		Console.BackgroundColor = darkMode ? ConsoleColor.Black : ConsoleColor.White;
		Console.ForegroundColor = darkMode ? ConsoleColor.White : ConsoleColor.Black;
		Console.Clear();
	}

	// =======================================
	// - - - - - - - - I N P U T - - - - - - - 
	// =======================================

	// ---------
	// DrukToets
	// ---------
	public static void DrukToets()
	{
		Console.Write("\nDruk een toets");
		Console.ReadKey();
	}

	// ----------
	// LeesString
	// ----------
	public static string? LeesString(string label = "", int minLength = 0, int maxLength = 1024, OptionMode optionMode = OptionMode.Optional, ReturnNullOrEmpty returnNullEmpty = ReturnNullOrEmpty.Empty)
	{
		// Correction on Parameter values
		if (minLength < 0) minLength = 0;
		else if (minLength == 0 && optionMode == OptionMode.Mandatory) minLength = 1;
		else if (minLength > 0) optionMode = OptionMode.Mandatory;

		if (maxLength < 1) maxLength = 1;

		// Input
		var input = string.Empty;

		while (true)
		{
			Console.Write($"{label + (optionMode == OptionMode.Mandatory ? "* : " : " : "),LengthInputLabel}");
			input = Console.ReadLine()!;

			if (optionMode == OptionMode.Mandatory && string.IsNullOrWhiteSpace(input))
			{
				ToonFoutBoodschap("Verplichte ingave...");
				continue;
			}
			else if (input.Length < minLength)
			{
				ToonFoutBoodschap($"De ingave is te kort (min {minLength} teken{(minLength > 1 ? "s" : "")})...");
				continue;
			}
			else if (input.Length > maxLength)
			{
				ToonFoutBoodschap($"De ingave is te lang (max {maxLength} teken{(maxLength > 1 ? "s" : "")})...");
				continue;
			}

			break;
		}

		if (string.IsNullOrWhiteSpace(input))
		{
			if (returnNullEmpty == ReturnNullOrEmpty.Null) return null;
			else return string.Empty;
		}

		return input;
	}

	// -------------
	// LeesDatumTijd
	// -------------
	public static DateTime? LeesDatumTijd(string label, DateTime min, DateTime max, OptionMode optionMode = OptionMode.Optional)
	{
		// Parameters
		if (min > max) throw new ArgumentException("Parameterfout: minDateTime moet vóór maxDateTime liggen.");

		// Input
		var input = string.Empty;
		var inpParsed = DateTime.MinValue;
		var formaat = "DD/MM/YYYY UU:MM:SS";

		while (true)
		{
			Console.Write($"{label + (optionMode == OptionMode.Mandatory ? "* : " : " : "),LengthInputLabel}");
			input = Console.ReadLine()!;

			if (optionMode == OptionMode.Mandatory && string.IsNullOrWhiteSpace(input))
			{
				ToonFoutBoodschap("Verplichte ingave...");
				continue;
			}

			if (!string.IsNullOrWhiteSpace(input))
			{
				if (!DateTime.TryParse(input, out inpParsed))
				{
					ToonFoutBoodschap($"Ongeldige DatumTijd ({formaat})...");
					continue;
				}

				if (inpParsed < min || inpParsed > max)
				{
					ToonFoutBoodschap($"De DatumTijd moet liggen tussen {min.ToString("dd-MMMM-yyyy hh:mm:ss")} en {max.ToString("dd-MMMM-yyyy hh:mm:ss")}...");
					continue;
				}
			}

			break;
		}

		return string.IsNullOrWhiteSpace(input) ? null : inpParsed;
	}

	// ---------
	// LeesDatum
	// ---------
	public static DateOnly? LeesDatum(string label, DateOnly min, DateOnly max, OptionMode optionMode = OptionMode.Optional)
	{
		// Parameters
		if (min > max) throw new ArgumentException("Parameterfout: minDate moet vóór maxDate liggen.");

		// Input
		var input = string.Empty;
		var inpParsed = DateOnly.MinValue;
		var formaat = "DD/MM/YYYY";

		while (true)
		{
			Console.Write($"{label + (optionMode == OptionMode.Mandatory ? "* : " : " : "),LengthInputLabel}");
			input = Console.ReadLine()!;

			if (optionMode == OptionMode.Mandatory && string.IsNullOrWhiteSpace(input))
			{
				ToonFoutBoodschap("Verplichte ingave...");
				continue;
			}

			if (!string.IsNullOrWhiteSpace(input))
			{
				if (!DateOnly.TryParse(input, out inpParsed))
				{
					ToonFoutBoodschap($"Ongeldige Datum ({formaat})...");
					continue;
				}

				if (inpParsed < min || inpParsed > max)
				{
					ToonFoutBoodschap($"De datum moet liggen tussen {min} en {max}...");
					continue;
				}
			}

			break;
		}

		return string.IsNullOrWhiteSpace(input) ? null : inpParsed;
	}

	// --------
	// LeesTijd
	// --------
	public static TimeOnly? LeesTijd(string label, TimeOnly? min = null, TimeOnly? max = null, OptionMode optionMode = OptionMode.Optional)
	{
		// Parameters
		if (min != null && max == null || min == null && max != null) throw new ArgumentException("Parameterfout: minTime of maxTime niet ingevuld.");
		if (min != null && max != null) if (min > max) throw new ArgumentException("Parameterfout: minTime moet vóór maxTime liggen.");

		// Input
		var input = string.Empty;
		var inpParsed = TimeOnly.MinValue;
		var formaat = "UU:MM:SS";

		while (true)
		{
			Console.Write($"{label + (optionMode == OptionMode.Mandatory ? "* : " : " : "),LengthInputLabel}");
			input = Console.ReadLine()!;

			if (optionMode == OptionMode.Mandatory && string.IsNullOrWhiteSpace(input))
			{
				ToonFoutBoodschap("Verplichte ingave...");
				continue;
			}

			if (!string.IsNullOrWhiteSpace(input))
			{
				if (!TimeOnly.TryParse(input, out inpParsed))
				{
					ToonFoutBoodschap($"Ongeldige Tijd ({formaat})...");
					continue;
				}

				if (min != null && max != null)
				{
					if (inpParsed < min || inpParsed > max)
					{
						ToonFoutBoodschap($"De Tijd moet liggen tussen {FormatTime(min)} en {FormatTime(max)}...");
						continue;
					}
				}
			}

			break;
		}

		return string.IsNullOrWhiteSpace(input) ? null : inpParsed;

		string FormatTime(TimeOnly? time) => time == null ? "" : $"{time.Value.Hour}:{time.Value.Minute}:{time.Value.Second}";
	}

	public static string FormatTime(this TimeOnly t) => $"{t.Hour}:{t.Minute}:{t.Second}";

	// ---------
	// LeesGetal
	// ---------
	public static T? LeesGetal<T>(string label, T? min, T? max, OptionMode optionMode = OptionMode.Optional) where T : INumber<T>, IMinMaxValue<T>
	{
		// Parameters
		if (min == null) min = T.MinValue;
		if (max == null) max = T.MaxValue;

		if (min > max) throw new ArgumentException("Parameterfout: minimum moet vóór maximum liggen.");

		// RegEx
		var strRegEx = string.Empty;

		switch (Type.GetTypeCode(typeof(T)))
		{
			case TypeCode.Byte:
				strRegEx = null!;
				break;
			case TypeCode.Char:
				strRegEx = null!;
				break;
			case TypeCode.DateTime:
				strRegEx = null!;
				break;
			case TypeCode.Decimal:
				strRegEx = @$"^[+-]?(\d*{CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator})?\d+$";
				break;
			case TypeCode.Double:
				strRegEx = @$"^[+-]?(\d*{CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator})?\d+$";
				break;
			case TypeCode.Int16:
				strRegEx = @"^[+-]?\d+$";
				break;
			case TypeCode.Int32:
				strRegEx = @"^[+-]?\d+$";
				break;
			case TypeCode.Int64:
				strRegEx = @"^[+-]?\d+$";
				break;
			case TypeCode.SByte:
				strRegEx = null!;
				break;
			case TypeCode.Single:
				strRegEx = @$"^[+-]?(\d*{CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator})?\d+$";
				break;
			case TypeCode.UInt16:
				strRegEx = @"^\d+$";
				break;
			case TypeCode.UInt32:
				strRegEx = @"^\d+$";
				break;
			case TypeCode.UInt64:
				strRegEx = @"^\d+$";
				break;
			default:
				strRegEx = @"^$";
				break;
		}

		var regEx = new Regex(strRegEx);

		// Input
		var input = string.Empty;
		var inpParsed = T.Zero;

		while (true)
		{
			input = LeesRegex(label, regEx, optionMode);

			if (!string.IsNullOrWhiteSpace(input))
			{
				if (!T.TryParse(input, null, out inpParsed))
				{
					ToonFoutBoodschap("Ongeldig getal...");
					continue;
				}

				if (inpParsed < min || inpParsed > max)
				{
					ToonFoutBoodschap($"Het getal moet liggen tussen {min} en {max}...");
					continue;
				}
			}

			break;
		}

		return string.IsNullOrWhiteSpace(input) ? default(T) : inpParsed;
	}

	// -------
	// LeesInt
	// -------
	public static int? LeesInt(string label = "", int min = int.MinValue, int max = int.MaxValue, OptionMode optionMode = OptionMode.Optional)
	{
		// Parameters
		if (min > max) throw new ArgumentException("Parameterfout: minimum moet vóór maximum liggen.");

		// Input
		var input = string.Empty;
		int inpParsed = 0;

		while (true)
		{
			input = LeesRegex(label, new Regex(@"^[+-]?\d+$"), optionMode);

			if (!string.IsNullOrWhiteSpace(input))
			{
				if (!int.TryParse(input, out inpParsed))
				{
					ToonFoutBoodschap("Ongeldig getal...");
					continue;
				}

				if (inpParsed < min || inpParsed > max)
				{
					ToonFoutBoodschap($"Het getal moet liggen tussen {min} en {max}...");
					continue;
				}
			}

			break;
		}

		return string.IsNullOrWhiteSpace(input) ? null : inpParsed;
	}

	// -----------
	// LeesDecimal
	// -----------
	public static decimal? LeesDecimal(string label = "", decimal min = decimal.MinValue, decimal max = decimal.MaxValue, OptionMode optionMode = OptionMode.Optional)
	{
		// Parameters
		if (min > max) throw new ArgumentException("Parameterfout: minimum moet vóór maximum liggen.");

		// Input
		var input = string.Empty;
		decimal inpParsed = 0m;

		while (true)
		{
			input = LeesRegex(label, new Regex(@$"^[+-]?(\d*{CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator})?\d+$"), optionMode);

			if (!string.IsNullOrWhiteSpace(input))
			{
				if (!decimal.TryParse(input, out inpParsed))
				{
					ToonFoutBoodschap("Ongeldig getal...");
					continue;
				}

				if (inpParsed < min || inpParsed > max)
				{
					ToonFoutBoodschap($"Het getal moet liggen tussen {min} en {max}...");
					continue;
				}
			}

			break;
		}

		return string.IsNullOrWhiteSpace(input) ? null : inpParsed;
	}

	// ---------
	// LeesFloat
	// ---------
	public static float? LeesFloat(string label = "", float min = float.MinValue, float max = float.MaxValue, OptionMode optionMode = OptionMode.Optional)
	{
		// Parameters
		if (min > max) throw new ArgumentException("Parameterfout: minimum moet vóór maximum liggen.");

		// Input
		var input = string.Empty;
		float inpParsed = 0f;

		while (true)
		{
			input = LeesRegex(label, new Regex(@$"^[+-]?(\d*{CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator})?\d+$"), optionMode);

			if (!string.IsNullOrWhiteSpace(input))
			{
				if (!float.TryParse(input, out inpParsed))
				{
					ToonFoutBoodschap("Ongeldig getal...");
					continue;
				}

				if (inpParsed < min || inpParsed > max)
				{
					ToonFoutBoodschap($"Het getal moet liggen tussen {min} en {max}...");
					continue;
				}
			}

			break;
		}

		return string.IsNullOrWhiteSpace(input) ? null : inpParsed;
	}

	// --------
	// LeesBool
	// --------
	public static bool? LeesBool(string label = "", OptionMode optionMode = OptionMode.Optional)
	{
		var input = string.Empty;

		while (true)
		{
			input = LeesString($"{label + " Y/N"}", 0, 1, optionMode, ReturnNullOrEmpty.Null);

			if (input == null) break;

			input = input.ToUpper();

			if (!(input == "Y" || input == "N"))
			{
				ToonFoutBoodschap("Ongeldige keuze (Y/N)...");
				continue;
			}

			break;
		}

		return input == "Y" ? true : (input == "N" ? false : null);
	}

	// ---------
	// LeesLijst
	// ---------
	public static List<object> LeesLijst(string titel, IEnumerable<object> l, List<string> displayValues, SelectionMode selectionMode = SelectionMode.None, OptionMode optionMode = OptionMode.Optional)
	{
		var lijst = l;

		ToonTitel($"{titel}");

		List<object> gekozenObjecten = new List<object>();

		while (true)
		{
			if (displayValues.Count == 0)
			{
				ToonFoutBoodschap("Lege lijst");
				break;
			}

			string seqKeuzes;
			int intKeuze;
			int seq = 0;

			displayValues.ForEach(i => Console.WriteLine($"{(selectionMode == SelectionMode.None ? string.Empty : string.Format("{0:0000}\t", ++seq))}{i}"));

			// Multiple selection
			if (selectionMode == SelectionMode.Multiple)
			{
				seqKeuzes = LeesString($"Geef de volgnummers uit de lijst {(selectionMode == SelectionMode.Multiple ? " (gescheiden door een comma)" : string.Empty)}", 0, 1000, optionMode)!;

				if (optionMode == OptionMode.Optional && string.IsNullOrWhiteSpace(seqKeuzes)) break;

				string[] keuzes = seqKeuzes.Split(',');

				var okLijst = true;
				gekozenObjecten.Clear();

				// Validate
				foreach (var keuze in keuzes)
				{
					if (int.TryParse(keuze, out intKeuze))
					{
						if (intKeuze > 0 & intKeuze <= seq)
							gekozenObjecten.Add(lijst.ElementAt(intKeuze - 1));
						else
						{
							ToonFoutBoodschap($"Ongeldige keuze.  Keuze tussen 1 en {seq}.  Probeer opnieuw...");
							okLijst = false;
							break;
						}
					}
					else
					{
						ToonFoutBoodschap("De lijst mag enkel cijfers bevatten...");
						okLijst = false;
						break;
					}
				}

				if (okLijst) break;
			}

			// Single Selection
			if (selectionMode == SelectionMode.Single)
			{
				int? leesInt = LeesInt($"Geef het volgnummer uit de lijst", 1, seq, optionMode);

				if (leesInt == null) break; // Only for optional

				gekozenObjecten.Add(lijst.ElementAt((int)leesInt - 1));
				break;
			}

			// No Selection (display only)
			if (selectionMode == SelectionMode.None) break;
		}

		Console.WriteLine();

		return gekozenObjecten;
	}

	// ------------------------
	// LeesKeuzeUitSimpeleLijst
	// ------------------------
	public static object? LeesKeuzeUitLijst(string label, List<object> keuzeLijst, OptionMode optionMode = OptionMode.Optional)
	{
		var input = string.Empty;
		var keuzes = " (" + string.Join(", ", keuzeLijst) + ")";

		while (true)
		{
			input = LeesString(label + keuzes, 0, 1024, optionMode)!;

			if (string.IsNullOrWhiteSpace(input)) break;    // Only for Optional

			if (!keuzeLijst.Contains(input))
			{
				ToonFoutBoodschap("Verkeerde keuze...");
				continue;
			}

			break;
		}

		return string.IsNullOrWhiteSpace(input) ? null : keuzeLijst.ElementAt(keuzeLijst.IndexOf(input));
	}

	// ---------
	// LeesRegEx
	// ---------
	public static string? LeesRegex(string label, Regex regex, OptionMode optionMode = OptionMode.Optional)
	{
		string input = string.Empty;

		while (true)
		{
			input = LeesString(label, 0, 1024, optionMode)!;

			if (string.IsNullOrWhiteSpace(input)) break;    // Only for Optional

			if (!regex.Match(input).Success)
			{
				ToonFoutBoodschap("Verkeerd formaat...");
				continue;
			}

			break;
		}

		return string.IsNullOrWhiteSpace(input) ? null : input;
	}

	// ------------------
	// LeesTelefoonnummer
	// ------------------
	public static string? LeesTelefoonNummer(string label = "Telefoonnummer", OptionMode optionMode = OptionMode.Optional)
		=> LeesRegex(label, new Regex(@"^((\+|00(\s|\s?\-\s?)?)32(\s|\s?\-\s?)?(\(0\)[\-\s]?)?|0)[1-9]((\s|\s?\-\s?)?[0-9])((\s|\s?-\s?)?[0-9])((\s|\s?-\s?)?[0-9])\s?[0-9]\s?[0-9]\s?[0-9]\s?[0-9]\s?[0-9]$"), optionMode);    // 0479 97 60 44

	// --------------
	// LeesEmailAdres
	// --------------
	public static string? LeesEmailAdres(string label = "Emailadres", OptionMode optionMode = OptionMode.Optional)
		=> LeesRegex(label, new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"), optionMode); // j.v@a.com

	// --------------
	// LeesWebsiteUrl
	// --------------
	public static string? LeesWebsiteUrl(string label = "Website", OptionMode optionMode = OptionMode.Optional)
		=> LeesRegex(label, new Regex(@"(?<Protocol>\w+):\/\/(?<Domain>[\w@][\w.:@]+)\/?[\w\.?=%&=\-@/$,]*"), optionMode);  // https://www.hln.be

	// --------
	// LeesEnum
	// --------
	public static List<object> LeesEnum<TEnum>(string label, SelectionMode selectionMode = SelectionMode.Single, OptionMode optionMode = OptionMode.Optional) where TEnum : struct, IConvertible, IComparable, IFormattable
	{
		Type type = typeof(TEnum);

		if (!type.IsEnum) throw new ArgumentException("TEnum moet een enumerated type zijn.");

		var lijst = new List<string>();

		foreach (var item in Enum.GetValues(type)) lijst.Add(Enum.GetName(type, item)!);

		List<object> input = LeesLijst(label, lijst, lijst, selectionMode, optionMode);

		//return string.IsNullOrWhiteSpace(input) ? null : input;
		return input;
	}

	// ------------
	// LeesPaswoord
	// ------------
	public static string? LeesPaswoord(string label = "Paswoord", int minlengte = 8, int maxLengte = 64, OptionMode optionMode = OptionMode.Optional)
	{
		var input = string.Empty;

		while (true)
		{
			input = LeesString(label, 0, maxLengte, optionMode)!;

			if (optionMode == OptionMode.Mandatory && string.IsNullOrWhiteSpace(input))
			{
				ToonFoutBoodschap("Verplichte ingave...");
				continue;
			}

			if (string.IsNullOrWhiteSpace(input)) break;    // Only for Optional

			// Minimum eight characters, one uppercase letter, one lowercase letter, one number and one special character:
			Regex regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&_])[A-Za-z\d@$!%*?&_]{" + minlengte + "," + maxLengte + "}$");

			if (!regex.Match(input).Success)
			{
				ToonFoutBoodschap($"Het paswoord moet minsens 1 kleine letter, 1 hoofdletter, 1 cijfer bevatten en één speciaal teken.\nHet paswoord moet minstens {minlengte} tekens lang zijn.");
				continue;
			}

			break;
		}

		return string.IsNullOrWhiteSpace(input) ? null : input;
	}

	// ==========================================
	// - - - - - - - - O U T P U T - - - - - - - 
	// ==========================================

	// ---------
	// ToonTekst
	// ---------
	public static void ToonTekst(string tekst, ConsoleColor tekstkleur = ConsoleColor.Black)
	{
		if (tekstkleur == ConsoleColor.Black && darkMode) tekstkleur = ConsoleColor.White;

		var color = Console.ForegroundColor;
		Console.ForegroundColor = tekstkleur;
		Console.WriteLine(tekst);
		Console.ForegroundColor = color;
	}

	// ----------------
	// ToonBoodschappen
	// ----------------
	public static void ToonFoutBoodschap(string tekst) => ToonTekst(tekst, ConsoleColor.Red);
	public static void ToonInfoBoodschap(string tekst) => ToonTekst(tekst, ConsoleColor.DarkCyan);
	public static void ToonWarningBoodschap(string tekst) => ToonTekst(tekst, ConsoleColor.DarkYellow);
	public static void ToonSuccessBoodschap(string tekst) => ToonTekst(tekst, ConsoleColor.DarkGreen);

	// ---------
	// ToonTitel
	// ---------
	//public static void ToonTitel(string titel, OptionMode optionMode = OptionMode.Optional) => Console.WriteLine($"\n{titel + (optionMode == OptionMode.Mandatory ? "*" : "")}");
	public static void ToonTitel(string titel, OptionMode optionMode = OptionMode.Optional) => WriteLineWithColor($"\n{titel + (optionMode == OptionMode.Mandatory ? "*" : "")}");

	// ------------------
	// WriteLineWithColor
	// ------------------
	public static void WriteLine(string tekst, ConsoleColor? color = null)
	{
		if (color.HasValue)
		{
			var oldColor = System.Console.ForegroundColor;

			if (color == oldColor) Console.WriteLine(tekst);
			else
			{
				Console.ForegroundColor = color.Value;
				Console.WriteLine(tekst);
				Console.ForegroundColor = oldColor;
			}
		}
		else Console.WriteLine(tekst);
	}

	public static void WriteLine(string tekst, string color)
	{
		if (string.IsNullOrEmpty(color))
		{
			WriteLine(tekst);
			return;
		}

		if (!Enum.TryParse(color, true, out ConsoleColor col)) WriteLine(tekst);
		else WriteLine(tekst, col);
	}

	public static void Write(string tekst, ConsoleColor? color = null)
	{
		if (color.HasValue)
		{
			var oldColor = System.Console.ForegroundColor;

			if (color == oldColor) Console.Write(tekst);
			else
			{
				Console.ForegroundColor = color.Value;
				Console.Write(tekst);
				Console.ForegroundColor = oldColor;
			}
		}
		else Console.Write(tekst);
	}

	public static void Write(string tekst, string color)
	{
		if (string.IsNullOrEmpty(color))
		{
			Write(tekst);
			return;
		}

		if (!ConsoleColor.TryParse(color, true, out ConsoleColor col)) Write(tekst);
		else Write(tekst, col);
	}

	private static Lazy<Regex> colorBlockRegEx = new Lazy<Regex>(() => new Regex("\\[(?<color>.*?)\\](?<text>[^[]*)\\[/\\k<color>\\]", RegexOptions.IgnoreCase), isThreadSafe: true);

	public static void WriteLineWithColor(string text, ConsoleColor? baseTextColor = null)
	{
		// Default color
		if (baseTextColor == null) baseTextColor = Console.ForegroundColor;

		// Nothing to write : \n
		if (string.IsNullOrEmpty(text))
		{
			WriteLine(string.Empty);    // Console.WriteLine();
			return;
		}

		// Check if any color in line
		int at1 = text.IndexOf("[");
		int at2 = text.IndexOf("]");

		if (at1 == -1 || at2 <= at1)
		{
			WriteLine(text, baseTextColor);
			return;
		}

		// For all color parts
		while (true)
		{
			var match = colorBlockRegEx.Value.Match(text);

			if (match.Length < 1)
			{
				Write(text, baseTextColor);
				break;
			}

			// write up to expression
			Write(text.Substring(0, match.Index), baseTextColor);

			// strip out the expression
			string highlightText = match.Groups["text"].Value;
			string colorVal = match.Groups["color"].Value;

			Write(highlightText, colorVal);

			// remainder of string
			text = text.Substring(match.Index + match.Value.Length);
		}

		Console.WriteLine();
	}
}
