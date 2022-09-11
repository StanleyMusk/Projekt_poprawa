using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

public class DbAplicationContext : DbContext
{
    public DbSet<Users> Users { get; set; }
    public DbSet<Workers> Workers{ get; set; }
    public DbSet<Bike> Bike { get; set; }
    public DbSet<ModelBike> ModelBike { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source = DataFile.db");
    }
}

public class Users
{
    [Key] //primary key
    public long ID { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}

public class Workers
{
    [Key]
    public long ID { get; set; }
    public string Name { get; set; }    
    public int Seniority { get; set; }
    public int Age { get; set; }
    public int Phone { get; set; }  
}

public class Bike
{
    [Key]
    public long ID { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public long WhoPrepared { get; set; }
}

public class ModelBike
{
    [Key]
    public long ID { get; set; }
    public string Mark { get; set; }
    public int WheelSize { get; set; }
    public int NumberGears { get; set; }
}