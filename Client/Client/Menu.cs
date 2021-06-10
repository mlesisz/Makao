using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public static class Menu
    {
        public static string[] Views = {
            "1 Zaloguj się. \n" +
                "2 Zarejestruj się.\n" +
                "0 Wyłącz program.",

            "3 Utwórz stół\n" +
                "4 Zobacz listę dostępnych stołów.\n" +
                "0 Wyloguj się.",

            "5. Dołącz do stołu\n",

            "6. Dobierz kartę.\n" +
                "7. Połórz kartę na stół. \n" +
                "8. Opuść stół. "
        };

        public static int ActiveView = 0;
    }
}
