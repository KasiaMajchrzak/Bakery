using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bakery.Migrations
{
    [Migration(20210221190400)]
    public class _20210221190400_AddColumnToOrder : ForwardOnlyMigration
    {
        public override void Up()
        {
            if (!Schema.Table("Order").Column("TemplateName").Exists())
            {
                Alter.Table("Order").AddColumn("TemplateName").AsString().Nullable();
            }
        }
    }
}
