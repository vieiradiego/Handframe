using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Globalization;
using System.Threading;
using System.Resources;
using Handframe.DataBase;
using System.Windows.Forms;

namespace Handframe.Persistence
{
    public class hSystem
    {
        public static string ConfigFile = "inicialization.hand";
        public static string DirFile = "dir.hand";
        public static hConnection connection { get; set; }
        public static string user { get; set; }
        public static Form main { get; set; }
        public static bool released { get; set; }
        public static string passwordHand { get; set; }
        
        public static bool VectorCompare<T>(T[] vector1, T[] vector2)
        {
            if (vector1.Length != vector2.Length)
            {
                return false;
            }
            for (int i = 0; i < vector1.Length; i++)
            {
                if (!vector1[i].Equals(vector2[i]))
                {
                    return false;
                }
            }
            return true;
        }
        public static string ToStringBytesPostgres(byte[] bytes)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in bytes)
            {
                sb.Append(b.ToString());
                sb.Append("/");
            }
            return sb.ToString();
        }
    }
}
