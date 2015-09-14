using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;

namespace Handframe.Fipe
{
    public class HFipe
    {

        //Modelo
        //http://fipeapi.appspot.com/api/1/[tipo]/[acao]/[parametros].json
        //Carros
        //http://fipeapi.appspot.com/api/1/carros/marcas.json
        //Motos
        //http://fipeapi.appspot.com/api/1/motos/marcas.json
        //Caminhoes
        //http://fipeapi.appspot.com/api/1/caminhoes/marcas.json

        public string type { get; set; }
        public string brand { get; set; }
        public string models { get; set; }
        public string model { get; set; }
        public string vehicle { get; set; }


        public String GetTypes()
        {
            return "motos; carros; caminhoes;";
        }
        public String GetBrands()
        {
            type = "carros";
            string m = new WebClient().DownloadString(@"http://fipeapi.appspot.com/api/1/" + type + "/marcas.json");
            return m;
        }

        public String GetModels(string modelo)
        {
            brand = "21";
            string m = new WebClient().DownloadString(@"http://fipeapi.appspot.com/api/1/"+ type + "/veiculos/"+ brand + ".json");
            return m;
        }
        public String GetModel(string modelo)
        {
            models = "4828";
            string m = new WebClient().DownloadString(@"http://fipeapi.appspot.com/api/1/" + type + "/veiculo/" + brand + "/" + models + ".json");
            return m;
        }

        public String GetVehicle(string modelo)
        {
            model = "2013-1";
            string m = new WebClient().DownloadString(@"http://fipeapi.appspot.com/api/1/" + type + "/veiculo/" + brand + "/" + models + "/" + model + ".json");
            return m;
        }
    }
}
