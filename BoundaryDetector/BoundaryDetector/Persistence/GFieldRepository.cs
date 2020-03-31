using BoundaryDetector.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoundaryDetector.Persistence
{
    public class GFieldRepository : Repository<GField, int>
    {
        public GFieldRepository(ISQLiteDb db) : base(db)
        {
        }
    }
}
