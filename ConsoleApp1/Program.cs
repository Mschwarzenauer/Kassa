/*--------------------------------------------------------------
 * Description: Kassaprogramm
 *--------------------------------------------------------------
*/

using System;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Spectre.Console;

Console.OutputEncoding = Encoding.UTF8;
Console.ForegroundColor = ConsoleColor.DarkGray;
Console.CursorVisible = false;

int[] Lagerstand = new int[1000];
double[] Preise = new double[1000];
string[] Produktname = new string[1000];

int Produktnummer;
double Ermäßigungen;
double Summe = 0;
double Verdienst = 0;

string weiterhinGeeoffnet;
string KomplettSchließen = "n";
double cash = 0;

var code = 1;


AnsiConsole.Write(
    new FigletText("Projekt - Kassa")
        .LeftJustified()
        .Color(ConsoleColor.Gray));


int Passwort = 2001;
do
{
    code = AnsiConsole.Prompt(
    new TextPrompt<int>("bitte code eingeben: ")
        .Secret());

    if (code == Passwort)
    {
        LadeDaten();

        void AskForSaving()
        {
            // Ask the user to confirm
            var confirmation = AnsiConsole.Prompt(
                new ConfirmationPrompt("Möchtest du die aktuellen Daten speichern?"));


            Console.Write(": ");
            string antwort = Console.ReadLine().ToLower();

            if (confirmation)
            {
                SpeichereDaten();
                Console.WriteLine("Daten wurden erfolgreich gespeichert.");
            }
            else
            {
                Console.WriteLine("Daten wurden nicht gespeichert.");
            }
        }

        void LadeDaten()
        {
            string pfad = "saved.csv";

            if (File.Exists(pfad))
            {
                string[] lines = File.ReadAllLines(pfad);
                string[] data;

                try
                {
                    for (int i = 1; i < lines.Length - 1; i++) // Kopfzeile überspringen
                    {
                        data = lines[i].Split(';');

                        Produktname[i] = data[0];
                        Lagerstand[i] = int.Parse(data[1]);
                        Preise[i] = double.Parse(data[2]);
                    }

                    if (double.TryParse(lines[1], out double gespeicherterCash))
                    {
                        cash = gespeicherterCash;
                    }
                    else
                    {
                        Console.WriteLine("⚠️ Konnte den Kontostand nicht aus der letzten Zeile lesen.");
                        cash = 0;
                    }

                    Console.WriteLine("Daten wurden geladen.");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Gespeicherte Daten konnten nicht geladen werden: {e.Message}");
                    LadeStandarddaten();
                }
            }
            else
            {
                Console.WriteLine("Keine gespeicherten Daten gefunden. Standarddaten werden verwendet.");
                LadeStandarddaten();
            }
        }

        void LadeStandarddaten()
        {
            cash = 0;

            void SetzeProdukt(int nr, string name, double preis, int bestand)
            {
                Produktname[nr] = name;
                Preise[nr] = preis;
                Lagerstand[nr] = bestand;
            }

            SetzeProdukt(1, "großes Buch", 5.00, 50);
            SetzeProdukt(2, "kleines Buch", 2.50, 50);
            SetzeProdukt(3, "Pixibuch", 1.50, 50);
            SetzeProdukt(4, "Stofftier Groß", 15.00, 50);
            SetzeProdukt(5, "Stofftier Mittel", 10.00, 50);
            SetzeProdukt(6, "Stofftier Klein", 5.00, 50);
            SetzeProdukt(7, "Chips 250g", 1.99, 50);
            SetzeProdukt(8, "Chips 500g", 2.50, 50);
            SetzeProdukt(9, "Chips 750g", 3.99, 50);
            SetzeProdukt(10, "Chips 1kg", 5.00, 50);
            SetzeProdukt(11, "Mehl 1kg", 2.39, 50);
            SetzeProdukt(12, "Mehl Paket", 7.50, 50);
            SetzeProdukt(13, "Mineral 750ml (6er Packung)", 9.99, 30);
            SetzeProdukt(14, "Mineral 750ml (einzeln)", 2.12, 50);
            SetzeProdukt(15, "Mineral 1l (6er Packung)", 11.33, 30);
            SetzeProdukt(16, "Mineral 1l (einzeln)", 3.45, 50);
            SetzeProdukt(17, "RedBull (6er Packung)", 5.00, 50);
            SetzeProdukt(18, "RedBull (einzeln)", 0.46, 50);
            SetzeProdukt(19, "Shampoo Loreal Paris", 3.44, 50);
            SetzeProdukt(20, "Shampoo Nivea Men", 2.89, 50);
            SetzeProdukt(21, "Shampoo Kerastase", 3.79, 50);
            SetzeProdukt(22, "Shampoo New Lenghts", 3.47, 50);
            SetzeProdukt(23, "Tierfutter 1kg", 11.00, 50);
            SetzeProdukt(24, "Kissen", 5.66, 50);

            Console.WriteLine("Standarddaten geladen.");
        }

        void SpeichereDaten()
        {
            string pfad = "saved.csv";
            try
            {
                if (File.Exists(pfad))
                {
                    File.Delete(pfad);
                }
                using StreamWriter writer = new StreamWriter(pfad, false, Encoding.UTF8);
                writer.WriteLine("ProduktName;Lagerstand;Preise");

                for (int i = 1; i < Produktname.Length; i++)
                {
                    if (!string.IsNullOrWhiteSpace(Produktname[i]))
                    {
                        writer.WriteLine($"{Produktname[i]};{Lagerstand[i]};{Preise[i]}");
                    }
                }

                writer.WriteLine(cash.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler beim Speichern der Daten: {ex.Message}");
            }
        }

        while (KomplettSchließen != "j")
        {
            Verdienst = 0.00;
            Console.Write("Produktnummer: ");
            Produktnummer = int.Parse(Console.ReadLine());
            Console.WriteLine(" ");

            weiterhinGeeoffnet = "Ja";

            while (Produktnummer != 0)
            {
                Console.WriteLine($"Produktname: {Produktname[Produktnummer]}");
                Console.WriteLine($"Produktpreis: {Preise[Produktnummer]:f2} Euro");
                Console.WriteLine($"Lagerstand: {Lagerstand[Produktnummer]} Stück");
                Console.WriteLine($" ");
                Console.Write("Produktnummer: ");
                Produktnummer = int.Parse(Console.ReadLine());
            }

            while (weiterhinGeeoffnet != "j")
            {
                Produktnummer = 1929;
                Console.WriteLine($"Kassaerfassung: ");
                while (Produktnummer != 0)
                {
                    Console.Write("Produktnummer: ");
                    Produktnummer = int.Parse(Console.ReadLine());
                    if (Produktnummer != 0)
                    {
                        if (Lagerstand[Produktnummer] > 0)
                        {
                            Console.WriteLine($"Produktname: {Produktname[Produktnummer]}");
                            Console.WriteLine($"Produktpreis: {Preise[Produktnummer]:f2} Euro");
                            Summe += Preise[Produktnummer];
                            Console.WriteLine($"{Summe:f2} Euro");
                            Lagerstand[Produktnummer]--;

                            if (Lagerstand[Produktnummer] <= 10)
                                Console.WriteLine("Vorsicht Lagerstand niedrig!!!!");
                        }
                        else
                        {
                            Console.WriteLine("Produkt ist nicht mehr im Lager oder existiert nicht!");
                        }
                    }
                    else
                    {
                        Console.Write("Ermäßigung (%): ");
                        Ermäßigungen = double.Parse(Console.ReadLine());
                        Ermäßigungen = 1 - (Ermäßigungen / 100);
                        Summe *= Ermäßigungen;
                        Verdienst += Summe;

                        Console.WriteLine($"Zu zahlender Betrag: {Summe:f2} Euro");
                        Summe = 0;
                    }
                }

                Console.Write("Schließen Sie jetzt? (j/n): ");
                weiterhinGeeoffnet = Console.ReadLine().ToLower();
            }

            Console.WriteLine();
            Console.WriteLine($"Verdienst Brutto: {Verdienst:f2} Euro");
            Verdienst /= 1.20;
            Console.WriteLine($"Verdienst Netto: {Verdienst:f2} Euro");

            cash += Verdienst;

            AuffüllenMitKosten();

            void ProduktHinzufügen()
            {
                Console.Write("Gib die Produktnummer ein (zwischen 1 und 999): ");
                int nummer = int.Parse(Console.ReadLine());

                if (nummer < 1 || nummer >= Produktname.Length)
                {
                    Console.WriteLine("Ungültige Produktnummer.");
                    return;
                }

                if (!string.IsNullOrEmpty(Produktname[nummer]))
                {
                    Console.WriteLine("An dieser Stelle existiert bereits ein Produkt.");
                    return;
                }

                Console.Write("Gib den Produktnamen ein: ");
                string name = Console.ReadLine();

                Console.Write("Gib den Verkaufspreis ein (z. B. 4.99): ");
                if (!double.TryParse(Console.ReadLine().Replace(',', '.'), System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out double preis) || preis <= 0)
                {
                    Console.WriteLine("Ungültiger Preis.");
                    return;
                }

                Console.Write("Wie viele Stück sollen eingelagert werden?: ");
                if (!int.TryParse(Console.ReadLine(), out int bestand) || bestand <= 0)
                {
                    Console.WriteLine("Ungültige Menge.");
                    return;
                }

                double einkaufswert = preis * 0.40 * bestand;

                if (einkaufswert > cash)
                {
                    Console.WriteLine($"Nicht genug Guthaben: benötigt {einkaufswert:F2} €, vorhanden {cash:F2} €.");
                    return;
                }

                Produktname[nummer] = name;
                Preise[nummer] = preis;
                Lagerstand[nummer] = bestand;
                cash -= einkaufswert;

                Console.WriteLine($"✔ Produkt '{name}' wurde erfolgreich angelegt!");
            }

            AskForSaving();

            Console.WriteLine($"Aktuelles Guthaben: {cash:F2} €");

            Console.Write("Willst du ein neues Produkt hinzufügen? (j/n): ");
            if (Console.ReadLine().ToLower() == "j")
            {
                ProduktHinzufügen();
            }

            void AuffüllenMitKosten()
            {
                const double einkaufsfaktor = 0.40;

                while (true)
                {
                    Console.Write("Produktnummer zum Auffüllen (0 = Ende): ");
                    if (!int.TryParse(Console.ReadLine(), out int nummer) || nummer == 0)
                        break;

                    if (string.IsNullOrEmpty(Produktname[nummer]))
                    {
                        Console.WriteLine("Dieses Produkt existiert nicht.");
                        continue;
                    }

                    Console.Write($"Wie viele Stück von '{Produktname[nummer]}'?: ");
                    if (!int.TryParse(Console.ReadLine(), out int menge) || menge <= 0)
                    {
                        Console.WriteLine("Ungültige Menge.");
                        continue;
                    }

                    double kosten = Preise[nummer] * einkaufsfaktor * menge;

                    if (kosten > cash)
                    {
                        Console.WriteLine($"Nicht genug Guthaben: benötigt {kosten:F2} €, vorhanden {cash:F2} €.");
                        continue;
                    }

                    Lagerstand[nummer] += menge;
                    cash -= kosten;

                    Console.WriteLine($"{menge} Stück von '{Produktname[nummer]}' wurden aufgefüllt. Neues Guthaben: {cash:F2} €");
                }
            }

            Console.Write("Drücken Sie Enter zum Fortfahren...");
            Console.ReadLine();
            Console.Clear();

            Console.Write("Wollen Sie den Tag beenden?");
            KomplettSchließen = Console.ReadLine();
        }
    }
    else
    {
        Console.WriteLine("Zugang verweigert.");
    }
} while (code != Passwort);