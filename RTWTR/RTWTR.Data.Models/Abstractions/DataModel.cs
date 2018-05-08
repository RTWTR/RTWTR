using RTWTR.Data.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace RTWTR.Data.Models.Abstractions
{
    public abstract class DataModel : IDeletable, IAuditable
    {
        public string Id { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}
