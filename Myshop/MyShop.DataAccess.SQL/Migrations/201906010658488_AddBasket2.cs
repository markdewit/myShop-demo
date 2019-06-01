namespace MyShop.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBasket2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BasketItems",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        BasketId = c.String(maxLength: 128),
                        ProductId = c.String(),
                        Quanity = c.Int(nullable: false),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.baskets", t => t.BasketId)
                .Index(t => t.BasketId);
            
            CreateTable(
                "dbo.baskets",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BasketItems", "BasketId", "dbo.baskets");
            DropIndex("dbo.BasketItems", new[] { "BasketId" });
            DropTable("dbo.baskets");
            DropTable("dbo.BasketItems");
        }
    }
}
