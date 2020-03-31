using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BoundaryDetector.iOS.Persistence;
using BoundaryDetector.Persistence;
using Foundation;
using SQLite;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLiteDb))]
namespace BoundaryDetector.iOS.Persistence
{
    public class SQLiteDb : ISQLiteDb
    {
        public SQLiteAsyncConnection GetConnection()
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var path = Path.Combine(documentsPath, "BoundaryDetector.db3");
            return new SQLiteAsyncConnection(path);
        }
    }
}