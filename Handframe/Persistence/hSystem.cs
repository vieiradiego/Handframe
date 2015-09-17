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

namespace Handframe.Persistence
{
    public class hSystem
    {
        public static string ArquivoConfiguracao = "inicializacao.gautica";
        public static string ArquivoDiretorio = "diretorio.gautica";
        public static hConnection connection { get; set; }
        public static string usuario { get; set; }
        public static Form principal { get; set; }
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
    }
}
