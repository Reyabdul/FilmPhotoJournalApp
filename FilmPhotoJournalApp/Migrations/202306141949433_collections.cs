namespace FilmPhotoJournalApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class collections : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Collections",
                c => new
                    {
                        CollectionID = c.Int(nullable: false, identity: true),
                        CollectionTitle = c.String(),
                    })
                .PrimaryKey(t => t.CollectionID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Collections");
        }
    }
}
