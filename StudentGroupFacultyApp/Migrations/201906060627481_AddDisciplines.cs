namespace StudentGroupFacultyApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDisciplines : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Disciplines",
                c => new
                    {
                        DisciplineId = c.Int(nullable: false, identity: true),
                        DisciplineName = c.String(),
                    })
                .PrimaryKey(t => t.DisciplineId);
            
            CreateTable(
                "dbo.FacultyDisciplines",
                c => new
                    {
                        FacultyDisciplineId = c.Int(nullable: false, identity: true),
                        FacultyId = c.Int(nullable: false),
                        DisciplineId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FacultyDisciplineId)
                .ForeignKey("dbo.Disciplines", t => t.DisciplineId)
                .ForeignKey("dbo.Faculties", t => t.FacultyId)
                .Index(t => t.FacultyId)
                .Index(t => t.DisciplineId);
            
            CreateTable(
                "dbo.StudentsDiscipline",
                c => new
                    {
                        StudentId = c.Int(nullable: false),
                        FacultyDisciplineId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.StudentId, t.FacultyDisciplineId })
                .ForeignKey("dbo.Students", t => t.StudentId)
                .ForeignKey("dbo.FacultyDisciplines", t => t.FacultyDisciplineId)
                .Index(t => t.StudentId)
                .Index(t => t.FacultyDisciplineId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentsDiscipline", "FacultyDisciplineId", "dbo.FacultyDisciplines");
            DropForeignKey("dbo.StudentsDiscipline", "StudentId", "dbo.Students");
            DropForeignKey("dbo.FacultyDisciplines", "FacultyId", "dbo.Faculties");
            DropForeignKey("dbo.FacultyDisciplines", "DisciplineId", "dbo.Disciplines");
            DropIndex("dbo.StudentsDiscipline", new[] { "FacultyDisciplineId" });
            DropIndex("dbo.StudentsDiscipline", new[] { "StudentId" });
            DropIndex("dbo.FacultyDisciplines", new[] { "DisciplineId" });
            DropIndex("dbo.FacultyDisciplines", new[] { "FacultyId" });
            DropTable("dbo.StudentsDiscipline");
            DropTable("dbo.FacultyDisciplines");
            DropTable("dbo.Disciplines");
        }
    }
}
