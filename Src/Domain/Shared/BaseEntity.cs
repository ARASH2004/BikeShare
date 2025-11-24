using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Shared
{
    public class BaseEntity
    {
        public long Id { get; protected set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }

        public BaseEntity()
        {
            CreatedDate = DateTime.Now;
            IsDeleted = false;
        }
        public void Delete()
        {
            IsDeleted = true;
        }
    }
}
