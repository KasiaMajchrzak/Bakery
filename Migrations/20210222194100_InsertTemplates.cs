using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bakery.Migrations
{
    [Migration(20210222194800)]
    public class _20210222194100_InsertTemplates : ForwardOnlyMigration
    {
        public override void Up()
        {
            if (!Schema.Table("OrdersAdditionals").Column("Details").Exists())
            {
                Alter.Table("OrdersAdditionals").AddColumn("Details").AsString().Nullable();
            }
            if (!Schema.Table("OrdersDecorations").Column("Details").Exists())
            {
                Alter.Table("OrdersDecorations").AddColumn("Details").AsString().Nullable();
            }

            Execute.Sql(@"Insert into [dbo].[Order] (BaseProduct_Id, Cake_Id, Cream_Id, Servings, TotalPrice, IsTemplate, TemplateName) Values
                ((Select BaseProduct_Id From [dbo].[BaseProduct] Where [Name] = 'Tort'), 
                 (Select Cake_Id From [dbo].[Cake] Where [Name] = 'Ciasto Czekoladowe'), 
                 (Select Cream_Id From [dbo].[Cream] Where [Name] = 'Krem Czekoladowy'), 1, 85, 1, 'Czekolada/Czarna porzeczka'),
                ((Select BaseProduct_Id From [dbo].[BaseProduct] Where [Name] = 'Tort'), 
                 (Select Cake_Id From [dbo].[Cake] Where [Name] = 'Ciemny Biszkopt'), 
                 (Select Cream_Id From [dbo].[Cream] Where [Name] = 'Krem Czekoladowy'), 1, 90, 1, 'Czekolada/Mango/Marakuja'),
                ((Select BaseProduct_Id From [dbo].[BaseProduct] Where [Name] = 'Tort'), 
                 (Select Cake_Id From [dbo].[Cake] Where [Name] = 'Ciemny Biszkopt'), 
                 (Select Cream_Id From [dbo].[Cream] Where [Name] = 'Krem Czekoladowy'), 1, 95, 1, 'Czekolada/Malina/Truskawka')");

            Execute.Sql(@"Insert into [dbo].[OrdersAdditionals] (Order_Id, Additional_Id, Quantity, Details) Values
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Czekolada/Czarna porzeczka'), 
                 (Select Additional_Id From [dbo].[Additional] Where [Name] like 'Konfitura z Czarnej Porzeczki'), 1, ''),
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Czekolada/Czarna porzeczka'), 
                 (Select Additional_Id From [dbo].[Additional] Where [Name] like 'Świeże owoce'), 1, 'Owoce leśne oraz czarna porzeczka'),
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Czekolada/Mango/Marakuja'), 
                 (Select Additional_Id From [dbo].[Additional] Where [Name] like 'Konfitura Mango-Marakuja'), 1, ''),
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Czekolada/Mango/Marakuja'), 
                 (Select Additional_Id From [dbo].[Additional] Where [Name] like 'Galaretka Mango-Marakuja'), 1, ''),
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Czekolada/Malina/Truskawka'), 
                 (Select Additional_Id From [dbo].[Additional] Where [Name] like 'Konfitura Malinowa'), 1, ''),
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Czekolada/Malina/Truskawka'), 
                 (Select Additional_Id From [dbo].[Additional] Where [Name] like 'Konfitura Truskawkowa'), 1, ''),
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Czekolada/Malina/Truskawka'), 
                 (Select Additional_Id From [dbo].[Additional] Where [Name] like 'Dodatkowy Smak Kremu'), 1, 'Krem malinowy')");

            Execute.Sql(@"Insert into [dbo].[OrdersDecorations] (Order_Id, Decoration_Id, Quantity, Details) Values
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Czekolada/Czarna porzeczka'),
                 (Select Decoration_Id From [dbo].[Decoration] Where [Name] like 'Tynk'), 1, 'Tynk z kremu z białej czekolady'),
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Czekolada/Czarna porzeczka'),
                 (Select Decoration_Id From [dbo].[Decoration] Where [Name] like 'Drip Czekoladowy'), 1, 'Drip z ciemnej czekolady'),
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Czekolada/Czarna porzeczka'),
                 (Select Decoration_Id From [dbo].[Decoration] Where [Name] like 'Kwiaty suszone'), 1, 'Różne kwiaty suszone jadalne'),
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Czekolada/Mango/Marakuja'),
                 (Select Decoration_Id From [dbo].[Decoration] Where [Name] like 'Tynk'), 1, 'Tynk z kremu z białej czekolady'),
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Czekolada/Mango/Marakuja'),
                 (Select Decoration_Id From [dbo].[Decoration] Where [Name] like 'Mirror Glaze'), 1, 'Mirror Glaze z białej czekolady'),
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Czekolada/Mango/Marakuja'),
                 (Select Decoration_Id From [dbo].[Decoration] Where [Name] like 'Kwiaty'), 1, ''),
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Czekolada/Mango/Marakuja'),
                 (Select Decoration_Id From [dbo].[Decoration] Where [Name] like 'Perełki'), 1, ''),
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Czekolada/Malina/Truskawka'),
                 (Select Decoration_Id From [dbo].[Decoration] Where [Name] like 'Naked Cake'), 1, ''),
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Czekolada/Malina/Truskawka'),
                 (Select Decoration_Id From [dbo].[Decoration] Where [Name] like 'Drip Czekoladowy'), 1, 'Drip z ciemnej czekolady'),
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Czekolada/Malina/Truskawka'),
                 (Select Decoration_Id From [dbo].[Decoration] Where [Name] like 'Świeże owoce'), 1, 'Maliny, truskawki i  owoce leśne'),
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Czekolada/Malina/Truskawka'),
                 (Select Decoration_Id From [dbo].[Decoration] Where [Name] like 'Beziki'), 1, '')");


            Execute.Sql(@"Insert into [dbo].[Order] (BaseProduct_Id, Cake_Id, Cream_Id, Servings, TotalPrice, IsTemplate, TemplateName) Values
                ((Select BaseProduct_Id From [dbo].[BaseProduct] Where [Name] = 'Tort'), 
                 (Select Cake_Id From [dbo].[Cake] Where [Name] = 'Ciasto Czekoladowe'), 
                 (Select Cream_Id From [dbo].[Cream] Where [Name] = 'Krem Palona Biała Czekolada'), 1, 95, 1, 'Palona Czekolada/Mango/Marakuja'),
                ((Select BaseProduct_Id From [dbo].[BaseProduct] Where [Name] = 'Tort'), 
                 (Select Cake_Id From [dbo].[Cake] Where [Name] = 'Ciasto Czekoladowe'), 
                 (Select Cream_Id From [dbo].[Cream] Where [Name] = 'Krem Kawowy'), 1, 80, 1, 'Kawa'),
                ((Select BaseProduct_Id From [dbo].[BaseProduct] Where [Name] = 'Tort'), 
                 (Select Cake_Id From [dbo].[Cake] Where [Name] = 'Ciemny Biszkopt'), 
                 (Select Cream_Id From [dbo].[Cream] Where [Name] = 'Krem Śmietankowy'), 1, 90, 1, 'Szwarcwaldzki'),
                ((Select BaseProduct_Id From [dbo].[BaseProduct] Where [Name] = 'Tort'), 
                 (Select Cake_Id From [dbo].[Cake] Where [Name] = 'Biszkopt Piernikowy'), 
                 (Select Cream_Id From [dbo].[Cream] Where [Name] = 'Krem Czekoladowy'), 1, 90, 1, 'Piernikowy')");

            Execute.Sql(@"Insert into [dbo].[OrdersAdditionals] (Order_Id, Additional_Id, Quantity, Details) Values
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Palona Czekolada/Mango/Marakuja'), 
                 (Select Additional_Id From [dbo].[Additional] Where [Name] like 'Czekolada'), 1, ''),
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Palona Czekolada/Mango/Marakuja'), 
                 (Select Additional_Id From [dbo].[Additional] Where [Name] like 'Konfitura Mango-Marakuja'), 1, 'Owoce leśne oraz czarna porzeczka'),
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Palona Czekolada/Mango/Marakuja'), 
                 (Select Additional_Id From [dbo].[Additional] Where [Name] like 'Galaretka Mango-Marakuja'), 1, ''),
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Kawa'), 
                 (Select Additional_Id From [dbo].[Additional] Where [Name] like 'Konfitura z Czarnej Porzeczki'), 1, ''),
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Szwarcwaldzki'), 
                 (Select Additional_Id From [dbo].[Additional] Where [Name] like 'Czekolada'), 1, ''),
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Szwarcwaldzki'), 
                 (Select Additional_Id From [dbo].[Additional] Where [Name] like 'Świeże owoce'), 1, 'Wiśnie'),
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Szwarcwaldzki'), 
                 (Select Additional_Id From [dbo].[Additional] Where [Name] like 'Wiśniówka'), 1, 'Lub domowy kompot wiśniowy'),
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Piernikowy'), 
                 (Select Additional_Id From [dbo].[Additional] Where [Name] like 'Powidła Śliwkowe'), 1, ''),
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Piernikowy'), 
                 (Select Additional_Id From [dbo].[Additional] Where [Name] like 'Rum'), 1, ''),
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Piernikowy'), 
                 (Select Additional_Id From [dbo].[Additional] Where [Name] like 'Dodatkowy Smak Kremu'), 1, 'Drugi rodzaj kremu czekoladowego')");

            Execute.Sql(@"Insert into [dbo].[OrdersDecorations] (Order_Id, Decoration_Id, Quantity, Details) Values
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Palona Czekolada/Mango/Marakuja'),
                 (Select Decoration_Id From [dbo].[Decoration] Where [Name] like 'Tynk'), 1, 'Tynk z kremu z palonej białej czekolady'),
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Palona Czekolada/Mango/Marakuja'),
                 (Select Decoration_Id From [dbo].[Decoration] Where [Name] like 'Drip Czekoladowy'), 1, 'Drip z ciemnej czekolady'),
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Palona Czekolada/Mango/Marakuja'),
                 (Select Decoration_Id From [dbo].[Decoration] Where [Name] like 'Kwiaty suszone'), 1, 'Różne kwiaty suszone jadalne'),
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Palona Czekolada/Mango/Marakuja'),
                 (Select Decoration_Id From [dbo].[Decoration] Where [Name] like 'Brokat jadalny'), 1, ''),
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Kawa'),
                 (Select Decoration_Id From [dbo].[Decoration] Where [Name] like 'Tynk'), 1, 'Tynk z kremu z ciemnej czekolady'),
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Kawa'),
                 (Select Decoration_Id From [dbo].[Decoration] Where [Name] like 'Czekolada'), 1, 'Pokruszona czekolada i kostki czekolady'),
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Kawa'),
                 (Select Decoration_Id From [dbo].[Decoration] Where [Name] like 'Beziki'), 1, ''),
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Szwarcwaldzki'),
                 (Select Decoration_Id From [dbo].[Decoration] Where [Name] like 'Tynk'), 1, 'Tynk z kremu z białej czekolady'),
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Szwarcwaldzki'),
                 (Select Decoration_Id From [dbo].[Decoration] Where [Name] like 'Drip Czekoladowy'), 1, 'Drip z ciemnej czekolady'),
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Szwarcwaldzki'),
                 (Select Decoration_Id From [dbo].[Decoration] Where [Name] like 'Świeże owoce'), 1, 'Wiśnie'),
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Piernikowy'),
                 (Select Decoration_Id From [dbo].[Decoration] Where [Name] like 'Naked Cake'), 1, ''),
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Piernikowy'),
                 (Select Decoration_Id From [dbo].[Decoration] Where [Name] like 'Drip Czekoladowy'), 1, 'Drip z ciemnej czekolady'),
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Piernikowy'),
                 (Select Decoration_Id From [dbo].[Decoration] Where [Name] like 'Pierniczki'), 1, '')");


            Execute.Sql(@"Insert into [dbo].[Order] (BaseProduct_Id, Cake_Id, Cream_Id, Servings, TotalPrice, IsTemplate, TemplateName) Values
                ((Select BaseProduct_Id From [dbo].[BaseProduct] Where [Name] = 'Tort'), 
                 (Select Cake_Id From [dbo].[Cake] Where [Name] = 'Jasny Biszkopt'), 
                 (Select Cream_Id From [dbo].[Cream] Where [Name] = 'Krem Śmietankowy'), 1, 95, 1, 'Owocowy'),
                ((Select BaseProduct_Id From [dbo].[BaseProduct] Where [Name] = 'Tort'), 
                 (Select Cake_Id From [dbo].[Cake] Where [Name] = 'Jasny Biszkopt'), 
                 (Select Cream_Id From [dbo].[Cream] Where [Name] = 'Krem Biała Czekoladowa'), 1, 95, 1, 'Truskawka/Limonka/Biała Czekolada'),
                ((Select BaseProduct_Id From [dbo].[BaseProduct] Where [Name] = 'Tort'), 
                 (Select Cake_Id From [dbo].[Cake] Where [Name] = 'Jasny Biszkopt'), 
                 (Select Cream_Id From [dbo].[Cream] Where [Name] = 'Krem Owocowy'), 1, 95, 1, 'Mango/Malina')");

            Execute.Sql(@"Insert into [dbo].[OrdersAdditionals] (Order_Id, Additional_Id, Quantity, Details) Values
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Owocowy'), 
                 (Select Additional_Id From [dbo].[Additional] Where [Name] like 'Dodatkowy Smak Kremu'), 1, 'Krem malina i owoce leśne'),
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Owocowy'), 
                 (Select Additional_Id From [dbo].[Additional] Where [Name] like 'Konfitura z Czarnej Porzeczki'), 1, ''),
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Truskawka/Limonka/Biała Czekolada'), 
                 (Select Additional_Id From [dbo].[Additional] Where [Name] like 'Dodatkowy Smak Kremu'), 1, 'Krem truskawkowy i limonkowy'),
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Truskawka/Limonka/Biała Czekolada'), 
                 (Select Additional_Id From [dbo].[Additional] Where [Name] like 'Galaretka Truskawkowo-Limonkowa'), 1, ''),
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Mango/Malina'), 
                 (Select Additional_Id From [dbo].[Additional] Where [Name] like 'Dodatkowy Smak Kremu'), 1, 'Kremy w torcie - mango/malina'),
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Mango/Malina'), 
                 (Select Additional_Id From [dbo].[Additional] Where [Name] like 'Galaretka Malinowa'), 1, ''),
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Mango/Malina'), 
                 (Select Additional_Id From [dbo].[Additional] Where [Name] like 'Konfitura Malinowa'), 1, '')");

            Execute.Sql(@"Insert into [dbo].[OrdersDecorations] (Order_Id, Decoration_Id, Quantity, Details) Values
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Owocowy'),
                 (Select Decoration_Id From [dbo].[Decoration] Where [Name] like 'Tynk'), 1, 'Tynk z kremu z białej czekolady'),
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Owocowy'),
                 (Select Decoration_Id From [dbo].[Decoration] Where [Name] like 'Drip Czekoladowy'), 1, 'Drip z ciemnej czekolady'),
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Owocowy'),
                 (Select Decoration_Id From [dbo].[Decoration] Where [Name] like 'Kwiaty'), 1, 'Różne kwiaty jadalne'),
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Owocowy'),
                 (Select Decoration_Id From [dbo].[Decoration] Where [Name] like 'Świeże owoce'), 1, 'Mix różnych sezonowych owoców'),
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Truskawka/Limonka/Biała Czekolada'),
                 (Select Decoration_Id From [dbo].[Decoration] Where [Name] like 'Mirror Glaze'), 1, 'Z ciemnej czekolady'),
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Truskawka/Limonka/Biała Czekolada'),
                 (Select Decoration_Id From [dbo].[Decoration] Where [Name] like 'Czekolada'), 1, 'Pokruszona biała i mini tabliczki białej czekolady'),
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Truskawka/Limonka/Biała Czekolada'),
                 (Select Decoration_Id From [dbo].[Decoration] Where [Name] like 'Beziki'), 1, ''),
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Truskawka/Limonka/Biała Czekolada'),
                 (Select Decoration_Id From [dbo].[Decoration] Where [Name] like 'Świeże owoce'), 1, 'Truskawki i ćwiartki limonki'),
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Mango/Malina'),
                 (Select Decoration_Id From [dbo].[Decoration] Where [Name] like 'Tynk'), 1, 'Tynk z kremu z białej czekolady'),
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Mango/Malina'),
                 (Select Decoration_Id From [dbo].[Decoration] Where [Name] like 'Świeże owoce'), 1, 'Mango i maliny'),
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Mango/Malina'),
                 (Select Decoration_Id From [dbo].[Decoration] Where [Name] like 'Perełki'), 1, ''),
                ((Select Order_Id From [dbo].[Order] Where [TemplateName] like 'Mango/Malina'),
                 (Select Decoration_Id From [dbo].[Decoration] Where [Name] like 'Drip Czekoladowy'), 1, 'Drip z ciemnej czekolady')");
        }
    }
}
