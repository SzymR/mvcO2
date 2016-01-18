using Repozytorium.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Repozytorium.IRepo
{
    public interface IOgloszenieRepo
    {
        /// <summary>
        /// // <Optymalizacja poprzez AsNoTracking dla danych tylko do odczytu>
        /// Include zamienione na AsNoTracking , ktory wylaczy sledzenie danych przez kontekst
        /// Dane sa przeznaczone tylko do odczytu, dlatego nie trzeba sledzic ich stanu ani
        /// przetrzymywac ich w pamieci operacyjne. Wykorzystuje Lazy Loading, aby nie
        /// pobierac danych uzytkownikow.
        /// Do bazy nie pojdzie zapytanie join, pobrane zostana tylko ogloszenia, co jest duzo szybsze
        /// AsNoTracking wylacza zapisywanie danych do obiektu przez co nie da sie go sledzic wykorzystujac debugowanie
        /// </summary>
        /// <returns></returns>
        IQueryable<Ogloszenie> PobierzOgloszenia();

        /// <summary>
        ///  Stronnicowanie (paginacja), aby nie pobierać wszystkich danych na raz
        /// </summary>
        /// <param name="page">Ktora strone wyswietlic</param>
        /// <param name="pageSize">Ile elementow na stronie</param>
        /// <returns></returns>
        IQueryable<Ogloszenie> PobierzStrone(int? page, int? pageSize);

        /// <summary>
        /// Pobiera wybrane ogłoszenie
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Ogloszenie GetOgloszenieById(int id);

        /// <summary>
        /// Usuniecie wpisow z tabeli Oloszenie_Kategoria, ktore w kluczu obcym OgloszenieID
        /// sa powiazane z usuwanym ogłoszeniem
        /// Zamiast ustawiania usuwania Kaskadowego
        /// </summary>
        /// <param name="idOgloszenia"></param>
        void UsunOgloszenie(int id);

        /// <summary>
        /// Dodanie ogloszenia do bazy danych
        /// </summary>
        /// <param name="ogloszenie"></param>
        void Dodaj(Ogloszenie ogloszenie, string[] category);
        void DodajKategorieDoOgloszenia(Ogloszenie ogloszenie, string[] category);

        IQueryable<Atrybut> PobierzAtrybutyZKategorii(int id);
        IQueryable<AtrybutWartosc> PobierzWartosciAtrybutowZAtrybutu(int id);


        /// <summary>
        /// Mamy kontrole nad tym kiedy dane są zapisywane do bazy, dzieki temu mozemy wykonac weiele
        /// operacji bez koniecznosci ciaglego odwolywania sie do bazy
        /// </summary>
        void SaveChanges();

        /// <summary>
        /// Aktualizuje wybrane ogłoszenie
        /// </summary>
        /// <param name="ogloszenie"></param>
        void Aktualizuj(Ogloszenie ogloszenie);
    }
}