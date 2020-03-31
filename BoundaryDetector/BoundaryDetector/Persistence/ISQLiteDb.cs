using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoundaryDetector.Persistence
{
    public interface ISQLiteDb
    {
        SQLiteAsyncConnection GetConnection();
    }
}
