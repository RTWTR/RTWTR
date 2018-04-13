using System;

namespace RTWTR.Data.Models.Contracts
{
    public abstract class DataModel : IDeletable, IAuditable
    {
        public int Id { get; set; } 

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}
