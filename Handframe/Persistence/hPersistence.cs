using Handframe.DataBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handframe.Persistence
{
    public class hPersistence
    {
        public string tabela;
        public hConnection connection { get; set; }
        public bool connectionDefault { get; set; }
        public List<hField> fields { get; set; }
        public hField identity { get; set; }
        public List<hField> key { get; set; }

        public hPersistence()
        {
            this.connectionDefault = true;
            this.connection = hSystem.connection;
            this.fields = new List<hField>();
            this.key = new List<hField>();
        }

        private hConnection GetConexao
        {
            get
            {
                if (this.connectionDefault)
                {
                    return hSystem.connection;
                }
                else
                {
                    return this.connection;
                }
            }
        }

        #region Save
        public bool Save()
        {
            return this.Save(null);
        }

        public bool Save(hRegisters register)
        {
            foreach (hField f in this.key)
            {
                if (f.value.Equals(""))//Se a chave estiver vazia, então é para usar o auto-incremento
                {
                    return this.Insert(false, register);
                }
            }
            return this.Update(register);
        }
        #endregion Save
        #region Update
        public bool Update(hRegisters register)
        {
            bool result = false;
            return result;
        }
        #endregion Update
        public bool Insert(hRegisters register)
        {
            bool result = false;
            return result;

        }
        public bool Insert(bool insertKey, hRegisters register)
        {
            bool result = false;
            return result;
        }
    }
}
