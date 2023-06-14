namespace FilmPhotoJournalApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class photocollection : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Photos", "CollectionID", c => c.Int(nullable: false));
            CreateIndex("dbo.Photos", "CollectionID");
            AddForeignKey("dbo.Photos", "CollectionID", "dbo.Collections", "CollectionID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Photos", "CollectionID", "dbo.Collections");
            DropIndex("dbo.Photos", new[] { "CollectionID" });
            DropColumn("dbo.Photos", "CollectionID");
        }
    }
}
