using System;
using System.Collections.Generic;
using System.Text;

namespace RTWTR.Data.Models.Contracts
{
    public interface IAuditable
    {
        DateTime? CreatedOn { get; set; }

        DateTime? UpdatedOn { get; set; }
    }
}
