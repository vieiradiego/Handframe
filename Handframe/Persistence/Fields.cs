using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Handframe.Persistence
{
    public interface Fields
    {
        void Clear(Object o);
        void Save(Object o);
    }
}
