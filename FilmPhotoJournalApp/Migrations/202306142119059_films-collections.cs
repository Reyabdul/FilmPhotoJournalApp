namespace FilmPhotoJournalApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class filmscollections : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Films",
                c => new
                    {
                        FilmID = c.Int(nullable: false, identity: true),
                        FilmName = c.String(),
                    })
                .PrimaryKey(t => t.FilmID);
            
            CreateTable(
                "dbo.FilmCollections",
                c => new
                    {
                        Film_FilmID = c.Int(nullable: false),
                        Collection_CollectionID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Film_FilmID, t.Collection_CollectionID })
                .ForeignKey("dbo.Films", t => t.Film_FilmID, cascadeDelete: true)
                .ForeignKey("dbo.Collections", t => t.Collection_CollectionID, cascadeDelete: true)
                .Index(t => t.Film_FilmID)
                .Index(t => t.Collection_CollectionID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FilmCollections", "Collection_CollectionID", "dbo.Collections");
            DropForeignKey("dbo.FilmCollections", "Film_FilmID", "dbo.Films");
            DropIndex("dbo.FilmCollections", new[] { "Collection_CollectionID" });
            DropIndex("dbo.FilmCollections", new[] { "Film_FilmID" });
            DropTable("dbo.FilmCollections");
            DropTable("dbo.Films");
        }
    }
}
