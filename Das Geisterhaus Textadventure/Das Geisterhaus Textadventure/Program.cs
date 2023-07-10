using System;
using System.Collections.Generic;

class DachbodenRaum : RaumMitHoherEnergie
{
    public DachbodenRaum(string beschreibung) : base(beschreibung) { }

    public override void Betreten(Spieler spieler)
    {
        List<Type> edelsteine = new List<Type> { typeof(Smaragd), typeof(Rubin), typeof(Diamant) };
        bool hatAlleEdelsteine = true;

        foreach (Type edelstein in edelsteine)
        {
            if (!spieler.HatItem(edelstein))
            {
                hatAlleEdelsteine = false;
                break;
            }
        }

        if (hatAlleEdelsteine)
        {
            base.Betreten(spieler);
        }
        else
        {
            Console.WriteLine("Du stehst vor einem Tor aus magischer Energie. Es sind drei Büsten aus einem glänzenden Metall zu sehen, die Aussparungen für drei Edelsteine haben. Sie rufen: 'Bring uns die Edelsteine: den Smaragd, den Rubin und den Diamanten. Nur dann wirst du hindurchgehen können!'");
            spieler.SetAktuellePosition(this);
        }
    }

}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Mögliche Befehle: vorne, zurueck, links, rechts, inventar, angriff, stats, map");

        RaumMitNiedrigerEnergie Tor = new RaumMitNiedrigerEnergie("Du stehst vor einem großen, rostigen Tor. Es scheint alt und verlassen.");
        RaumMitNiedrigerEnergie Weg = new RaumMitNiedrigerEnergie("Ein schmaler, gepflasterter Weg führt zum Haupteingang des Gebäudes.");
        RaumMitNiedrigerEnergie Haupteingang = new RaumMitNiedrigerEnergie("Der Haupteingang des Gebäudes. Die Tür steht offen.");
        RaumMitNiedrigerEnergie Flur = new RaumMitNiedrigerEnergie("Ein langer, dunkler Flur. Es ist still.");
        RaumMitNiedrigerEnergie RaumU1 = new RaumMitNiedrigerEnergie("Ein kleiner, unmarkierter Raum. Es riecht muffig.");
        RaumMitNiedrigerEnergie RaumU2 = new RaumMitNiedrigerEnergie("Ein weiterer kleiner Raum. Die Wände sind mit Schimmel bedeckt.");
        RaumMitNiedrigerEnergie Treppe = new RaumMitNiedrigerEnergie("Eine alte, knarrende Holztreppe führt nach oben.");
        RaumMitNiedrigerEnergie ErsterStock = new RaumMitNiedrigerEnergie("Du stehst im ersten Stock. Es ist ruhig hier oben.");
        RaumMitHoherEnergie Raum1 = new RaumMitHoherEnergie("Ein leeres Zimmer. Der Staub hängt in der Luft.");
        RaumMitHoherEnergie Raum2 = new RaumMitHoherEnergie("Ein Raum mit einem kaputten Fenster. Der Wind pfeift durch.");
        RaumMitNiedrigerEnergie ErsterStockWeiter = new RaumMitNiedrigerEnergie("Der Flur im ersten Stock geht weiter. Es ist dunkel am Ende.");
        RaumMitHoherEnergie Raum3 = new RaumMitHoherEnergie("Ein dunkles Zimmer. Es ist kalt.");
        RaumMitHoherEnergie Raum4 = new RaumMitHoherEnergie("Ein helles Zimmer. Die Sonne scheint durch ein Loch im Dach.");
        RaumMitNiedrigerEnergie Treppe2 = new RaumMitNiedrigerEnergie("Eine weitere Treppe. Sie führt zum Dachboden.");
        DachbodenRaum Dachboden = new DachbodenRaum("Der Dachboden!");

        Arznei arznei = new Arznei("Arznei!", 50);
        Schutz schutz = new Schutz("Schutz", 25);
        Batterie batterie = new Batterie("Batterie", 2);
        Item Smaragd = new Smaragd("Smaragd", 10);
        Item Rubin = new Rubin("Rubin!", 10);
        Item Diamant = new Diamant("Diamant", 10);
        Item LeereDose = new WertloserGegenstand("leere Dose", 0);
        Item VerstaubtesBuch = new WertloserGegenstand("Verstaubtes Buch", 0);
        Item SchickerHut = new WertloserGegenstand("Schicker Hut", 0);

        RaumU1.AddItem(schutz);
        RaumU1.AddItem(batterie);
        RaumU1.AddItem(VerstaubtesBuch);
        RaumU2.AddItem(schutz);
        RaumU2.AddItem(arznei);
        RaumU2.AddItem(LeereDose);
        Raum4.AddItem(schutz);
        Raum4.AddItem(arznei);
        Raum4.AddItem(batterie);
        Raum4.AddItem(SchickerHut);

        Geist geist1 = new Geist("Paul der Geist", Smaragd);
        Geist geist2 = new Geist("Gerlinde das Gespenst", Rubin);
        Geist geist3 = new Geist("Uwe aus der Unterwelt", Diamant);

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
        Tor.SetVor(Weg);
        Weg.SetZurueck(Tor);
        Weg.SetVor(Haupteingang);
        Dachboden.SetZurueck(Treppe2);
        Treppe2.SetVor(Dachboden);

        //Setup Player
        Spieler player1 = new Spieler("Geisterjäger", Tor, Tor, Weg, Haupteingang, Flur, RaumU1, RaumU2, Treppe, ErsterStock, Raum1, Raum2, ErsterStockWeiter, Raum3, Raum4, Treppe2, Dachboden);

        Dictionary<Raum, string> raumBuchstaben = new Dictionary<Raum, string>()
    {
        {Tor, "A"},
        {Weg, "B"},
        {Haupteingang, "C"},
        {Flur, "D"},
        {RaumU1, "E"},
        {RaumU2, "F"},
        {Treppe, "G"},
        {ErsterStock, "H"},
        {Raum1, "I"},
        {Raum2, "J"},
        {ErsterStockWeiter, "K"},
        {Raum3, "L"},
        {Raum4, "M"},
        {Treppe2, "N"},
        {Dachboden, "O"},
    };
        player1.ZeigeKarte();

        //GameLoop
        while (true)
        {
            Raum aktuellePosition = player1.GetAktuellePosition();
            aktuellePosition.Betreten(player1);

            if (!player1.IstAmLeben())
            {
                Console.WriteLine("Leider bist du gestorben! Starte das Spiel neu!");
                Console.ReadLine();

                return;
            }


            if (aktuellePosition is RaumMitHoherEnergie)
            {
                RaumMitHoherEnergie hoherEnergieRaum = aktuellePosition as RaumMitHoherEnergie;
                Geist geist = hoherEnergieRaum.GetGeist();
                if (geist != null)
                {
                    player1.ReduziereStats(0, 25);
                    if (player1.GetSchutzanzeige() == 0)
                    {
                        player1.ReduziereStats(25, 0);
                    }
                    Console.WriteLine($"Ein Geist ist hier! Du hast {25 - player1.GetSchutzanzeige()} Leben und {player1.GetSchutzanzeige()} Schutz verloren.");
                }
            }

            if (player1.GetAktuellePosition() == Dachboden && player1.HatItem(typeof(Smaragd)) && player1.HatItem(typeof(Rubin)) && player1.HatItem(typeof(Diamant)))
            {
                Console.WriteLine("Du hast alle Edelsteine gesammelt und den Dachboden betreten. Das Siegel ist gebrochen und der Fluch ist gelüftet! Du hast das Spiel gewonnen!");
                break;
            }

            Console.WriteLine("Wohin möchtest du gehen? (vorne, zurueck, links, rechts)");
            string richtung = Console.ReadLine().ToLower();

            switch (richtung)
            {
                case "vorne":
                case "zurueck":
                case "links":
                case "rechts":
                    Raum naechsterRaum = null;

                    switch (richtung)
                    {
                        case "vorne":
                            naechsterRaum = player1.GetAktuellePosition().GetVor();
                            break;
                        case "zurueck":
                            naechsterRaum = player1.GetAktuellePosition().GetZurueck();
                            break;
                        case "links":
                            naechsterRaum = player1.GetAktuellePosition().GetLinks();
                            break;
                        case "rechts":
                            naechsterRaum = player1.GetAktuellePosition().GetRechts();
                            break;
                    }

                    if (naechsterRaum == null)
                    {
                        Console.WriteLine($"Da ist kein Weg {richtung} von dir!");
                    }
                    else if (naechsterRaum is RaumMitHoherEnergie)
                    {
                        Console.WriteLine($"Dein Energiedetektor schlägt in Richtung {richtung} aus. Möchtest du eintreten? (y/n)");
                        string eingabe = Console.ReadLine().ToLower();
                        if (eingabe == "y")
                        {
                            player1.SetAktuellePosition(naechsterRaum);
                        }
                    }
                    else
                    {
                        player1.SetAktuellePosition(naechsterRaum);
                    }
                    break;

                case "inventar":
                    player1.ZeigeInventar(raumBuchstaben);
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
                case "map":
                    player1.ZeigeKarte();
                    break;
                default:
                    Console.WriteLine("Ungültige Richtung. Bitte versuche es erneut.");
                    break;
                case "death":
                    player1.ReduziereStats(player1.GetLebensanzeige(), 0);
                    Console.WriteLine("Du hast den Tod-Befehl eingegeben. Deine Lebenspunkte wurden auf 0 gesetzt.");
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
            List<Item> itemsToPickup = new List<Item>();
            foreach (Item item in items)
            {
                Console.WriteLine(item.GetBeschreibung());
                itemsToPickup.Add(item);
            }
            foreach (Item item in itemsToPickup)
            {
                spieler.Aufheben(item);
                item.Handeln(spieler);
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
    private Raum Tor, Weg, Haupteingang, Flur, RaumU1, RaumU2, Treppe, ErsterStock, Raum1, Raum2, ErsterStockWeiter, Raum3, Raum4, Treppe2, Dachboden;
    private Dictionary<Item, Raum> itemFundorte;
    public bool IstAmLeben()
    {
        return lebensanzeige > 0;
    }
    public int GetSchutzanzeige()
    {
        return schutzanzeige;
    }
    public int GetLebensanzeige()
    {
        return lebensanzeige;
    }
    public void ErhoeheLeben(int wert)
    {
        lebensanzeige += wert;
    }

    public void ErhoeheSchutz(int wert)
    {
        schutzanzeige += wert;
    }

    public void ErhoeheBatterieladungen(int wert)
    {
        batterieladungen += wert;
    }

    public bool HatItem(Type itemTyp)
    {
        foreach (Item item in inventar)
        {
            if (item.GetType() == itemTyp)
            {
                return true;
            }
        }

        return false;
    }


    private Dictionary<Raum, string> raumNamen;

    public Spieler(string name, Raum startposition, Raum Tor, Raum Weg, Raum Haupteingang, Raum Flur, Raum RaumU1, Raum RaumU2, Raum Treppe, Raum ErsterStock, Raum Raum1, Raum Raum2, Raum ErsterStockWeiter, Raum Raum3, Raum Raum4, Raum Treppe2, Raum Dachboden)
    {
        this.name = name;
        aktuellePosition = startposition;
        inventar = new List<Item>();
        lebensanzeige = 100;
        schutzanzeige = 50;
        batterieladungen = 3;
        itemFundorte = new Dictionary<Item, Raum>();

        this.Tor = Tor;
        this.Weg = Weg;
        this.Haupteingang = Haupteingang;
        this.Flur = Flur;
        this.RaumU1 = RaumU1;
        this.RaumU2 = RaumU2;
        this.Treppe = Treppe;
        this.ErsterStock = ErsterStock;
        this.Raum1 = Raum1;
        this.Raum2 = Raum2;
        this.ErsterStockWeiter = ErsterStockWeiter;
        this.Raum3 = Raum3;
        this.Raum4 = Raum4;
        this.Treppe2 = Treppe2;
        this.Dachboden = Dachboden;

        raumNamen = new Dictionary<Raum, string>()
        {
            {Tor, "Tor"},
            {Weg, "Weg"},
            {Haupteingang, "Haupteingang"},
            {Flur, "Flur"},
            {RaumU1, "RaumU1"},
            {RaumU2, "RaumU2"},
            {Treppe, "Treppe"},
            {ErsterStock, "ErsterStock"},
            {Raum1, "Raum1"},
            {Raum2, "Raum2"},
            {ErsterStockWeiter, "ErsterStockWeiter"},
            {Raum3, "Raum3"},
            {Raum4, "Raum4"},
            {Treppe2, "Treppe2"},
            {Dachboden, "Dachboden"},
        };

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
        itemFundorte[item] = aktuellePosition;
        Console.WriteLine($"Du hast {item.GetBeschreibung()} aufgenommen.");
    }


    public void ZeigeInventar(Dictionary<Raum, string> raumBuchstaben)
    {
        if (inventar.Count > 0)
        {
            Console.WriteLine("Inventar:");
            foreach (Item item in inventar)
            {
                Raum fundort = itemFundorte[item];
                string raumBuchstabe = raumBuchstaben[fundort];
                Console.WriteLine($"{item.GetBeschreibung()} gefunden in Raum {raumBuchstabe}");
            }
        }
        else
        {
            Console.WriteLine("Dein Inventar ist leer.");
        }
    }

    public void ReduziereStats(int leben, int schutz)
    {
        if (schutzanzeige - schutz >= 0)
        {
            schutzanzeige -= schutz;
        }
        else
        {
            leben += schutz - schutzanzeige;
            schutzanzeige = 0;
        }

        lebensanzeige -= leben;
        if (lebensanzeige < 0)
        {
            lebensanzeige = 0;
        }
    }


    public void Angreifen(Geist geist)
    {
        if (batterieladungen >= 2)
        {
            batterieladungen -= 2;
            Item edelstein = geist.DropEdelstein();
            if (edelstein != null)
            {
                inventar.Add(edelstein);
                itemFundorte[edelstein] = aktuellePosition;
                Console.WriteLine($"Du hast den Geist angegriffen und einen {edelstein.GetBeschreibung()} erhalten!");
            }
            else
            {
                Console.WriteLine("Dieser Geist wurde bereits besiegt.");
            }
            if (geist.IstBesiegt())
            {
                RaumMitHoherEnergie hoherEnergieRaum = aktuellePosition as RaumMitHoherEnergie;
                hoherEnergieRaum.SetGeist(null);
                Console.WriteLine("Der Geist ist verschwunden!");
            }
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
    public void ZeigeKarte()
    {
        Dictionary<Raum, string> raumBuchstaben = ErstelleKartenReferenzen();
        Console.WriteLine("Karte:");
        Console.WriteLine("EG:    E--D--F");
        Console.WriteLine("          |");
        Console.WriteLine("     A--B--C--G");
        Console.WriteLine("          |");
        Console.WriteLine("1.OG:     I--H--K--M");
        Console.WriteLine("          |");
        Console.WriteLine("     J--L--N----");
        Console.WriteLine("               |");
        Console.WriteLine("2.OG:          O");

        Console.WriteLine("\nLegende:");
        foreach (var raum in raumBuchstaben)
        {
            string raumName = raumNamen[raum.Key];

            Console.WriteLine($"{raum.Value}: {raumName}");
        }
    }
    private Dictionary<Raum, string> ErstelleKartenReferenzen()
    {
        return new Dictionary<Raum, string>()
        {
            {Tor, "A"},
            {Weg, "B"},
            {Haupteingang, "C"},
            {Flur, "D"},
            {RaumU1, "E"},
            {RaumU2, "F"},
            {Treppe, "G"},
            {ErsterStock, "H"},
            {Raum1, "I"},
            {Raum2, "J"},
            {ErsterStockWeiter, "K"},
            {Raum3, "L"},
            {Raum4, "M"},
            {Treppe2, "N"},
            {Dachboden, "O"},
        };
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

    public virtual void Handeln(Spieler spieler) { }
}

class Arznei : Item
{
    private int wert;

    public Arznei(string beschreibung, int wert) : base(beschreibung)
    {
        this.wert = wert;
    }

    public override void Handeln(Spieler spieler)
    {
        spieler.ErhoeheLeben(wert);
        Console.WriteLine($"Deine Lebensanzeige wurde um {wert} erhöht!");
    }
}

class Schutz : Item
{
    private int wert;

    public Schutz(string beschreibung, int wert) : base(beschreibung)
    {
        this.wert = wert;
    }

    public override void Handeln(Spieler spieler)
    {
        spieler.ErhoeheSchutz(wert);
        Console.WriteLine($"Deine Schutzanzeige wurde um {wert} erhöht!");
    }
}

class Batterie : Item
{
    private int wert;

    public Batterie(string beschreibung, int wert) : base(beschreibung)
    {
        this.wert = wert;
    }

    public override void Handeln(Spieler spieler)
    {
        spieler.ErhoeheBatterieladungen(wert);
        Console.WriteLine($"Deine Batterieladungen wurden um {wert} erhöht!");
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


class Smaragd : Item
{
    public Smaragd(string beschreibung, int wert) : base(beschreibung)
    {

    }

    public override void Handeln(Spieler spieler)
    {
        spieler.Aufheben(this);
        Console.WriteLine("Du hast den Smaragd aufgenommen!");
    }
}

class Rubin : Item
{
    public Rubin(string beschreibung, int wert) : base(beschreibung)
    {

    }

    public override void Handeln(Spieler spieler)
    {
        spieler.Aufheben(this);
        Console.WriteLine("Du hast den Rubin aufgenommen!");
    }
}

class Diamant : Item
{
    public Diamant(string beschreibung, int wert) : base(beschreibung)
    {

    }

    public override void Handeln(Spieler spieler)
    {
        spieler.Aufheben(this);
        Console.WriteLine("Du hast den Diamanten aufgenommen!");
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
        if (edelstein != null)
        {
            Console.WriteLine($" {name} wurde besiegt und hat einen {edelstein.GetBeschreibung()} fallen gelassen!");
            Item droppedEdelstein = edelstein;
            edelstein = null;
            return droppedEdelstein;
        }
        return null;
    }

    public bool IstBesiegt()
    {
        return edelstein == null;
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