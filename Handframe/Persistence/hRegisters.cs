using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handframe.Persistence
{
    public class hRegisters : hPersistence
    {
        public const int ACCESS = 0;
        public const int INCLUSION = 1;
        public const int CHANGE = 2;
        public const int EXCLUSION = 3;
        public string id_register { get { return this.fields[0].value; } set { this.fields[0].value = value; } }
        public string register { get { return this.fields[1].value; } set { this.fields[1].value = value; } }
        public string operation { get { return this.fields[2].value; } set { this.fields[2].value = value; } }
        public string dateTime { get { return this.fields[3].value; } set { this.fields[3].value = value; } }
        public string user { get { return this.fields[4].value; } set { this.fields[4].value = value; } }
        public string computer { get { return this.fields[5].value; } set { this.fields[5].value = value; } }
        public string contents { get { return this.fields[6].value; } set { this.fields[6].value = value; } }
        public hRegisters() : this("")
        {
        }

        public hRegisters(string contents)
        {
            base.tabela = "registers";//Criação da tabela através do código
            this.AddIdentity("id_register");
            this.AddText("register");
            this.AddInteger("operation");
            this.AddDateTime("datetime");
            this.AddText("user");
            this.AddText("computer");
            this.AddText("content");
            this.contents = contents;
        }

        public new void Insert()
        {
            try
            {
                hSystem.connection.Update("INSERT INTO registros (registro, operacao, datahora, usuario, computador, conteudo)" +
                                          "VALUES ('" + register + "', " + operation + ", current_timestamp, '" + hSystem.user.Replace("'", "''") +
                                          "', '" + System.Environment.MachineName.Replace("'", "''") + "', '" + contents.Replace("'", "''") + "')", false);
            }
            catch (Exception ex)
            {
            }
        }
    }
}
