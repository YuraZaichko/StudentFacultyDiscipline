namespace StudentGroupFacultyApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Item2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Faculties", "GroupId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Faculties", "GroupId", c => c.Int(nullable: false));
        }
    }
}
