namespace hackaton2018.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cadastroes", "Endereco", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cadastroes", "Endereco");
        }
    }
}
