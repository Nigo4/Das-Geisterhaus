using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        RaumMitNiedrigerEnergie Tor = new RaumMitNiedrigerEnergie("Du stehst vor einem großen, rostigen Tor. Es scheint alt und verlassen.");
        RaumMitNiedrigerEnergie Weg = new RaumMitNiedrigerEnergie("Ein schmaler, gepflasterter Weg führt zum Haupteingang des Gebäudes.");
        RaumMitNiedrigerEnergie Haupteingang = new RaumMitNiedrigerEnergie("Der Haupteingang des Gebäudes. Die Tür steht offen.");
        RaumMitNiedrigerEnergie Flur = new RaumMitNiedrigerEnergie("Ein langer, dunkler Flur. Es ist still.");
        RaumMitNiedrigerEnergie RaumU1 = new RaumMitNiedrigerEnergie("Ein kleiner, unmarkierter Raum. Es riecht muffig.");
        RaumMitNiedrigerEnergie RaumU2 = new RaumMitNiedrigerEnergie("Ein weiterer kleiner Raum. Die Wände sind mit Schimmel bedeckt.");
        RaumMitNiedrigerEnergie Treppe = new RaumMitNiedrigerEnergie("Eine alte, knarrende Holztreppe führt nach oben.");
        RaumMitHoherEnergie ErsterStock = new RaumMitHoherEnergie("Du stehst im ersten Stock. Es ist ruhig hier oben.");
        RaumMitHoherEnergie Raum1 = new RaumMitHoherEnergie("Ein leeres Zimmer. Der Staub hängt in der Luft.");
        RaumMitHoherEnergie Raum2 = new RaumMitHoherEnergie("Ein Raum mit einem kaputten Fenster. Der Wind pfeift durch.");
        RaumMitHoherEnergie ErsterStockWeiter = new RaumMitHoherEnergie("Der Flur im ersten Stock geht weiter. Es ist dunkel am Ende.");
        RaumMitHoherEnergie Raum3 = new RaumMitHoherEnergie("Ein dunkles Zimmer. Es ist kalt.");
        RaumMitHoherEnergie Raum4 = new RaumMitHoherEnergie("Ein helles Zimmer. Die Sonne scheint durch ein Loch im Dach.");
        RaumMitHoherEnergie Treppe2 = new RaumMitHoherEnergie("Eine weitere Treppe. Sie führt zum Dachboden.");
        RaumMitHoherEnergie Dachboden = new RaumMitHoherEnergie("Der Dachboden. Es riecht nach altem Holz.");

        Item Arznei = new Pickup("Du hast Arznei gefunden!", 10);
        Item Schutz = new Pickup("Du hast Schutz gefunden!", 10);
        Item Batterie = new Pickup("Du hast eine Batterie gefunden!", 10);
        Item Smaragd = new Edelstein("Du hast den Smaragd gefunden!", 10);
        Item Rubin = new Edelstein("Du hast den Rubin gefunden!", 10);
        Item Diamant = new Edelstein("Du hast den Diamant gefunden!", 10);
        Item LeereDose = new WertloserGegenstand("Du hast eine leere Dose gefunden!", 0);
        Item RostigesBesteck = new WertloserGegenstand("Du hast rostiges Besteck gefunden!", 0);
        Item VerstaubtesBuch = new WertloserGegenstand("Du hast ein verstaubtes Buch gefunden!", 0);

        RaumU1.AddItem(Schutz);
        RaumU1.AddItem(Batterie);

        Geist geist1 = new Geist("Geist 1", Smaragd);
        Geist geist2 = new Geist("Geist 2", Rubin);
        Geist geist3 = new Geist("Geist 3", Diamant);

        Raum1.SetGeist(geist1);
        Raum2.SetGeist(geist2);
        Raum3.SetGeist(geist3);

        // Verknüpfen der Räume
        Tor.SetVor(Weg);
        Weg.SetVor(Haupteingang);
        Haupteingang.SetZurueck(Weg);
        Haupteingang.SetVor(Flur);
        Flur.SetZurueck(Haupteingang);
        Flur.SetLinks(RaumU1);
        RaumU1.SetRechts(Flur);
        Flur.SetRechts(RaumU2);
        RaumU2.SetLinks(Flur);
        Flur.SetVor(Treppe);
        Treppe.SetZurueck(Flur);
        Treppe.SetVor(ErsterStock);
        ErsterStock.SetZurueck(Treppe);
        ErsterStock.SetLinks(Raum1);
        Raum1.SetRechts(ErsterStock);
        ErsterStock.SetRechts(Raum2);
        Raum2.SetLinks(ErsterStock);
        ErsterStock.SetVor(ErsterStockWeiter);
        ErsterStockWeiter.SetZurueck(ErsterStock);
        ErsterStockWeiter.SetLinks(Raum3);
        Raum3.SetRechts(ErsterStockWeiter);
        ErsterStockWeiter.SetRechts(Raum4);
        Raum4.SetLinks(ErsterStockWeiter);
        ErsterStockWeiter.SetVor(Treppe2);
        Treppe2.SetZurueck(ErsterStockWeiter);
        Treppe2.SetVor(Dachboden);

        //Setup Player
        Spieler player1 = new Spieler("Geisterjäger", Tor);

        //GameLoop
        while (true)
        {
            Raum aktuellePosition = player1.GetAktuellePosition();
            aktuellePosition.Betreten(player1);

            if (aktuellePosition is RaumMitHoherEnergie)
            {
                RaumMitHoherEnergie hoherEnergieRaum = aktuellePosition as RaumMitHoherEnergie;
                Geist geist = hoherEnergieRaum.GetGeist();
                if (geist != null)
                {
                    player1.ReduziereStats(25, 25);
                    Console.WriteLine("Ein Geist ist hier! Du hast 25 Leben und 25 Schutz verloren.");
                }
            }

            if (player1.GetAktuellePosition() == Dachboden)
            {
                Console.WriteLine("Das Spiel ist vorbei!");
                break;
            }

            Console.WriteLine("Wohin möchtest du gehen? (vorne, links, rechts)");
            string richtung = Console.ReadLine().ToLower();

            switch (richtung)
            {
                case "vorne":
                    if (player1.GetAktuellePosition().GetVor() != null)
                    {
                        player1.SetAktuellePosition(player1.GetAktuellePosition().GetVor());
                    }
                    else
                    {
                        Console.WriteLine("Da ist kein Weg vor dir!");
                    }
                    break;
                case "links":
                    if (player1.GetAktuellePosition().GetLinks() != null)
                    {
                        player1.SetAktuellePosition(player1.GetAktuellePosition().GetLinks());
                    }
                    else
                    {
                        Console.WriteLine("Da ist kein Weg links von dir. Bitte versuche es erneut.");
                    }
                    break;
                case "rechts":
                    if (player1.GetAktuellePosition().GetRechts() != null)
                    {
                        player1.SetAktuellePosition(player1.GetAktuellePosition().GetRechts());
                    }
                    else
                    {
                        Console.WriteLine("Da ist kein Weg rechts von dir. Bitte versuche es erneut.");
                    }
                    break;
                case "inventar":
                    player1.ZeigeInventar();
                    break;
                case "angriff":
                    if (aktuellePosition is RaumMitHoherEnergie)
                    {
                        RaumMitHoherEnergie hoherEnergieRaum = aktuellePosition as RaumMitHoherEnergie;
                        Geist geist = hoherEnergieRaum.GetGeist();
                        if (geist != null)
                        {
                            player1.Angreifen(geist);
                        }
                        else
                        {
                            Console.WriteLine("Kein Geist hier zum Angreifen.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Es gibt keine Geister in Räumen mit niedriger Energie.");
                    }
                    break;
                case "stats":
                    player1.ZeigeStats();
                    break;
                default:
                    Console.WriteLine("Ungültige Richtung. Bitte versuche es erneut.");
                    break;
            }
        }
    }
}

