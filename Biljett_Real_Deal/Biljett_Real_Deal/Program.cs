using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Biljett_Real_Deal
{
    class Program
    {
        //GLOBALA VARIABLER
        string väg = @"..\..\..\Sparfil.txt";
        int nummer = 0;
        int totalÅterköp = 0;
        int barnÅterköp = 0;
        int vuxenÅterköp = 0;
        int pensionärÅterköp = 0;


        int återköpKostnad = 0;
        string laddaData; // variabel som checkar ifall spardata ska laddas eller inte


        int PlayboiCarti = 0;
        int SlowDive = 0;
        int KendrickLamar = 0;
        int BrandNew = 0;


        static void Main()
        {
            Program p = new Program();
            p.Run();

        }

        void Run()
        {
            //MAIN PROGRAMMET


            //SPARDATA HANTERING
            Console.WriteLine("Startar Bilejett_AutomaterTM.....\n");
            Console.Write("Vill du ladda tidigare data?\nJa eller Nej: ");
            laddaData = Console.ReadLine().ToLower();
            bool ogiltigtSvar = true;
            while (ogiltigtSvar == true)
            {
                if (laddaData == "ja") //Laddar och använder spardata för resten av programmet
                {
                    Console.WriteLine("\nLaddar data.....");
                    Ladda();
                    Console.WriteLine("");
                    ogiltigtSvar = false; //Stoppar "while loopen"
                }
                else if (laddaData == "nej")//Programmet körs utan spardata som om det skulle vara första gången det körs
                {
                    Console.WriteLine("\nFortsätter utan spardata.....\n");
                    ogiltigtSvar = false; //Stoppar "while loopen"
                }
                else //Om svaret varken är "Ja" eller "Nej" så loopas frågan tills ett giltigt svar anges
                {
                    Console.WriteLine("Ogiltigt Svar....");
                    Console.Write("Ja eller Nej: ");
                    laddaData = Console.ReadLine();
                }
            }


            Console.WriteLine("Hej! Vad kan vi hjälpa dig med? Skriv \"Hjälp\" för att få mer information om vad du kan göra.");

            while (true)
            {
                //MENYERNA
                Console.Write("Vad vill du göra: ");
                string aktivitet = Console.ReadLine().ToLower();

                if (aktivitet == "hjälp")
                {
                    //KOMMANDOLISTAN
                    Console.WriteLine("COMMANDS:");
                    Console.WriteLine("\"Köp biljett\": Tar dig till biljett kassan");
                    Console.WriteLine("\"Återköp\": Om du av någon anledning vilja få ett återköp på en biljett gå hit");
                    Console.WriteLine("\"Återköp historia\": Ger data om föregående återköp");
                    Console.WriteLine("\"Biljett lista\": Visar dig listan av kunder");
                    Console.WriteLine("\"Konsert stats\": Visar konserternas stats");
                    Console.WriteLine("\"Vinster\": Visar företagets vinster i rena pengar samt biljetter av varje åldersgrupp sålda");
                    Console.WriteLine("\"Exit\": Stänger ned programmet");
                    Console.WriteLine("");

                }
                else if (aktivitet == "köp biljett")
                {
                    //KÖR KÖP_BILJETT METODEN
                    KöpBiljett();
                }
                else if (aktivitet == "konsert stats")
                {
                    //KÖR KONSERT_STATS METODEN
                    KonsertStats();
                }
                else if (aktivitet == "biljett lista")
                {
                    //KÖR BILJETT_LISTA METODEN
                    SkrivUtLista();
                }
                else if (aktivitet == "vinster")
                {
                    //KÖR VINSTER METODEN
                    Vinster();
                }
                else if (aktivitet == "återköp")
                {
                    //KÖR ÅTERKÖP METODEN
                    Återköp();
                }
                else if (aktivitet == "återköp historia")
                {
                    //KÖR ÅTERKÖP_HISTORIA METODEN
                    AntalÅterköp();
                }
                else if (aktivitet == "exit")
                {
                    //STÄNGER NER PROGRAMMET
                    Environment.Exit(0);
                }

            }

        }



        private void KöpBiljett() // Metoden som låter användaren köpa biljetter
        {
            Console.WriteLine("\nDet här är kvällens föreställningar!");
            Console.WriteLine("Playboi Carti");
            Console.WriteLine("Slow Dive");
            Console.WriteLine("Kendrick Lamar");
            Console.WriteLine("Brand new");
            Console.Write("\nVilken vill du gå på? Skriv här: ");
            string konsert = Console.ReadLine().ToLower();
  
            if (konsert == "playboi carti" || konsert == "slow dive" || konsert == "kendrick lamar" || konsert == "brand new")
            {
                Console.Write("Ditt namn: ");
                string namn = Console.ReadLine().ToLower();
                Console.Write("Din ålder: ");

                try //Testar och checkar inputten innan det körs för att motverka potentiella problem, i detta fallet om användaren skriver in en siffra eller inte
                {
                    int ålder = int.Parse(Console.ReadLine());
                    int kostnad = 0;
                    if (ålder >= 0 && ålder < 18)
                    {
                        kostnad = 25;
                    }
                    else if (ålder > 64)
                    {
                        kostnad = 75;
                    }
                    else if (ålder >= 18 && ålder < 65)
                    {
                        kostnad = 100;
                    }
                    Kund kund = new Kund(namn, kostnad, ålder, konsert);
                    kundLista.Add(kund);


                    nummer++;
                    string order = "nr " + nummer;
                    kund.order = "nr " + nummer;


                    Console.WriteLine("\n" + konsert + " " + order + " har köpts för: " + kostnad + " kr!\n");


                    Spara();

                }
                catch //Problemet fångas och programmet ger tillbaka svar om felen, i detta fallet om användaren inte skrev in ett nummer som ålder
                {
                    Console.WriteLine("Det är inte en giltig ålder...."); 
                    Console.WriteLine("Endast nummer accepteras");
                }

            }
            else //Om användaren skrev in en konsert som inte existerar i programmet så körs det här
            {
                Console.WriteLine("Ogiltig konsert....");
                Console.WriteLine("Försök igen!");
            }



                


            
        }


        void Ladda() //Laddar sparad data
        {
            try
            {
                StreamReader sr = new StreamReader(väg);

                string text;
                while((text = sr.ReadLine()) != null)
                {
                    string[] strings = text.Split(char.Parse("$"));
                    
                    kundLista.Add(new Kund(strings[0], int.Parse(strings[1]), int.Parse(strings[2]), strings[3]) { namn = strings[0], ålder = int.Parse(strings[1]), kostnad = int.Parse(strings[2]), konsert = strings[3], order = strings[4] });
                    
                }
                sr.Close();

                foreach (Kund k in kundLista)
                {
                    Console.WriteLine(k.ToString());
                }

            }
            catch
            {

            }
        }


        private void KonsertStats() //Ger stats om Konserter
        {
            foreach (Kund k in kundLista) //Kollar varje "kund" i "kundLista" och sorterar de baserat på vilken konsert deras biljett är för
            {
                if (k.konsert == "playboi carti")
                {
                    PlayboiCarti++;
                }
                else if (k.konsert == "slow dive")
                {
                    SlowDive++;
                }
                else if (k.konsert == "kendrick lamar")
                {
                    KendrickLamar++;
                }
                else if (k.konsert == "brand new")
                {
                    BrandNew++;
                }
            }
            Console.WriteLine("\nVilken konsert vill du få info om?");
            Console.Write("\"Alla\" ger info om varje konsert, för individuell konsert info skriv konsertens namn: ");
            string konsertInfo = Console.ReadLine().ToLower();

            if (konsertInfo == "alla")
            {
                Console.WriteLine("\nPlayboi Carti biljetter sålda: " + PlayboiCarti);
                Console.WriteLine("Slow Dive biljetter sålda: " + SlowDive);
                Console.WriteLine("Kendrick Lamar biljetter sålda: " + KendrickLamar);
                Console.WriteLine("Brand New biljetter sålda: " + BrandNew + "\n");
            }
            else if (konsertInfo == "playboi carti")
            {
                Console.WriteLine("\nPlayboi Carti biljetter sålda: " + PlayboiCarti + "\n");
            }
            else if (konsertInfo == "slow dive")
            {
                Console.WriteLine("\nSlow Dive biljetter sålda: " + SlowDive + "\n");
            }
            else if (konsertInfo == "kendrick lamar")
            {
                Console.WriteLine("\nKendrick Lamar biljetter sålda: " + KendrickLamar + "\n");
            }
            else if (konsertInfo == "brand new")
            {
                Console.WriteLine("\nBrand New biljetter sålda: " + BrandNew + "\n");
            }
            else//Körs ifall användaren skrev in en konsert som inte existerar inom programmet
            {
                Console.WriteLine("\nOgiltig konsert....");
                Console.WriteLine("Försök igen!\n");
            }



        }

        private void SkrivUtLista()
        {
            //Printar ut kundLista och visar varje kunds individuella attribut
            Console.WriteLine("Biljett Lista:\n");
            for (int i = 0; i < kundLista.Count; i++)
            {
                Console.WriteLine("Namn: " + kundLista[i].namn + " |Ålder: " + kundLista[i].ålder + " |Konsert: " + kundLista[i].konsert + " |Order nummer: " + kundLista[i].order);
            }
            Console.WriteLine("");
        }

        void Vinster() //Ger datan om både det totala antalet biljetter köpta samt summan pengar tjänad och specifik köp data om varje åldersgrupp 
        {
            int Summa = 0;
            foreach (Kund k in kundLista)
            {
                Summa += k.kostnad;
            }
            Console.WriteLine("\nTotala vinster: " + Summa + "kr");

            int barn = 0;
            int vuxen = 0;
            int pensionär = 0;

            foreach (Kund k in kundLista)
            {
                int ålder = k.ålder;
                if (ålder >= 0 && ålder < 18)
                {
                    barn += 1;
                }
                else if (ålder >= 18 && ålder < 65)
                {
                    vuxen += 1;
                }
                else if (ålder >= 65)
                {
                    pensionär += 1;
                }
            }
            int total = barn + vuxen + pensionär;
            int barnVinster = barn * 25;
            int vuxenVinster = vuxen * 100;
            int pensionärVinster = pensionär * 75;

            Console.WriteLine("Totala antalet sålda biljetter: " + total + "\n");
            Console.WriteLine("Barn biljetter sålda: " + barn + " | " + barnVinster + "kr");
            Console.WriteLine("Vuxen biljetter sålda: " + vuxen + " | " + vuxenVinster + "kr");
            Console.WriteLine("Senior/Pensionär biljetter sålda: " + pensionär + " | " + pensionärVinster + "kr");


        }

        void Återköp() //Gör så att kunden kan få ett återköp genom att specificera konserten + order nummret
        {
            Console.WriteLine("Vilken konsert vill du göra ett återköp för?");
            Console.Write("\"Playboi Carti\", \"Slow Dive\", \"Kendrick Lamar\" eller \"Brand New\": ");
            string återköpKonsert = Console.ReadLine().ToLower();
            if (återköpKonsert == "playboi carti")
            {
                Console.Write("Ange order nummer(order nummer skrivs \"nr x\"): ");
                string återköpOrder = Console.ReadLine();
                for (int i = 0; i < kundLista.Count; i++)
                {
                    Kund k = kundLista[i];
                    if (k.konsert == återköpKonsert)
                    {
                        if (k.order == återköpOrder)
                        {
                            totalÅterköp++;

                            if (k.ålder >= 0 && k.ålder < 18)
                            {
                                återköpKostnad += 25;
                                barnÅterköp++;
                            }
                            else if (k.ålder > 17 && k.ålder < 65)
                            {
                                återköpKostnad += 100;
                                vuxenÅterköp++;
                            }
                            else if (k.ålder > 64)
                            {
                                återköpKostnad += 75;
                                pensionärÅterköp++;
                            }
                            kundLista.Remove(k);
                            i--;
                            Console.WriteLine("Din " + återköpKonsert + " biljett har återbetalas\n");
                        }
                    }
                }
            }
            else if (återköpKonsert == "slow dive")
            {
                Console.Write("Ange order nummret: ");
                string återköpOrder = Console.ReadLine();
                for (int i = 0; i < kundLista.Count; i++)
                {
                    Kund k = kundLista[i];
                    if (k.konsert == återköpKonsert)
                    {
                        if (k.order == återköpOrder)
                        {
                            totalÅterköp++;

                            if (k.ålder >= 0 && k.ålder < 18)
                            {
                                återköpKostnad += 25;
                                barnÅterköp++;
                            }
                            else if (k.ålder > 17 && k.ålder < 65)
                            {
                                återköpKostnad += 100;
                                vuxenÅterköp++;
                            }
                            else if (k.ålder > 64)
                            {
                                återköpKostnad += 75;
                                pensionärÅterköp++;
                            }
                            kundLista.Remove(k);
                            i--;
                            Console.WriteLine("Din " + återköpKonsert + " biljett har återbetalas\n");
                        }
                    }
                }
            }
            else if (återköpKonsert == "kendrick lamar")
            {
                Console.Write("Ange order nummret: ");
                string återköpOrder = Console.ReadLine();
                for (int i = 0; i < kundLista.Count; i++)
                {
                    Kund k = kundLista[i];
                    if (k.konsert == återköpKonsert)
                    {
                        if (k.order == återköpOrder)
                        {
                            totalÅterköp++;

                            if (k.ålder >= 0 && k.ålder < 18)
                            {
                                återköpKostnad += 25;
                                barnÅterköp++;
                            }
                            else if (k.ålder > 17 && k.ålder < 65)
                            {
                                återköpKostnad += 100;
                                vuxenÅterköp++;
                            }
                            else if (k.ålder > 64)
                            {
                                återköpKostnad += 75;
                                pensionärÅterköp++;
                            }
                            kundLista.Remove(k);
                            i--;
                            Console.WriteLine("Din " + återköpKonsert + " biljett har återbetalas\n");
                        }
                    }
                }
            }
            else if (återköpKonsert == "brand new")
            {
                Console.Write("Ange order nummret: ");
                string återköpOrder = Console.ReadLine();
                for (int i = 0; i < kundLista.Count; i++)
                {
                    Kund k = kundLista[i];
                    if (k.konsert == återköpKonsert)
                    {
                        if (k.order == återköpOrder)
                        {
                            totalÅterköp++;

                            if (k.ålder >= 0 && k.ålder < 18)
                            {
                                återköpKostnad += 25;
                                barnÅterköp++;
                            }
                            else if (k.ålder > 17 && k.ålder < 65)
                            {
                                återköpKostnad += 100;
                                vuxenÅterköp++;
                            }
                            else if (k.ålder > 64)
                            {
                                återköpKostnad += 75;
                                pensionärÅterköp++;
                            }
                            kundLista.Remove(k);
                            i--;
                            Console.WriteLine("Din " + återköpKonsert + " biljett har återbetalas\n");
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Ogiltig konsert....");
                Console.WriteLine("Försök igen!\n");
            }
        }

        void AntalÅterköp() //skriver ut och tillåter en se all föregången data gällande återköp.
        {
            Console.WriteLine("\nTotala antalet återköp: " + totalÅterköp);
            Console.WriteLine("Totala kostnaden av återköp: " + återköpKostnad);

            Console.WriteLine("\nÅldersgrupp återköp");
            Console.WriteLine("Antalet återköp av barn biljetter: " + barnÅterköp);
            Console.WriteLine("Antalet återköp av vuxen biljetter: " + vuxenÅterköp);
            Console.WriteLine("Antalet återköp av pensionär biljetter: \n" + pensionärÅterköp);



        }




        private List<Kund> kundLista = new List<Kund>(); //Listan där all kunder samlas

        private void Spara() //Sparar all data 
        {
            StreamWriter sw = new StreamWriter(väg);
            foreach (Kund k in kundLista)
            {
                sw.WriteLine(k.ToString());
            }
            sw.Close();
        }


        
    }

        
}

