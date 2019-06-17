namespace StudentGroupFacultyApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserBindings : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "FacultyId", c => c.Int());
            AddColumn("dbo.AspNetUsers", "StudentId", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "FacultyId");
            CreateIndex("dbo.AspNetUsers", "StudentId");
            AddForeignKey("dbo.AspNetUsers", "FacultyId", "dbo.Faculties", "FacultyId");
            AddForeignKey("dbo.AspNetUsers", "StudentId", "dbo.Students", "StudentId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "StudentId", "dbo.Students");
            DropForeignKey("dbo.AspNetUsers", "FacultyId", "dbo.Faculties");
            DropIndex("dbo.AspNetUsers", new[] { "StudentId" });
            DropIndex("dbo.AspNetUsers", new[] { "FacultyId" });
            DropColumn("dbo.AspNetUsers", "StudentId");
            DropColumn("dbo.AspNetUsers", "FacultyId");
        }
    }
}
