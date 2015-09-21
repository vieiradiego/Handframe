using Handframe.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Handframe.Fipe
{
    public class hModelos : hFipe
    {
        public override String Get(int tipo)
        {
          return new WebClient().DownloadString(@"http://fipeapi.appspot.com/api/1/" + tipo + "/veiculos/" + this.marca + ".json");
        }
    }
}
