using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bakery.Migrations
{
    [Migration(20210119203300)]
    public class _20210119203300_InsertDataIntoDecoration : ForwardOnlyMigration
    {
        public override void Up()
        {
            Insert.IntoTable("Decoration")
               .Row(new { Name = "Tynk", Description = "Warstwa kremu na torcie w dowolnym kolorze.", Price = 0 })
               .Row(new { Name = "Naked Cake", Description = "Brak warsty na torcie, tzn. Naked Cake - tort nieotynkowany. Cudownie prezentuje wszystkie warstwy tortu.", Price = 0 });
        }
    }
}
