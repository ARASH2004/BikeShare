using Domain.Bikes;
using Domain.Cards;
using Domain.Reservations;
using Domain.Shared;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrustructure.PersistantEf;

public class BikeRentContext:DbContext
{
    private readonly IConfiguration _Configuration;

    public BikeRentContext(IConfiguration configuration)
    {
        this._Configuration = configuration;
    }

    public DbSet<User> Users { get; set; }
   public DbSet<Reservation> Reservations { get; set; }
   public DbSet<Bike> bikes { get; set; }
    public DbSet<Card> cards { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        var ConectionString = _Configuration.GetConnectionString("BikeShareConnectionString");
        optionsBuilder.UseSqlServer(ConectionString);
    }
  
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BikeRentContext).Assembly);
    }

}
