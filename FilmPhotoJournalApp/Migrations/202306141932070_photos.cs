namespace FilmPhotoJournalApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class photos : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Photos",
                c => new
                    {
                        PhotoID = c.Int(nullable: false, identity: true),
                        Iso = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PhotoID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Photos");
        }
    }
}
