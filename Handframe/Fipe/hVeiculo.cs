using Handframe.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Handframe.Fipe
{
    public class hVeiculo : hFipe
    {
       public override String Get(int tipo)
        {
            string m = new WebClient().DownloadString(@"http://fipeapi.appspot.com/api/1/" + tipo + "/veiculo/" + this.marca + "/" + this.modelo + "/" + this.veiculo + ".json");
            return m;
        }
    }
}
