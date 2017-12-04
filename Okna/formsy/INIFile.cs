using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.Reflection;

namespace Okna
{
    class INIFile
    {
        string Path;
        string WektorSettings = Assembly.GetExecutingAssembly().GetName().Name;

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern long WritePrivateProfileString(string Section, string Key, string Value, string FilePath);

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);

        public INIFile(string IniPath = null)
        {
            Path = new FileInfo(IniPath ?? WektorSettings + ".ini").FullName.ToString();
        }

        public string Read(string Key, string Section = null)
        {
            var RetVal = new StringBuilder(255);
            GetPrivateProfileString(Section ?? WektorSettings, Key, "", RetVal, 255, Path);
            return RetVal.ToString();
        }

        public void Write(string Key, string Value, string Section = null)
        {
            WritePrivateProfileString(Section ?? WektorSettings, Key, Value, Path);
        }

        public void DeleteKey(string Key, string Section = null)
        {
            Write(Key, null, Section ?? WektorSettings);
        }

        public void DeleteSection(string Section = null)
        {
            Write(null, null, Section ?? WektorSettings);
        }

        public bool KeyExists(string Key, string Section = null)
        {
            return Read(Key, Section).Length > 0;
        }
    }
}
