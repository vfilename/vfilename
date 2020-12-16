using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;

namespace vfilename
{
    static class ConfigTxt
    {
        public static string Read(string Name, string DefaultValue)
        {
            string ExePath = System.Reflection.Assembly.GetEntryAssembly().Location;
            string TxtPath = ExePath + "." + Name + ".txt";
            if(!File.Exists(TxtPath))
            {
                return DefaultValue;
            }
            string contents = File.ReadAllText(TxtPath);
            if(contents == "")
            {
                return DefaultValue;
            }
            return contents;
        }
        public static void Write(string Name, string Value, string DefaultValue)
        {
            if(Value==DefaultValue)
            {
                return;
            }
            string ExePath = System.Reflection.Assembly.GetEntryAssembly().Location;
            string TxtPath = ExePath + "." + Name + ".txt";
            File.WriteAllText(TxtPath, Value);
        }
    }
}
