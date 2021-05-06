using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bakery.Migrations
{
    [Migration(20210224210600)]
    public class _20210224210600_InsertTemplatesForMonoDesserts : ForwardOnlyMigration
    {
        public override void Up()
        {
            Insert.IntoTable("Cake")
                .Row(new { Name = "Brownie", Type = "Brownie", Flavour = "Czekolada", Price = 10, Description = "Klasyczne ciasto brownie" })
                .Row(new { Name = "Kruchy Spód", Type = "Ciasto Kruche", Flavour = "Czekolada", Price = 10, Description = "Klasyczne kruche ciasto waniliowe" });
            Insert.IntoTable("Cream")
                .Row(new { Name = "Mus Jogurtowy", Type = "Mus", Flavour = "Jogurt", Description = "Mus jogurtowy na bazie jogurtu", Price = 10 })
                .Row(new { Name = "Mus Waniliowy", Type = "Mus", Flavour = "Wanilia/Biała Czekolada", Description = "Mus waniliowy na bazie białej czekolady Callebaut", Price = 10 })
                .Row(new { Name = "Mus Czekoladowy", Type = "Mus", Flavour = "Czekolada", Description = "Mus czekoladowy na bazie belgijskiej czekolady deserowej Callebaut 54,5%", Price = 10 })
                .Row(new { Name = "Mus Palona Biała Czekolada", Type = "Mus", Flavour = "Palona Biała Czekolada", Description = "Mus na bazie palonej białej czekolady Callebaut", Price = 10 })
                .Row(new { Name = "Mus Kawowy", Type = "Mus", Flavour = "Kawa", Description = "Mus kawowy", Price = 10 })
                .Row(new { Name = "Mus Marakuja", Type = "Mus", Flavour = "Marakuja", Description = "Mus na bazie marakui", Price = 10 })
                .Row(new { Name = "Mus Owoce Leśne", Type = "Mus", Flavour = "Owoce Leśne", Description = "Mus 'owoce leśne' na bazie truskawki, jeżyny i czarnej porzeczki", Price = 10 })
                .Row(new { Name = "Mus Sernik", Type = "Mus", Flavour = "Serowy", Description = "Mus serowy na bazie serka Philadelphia", Price = 10 })
                .Row(new { Name = "Mus Biała Czekolada", Type = "Mus", Flavour = "Biała Czekolada", Description = "Mus na bazie białej czekolady", Price = 10 })
                .Row(new { Name = "Mus Malina/Truskawka", Type = "Mus", Flavour = "Malina/Truskawka", Description = "Mus malinowo-truskawkowy", Price = 10 })
                .Row(new { Name = "Mus Malina", Type = "Mus", Flavour = "Malina", Description = "Mus malinowy", Price = 10 })
                .Row(new { Name = "Mus Mleczna Czekolada/Malina", Type = "Mus", Flavour = "Mleczna Czekolada/Malina", Description = "Mus malinowy na bazie mlecznej czekolady", Price = 10 })
                .Row(new { Name = "Mus Czekolada/Whisky", Type = "Mus", Flavour = "Czekolada/Whisky", Description = "Alkoholowy mus czekoladowy z whisky Ballantine's", Price = 10 })
                .Row(new { Name = "Lemon Curd", Type = "Mus", Flavour = "Cytryna", Description = "Krem cytrynowy - lemon curd", Price = 10 });
          
            Insert.IntoTable("Additional")
                .Row(new { Name = "Sos Owocowy", Description = "Domowy sos z dowolnych owoców", Price = 5 })
                .Row(new { Name = "Herbatnik", Description = "Kruchy słony herbatnik", Price = 0 })
                .Row(new { Name = "Syrop Z Marakui", Description = "Syrop z marakui", Price = 5 })
                .Row(new { Name = "Kruche Ciastko Kakaowe", Description = "Kakaowe ciastko kruche 'sable breton'", Price = 0 })
                .Row(new { Name = "Likier Baileys", Description = "", Price = 5 })
                .Row(new { Name = "Ciastko Waniliowe", Description = "Klasyczne tarte kruche ciastkoi", Price = 0 })
                .Row(new { Name = "Ciastko Maślane", Description = "Kruche ciastko maślane", Price = 0 })
                .Row(new { Name = "Wiśnie W Sosie Wiśniowym", Description = "Wiśnie w sosie wiśniowym", Price = 5 })
                .Row(new { Name = "Ganache Biała Czekolada/Wanilia", Description = "Ganache na bazie białej czekolady z dodatkiem samodzielmnie wykonanego ekstraktu z wanilii", Price = 0 })
                .Row(new { Name = "Ganache Czekoladowy", Description = "Ganache czekoladowy na bazie kremu angielskiego", Price = 5 });

            Insert.IntoTable("Decoration")
                .Row(new { Name = "Czekoladowe Kamienie", Description = "Herbatniki obtoczone w belgijskiej czekoladzie deserowej", Price = 5 })
                .Row(new { Name = "Beza Włoska", Description = "Opalana palnikiem beza włoska", Price = 5 })
                .Row(new { Name = "Złoty Płatek Czekoladowy", Description = "Płatek czekolady deserowej z 23-karatowym złotem jadalnym", Price = 5 })
                .Row(new { Name = "Płatek Czekoladowy", Description = "Płatek czekolady deserowej", Price = 0 });

            Execute.Sql(@"Insert into [dbo].[Order] (BaseProduct_Id, Cake_Id, Cream_Id, Servings, TotalPrice, IsTemplate, TemplateName) Values
                ((Select BaseProduct_Id From [dbo].[BaseProduct] Where [Name] = 'Mono-deser'), 
                 (Select Cake_Id From [dbo].[Cake] Where [Name] = 'Ciemny Biszkopt'), 
                 (Select Cream_Id From [dbo].[Cream] Where [Name] = 'Mus Jogurtowy'), 1, 14, 1, 'Jogurt/Truskawka/Czarna porzeczka');
                Insert into [dbo].[OrdersAdditionals] (Order_Id, Additional_Id, Quantity, Details) Values
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Jogurt/Truskawka/Czarna porzeczka'), 
                 (Select Additional_Id From [dbo].[Additional] Where [Name] like 'Sos Owocowy'), 1, 'Sos z truskawek'),
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Jogurt/Truskawka/Czarna porzeczka'), 
                 (Select Additional_Id From [dbo].[Additional] Where [Name] like 'Konfitura z Czarnej Porzeczki'), 1, ''),
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Jogurt/Truskawka/Czarna porzeczka'), 
                 (Select Additional_Id From [dbo].[Additional] Where [Name] like 'Herbatnik'), 1, '');
                Insert into [dbo].[OrdersDecorations] (Order_Id, Decoration_Id, Quantity, Details) Values
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Jogurt/Truskawka/Czarna porzeczka'),
                 (Select Decoration_Id From [dbo].[Decoration] Where [Name] like 'Mirror Glaze'), 1, 'Polewa z białej czekolady'),
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Jogurt/Truskawka/Czarna porzeczka'),
                 (Select Decoration_Id From [dbo].[Decoration] Where [Name] like 'Kwiaty suszone'), 1, '');");

            Execute.Sql(@"Insert into [dbo].[Order] (BaseProduct_Id, Cake_Id, Cream_Id, Servings, TotalPrice, IsTemplate, TemplateName) Values
                ((Select BaseProduct_Id From [dbo].[BaseProduct] Where [Name] = 'Mono-deser'), 
                 (Select Cake_Id From [dbo].[Cake] Where [Name] = 'Ciemny Biszkopt'), 
                 (Select Cream_Id From [dbo].[Cream] Where [Name] = 'Mus Waniliowy'), 1, 11.50, 1, 'Wanilia/Jagoda');
                Insert into [dbo].[OrdersAdditionals] (Order_Id, Additional_Id, Quantity, Details) Values
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Wanilia/Jagoda'), 
                 (Select Additional_Id From [dbo].[Additional] Where [Name] like 'Sos Owocowy'), 1, 'Sos z jagód'),
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Wanilia/Jagoda'), 
                 (Select Additional_Id From [dbo].[Additional] Where [Name] like 'Herbatnik'), 1, '');
                Insert into [dbo].[OrdersDecorations] (Order_Id, Decoration_Id, Quantity, Details) Values
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Wanilia/Jagoda'),
                 (Select Decoration_Id From [dbo].[Decoration] Where [Name] like 'Mirror Glaze'), 1, 'Polewa z ciemnej czekolady'),
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Wanilia/Jagoda'),
                 (Select Decoration_Id From [dbo].[Decoration] Where [Name] like 'Kwiaty suszone'), 1, '');");

            Execute.Sql(@"Insert into [dbo].[Order] (BaseProduct_Id, Cake_Id, Cream_Id, Servings, TotalPrice, IsTemplate, TemplateName) Values
                ((Select BaseProduct_Id From [dbo].[BaseProduct] Where [Name] = 'Mono-deser'), 
                 (Select Cake_Id From [dbo].[Cake] Where [Name] = 'Jasny Biszkopt'), 
                 (Select Cream_Id From [dbo].[Cream] Where [Name] = 'Mus Czekoladowy'), 1, 14, 1, 'Mango/Marakuja/Czekolada');
                Insert into [dbo].[OrdersAdditionals] (Order_Id, Additional_Id, Quantity, Details) Values
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Mango/Marakuja/Czekolada'), 
                 (Select Additional_Id From [dbo].[Additional] Where [Name] like 'Syrop Z Marakui'), 1, 'Użyty do nasączenia biszkoptu'),
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Mango/Marakuja/Czekolada'), 
                 (Select Additional_Id From [dbo].[Additional] Where [Name] like 'Żelka Mango-Marakuja'), 1, ''),
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Mango/Marakuja/Czekolada'), 
                 (Select Additional_Id From [dbo].[Additional] Where [Name] like 'Herbatnik'), 1, '');
                Insert into [dbo].[OrdersDecorations] (Order_Id, Decoration_Id, Quantity, Details) Values
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Mango/Marakuja/Czekolada'),
                 (Select Decoration_Id From [dbo].[Decoration] Where [Name] like 'Mirror Glaze'), 1, 'Polewa z ciemnej czekolady'),
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Mango/Marakuja/Czekolada'),
                 (Select Decoration_Id From [dbo].[Decoration] Where [Name] like 'Kwiaty suszone'), 1, '');");

            Execute.Sql(@"Insert into [dbo].[Order] (BaseProduct_Id, Cake_Id, Cream_Id, Servings, TotalPrice, IsTemplate, TemplateName) Values
                ((Select BaseProduct_Id From [dbo].[BaseProduct] Where [Name] = 'Mono-deser'), 
                 (Select Cake_Id From [dbo].[Cake] Where [Name] = 'Ciasto Czekoladowe'), 
                 (Select Cream_Id From [dbo].[Cream] Where [Name] = 'Mus Palona Biała Czekolada'), 1, 11.50, 1, 'Mango/Marakuja/Palona Czekolada');
                Insert into [dbo].[OrdersAdditionals] (Order_Id, Additional_Id, Quantity, Details) Values
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Mango/Marakuja/Palona Czekolada'), 
                 (Select Additional_Id From [dbo].[Additional] Where [Name] like 'Żelka Mango-Marakuja'), 1, ''),
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Mango/Marakuja/Palona Czekolada'), 
                 (Select Additional_Id From [dbo].[Additional] Where [Name] like 'Kruche Ciastko Kakaowe'), 1, '');
                Insert into [dbo].[OrdersDecorations] (Order_Id, Decoration_Id, Quantity, Details) Values
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Mango/Marakuja/Palona Czekolada'),
                 (Select Decoration_Id From [dbo].[Decoration] Where [Name] like 'Mirror Glaze'), 1, 'Polewa z białej czekolady'),
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Mango/Marakuja/Palona Czekolada'),
                 (Select Decoration_Id From [dbo].[Decoration] Where [Name] like 'Płatek Czekoladowy'), 1, '');");

            Execute.Sql(@"Insert into [dbo].[Order] (BaseProduct_Id, Cake_Id, Cream_Id, Servings, TotalPrice, IsTemplate, TemplateName) Values
                ((Select BaseProduct_Id From [dbo].[BaseProduct] Where [Name] = 'Mono-deser'), 
                 (Select Cake_Id From [dbo].[Cake] Where [Name] = 'Ciasto Czekoladowe'), 
                 (Select Cream_Id From [dbo].[Cream] Where [Name] = 'Mus Czekoladowy'), 1, 9, 1, 'Czekolada/Wanilia');
                Insert into [dbo].[OrdersAdditionals] (Order_Id, Additional_Id, Quantity, Details) Values
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Czekolada/Wanilia'), 
                 (Select Additional_Id From [dbo].[Additional] Where [Name] like 'Ganache Biała Czekolada/Wanilia'), 1, ''),
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Czekolada/Wanilia'), 
                 (Select Additional_Id From [dbo].[Additional] Where [Name] like 'Ciastko Waniliowe'), 1, '');
                Insert into [dbo].[OrdersDecorations] (Order_Id, Decoration_Id, Quantity, Details) Values
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Czekolada/Wanilia'),
                 (Select Decoration_Id From [dbo].[Decoration] Where [Name] like 'Mirror Glaze'), 1, 'Polewa z ciemnej czekolady');");

            Execute.Sql(@"Insert into [dbo].[Order] (BaseProduct_Id, Cake_Id, Cream_Id, Servings, TotalPrice, IsTemplate, TemplateName) Values
                ((Select BaseProduct_Id From [dbo].[BaseProduct] Where [Name] = 'Mono-deser'), 
                 (Select Cake_Id From [dbo].[Cake] Where [Name] = 'Ciemny Biszkopt'), 
                 (Select Cream_Id From [dbo].[Cream] Where [Name] = 'Mus Kawowy'), 1, 15, 1, 'Kawa/Baileys/Czarna Poczeczka');
                Insert into [dbo].[OrdersAdditionals] (Order_Id, Additional_Id, Quantity, Details) Values
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Kawa/Baileys/Czarna Poczeczka'), 
                 (Select Additional_Id From [dbo].[Additional] Where [Name] like 'Likier Baileys'), 1, 'Do nasączenia biszkoptu'),
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Kawa/Baileys/Czarna Poczeczka'), 
                 (Select Additional_Id From [dbo].[Additional] Where [Name] like 'Konfitura z Czarnej Porzeczki'), 1, '');
                Insert into [dbo].[OrdersDecorations] (Order_Id, Decoration_Id, Quantity, Details) Values
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Kawa/Baileys/Czarna Poczeczka'),
                 (Select Decoration_Id From [dbo].[Decoration] Where [Name] like 'Mirror Glaze'), 1, 'Polewa z ciemnej czekolady'),
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Kawa/Baileys/Czarna Poczeczka'),
                 (Select Decoration_Id From [dbo].[Decoration] Where [Name] like 'Czekoladowe Kamienie'), 1, '');");

            Execute.Sql(@"Insert into [dbo].[Order] (BaseProduct_Id, Cake_Id, Cream_Id, Servings, TotalPrice, IsTemplate, TemplateName) Values
                ((Select BaseProduct_Id From [dbo].[BaseProduct] Where [Name] = 'Mono-deser'), 
                 (Select Cake_Id From [dbo].[Cake] Where [Name] = 'Jasny Biszkopt'), 
                 (Select Cream_Id From [dbo].[Cream] Where [Name] = 'Mus Marakuja'), 1, 14, 1, 'Marakuja/Czekolada');
                Insert into [dbo].[OrdersAdditionals] (Order_Id, Additional_Id, Quantity, Details) Values
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Marakuja/Czekolada'), 
                 (Select Additional_Id From [dbo].[Additional] Where [Name] like 'Ganache Czekoladowy'), 1, ''),
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Marakuja/Czekolada'), 
                 (Select Additional_Id From [dbo].[Additional] Where [Name] like 'Konfitura Mango-Marakuja'), 1, ''),
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Marakuja/Czekolada'), 
                 (Select Additional_Id From [dbo].[Additional] Where [Name] like 'Ciastko Waniliowe'), 1, '');
                Insert into [dbo].[OrdersDecorations] (Order_Id, Decoration_Id, Quantity, Details) Values
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Marakuja/Czekolada'),
                 (Select Decoration_Id From [dbo].[Decoration] Where [Name] like 'Mirror Glaze'), 1, 'Polewa z białej czekolady'),
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Marakuja/Czekolada'),
                 (Select Decoration_Id From [dbo].[Decoration] Where [Name] like 'Płatek Czekoladowy'), 1, '');");

            Execute.Sql(@"Insert into [dbo].[Order] (BaseProduct_Id, Cake_Id, Cream_Id, Servings, TotalPrice, IsTemplate, TemplateName) Values
                ((Select BaseProduct_Id From [dbo].[BaseProduct] Where [Name] = 'Mono-deser'), 
                 (Select Cake_Id From [dbo].[Cake] Where [Name] = 'Brownie'), 
                 (Select Cream_Id From [dbo].[Cream] Where [Name] = 'Mus Owoce Leśne'), 1, 14, 1, 'Owowce Leśne');
                Insert into [dbo].[OrdersAdditionals] (Order_Id, Additional_Id, Quantity, Details) Values
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Owowce Leśne'), 
                 (Select Additional_Id From [dbo].[Additional] Where [Name] like 'Sos Owocowy'), 1, 'Z truskawek, jeżyn i czarnej porzeczki'),
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Owowce Leśne'), 
                 (Select Additional_Id From [dbo].[Additional] Where [Name] like 'Konfitura z Czarnej Porzeczki'), 1, ''),
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Owowce Leśne'), 
                 (Select Additional_Id From [dbo].[Additional] Where [Name] like 'Herbatnik'), 1, '');
                Insert into [dbo].[OrdersDecorations] (Order_Id, Decoration_Id, Quantity, Details) Values
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Owowce Leśne'),
                 (Select Decoration_Id From [dbo].[Decoration] Where [Name] like 'Mirror Glaze'), 1, 'Polewa z ciemnej czekolady'),
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Owowce Leśne'),
                 (Select Decoration_Id From [dbo].[Decoration] Where [Name] like 'Płatek Czekoladowy'), 1, ''),
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Owowce Leśne'),
                 (Select Decoration_Id From [dbo].[Decoration] Where [Name] like 'Kwiaty suszone'), 1, '');");

            Execute.Sql(@"Insert into [dbo].[Order] (BaseProduct_Id, Cake_Id, Cream_Id, Servings, TotalPrice, IsTemplate, TemplateName) Values
                ((Select BaseProduct_Id From [dbo].[BaseProduct] Where [Name] = 'Mono-deser'), 
                 (Select Cake_Id From [dbo].[Cake] Where [Name] = 'Jasny Biszkopt'), 
                 (Select Cream_Id From [dbo].[Cream] Where [Name] = 'Mus Sernik'), 1, 9, 1, 'Serniczek/Czekolada');
                Insert into [dbo].[OrdersAdditionals] (Order_Id, Additional_Id, Quantity, Details) Values
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Serniczek/Czekolada'), 
                 (Select Additional_Id From [dbo].[Additional] Where [Name] like 'Ganache Biała Czekolada/Wanilia'), 1, ''),
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Serniczek/Czekolada'), 
                 (Select Additional_Id From [dbo].[Additional] Where [Name] like 'Ciastko Maślane'), 1, '');
                Insert into [dbo].[OrdersDecorations] (Order_Id, Decoration_Id, Quantity, Details) Values
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Serniczek/Czekolada'),
                 (Select Decoration_Id From [dbo].[Decoration] Where [Name] like 'Mirror Glaze'), 1, 'Polewa z jasnej czekolady'),
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Serniczek/Czekolada'),
                 (Select Decoration_Id From [dbo].[Decoration] Where [Name] like 'Perełki'), 1, ''),
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Serniczek/Czekolada'),
                 (Select Decoration_Id From [dbo].[Decoration] Where [Name] like 'Kwiaty suszone'), 1, '');");

            Execute.Sql(@"Insert into [dbo].[Order] (BaseProduct_Id, Cake_Id, Cream_Id, Servings, TotalPrice, IsTemplate, TemplateName) Values
                ((Select BaseProduct_Id From [dbo].[BaseProduct] Where [Name] = 'Mono-deser'), 
                 (Select Cake_Id From [dbo].[Cake] Where [Name] = 'Jasny Biszkopt'), 
                 (Select Cream_Id From [dbo].[Cream] Where [Name] = 'Mus Sernik'), 1, 11.50, 1, 'Serniczek/Malina');
                Insert into [dbo].[OrdersAdditionals] (Order_Id, Additional_Id, Quantity, Details) Values
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Serniczek/Malina'), 
                 (Select Additional_Id From [dbo].[Additional] Where [Name] like 'Konfitura Malinowa'), 1, ''),
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Serniczek/Malina'), 
                 (Select Additional_Id From [dbo].[Additional] Where [Name] like 'Ciastko Maślane'), 1, '');
                Insert into [dbo].[OrdersDecorations] (Order_Id, Decoration_Id, Quantity, Details) Values
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Serniczek/Malina'),
                 (Select Decoration_Id From [dbo].[Decoration] Where [Name] like 'Mirror Glaze'), 1, 'Polewa z jasnej czekolady'),
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Serniczek/Malina'),
                 (Select Decoration_Id From [dbo].[Decoration] Where [Name] like 'Beziki'), 1, '');");

            Execute.Sql(@"Insert into [dbo].[Order] (BaseProduct_Id, Cake_Id, Cream_Id, Servings, TotalPrice, IsTemplate, TemplateName) Values
                ((Select BaseProduct_Id From [dbo].[BaseProduct] Where [Name] = 'Mono-deser'), 
                 (Select Cake_Id From [dbo].[Cake] Where [Name] = 'Ciemny Biszkopt'), 
                 (Select Cream_Id From [dbo].[Cream] Where [Name] = 'Mus Biała Czekolada'), 1, 14, 1, 'Szwarcwald');
                Insert into [dbo].[OrdersAdditionals] (Order_Id, Additional_Id, Quantity, Details) Values
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Szwarcwald'), 
                 (Select Additional_Id From [dbo].[Additional] Where [Name] like 'Wiśnie W Sosie Wiśniowym'), 1, ''),
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Szwarcwald'), 
                 (Select Additional_Id From [dbo].[Additional] Where [Name] like 'Kruche Ciastko Kakaowe'), 1, ''),
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Szwarcwald'), 
                 (Select Additional_Id From [dbo].[Additional] Where [Name] like 'Czekolada'), 1, 'Pokruszona belgijska czekolada');
                Insert into [dbo].[OrdersDecorations] (Order_Id, Decoration_Id, Quantity, Details) Values
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Szwarcwald'),
                 (Select Decoration_Id From [dbo].[Decoration] Where [Name] like 'Mirror Glaze'), 1, 'Polewa z ciemnej czekolady');");

            Execute.Sql(@"Insert into [dbo].[Order] (BaseProduct_Id, Cake_Id, Cream_Id, Servings, TotalPrice, IsTemplate, TemplateName) Values
                ((Select BaseProduct_Id From [dbo].[BaseProduct] Where [Name] = 'Mono-deser'), 
                 (Select Cake_Id From [dbo].[Cake] Where [Name] = 'Kruchy Spód'), 
                 (Select Cream_Id From [dbo].[Cream] Where [Name] = 'Lemon Curd'), 1, 9, 1, 'Tartaletka Lemon Curd/Beza Włoska');
                Insert into [dbo].[OrdersDecorations] (Order_Id, Decoration_Id, Quantity, Details) Values
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Tartaletka Lemon Curd/Beza Włoska'),
                 (Select Decoration_Id From [dbo].[Decoration] Where [Name] like 'Beza Włoska'), 1, '');");
        }
    }
}
