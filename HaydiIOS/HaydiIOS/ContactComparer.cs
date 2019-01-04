using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaydiIOS
{
    public class ContactComparer : IEqualityComparer<CustomContact>
    {
        public bool Equals(CustomContact x, CustomContact y)
        {
            return x.Name == y.Name;
        }

        public int GetHashCode(CustomContact obj)
        {
            return obj.Name.GetHashCode();
        }
    }
}
