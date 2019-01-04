using SQLite.Net;
using SQLite.Net.Interop;
using SQLiteNetExtensions.Attributes;
using SQLiteNetExtensions.Exceptions;
using SQLiteNetExtensions.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HaydiIOS
{
    public class GroupsDB
    {
        public static SQLiteConnection Connection { get; set; }
        public static string Root { get; set; } = string.Empty;
        public ContactComparer Comparer = new ContactComparer();

        public GroupsDB()
        {

            Connection = DependencyService.Get<ISQLite>().GetConnection();
            Connection.CreateTable<CustomGroup>();
            Connection.CreateTable<CustomContact>();

            //var location = "ContactsTest.db3";
            //location = System.IO.Path.Combine(Root, location);
        }

        public IEnumerable<CustomGroup> GetGroups()
        {
            List<CustomGroup> groups = new List<CustomGroup>();
            groups = Connection.GetAllWithChildren<CustomGroup>().ToList();
            return groups;
        }

        public CustomGroup GetGroup(int id)
        {
            return Connection.GetWithChildren<CustomGroup>(id);
        }

        public void DeleteGroup(int id)
        {
            Data.GroupsList.Remove(GetGroup(id));
            Connection.Delete<CustomGroup>(id);

            var group = Data.GroupsList.Where(item => item.ID == id).First();

            Data.GroupsList.Remove(group);
            var groups = GetGroups();
            //Data.GroupEdited = true;
        }

        public void AddGroup(CustomGroup group)
        {
            foreach(CustomContact cc in group.People)
            {
                Connection.Insert(cc);
            }
            Connection.InsertWithChildren(group);
            //var test = Connection.GetWithChildren<CustomGroup>(group.ID);
        }

        public void UpdateGroup(CustomGroup group)
        {
            var tmpGroup = Connection.GetWithChildren<CustomGroup>(group.ID);
            var tmpList = group.People.Except(tmpGroup.People, Comparer).ToList();
            foreach(CustomContact cc in tmpList)
            {
                Connection.Insert(cc);
            }

            Connection.UpdateWithChildren(group);
            //Data.GroupEdited = true;

            //var test = Connection.GetWithChildren<CustomGroup>(group.ID);
        }
    }
}
