using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.DirectoryServices.Protocols;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;

namespace Program
{
    static void Main(string[] args)
    {
        DirectorySearcher ds = new DirectorySearcher();
        ds.SearchRoot = new DirectoryEntry("");
        // \1\72\69\5C\2\36\5C\42\BC\45\F6\C\77\40\2C\72
        Guid guid = new Guid("f3457da4-2aaf-45b7-9f85-74d8b8b64982");
        byte[] bytes = guid.ToByteArray();
        StringBuilder sb = new StringBuilder();
        foreach (byte b in bytes)
        {
            sb.Append(string.Format(@"\{0}", b.ToString("X")));
        }
        string dest = sb.ToString();
        ds.Filter = string.Format(@"(&(ObjectCategory=user)(objectGuid={0}))", sb.ToString());
        foreach (SearchResult result in ds.FindAll())
        {
            foreach (string name in result.GetDirectoryEntry().Properties.PropertyNames)
            {
                Console.WriteLine("{0} --> {1}", name, result.GetDirectoryEntry().Properties[name].Value);
            }
        }
    }
}
