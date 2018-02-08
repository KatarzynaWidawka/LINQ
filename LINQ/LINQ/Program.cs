using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LINQ
{
    partial class Program
    {
        static void Main(string[] args)
        {
            int countUser = 10; // liczba użytkowników do wygenerowania
            var r = Helper.Rand; // odwołanie się do ref obiektu klasy Random utworzonego w klasie pomocniczej
            var names = Helper.Names;// odwołanie się do ref zmiennej tablicowej utworzonej w klasie pomocniczej

            List<IUser> userList = new List<IUser>(); // lista różnych użytkowników (lekarze i pacjenci nie przypisani do lekarza żadni)
            for (int i = 0; i < countUser; i++)
            {
                double nextHealthLevel = r.NextDouble(); // poziom zdrowia
                int randomValue = r.Next(0, 100);

                int indName = r.Next(0, names.Length); // losowy indeks
                string name = names[indName];// losowe imie

                if (randomValue % 2 == 0) // sprawdzenie parzystości
                {
                    var newDoctor = new Doctor(name, nextHealthLevel); // stworzenie nowego lekarza (proszę przejść do ctor)
                    userList.Add(newDoctor);
                }
                else
                {
                    var newPatient = new Patient(name, nextHealthLevel); // stworzenie pacjenta nie związanego z żadnym lekarzem (proszę przejść do ctor)
                    userList.Add(newPatient);
                }
            }

            /**************************************** ZADANIE 1 ****************************************/

            // Wyswietlenie wszystkich osób z listy użytkowników
            Show("------------------- Lista osób -------------------", userList);

            // Posortuj listę użytkowników ze względu na ID użytkownika (rosnąco)
            //...
            var listauzytkownikowrozsnaca = userList.OrderBy(x => x.Id)
                                                    .ToList();
            //Show("Posortowana lista użytkowników:", listauzytkownikowrozsnaca);
            
            // Pobierz imiona wszystkich użytkowników
            //...
            var listaimion = userList.Select(x => x.Name)
                                     .ToList();
            //Show("Lista imion wszystkich użytkowników:",listaimion);

            // Pobierz liczbę użytkowników posiadających nieparzyste ID
            //...
            var nieparzysteid = userList.Count(x => x.Id % 2 == 1);
            //    .ToList();

            //Show("Liczba użytkowników posiadających nieparzyste ID:",nieparzysteid);

            // Pobierz użytkownika posiadającego ID = 5 
            //...
            var id5 = userList.Where(x => x.Id == 5)
                              .ToList();
            //Show("Użytkownik posiadający ID=5:", id5);


            // Pobierz użytkownika posiadającego ID = 234
            //...
            var id234 = userList.Where(x => x.Id == 234)
                                .ToList();
            //Show("Użytkownik posiadający ID=234:", id234);

            // Usuń użytkowników posiadających stan zdrowia większe niż 0,4 (po zweryfikowaniu działania metody proszę o jej zakomentowanie)
            //...
            //var usun = userList.RemoveAll(x => x.HealthLevel > 0.4);


            // Pogrupuj listę użytkowników ze względu na typ użytkownika (lekarze/pacjenci)
            //...
            var grupuj = userList.GroupBy(x => x.GetType() == typeof(Doctor)) 
                                  .ToList();
            Show("Lista użytkowników:",grupuj);


            // Pobierz maksymalną wartość stanu zdrowia z listy wszystkich użytkowników
            //...
            var maks = userList.Max(x => x.HealthLevel);

            //Show("Maksymalna wartość stanu zdrowia z listy wszystkich użytkowników", maks);


            // Oblicz sumę stanu zdrowia wszystkich użytkowników, których imiona zaczynają się na literę 'A'
            //...
            var sum = userList.Where(x => x.Name.StartsWith("A"))
                             .Sum(x => x.HealthLevel);

            // Pobierz 5 najwcześniej utworzonych użytkowników (Date)
            //...
            var utworzeninajwczesniej = userList.OrderBy(x => x.Date)
                                                .Take(5)
                                                .ToList();
            //Show(" 5 najwcześniej utworzonych użytkowników: ", utworzeninajwczesniej);
            /**************************************** ZADANIE 2 ****************************************/

            // Pobierz pierwszego pacjenta z listy użytkowników  (GetType(), typeof, is)
            //...

            var pierwszypacjent = userList.Where(x => x.GetType() == typeof(Patient))
                                        .First();
            //Show("Pierwszy pacjent z listy użytkowników: ", pierwszypacjent);

            // Pobierz tylko lekarzy z listy użytkowników
            //...
            var lekarze = userList.Where(x => x.GetType() == typeof(Doctor))
                                  .ToList();
            //Show("Lekarze: ", lekarze);

            // Pobierz tylko lekarzy posiadających parzyste ID
            //...
            var lekarzeparzysteid=userList.Where(x=>x.Id % 2 == 0)
                                          .Where(x => x.GetType() == typeof(Doctor));
            Show("lekarze posiadające parzyste ID: ", lekarzeparzysteid);
                    
            // Oblicz średnią wartość stanu zdrowia lekarzy
            //...
            var srednialekarze = userList
                                         .Where(x => x.GetType() == typeof(Doctor))
                                         .Average(x => x.HealthLevel);

            //Show("Średnia wartość stanu zdrowia lekarzy: ", srednialekarze);
            // Podziel listę użytkowników na 5 elementowe równoliczne grupy (grupy mieszane)
            //...
            int a = 5;
            var group1 = userList.Take(a);
            var group2 = userList.Skip(group1.Count())
                               .Take(a);

            // Pobierz 5 użytkowników z listy użytkowników, zaczynając od użytkownika z ID = 2
            //...
            var odid2 = userList.FindIndex(x => x.Id == 2);

            /**************************************** ZADANIE 3 ****************************************/

            // Zlokalizuj lekarza posiadającego największą liczbę pacjentów (doctor.PacientCount)
            //...
            var najwiekszaliczbapacjentow = userList.Where(x => x.GetType() == typeof(Doctor))
                                                    .Select(x => (Doctor)x)
                                                    .OrderBy(x => x.PacientCount)
                                                    .First();
            //Show("Lekarz posiadający największą liczbę pacjentów: ", najwiekszaliczbapacjentow);

            // Pobierz wszystkich pacjentów przypisanych do lekarzy (doctor.PacientList)
            //...
            var pacjenciprzypisanidolekarzy = userList.Where(x => x.GetType() == typeof(Doctor))
                                                    .Select(x => (Doctor)x)
                                                    .Select(x => x.PacientList);
            //Show("Wszyscy pacjenci przypisani do lekarzy", pacjenciprzypisanidolekarzy);
            // Pobierz wszystkich użytkowników(lekarzy, pacjentów przypisanych i nieprzypisanych) posiadających imię rozpoczynające się na litere 'P' 
            //...
            var wszyscy = userList.Select(x => x.Name.StartsWith("P"))
                                  .ToList();
            //Show("Wszyscy użtkownicy posiadający imię rozpoczynające się na litere P", wszyscy);

            Console.ReadKey();
        }
        static void Show(string header, IEnumerable<object> userList)
        {
            Console.WriteLine(header);
            foreach (var i in userList)
            {
                Console.WriteLine(i);
            }
        }
    }
}