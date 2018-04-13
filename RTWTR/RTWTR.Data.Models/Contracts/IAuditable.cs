using System;

namespace RTWTR.Data.Models.Contracts
{
    public interface IAuditable
    {
        DateTime? CreatedOn { get; set; }

        DateTime? UpdatedOn { get; set; }
    }
}
