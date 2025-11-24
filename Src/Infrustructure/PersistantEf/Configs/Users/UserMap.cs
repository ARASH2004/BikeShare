using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrustructure.PersistantEf.Configs.Users
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
         builder.HasKey(x => x.Id);
         builder.HasMany(u=>u.Reservations).WithOne(r=> r.User).HasForeignKey(u=>u.UserId);
         builder.HasMany(u=>u.Cards).WithOne(p=>p.User).HasForeignKey(u=>u.UserId);
        }
    }
}
