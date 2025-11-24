using Domain.Reservations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrustructure.PersistantEf.Configs.Reservatiions
{
    public class ReservationMap : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(r => r.Card)
                   .WithMany(p => p.Reservations)
                   .HasForeignKey(r => r.CardId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
