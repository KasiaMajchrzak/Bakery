using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bakery.Migrations
{
    [Migration(20210110180700)]
    public class _20210110180700_InsertDataIntoDatabase : ForwardOnlyMigration
    {
        public override void Up()
        {
            Insert.IntoTable("Additional")
                .Row(new { Name = "Świeże owoce", Description = "Dowolne świeże owoce dostępne w danym sezonie", Price = 5 })
                .Row(new { Name = "Mrożone owoce", Description = "Dowolne mrożone owoce", Price = 5 })
                .Row(new { Name = "Czekoladki Kinder", Description = "Mix słodyczy Kinder", Price = 5 })
                .Row(new { Name = "Konfitura Mango-Marakuja", Description = "Konfitura z mango i marakui", Price = 5 })
                .Row(new { Name = "Konfitura Owoce Leśne", Description = "Konfitura z owoców leśnych", Price = 5 })
                .Row(new { Name = "Konfitura Truskawkowa", Description = "Konfitura z truskawek", Price = 5 })
                .Row(new { Name = "Konfitura Malinowa", Description = "Konfitura z malin", Price = 5 })
                .Row(new { Name = "Konfitura Morelowo-Rozmarynowa", Description = "Konfitura z moreli z dodatkiem rozmarynu", Price = 5 })
                .Row(new { Name = "Konfitura z Czarnej Porzeczki", Description = "Konfitura z Czarnej Porzeczki", Price = 5 })
                .Row(new { Name = "Żelka Mango-Marakuja", Description = "Żelka zrobiona z dżemu", Price = 5 })
                .Row(new { Name = "Żelka Owoce Leśne", Description = "Żelka zrobiona z dżemu", Price = 5 })
                .Row(new { Name = "Żelka Truskawkowa", Description = "Żelka zrobiona z dżemu", Price = 5 })
                .Row(new { Name = "Żelka Morelowo-Rozmarynowa", Description = "Żelka zrobiona z dżemu", Price = 5 })
                .Row(new { Name = "Żelka Malinowa", Description = "Żelka zrobiona z dżemu", Price = 5 })
                .Row(new { Name = "Czekolada", Description = "Kawałki czekolady: do wyboru mleczna, gorzka oraz biała", Price = 5 })
                .Row(new { Name = "Blat bezowy", Description = "Cienki blat bezowy", Price = 5 })
                .Row(new { Name = "Galaretka Mango-Marakuja", Description = "Galaretka z mango i marakui", Price = 5 })
                .Row(new { Name = "Galaretka Owoce Leśne", Description = "Galaretka z owoców leśnych", Price = 5 })
                .Row(new { Name = "Galaretka Truskawkowa", Description = "Galaretka z truskawek", Price = 5 })
                .Row(new { Name = "Galaretka Truskawkowo-Limonkowa", Description = "Galaretka z truskawek i limonek", Price = 5 })
                .Row(new { Name = "Galaretka Malinowa", Description = "Galaretka z malin", Price = 5 })
                .Row(new { Name = "Galaretka Morelowo-Rozmarynowa", Description = "Galaretka z moreli z dodatkiem rozmarynu", Price = 5 })
                .Row(new { Name = "Galaretka z Czarnej Porzeczki", Description = "Galaretka z Czarnej Porzeczki", Price = 5 })
                .Row(new { Name = "Dodatkowy Smak Kremu", Description = "Dowolny drugi smak kremu", Price = 5 })
                .Row(new { Name = "Dodatkowy Smak Biszkoptu", Description = "Dowolny drugi smak biszkoptu", Price = 5 })
                .Row(new { Name = "Wiśniówka", Description = "Domowa wiśniówka", Price = 0 })
                .Row(new { Name = "Kompot Owocowy", Description = "Kompot z Dowolnych Owowców", Price = 0 })
                .Row(new { Name = "Rum", Description = "Rum", Price = 0 })
                .Row(new { Name = "Powidła Śliwkowe", Description = "Domowe powidła śliwkowe", Price = 5 });

            Insert.IntoTable("BaseProduct")
                .Row(new { Name = "Tort", Category = "Torty", Price = 50, Unit = "kg" })
                .Row(new { Name = "Mono-deser", Category = "Mono-desery", Price = 3, Unit = "kg" });

            Insert.IntoTable("Cake")
                .Row(new { Name = "Jasny Biszkopt", Type = "Biszkopt tradycyjny", Flavour = "Wanilia", Description = "Tradycyjny biszkopt waniliowy", Price = 10 })
                .Row(new { Name = "Ciemny Biszkopt", Type = "Biszkopt tradycyjny", Flavour = "Kakao", Description = "Tradycyjny biszkopt kakaowy", Price = 10 })
                .Row(new { Name = "Biszkopt Orzechowy", Type = "Biszkopt tradycyjny", Flavour = "Orzechowy", Description = "Tradycyjny biszkopt orzechowy", Price = 10 })
                .Row(new { Name = "Biszkopt Piernikowy", Type = "Biszkopt tradycyjny", Flavour = "Piernik", Description = "Tradycyjny ciemny biszkopt o smaku piernikowym", Price = 10 })
                .Row(new
                {
                    Name = "Ciasto Czekoladowe",
                    Type = "Ciasto Madeira - Biszkopt tłuszczowy",
                    Flavour = "Czekolada",
                    Description = "Ciasto Madeira idealnie nadaje się do formowania. Jest puszyste, zwarte i wilgotne. Nie kruszy się tak jak biszkopt tradycyjny.",
                    Price = 10
                })
                .Row(new { Name = "Jasny Biszkopt Bezglutenowy", Type = "Biszkopt bezglutenowy", Flavour = "Wanilia", Description = "Biszkopt waniliowy bezglutenowy", Price = 10 })
                .Row(new { Name = "Ciemny Biszkopt Bezglutenowy", Type = "Biszkopt bezglutenowy", Flavour = "Kakao", Description = "Biszkopt kakaowy bezglutenowy", Price = 10 })
                .Row(new { Name = "Piernikowy Biszkopt Bezglutenowy", Type = "Biszkopt bezglutenowy", Flavour = "Piernik", Description = "Biszkopt piernikowy bezglutenowy", Price = 10 })
                .Row(new { Name = "Jasny Biszkopt Wegański", Type = "Biszkopt wegański", Flavour = "Wanilia", Description = "Biszkopt waniliowy bezglutenowy", Price = 10 })
                .Row(new { Name = "Ciemny Biszkopt Wegański", Type = "Biszkopt wegański", Flavour = "Kakao", Description = "Biszkopt kakaowy wegański", Price = 10 })
                .Row(new { Name = "Piernikowy Biszkopt Wegański", Type = "Biszkopt wegański", Flavour = "Piernik", Description = "Biszkopt piernikowy wegański", Price = 10 })
                .Row(new { Name = "Biszkopt Orzechowy Bezglutenowy", Type = "Biszkopt bezglutenowy", Flavour = "Orzechowy", Description = "Biszkopt orzechowy bezglutenowy", Price = 10 })
                .Row(new { Name = "Biszkopt Orzechowy Wegański", Type = "Biszkopt wegański", Flavour = "Orzechowy", Description = "Biszkopt orzechowy wegański", Price = 10 });

            Insert.IntoTable("Cream")
                .Row(new { Name = "Krem Waniliowy", Type = "Krem Mascarpone-Bita śmietana", Flavour = "Wanilia", Description = "Krem na bazie serka Mascarpone i bitej śmietany o smaku waniliowym", Price = 10 })
                .Row(new { Name = "Krem Czekoladowy", Type = "Krem Mascarpone-Bita śmietana", Flavour = "Czekolada", Description = "Krem na bazie serka Mascarpone i bitej śmietany o smaku czekoladowym", Price = 10 })
                .Row(new { Name = "Krem Biała Czekoladowa", Type = "Krem Mascarpone-Bita śmietana", Flavour = "Biała Czekolada", Description = "Krem na bazie serka Mascarpone i bitej śmietany o smaku białej czekolady", Price = 10 })
                .Row(new { Name = "Krem Palona Biała Czekolada", Type = "Krem Mascarpone-Bita śmietana", Flavour = "Palona Biała Czekolada", Description = "Krem na bazie serka Mascarpone i bitej śmietany o smaku palonej białej czekolady", Price = 10 })
                .Row(new { Name = "Krem Orzechowy", Type = "Krem Mascarpone-Bita śmietana", Flavour = "Orzechowy", Description = "Krem na bazie serka Mascarpone i bitej śmietany o smaku orzechowym", Price = 10 })
                .Row(new { Name = "Krem Kawowy", Type = "Krem Mascarpone-Bita śmietana", Flavour = "Kawowy", Description = "Krem kawowy na bazie serka Mascarpone i bitej śmietany", Price = 10 })
                .Row(new { Name = "Krem Śmietankowy", Type = "Krem Mascarpone-Bita śmietana", Flavour = "Śmietanka", Description = "Krem na bazie serka Mascarpone i bitej śmietany", Price = 10 })
                .Row(new { Name = "Krem Owocowy", Type = "Krem Mascarpone-Bita śmietana", Flavour = "Owocowy", Description = "Krem na bazie serka Mascarpone i bitej śmietany z dodatkiem dowolnych owoców", Price = 10 })
                .Row(new { Name = "Bita Śmietana", Type = "Bita Śmietana", Flavour = "Śmietanka", Description = "Bita Śmietana", Price = 10 })
                .Row(new { Name = "Krem Tiramisu", Type = "Krem Mascarpone", Flavour = "Śmietanka", Description = "Mascarpone z ubitymi żółtkami oraz pianą z białek", Price = 10 })
                .Row(new { Name = "Krem Pistacjowy", Type = "Krem Patissiere", Flavour = "Pistacjowy", Description = "Krem pistacjowy", Price = 10 })
                .Row(new { Name = "Krem Budyniowy", Type = "Krem Patissiere", Flavour = "Wanilia", Description = "Tradycyjny krem patissiere", Price = 10 })
                .Row(new { Name = "Krem Whisky", Type = "Krem Mascarpone-Bita śmietana", Flavour = "Whisky", Description = "Krem na bazie serka Mascarpone i bitej śmietany z dodatkiem Whisky", Price = 10 });

            Insert.IntoTable("Decoration")
               .Row(new { Name = "Świeże owoce", Description = "Dowolne świeże owoce dostępne w danym sezonie", Price = 5 })
               .Row(new { Name = "Kwiaty", Description = "Dowolne kwiaty", Price = 5 })
               .Row(new { Name = "Kwiaty suszone", Description = "Dowolne kwiaty suszone", Price = 0 })
               .Row(new { Name = "Czekoladki Kinder", Description = "Mix słodyczy Kinder", Price = 5 })
               .Row(new { Name = "Czekolada", Description = "Kawałki czekolady: do wyboru mleczna, gorzka oraz biała lub mix", Price = 5 })
               .Row(new { Name = "Beziki", Description = "Małe beziki", Price = 0 })
               .Row(new { Name = "Drip Czekoladowy", Description = "Drip czekoladowy: do wybory czekolada mleczna, gorzka oraz biała.", Price = 5 })
               .Row(new { Name = "Mirror Glaze", Description = "Gładka polewa mirror glaze.", Price = 5 })
               .Row(new { Name = "Brokat jadalny", Description = "Jadalny brokat.", Price = 5 })
               .Row(new { Name = "Jadalne złoto", Description = "Jadalne złoto.", Price = 10 })
               .Row(new { Name = "Perełki", Description = "Jadalne perełki.", Price = 0 })
               .Row(new { Name = "Mini buteleczki Jack Daniels / Jim Bean", Description = "Malutkie buteleczki z whisky", Price = 10 })
               .Row(new { Name = "Pierniczki", Description = "Domowe pierniczki", Price = 5 })
               .Row(new { Name = "Tynk", Description = "Warstwa kremu na torcie w dowolnym kolorze.", Price = 0 })
               .Row(new { Name = "Naked Cake", Description = "Brak warsty na torcie, tzn. Naked Cake - tort nieotynkowany. Cudownie prezentuje wszystkie warstwy tortu.", Price = 0 });
        }
    }
}