class Raum
{
    private string beschreibung;
    private Raum vor;
    private Raum zurueck;
    private Raum links;
    private Raum rechts;
    private List<Item> items;

    public Raum(string beschreibung)
    {
        this.beschreibung = beschreibung;
        items = new List<Item>();
    }

    public string GetBeschreibung()
    {
        return beschreibung;
    }

    public Raum GetVor()
    {
        return vor;
    }

    public void SetVor(Raum raum)
    {
        vor = raum;
    }

    public Raum GetZurueck()
    {
        return zurueck;
    }

    public void SetZurueck(Raum raum)
    {
        zurueck = raum;
    }

    public Raum GetLinks()
    {
        return links;
    }

    public void SetLinks(Raum raum)
    {
        links = raum;
    }

    public Raum GetRechts()
    {
        return rechts;
    }

    public void SetRechts(Raum raum)
    {
        rechts = raum;
    }

    public void AddItem(Item item)
    {
        items.Add(item);
    }

    public void RemoveItem(Item item)
    {
        items.Remove(item);
    }

    public List<Item> GetItems()
    {
        return items;
    }

    public virtual void Betreten(Spieler spieler)
    {
        Console.WriteLine(beschreibung);
        if (items.Count > 0)
        {
            Console.WriteLine("Du siehst folgende Gegenstände:");
            foreach (Item item in items)
            {
                Console.WriteLine(item.GetBeschreibung());
                spieler.Aufheben(item);
            }
        }
    }
}

