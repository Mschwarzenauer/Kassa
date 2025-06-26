/*--------------------------------------------------------------
 * Description: Kassaprogramm
 *--------------------------------------------------------------
*/

using System;
using System.Text;
using System.IO;

Console.OutputEncoding = Encoding.UTF8;
Console.ForegroundColor = ConsoleColor.DarkGray;
Console.CursorVisible = true;


int[] Lagerstand = new int[1000]; 
double[] Preise = new double[1000]; 
string[] Produktname = new string[1000]; 

string[] save = new string[1000];

int Produktnummer; 
double Ermäßigungen; 
double Summe = 0; 
double Verdienst = 0; 

string weiterhinGeeoffnet;
int Auffüllobjekte = 1; 
int KomplettSchließen = 1;
double cash = 0;

FrageCode();

async void FrageCode()
{
    int Passwort = 2001;
    int Wiederholungen = 0;



    Console.Write("Geben Sie den Code ein: ");
    int Versuch = int.Parse(Console.ReadLine());

    if (Versuch == Passwort)
    {

        LadeDaten();

        void AskForSaving()
        {
            Console.Write("Möchtest du die aktuellen Daten speichern? (j/n): ");
            string antwort = Console.ReadLine().ToLower();

            if (antwort == "j")
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


            if (File.Exists("saved.csv"))
            {
                string[] lines = File.ReadAllLines("saved.csv");
                string[] data;

                try
                {
                    for (int i = 0; i < lines.Length - 1; i++)
                    {
                        data = lines[i].Split(';');

                        Produktname[i] = data[0];
                        Lagerstand[i] = int.Parse(data[1]);
                        Preise[i] = double.Parse(data[2]);
                    }

                    cash = double.Parse(lines[lines.Length - 1]);
                    Console.WriteLine("daten wurden geladen");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Gespeicherte Daten wurden aufgrund des Fehlers {e.Message} nicht geladen.");
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

            Produktname[1] = "großes Buch";
            Lagerstand[1] = 50;
            Preise[1] = 5.00;

            Produktname[2] = "kleines Buch";
            Lagerstand[2] = 50;
            Preise[2] = 2.50;

            Produktname[3] = "Pixibuch";
            Lagerstand[3] = 50;
            Preise[3] = 1.50;

            Produktname[4] = "Stofftier Groß";
            Lagerstand[4] = 50;
            Preise[4] = 15.00;

            Produktname[5] = "Stofftier Mittel";
            Lagerstand[5] = 50;
            Preise[5] = 10.00;

            Produktname[6] = "Stofftier Klein";
            Lagerstand[6] = 50;
            Preise[6] = 5.00;

            Produktname[7] = "Chips 250g";
            Lagerstand[7] = 50;
            Preise[7] = 1.99;

            Produktname[8] = "Chips 500g";
            Lagerstand[8] = 50;
            Preise[8] = 2.50;

            Produktname[9] = "Chips 750g";
            Lagerstand[9] = 50;
            Preise[9] = 3.99;

            Produktname[10] = "Chips 1kg";
            Lagerstand[10] = 50;
            Preise[10] = 5.00;

            Produktname[11] = "Mehl 1kg";
            Lagerstand[11] = 50;
            Preise[11] = 2.39;

            Produktname[12] = "Mehl Paket";
            Lagerstand[12] = 50;
            Preise[12] = 7.50;

            Produktname[13] = "Mineral 750ml (6er Packung)";
            Lagerstand[13] = 30;
            Preise[13] = 9.99;

            Produktname[14] = "Mineral 750ml (einzeln)";
            Lagerstand[14] = 50;
            Preise[14] = 2.12;

            Produktname[15] = "Mineral 1l (6er Packung)";
            Lagerstand[15] = 30;
            Preise[15] = 11.33;

            Produktname[16] = "Mineral 1l (einzeln)";
            Lagerstand[16] = 50;
            Preise[16] = 3.45;

            Produktname[17] = "RedBull (6er Packung)";
            Lagerstand[17] = 50;
            Preise[17] = 5.00;

            Produktname[18] = "RedBull (einzeln)";
            Lagerstand[18] = 50;
            Preise[18] = 0.46;

            Produktname[19] = "Shampoo Loreal Paris";
            Lagerstand[19] = 50;
            Preise[19] = 3.44;

            Produktname[20] = "Shampoo Nivea Men";
            Lagerstand[20] = 50;
            Preise[20] = 2.89;

            Produktname[21] = "Shampoo Kerastase";
            Lagerstand[21] = 50;
            Preise[21] = 3.79;

            Produktname[22] = "Shampoo New Lenghts";
            Lagerstand[22] = 50;
            Preise[22] = 3.47;

            Produktname[23] = "Tierfutter 1kg";
            Lagerstand[23] = 50;
            Preise[23] = 11.00;

            Produktname[24] = "Kissen";
            Lagerstand[24] = 50;
            Preise[24] = 5.66;

            Console.WriteLine("Standarddaten geladen.");
        }




        void SpeichereDaten()
        {
            try
            {
                string[] data = new string[Produktname.Length];
                string[] lines = new string[Produktname.Length];

                data[0] = "ProduktName;Lagerstand;Preise";

                for (int i = 0; i < Produktname.Length; i++)
                {
                    lines[0] = Produktname[i];
                    lines[1] = Convert.ToString(Lagerstand[i]);
                    lines[2] = Convert.ToString(Preise[i]);

                    string combined = lines[0] + ";" + lines[1] + ";" + lines[2];
                    data[i + 1] = combined;
                }

                data[data.Length - 1] = Convert.ToString(cash);
                File.WriteAllLines("saved.csv", data);

                Console.WriteLine($"Daten wurden erfolgreich in '' gespeichert.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler beim Speichern der Daten: {ex.Message}");
            }
        }


        while (KomplettSchließen != 0)
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
                        if (Lagerstand[Produktnummer] != 0)
                        {
                            Console.WriteLine($"Produktname: {Produktname[Produktnummer]}");
                            Console.WriteLine($"Produktpreis: {Preise[Produktnummer]:f2} Euro");
                            Summe = Summe + Preise[Produktnummer];
                            Console.WriteLine($"{Summe:f2} Euro");
                            Lagerstand[Produktnummer] = Lagerstand[Produktnummer] - 1;
                            if (Lagerstand[Produktnummer] <= 10)
                            {
                                Console.WriteLine("Vorsicht Lagerstand niedrig!!!!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Nicht möglich dieses Produkt ist nicht mehr im Lager oder existiert nicht!!!!");
                        }
                    }
                    else
                    {
                        Console.Write("Ermäßigung:");
                        Ermäßigungen = double.Parse(Console.ReadLine());

                        Ermäßigungen = 1 - (Ermäßigungen / 100);

                        Summe = Summe * Ermäßigungen;
                        Verdienst = Verdienst + Summe;

                        Console.WriteLine($"Zu zahlender Betrag: {Summe:f2} Euro");
                        Summe = 0;
                    }
                }
                Console.WriteLine(" ");
                Console.Write("Schließen Sie jetzt? (j(Ja)/n(Nein)):");
                weiterhinGeeoffnet = Console.ReadLine();
            }

            Console.WriteLine(" ");
            Console.WriteLine($"Verdienst in Brutto: {Verdienst:f2}");
            Verdienst = Verdienst / 1.20;
            Console.WriteLine($"-----------------------------");
            Console.WriteLine($"Verdienst in Netto: {Verdienst:f2}");
            cash = cash + Verdienst;


            Console.WriteLine("Lagerstand auffüllen:");

            {
                File.WriteAllLines("SavedData.csv", save);
            }

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
                    Console.WriteLine("An dieser Stelle existiert bereits ein Produkt. Bitte wähle eine andere Nummer.");
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
                    Console.WriteLine($"Nicht genug Guthaben. Du brauchst {einkaufswert:F2} €, aber hast nur {cash:F2} €.");
                    return;
                }


                Produktname[nummer] = name;
                Preise[nummer] = preis;
                Lagerstand[nummer] = bestand;
                cash -= einkaufswert;

                Console.WriteLine($"✔ Produkt '{name}' wurde erfolgreich angelegt!");
                Console.WriteLine($"Du hast {bestand} Stück zu je {preis:F2} € verkauftem Preis eingelagert.");
                Console.WriteLine($"Einkaufskosten (40 %): {einkaufswert:F2} €. Neues Guthaben: {cash:F2} €");
            }



            AskForSaving();

            Console.WriteLine($"Sie haben aktuell {cash}€ am Bankkonto");

            Console.Write("Willst du ein neues Produkt hinzufügen? (j/n): ");
            string antwort = Console.ReadLine().ToLower();

            if (antwort == "j")
            {
                ProduktHinzufügen();
            }


            void AuffüllenMitKosten()
            {
                const double einkaufsfaktor = 0.40;

                while (true)
                {
                    Console.Write("Produktnummer zum Auffüllen (0 zum Beenden): ");
                    if (!int.TryParse(Console.ReadLine(), out int nummer) || nummer == 0) break;

                    if (string.IsNullOrEmpty(Produktname[nummer]))
                    {
                        Console.WriteLine("Dieses Produkt existiert nicht.");
                        continue;
                    }

                    Console.Write($"Wie viele Stück von '{Produktname[nummer]}' sollen aufgefüllt werden?: ");
                    if (!int.TryParse(Console.ReadLine(), out int menge) || menge <= 0)
                    {
                        Console.WriteLine("Ungültige Menge.");
                        continue;
                    }

                    double einkaufspreis = Preise[nummer] * einkaufsfaktor;
                    double kosten = einkaufspreis * menge;

                    if (kosten > cash)
                    {
                        Console.WriteLine($"Nicht genug Guthaben! Zum Auffüllen brauchst du {kosten:F2} €, aber du hast nur {cash:F2} €.");
                        continue;
                    }

                    Lagerstand[nummer] += menge;
                    cash -= kosten;

                    Console.WriteLine($"{menge} Stück von '{Produktname[nummer]}' wurden aufgefüllt.");
                    Console.WriteLine($"Kosten: {kosten:F2} € ({einkaufsfaktor * 100:F0}% von {Preise[nummer]:F2} € pro Stück). Neues Guthaben: {cash:F2} €");
                }
            }




            Console.Write("Drücken Sie die Eingabetaste:");
            Console.ReadLine();
            Console.Clear();
        }
        Console.CursorVisible = true;
    }
    else
    {

        for(int i = 0; i < 10; i++)
        {
            Console.Beep();
            await Task.Delay(1000);
        } 
    }
}