using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SQLite.Net;
using HaydiIOS.iOS;
using Xamarin.Forms;
using System.IO;

[assembly: Dependency(typeof(SQLite_iOS))]

namespace HaydiIOS.iOS
{
    public class SQLite_iOS : ISQLite
    {
        public SQLiteConnection GetConnection()
        {
            var filename = "Groups.db3";
            var documentspath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var librarypath = Path.Combine(documentspath, "..", "Library");
            var path = Path.Combine(librarypath, filename);

            var platform = new SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS();
            var connection = new SQLite.Net.SQLiteConnection(platform, path);

            return connection;
        }
    }
}