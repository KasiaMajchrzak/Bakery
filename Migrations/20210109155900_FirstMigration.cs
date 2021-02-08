using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bakery.Migrations
{
    [Migration(20210109155900)]
    public class _20210109155900_FirstMigration : ForwardOnlyMigration
    {
        public override void Up()
        {
            if (!Schema.Table("Additional").Exists())
            {
                Create.Table("Additional")
                    .WithColumn("Additional_Id").AsInt32().PrimaryKey().Identity()
                    .WithColumn("Name").AsString().NotNullable()
                    .WithColumn("Description").AsString().Nullable()
                    .WithColumn("Price").AsDecimal().NotNullable();
            }
            if (!Schema.Table("BaseProduct").Exists())
            {
                Create.Table("BaseProduct")
                    .WithColumn("BaseProduct_Id").AsInt32().PrimaryKey().Identity()
                    .WithColumn("Name").AsString().NotNullable()
                    .WithColumn("Category").AsString().NotNullable()
                    .WithColumn("Price").AsDecimal().NotNullable()
                    .WithColumn("Unit").AsString().Nullable();
            }
            if (!Schema.Table("Cake").Exists())
            {
                Create.Table("Cake")
                    .WithColumn("Cake_Id").AsInt32().PrimaryKey().Identity()
                    .WithColumn("Name").AsString().NotNullable()
                    .WithColumn("Type").AsString().Nullable()
                    .WithColumn("Flavour").AsString().NotNullable()
                    .WithColumn("Description").AsString().Nullable()
                    .WithColumn("Price").AsDecimal().NotNullable();                   
            }
            if (!Schema.Table("Cream").Exists())
            {
                Create.Table("Cream")
                    .WithColumn("Cream_Id").AsInt32().PrimaryKey().Identity()
                    .WithColumn("Name").AsString().NotNullable()
                    .WithColumn("Type").AsString().Nullable()
                    .WithColumn("Flavour").AsString().NotNullable()
                    .WithColumn("Description").AsString().Nullable()
                    .WithColumn("Price").AsDecimal().NotNullable();
            }
            if (!Schema.Table("Decoration").Exists())
            {
                Create.Table("Decoration")
                    .WithColumn("Decoration_Id").AsInt32().PrimaryKey().Identity()
                    .WithColumn("Name").AsString().NotNullable()
                    .WithColumn("Description").AsString().Nullable()
                    .WithColumn("Price").AsDecimal().NotNullable();
            }
            if (!Schema.Table("Order").Exists())
            {
                Create.Table("Order")
                    .WithColumn("Order_Id").AsInt32().PrimaryKey().Identity()
                    .WithColumn("BaseProduct_Id").AsInt32().NotNullable().ForeignKey("FK_Order_ToBaseProduct", "BaseProduct", "BaseProduct_Id")
                    .WithColumn("Cream_Id").AsInt32().NotNullable().ForeignKey("FK_Order_ToCream", "Cream", "Cream_Id")
                    .WithColumn("Cake_Id").AsInt32().NotNullable().ForeignKey("FK_Order_ToCake", "Cake", "Cake_Id")
                    .WithColumn("Servings").AsInt32().NotNullable()
                    .WithColumn("OrderedOn").AsDateTime().NotNullable()
                    .WithColumn("TotalPrice").AsDecimal().NotNullable();
            }
            if (!Schema.Table("OrdersAdditionals").Exists())
            {
                Create.Table("OrdersAdditionals")
                    .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                    .WithColumn("Order_Id").AsInt32().NotNullable().ForeignKey("FK_OrdersAdditionals_ToOrder", "Order", "Order_Id")
                    .WithColumn("Additional_Id").AsInt32().NotNullable().ForeignKey("FK_OrdersAdditionals_ToAdditional", "Additional", "Additional_Id")
                    .WithColumn("Quantity").AsInt32().NotNullable();
            }
            if (!Schema.Table("OrdersDecorations").Exists())
            {
                Create.Table("OrdersDecorations")
                    .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                    .WithColumn("Order_Id").AsInt32().NotNullable().ForeignKey("FK_OrdersDecorations_ToOrder", "Order", "Order_Id")
                    .WithColumn("Decoration_Id").AsInt32().NotNullable().ForeignKey("FK_OrdersDecorations_ToDecoration", "Decoration", "Decoration_Id")
                    .WithColumn("Quantity").AsInt32().NotNullable();
            }
        }
    }
}
