using Domain.Bikes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrustructure.PersistantEf.Configs.Bikes
{
    public class BikeMap : IEntityTypeConfiguration<Bike>
    {
        public void Configure(EntityTypeBuilder<Bike> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasMany(b=>b.Reservations).WithOne(r => r.Bike).HasForeignKey(r=>r.BikeId);
            builder.Property(b => b.PricePerHour).IsRequired();
            builder.Property(b=>b.AvalableAtLocation).HasMaxLength(100);
        }
    }
}
