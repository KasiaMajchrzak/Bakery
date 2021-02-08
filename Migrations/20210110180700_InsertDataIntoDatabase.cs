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
                .Row(new { Name = "Dżem Mango-Marakuja", Description = "Dżem z mango i marakui", Price = 5 })
                .Row(new { Name = "Dżem Owoce Leśne", Description = "Dżem z owoców leśnych", Price = 5 })
                .Row(new { Name = "Dżem Truskawkowy", Description = "Dżem z truskawek", Price = 5 })
                .Row(new { Name = "Dżem Malinowy", Description = "Dżem z malin", Price = 5 })
                .Row(new { Name = "Dżem Morelowo-Rozmarynowy", Description = "Dżem z moreli z dodatkiem rozmarynu", Price = 65 })
                .Row(new { Name = "Żelka Mango-Marakuja", Description = "Żelka zrobiona z dżemu", Price = 5 })
                .Row(new { Name = "Żelka Owoce Leśne", Description = "Żelka zrobiona z dżemu", Price = 5 })
                .Row(new { Name = "Żelka Truskawkowa", Description = "Żelka zrobiona z dżemu", Price = 5 })
                .Row(new { Name = "Żelka Morelowo-Rozmarynowa", Description = "Żelka zrobiona z dżemu", Price = 5 })
                .Row(new { Name = "Żelka Malinowa", Description = "Żelka zrobiona z dżemu", Price = 5 })
                .Row(new { Name = "Czekolada", Description = "Kawałki czekolady: do wyboru mleczna, gorzka oraz biała", Price = 5 })
                .Row(new { Name = "Blat bezowy", Description = "Cienki blat bezowy", Price = 5 });

            Insert.IntoTable("BaseProduct")
                .Row(new { Name = "Tort", Category = "Torty", Price = 50, Unit = "kg" })
                .Row(new { Name = "Mono-deser", Category = "Mono-desery", Price = 3, Unit = "kg" });

            Insert.IntoTable("Cake")
                .Row(new { Name = "Jasny Biszkopt", Type = "Biszkopt tradycyjny", Flavour = "Wanilia", Description = "Tradycyjny biszkopt waniliowy", Price = 10 })
                .Row(new { Name = "Ciemny Biszkopt", Type = "Biszkopt tradycyjny", Flavour = "Kakao", Description = "Tradycyjny biszkopt kakaowy", Price = 10 })
                .Row(new { Name = "Biszkopt Orzechowy", Type = "Biszkopt tradycyjny", Flavour = "Orzechowy", Description = "Tradycyjny biszkopt orzechowy", Price = 10 })
                .Row(new { Name = "Biszkopt Piernikowy", Type = "Biszkopt tradycyjny", Flavour = "Piernik", Description = "Tradycyjny ciemny biszkopt o smaku piernikowym", Price = 10 })
                .Row(new { Name = "Ciasto Czekoladowe", Type = "Ciasto Madeira - Biszkopt tłuszczowy", Flavour = "Czekolada",
                    Description = "Ciasto Madeira idealnie nadaje się do formowania. Jest puszyste, zwarte i wilgotne. Nie kruszy się tak jak biszkopt tradycyjny.", Price = 10 })
                .Row(new { Name = "Jasny Biszkopt Bezglutenowy", Type = "Biszkopt bezglutenowy", Flavour = "Wanilia", Description = "Biszkopt waniliowy bezglutenowy", Price = 10 })
                .Row(new { Name = "Ciemny Biszkopt Bezglutenowy", Type = "Biszkopt bezglutenowy", Flavour = "Kakao", Description = "Biszkopt kakaowy bezglutenowy", Price = 10 })
                .Row(new { Name = "Piernikowy Biszkopt Bezglutenowy", Type = "Biszkopt bezglutenowy", Flavour = "Piernik", Description = "Biszkopt piernikowy bezglutenowy", Price = 10 })
                .Row(new { Name = "Jasny Biszkopt Wegański", Type = "Biszkopt wegański", Flavour = "Wanilia", Description = "Biszkopt waniliowy bezglutenowy", Price = 10 })
                .Row(new { Name = "Ciemny Biszkopt Wegański", Type = "Biszkopt wegański", Flavour = "Kakao", Description = "Biszkopt kakaowy wegański", Price = 10 })
                .Row(new { Name = "Piernikowy Biszkopt Wegański", Type = "Biszkopt wegański", Flavour = "Piernik", Description = "Biszkopt piernikowy wegański", Price = 10 })
                .Row(new { Name = "Biszkopt Orzechowy Bezglutenowy", Type = "Biszkopt bezglutenowy", Flavour = "Orzechowy", Description = "Biszkopt orzechowy bezglutenowy", Price = 10 })
                .Row(new { Name = "Biszkopt Orzechowy Wegański", Type = "Biszkopt wegański", Flavour = "Orzechowy", Description = "Biszkopt orzechowy wegański", Price = 10 });

            Insert.IntoTable("Cream")
                .Row(new { Name = "Krem Waniliowy", Type = "Krem maślany", Flavour = "Wanilia", Description = "Tradycyjny krem maślany waniliowy", Price = 10 })
                .Row(new { Name = "Krem Czekoladowy", Type = "Krem maślany", Flavour = "Czekolada", Description = "Tradycyjny krem maślany czekoladowy", Price = 10 })
                .Row(new { Name = "Krem Orzechowy", Type = "Krem maślany", Flavour = "Orzechowy", Description = "Tradycyjny krem maślany orzechowy", Price = 10 })
                .Row(new { Name = "Krem Kawowy", Type = "Krem maślany", Flavour = "Kawowy", Description = "Tradycyjny krem maślany kawowy", Price = 10 })
                .Row(new { Name = "Krem Śmietankowy", Type = "Krem Mascarpone-Bita śmietana", Flavour = "Śmietanka", Description = "Krem na bazie serka Mascarpone i bitej śmietany", Price = 10 })
                .Row(new { Name = "Krem Owocowy", Type = "Krem Mascarpone-Bita śmietana", Flavour = "Owocowy", Description = "Krem na bazie serka Mascarpone i bitej śmietany z dodatkiem dowolnych owoców", Price = 10 })
                .Row(new { Name = "Bita Śmietana", Type = "Bita Śmietana", Flavour = "Śmietanka", Description = "Bita Śmietana", Price = 10})
                .Row(new { Name = "Krem Tiramisu", Type = "Krem Mascarpone", Flavour = "Śmietanka", Description = "Mascarpone z ubitymi żółtkami oraz pianą z białek", Price = 10 })
                .Row(new { Name = "Krem Pistacjowy", Type = "Krem Patissiere", Flavour = "Pistacjowy", Description = "Krem pistacjowy", Price = 10})
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
               .Row(new { Name = "Mini buteleczki Jack Daniels / Jim Bean", Description = "Malutkie buteleczki z whisky", Price = 10 });
        }
    }
}
