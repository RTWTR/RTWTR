using System;
using System.Collections.Generic;
using System.Text;
using RTWTR.Data.Access.Contracts;
using RTWTR.Data.Models;

namespace RTWTR.Data.Access
{
    public class Saver : ISaver
    {
        private RTWTRDbContext context;

        public Saver(RTWTRDbContext context)
        {
            this.context = context;
        }

        public int SaveChanges()
        {
            return context.SaveChanges();
        }
    }
}
