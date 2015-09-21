using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
using Handframe.Persistence;

namespace Handframe.Fipe
{
    public class hFipe : hPersistence
    {
        public const int CARROS = 1;
        public const int MOTOS = 2;
        public const int CAMINHOES = 3;

        public String name { get; set; }
        public String key { get; set; }
        public String id { get; set; }
        public String fipe_codigo { get; set; }
        public String combustivel { get; set; }
        public String marca { get; set; }
        public String ano_modelo { get; set; }
        public String preco { get; set; }
        public String veiculo { get; set; }
        public String referencia { get; set; }
        public String time { get; set; }
        public String fipe_name { get; set; }
        public String order { get; set; }
        public String fipe_marca { get; set; }
        public String modelo { get; set; }
        public virtual String Get(int type)
        {
            return type.ToString();
        }
    }
}
