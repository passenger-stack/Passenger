using System;
using System.Collections.Generic;

namespace Passenger.Core.Domain
{
    public class Driver
    {
        public Guid UserId { get; protected set; }
        public Vehicle Vehicle { get; protected set; }
        public IEnumerable<Route> Routes { get; protected set; }
        public IEnumerable<DailyRoute> DailyRoutes { get; protected set; }
        public DateTime UpdatedAt { get; private set; }

        protected Driver() 
        {
        }

        public Driver (Guid userid)
        {
            UserId = userid;
        }

        // w jaki sposob mozemy je konstruować
        // a) porobić odp konstruktory
        // b) przemysleć jakie dane mogą być przekazywane jako argumenty
        // c) jaka walidacja:
        //uzytkownik nie moze byc nullem
        // uzytkownik wywala ten adres email i haslo ktore jest puste
        // OPAKOWAC KLASY KONSTRUKTORAMI, jakieś metody do SETTOW, do walidacji bazujacej na podstawowych wyjątkach
        // pomysleć o pierwszych interakcjach ktore mogą zachodzić pomiedzy jedną klasa a drugą
        // np klasa driver konstrujemy ja za pomoca usera,
        // mozna pomyslec czy klasa user moze miec taka wlasciwosc jak driver
        // nasza klasa reprezentujaca route albo dailyroute powinna miec metody w stylu ustaw sciezke, dodaj pkt odbioru pasazera, usun pkt odbioru pasazerow.
    }
}