namespace IdeoTest
{
    using System;
    using System.Data.Entity;


    public class ModelContext : DbContext
    {
        // Your context has been configured to use a 'Model' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'IdeoTest.Model' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'Model' 
        // connection string in the application configuration file.
        public ModelContext()
            : base("ModelContext")
        {
        }
  
        public DbSet<Node> Nodes { get; set; }
       

    }
}