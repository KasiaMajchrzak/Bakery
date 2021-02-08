using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bakery.Migrations
{
    [Migration(20210126215500)]
    public class _20210126_215500_AlterColumnOrderedOnFromTableOrder : ForwardOnlyMigration
    {
        public override void Up()
        {
            if (Schema.Table("Order").Column("OrderedOn").Exists())
            {
                Alter.Column("OrderedOn").OnTable("Order").AsDateTime().Nullable().WithDefaultValue(DateTime.Now);
            }

        }
    }
}
