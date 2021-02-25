using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bakery.Migrations
{

    [Migration(20210221184100)]
    public class _20210221184100_AddColumnToOrder : ForwardOnlyMigration
    {
        public override void Up()
        {
            if (!Schema.Table("Order").Column("CompletionDate").Exists())
            {
                Alter.Table("Order").AddColumn("CompletionDate").AsDateTime().WithDefaultValue(DateTime.Now);
            }

            if (!Schema.Table("Order").Column("IsTemplate").Exists())
            {
                Alter.Table("Order").AddColumn("IsTemplate").AsBoolean().WithDefaultValue(false);
            }
        }
    }
}
