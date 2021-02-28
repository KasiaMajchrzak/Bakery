using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bakery.Migrations
{
    [Migration(20210227161900)]
    public class _20210227161900_AddColumnToOrder : ForwardOnlyMigration
    {
        public override void Up()
        {
            if (!Schema.Table("Order").Column("Discount").Exists())
            {
                Alter.Table("Order").AddColumn("Discount").AsDecimal().WithDefaultValue(0);
            }
        }
    }
}
