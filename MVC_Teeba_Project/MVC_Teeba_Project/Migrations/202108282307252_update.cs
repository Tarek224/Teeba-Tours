namespace MVC_Teeba_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Dependent", "Passenger_Id", "dbo.Passenger");
            DropForeignKey("dbo.Tour_Program", "Tour_Id", "dbo.Tour");
            DropForeignKey("dbo.Tour_Program", "Program_Id", "dbo.Program");
            DropForeignKey("dbo.Program", "Place_Id", "dbo.Place");
            DropIndex("dbo.Dependent", new[] { "Passenger_Id" });
            DropIndex("dbo.Tour_Program", new[] { "Program_Id" });
            DropIndex("dbo.Tour_Program", new[] { "Tour_Id" });
            DropIndex("dbo.Program", new[] { "Place_Id" });
            AddColumn("dbo.Tour", "Images_image1", c => c.String());
            AddColumn("dbo.Tour", "Images_image2", c => c.String());
            AddColumn("dbo.Tour", "Images_image3", c => c.String());
            AddColumn("dbo.Tour", "Images_image4", c => c.String());
            AddColumn("dbo.Tour", "Images_image5", c => c.String());
            AddColumn("dbo.Place", "Images_image1", c => c.String());
            AddColumn("dbo.Place", "Images_image2", c => c.String());
            AddColumn("dbo.Place", "Images_image3", c => c.String());
            AddColumn("dbo.Place", "Images_image4", c => c.String());
            AddColumn("dbo.Place", "Images_image5", c => c.String());
            AlterColumn("dbo.Dependent", "Passenger_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Tour_Program", "Program_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Tour_Program", "Tour_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Program", "Place_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Dependent", "Passenger_Id");
            CreateIndex("dbo.Tour_Program", "Program_Id");
            CreateIndex("dbo.Tour_Program", "Tour_Id");
            CreateIndex("dbo.Program", "Place_Id");
            AddForeignKey("dbo.Dependent", "Passenger_Id", "dbo.Passenger", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Tour_Program", "Tour_Id", "dbo.Tour", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Tour_Program", "Program_Id", "dbo.Program", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Program", "Place_Id", "dbo.Place", "ID", cascadeDelete: true);
            DropColumn("dbo.Dependent", "Nationality");
            DropColumn("dbo.Tour", "Images");
            DropColumn("dbo.Place", "Images");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Place", "Images", c => c.Int(nullable: false));
            AddColumn("dbo.Tour", "Images", c => c.Int(nullable: false));
            AddColumn("dbo.Dependent", "Nationality", c => c.String());
            DropForeignKey("dbo.Program", "Place_Id", "dbo.Place");
            DropForeignKey("dbo.Tour_Program", "Program_Id", "dbo.Program");
            DropForeignKey("dbo.Tour_Program", "Tour_Id", "dbo.Tour");
            DropForeignKey("dbo.Dependent", "Passenger_Id", "dbo.Passenger");
            DropIndex("dbo.Program", new[] { "Place_Id" });
            DropIndex("dbo.Tour_Program", new[] { "Tour_Id" });
            DropIndex("dbo.Tour_Program", new[] { "Program_Id" });
            DropIndex("dbo.Dependent", new[] { "Passenger_Id" });
            AlterColumn("dbo.Program", "Place_Id", c => c.Int());
            AlterColumn("dbo.Tour_Program", "Tour_Id", c => c.Int());
            AlterColumn("dbo.Tour_Program", "Program_Id", c => c.Int());
            AlterColumn("dbo.Dependent", "Passenger_Id", c => c.Int());
            DropColumn("dbo.Place", "Images_image5");
            DropColumn("dbo.Place", "Images_image4");
            DropColumn("dbo.Place", "Images_image3");
            DropColumn("dbo.Place", "Images_image2");
            DropColumn("dbo.Place", "Images_image1");
            DropColumn("dbo.Tour", "Images_image5");
            DropColumn("dbo.Tour", "Images_image4");
            DropColumn("dbo.Tour", "Images_image3");
            DropColumn("dbo.Tour", "Images_image2");
            DropColumn("dbo.Tour", "Images_image1");
            CreateIndex("dbo.Program", "Place_Id");
            CreateIndex("dbo.Tour_Program", "Tour_Id");
            CreateIndex("dbo.Tour_Program", "Program_Id");
            CreateIndex("dbo.Dependent", "Passenger_Id");
            AddForeignKey("dbo.Program", "Place_Id", "dbo.Place", "ID");
            AddForeignKey("dbo.Tour_Program", "Program_Id", "dbo.Program", "ID");
            AddForeignKey("dbo.Tour_Program", "Tour_Id", "dbo.Tour", "ID");
            AddForeignKey("dbo.Dependent", "Passenger_Id", "dbo.Passenger", "ID");
        }
    }
}