class Spieler
{
    private string name;
    private Raum aktuellePosition;
    private List<Item> inventar;
    private int lebensanzeige;
    private int schutzanzeige;
    private int batterieladungen;

    public Spieler(string name, Raum startposition)
    {
        this.name = name;
        aktuellePosition = startposition;
        inventar = new List<Item>();
        lebensanzeige = 100;
        schutzanzeige = 0;
        batterieladungen = 100;
    }

    public Raum GetAktuellePosition()
    {
        return aktuellePosition;
    }

    public void SetAktuellePosition(Raum raum)
    {
        aktuellePosition = raum;
    }

    public void Aufheben(Item item)
    {
        inventar.Add(item);
        aktuellePosition.RemoveItem(item);
        Console.WriteLine($"Du hast {item.GetBeschreibung()} aufgenommen.");
    }

    public void ZeigeInventar()
    {
        if (inventar.Count > 0)
        {
            Console.WriteLine("Inventar:");
            foreach (Item item in inventar)
            {
                Console.WriteLine(item.GetBeschreibung());
            }
        }
        else
        {
            Console.WriteLine("Dein Inventar ist leer.");
        }
    }

    public void ReduziereStats(int leben, int schutz)
    {
        lebensanzeige -= leben;
        schutzanzeige -= schutz;
    }

    public void Angreifen(Geist geist)
    {
        if (batterieladungen >= 2)
        {
            batterieladungen -= 2;
            Item edelstein = geist.DropEdelstein();
            inventar.Add(edelstein);
            Console.WriteLine($"Du hast den Geist angegriffen und einen {edelstein.GetBeschreibung()} erhalten!");
        }
        else
        {
            Console.WriteLine("Du hast nicht genug Batterieladungen, um anzugreifen.");
        }
    }

    public void ZeigeStats()
    {
        Console.WriteLine($"Lebensanzeige: {lebensanzeige}");
        Console.WriteLine($"Schutzanzeige: {schutzanzeige}");
        Console.WriteLine($"Batterieladungen: {batterieladungen}");
    }
}

class Item
{
    protected string beschreibung;

    public Item(string beschreibung)
    {
        this.beschreibung = beschreibung;
    }

    public string GetBeschreibung()
    {
        return beschreibung;
    }
}

class Pickup : Item
{
    private int wert;

    public Pickup(string beschreibung, int wert) : base(beschreibung)
    {
        this.wert = wert;
    }

    public int GetWert()
    {
        return wert;
    }
}

class Gegenstand : Item
{
    public Gegenstand(string beschreibung) : base(beschreibung)
    {
    }
}

class WertloserGegenstand : Gegenstand
{
    private int wert;

    public WertloserGegenstand(string beschreibung, int wert) : base(beschreibung)
    {
        this.wert = wert;
    }

    public int GetWert()
    {
        return wert;
    }
}

class Edelstein : Gegenstand
{
    private int wert;

    public Edelstein(string beschreibung, int wert) : base(beschreibung)
    {
        this.wert = wert;
    }

    public int GetWert()
    {
        return wert;
    }
}


class Geist
{
    private string name;
    private Item edelstein;

    public Geist(string name, Item edelstein)
    {
        this.name = name;
        this.edelstein = edelstein;
    }

    public Item DropEdelstein()
    {
        Console.WriteLine($"Der {name} wurde besiegt und hat einen {edelstein.GetBeschreibung()} fallen gelassen!");
        return edelstein;
    }
}

class RaumMitHoherEnergie : Raum
{
    private Geist geist;

    public RaumMitHoherEnergie(string beschreibung) : base(beschreibung) { }

    public void SetGeist(Geist geist)
    {
        this.geist = geist;
    }

    public Geist GetGeist()
    {
        return geist;
    }
}

class RaumMitNiedrigerEnergie : Raum
{
    public RaumMitNiedrigerEnergie(string beschreibung) : base(beschreibung) { }
}
