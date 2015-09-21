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

        /// <summary>
        /// Salve todos os elementos contidos no Objeto hRegisters
        /// </summary>
        /// <param name="register">Objeto do tipo hRegisters</param>
        /// <returns>Retorna o status</returns>
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
        /// <summary>
        /// Atualiza todos os elementos contidos no Objeto hRegisters
        /// </summary>
        /// <param name="register"></param>
        /// <returns>Retorna o status</returns>
        public bool Update(hRegisters register)
        {
            bool result = false;
            return result;
        }
        #endregion Update

        #region Insert
        /// <summary>
        /// Insere todos os elementos contidos no Objeto hRegisters
        /// </summary>
        /// <param name="register"></param>
        /// <returns>Retorna o status</returns>
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
        #endregion Insert
        
        #region Field ADD
        /// <summary>
        /// Adiciona um campo Chave não nulo que é uma identidade (Auto-Incremento)
        /// </summary>
        /// <param name="fieldName"></param>
        public void AddIdentity(string fieldName)
        {
            hField f = new hField(fieldName);
            f.integer_ = true;
            f.key_ = true;
            f.identity_ = true;
            f.null_ = false;
            this.fields.Add(f);
            this.identity = f;
            this.key.Add(f);
        }
        /// <summary>
        /// Adiciona um campo chave do tipo inteiro não nulo
        /// </summary>
        /// <param name="fieldName"></param>
        public void AddKey(string fieldName)
        {
            hField f = new hField(fieldName);
            f.integer_ = true;
            f.key_ = true;
            f.null_ = false;
            this.fields.Add(f);
            this.fields.Add(f);
        }

        /// <summary>
        /// Adiciona um campo chave do tipo inteiro não nulo.
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="tablefk">A tabela FK. O campo FK é o nomeCampo. On Delete CASCADE e ON Update CASCADE</param>
        public void AddKey(string fieldName, string tablefk)
        {
            hField f = new hField(fieldName);
            f.integer_ = true;
            f.key_ = true;
            f.null_ = false;
            f.foreignKey_ = true;
            f.tableFK_ = tablefk;
            f.todelete_ = DeleteUpdate.CASCADE;
            f.toupdate_ = DeleteUpdate.CASCADE;
            this.fields.Add(f);
            this.key.Add(f);
        }

        /// <summary>
        /// Adiciona um campo chave do tipo inteiro não nulo.
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="tablefk"></param>
        /// <param name="todelete"></param>
        /// <param name="toupdate"></param>
        public void AddKey(string fieldName, string tablefk, DeleteUpdate todelete, DeleteUpdate toupdate)
        {
            hField f = new hField(fieldName);
            f.integer_ = true;
            f.key_ = true;
            f.null_ = false;
            f.foreignKey_ = true;
            f.tableFK_ = tablefk;
            f.todelete_ = todelete;
            f.toupdate_ = toupdate;
            this.fields.Add(f);
            this.key.Add(f);
        }

        /// <summary>
        /// Adiciona um campo chave do tipo inteiro não nulo.
        /// </summary>
        /// <param name="nomeCampo"></param>
        /// <param name="tablefk"></param>
        /// <param name="campofk"></param>
        /// <param name="aoapagar"></param>
        /// <param name="aoatualizar"></param>
        public void AddKey(string fieldName, string tablefk, DeleteUpdate aoapagar, DeleteUpdate aoatualizar, string campofk2)
        {
            hField f = new hField(fieldName);
            f.integer_ = true;
            f.key_ = true;
            f.null_ = false;
            f.foreignKey_ = true;
            f.tableFK_ = tablefk;
            f.fieldFK2_ = campofk2;
            f.todelete_ = aoapagar;
            f.toupdate_ = aoatualizar;
            this.fields.Add(f);
            this.key.Add(f);
        }

        /// <summary>
        /// Adiciona um campo não chave do tipo inteiro não nulo que representa o id da tabela Cabecalho
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="tablefk">A tabela FK. O campo FK é o fieldName. On Delete CASCADE e ON Update CASCADE</param>
        public void AddHeader(string fieldName, string tablefk)
        {
            hField f = new hField(fieldName);
            f.integer_ = true;
            f.header_ = true;
            f.null_ = false;
            f.foreignKey_ = true;
            f.tableFK_ = tablefk;
            f.todelete_ = DeleteUpdate.CASCADE;
            f.todelete_ = DeleteUpdate.CASCADE;
            this.fields.Add(f);
        }

        /// <summary>
        /// Adiciona um campo chave do tipo inteiro não nulo que representa o id da tabela Cabecalho
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="tablefk">A tabela FK. O campo FK é o fieldName. On Delete CASCADE e ON Update CASCADE</param>
        public void AddHeaderKey(string fieldName, string tablefk)
        {
            hField f = new hField(fieldName);
            f.integer_ = true;
            f.key_ = true;
            f.header_ = true;
            f.null_ = false;
            f.foreignKey_ = true;
            f.tableFK_ = tablefk;
            f.todelete_ = DeleteUpdate.CASCADE;
            f.toupdate_ = DeleteUpdate.CASCADE;
            this.fields.Add(f);
            this.key.Add(f);
        }
        /// <summary>
        /// Adiciona um campo chave do tipo texto não Nulo
        /// </summary>
        /// <param name="fieldName">Nome do campo</param>
        public void AddTextKey(string fieldName)
        {
            hField f = new hField(fieldName);
            f.text_ = true;
            f.key_ = true;
            f.null_ = false;
            this.fields.Add(f);
            this.key.Add(f);
        }
        /// <summary>
        /// Adiciona um campo chave do tipo data não Nulo
        /// </summary>
        /// <param name="fieldName">Nome do campo</param>
        public void AddDateKey(string fieldName)
        {
            hField f = new hField(fieldName);
            f.date_ = true;
            f.key_ = true;
            f.null_ = false;
            this.fields.Add(f);
            this.key.Add(f);
        }
        /// <summary>
        /// Adiciona um campo inteiro podendo ser nulo
        /// </summary>
        /// <param name="fieldName">Nome do campo</param>
        public void AddInteger(string fieldName)
        {
            hField f = new hField(fieldName);
            f.integer_ = true;
            this.fields.Add(f);
        }

        /// <summary>
        /// Adiciona um campo chave do tipo inteiro não nulo.
        /// </summary>
        /// <param name="fieldName">Nome do campo</param>
        /// <param name="tablefk">A tabela FK. O campo FK é o fieldName. On Delete NOACTION e ON Update NOACTION</param>
        public void AddInteger(string fieldName, string tablefk)
        {
            hField f = new hField(fieldName);
            f.integer_ = true;
            f.foreignKey_ = true;
            f.tableFK_ = tablefk;
            f.todelete_ = DeleteUpdate.NOACTION;
            f.toupdate_ = DeleteUpdate.NOACTION;
            this.fields.Add(f);
        }

        /// <summary>
        /// Adiciona um campo chave do tipo inteiro não nulo.
        /// </summary>
        /// <param name="fieldName">Nome do campo</param>
        /// <param name="tablefk">Tabela da chave estrangeira</param>
        /// <param name="campofk">Campo chave</param>
        /// <param name="todelete">Ação ao apagar</param>
        /// <param name="toupdate">Ação ao atualizar</param>
        public void AddInteger(string fieldName, string tablefk, DeleteUpdate todelete, DeleteUpdate toupdate)
        {
            hField f = new hField(fieldName);
            f.integer_ = true;
            f.foreignKey_ = true;
            f.tableFK_= tablefk;
            f.todelete_ = todelete;
            f.toupdate_ = toupdate;
            this.fields.Add(f);
        }

        /// <summary>
        /// Adiciona um campo chave do tipo inteiro não nulo.
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="tablefk"></param>
        /// <param name="campofk"></param>
        /// <param name="aoapagar"></param>
        /// <param name="aoatualizar"></param>
        public void AddInteger(string fieldName, string tablefk, DeleteUpdate aoapagar, DeleteUpdate aoatualizar, string campofk2)
        {
            hField f = new hField(fieldName);
            f.integer_ = true;
            f.foreignKey_ = true;
            f.tableFK_ = tablefk;
            f.fieldFK2_ = campofk2;
            f.todelete_ = aoapagar;
            f.toupdate_ = aoatualizar;
            this.fields.Add(f);
        }

        public void AddFloat(string fieldName)
        {
            hField f = new hField(fieldName);
            f.float_ = true;
            this.fields.Add(f);
        }

        /// <summary>
        /// Tipo NUMERO
        /// Utilizado para VALORES com padrão: (19,2) - (tamanho total, tamanho decimal) - ((17)00000000000000000.00(2))
        /// </summary>
        public void AddCurrency(string fieldName)
        {
            hField f = new hField(fieldName);
            f.number_ = true;
            f.size_ = 19;
            f.sizeDecimal_ = 2;
            this.fields.Add(f);
        }

        /// <summary>
        /// Utilizado para QUANTIDADES e VALORES (19, 9) - ((10)0000000000.000000000(9))
        /// Padrão: 9 decimais
        /// </summary>
        public void AddNumber(string fieldName)
        {
            hField f = new hField(fieldName);
            f.number_ = true;
            f.size_ = 19;
            f.sizeDecimal_ = 9;
            this.fields.Add(f);
        }

        /// <summary>
        /// Utilizado para QUANTIDADES e VALORES (19, tamanho decimal) - ((10)0000000000.000000000(9))
        /// Padrão: Utilizar preferencialmente 9 decimais
        /// </summary>
		public void AddNumber(string fieldName, int tamanhoDecimalhField)
        {
            hField f = new hField(fieldName);
            f.number_ = true;
            f.size_ = 19;
            f.sizeDecimal_ = tamanhoDecimalhField;
            this.fields.Add(f);
        }

        public void AddNumber(string fieldName, int tamanhoDecimalhField, int tamanho)
        {
            hField f = new hField(fieldName);
            f.number_ = true;
            f.size_ = tamanho;
            f.sizeDecimal_ = tamanhoDecimalhField;
            this.fields.Add(f);
        }

        /// <summary>
        /// Utilizado para QUALQUER TEXTO, inclusive OBSERVAÇÕES
        /// Padrão: nvarchar(MAX)
        /// </summary>
        /// <param name="fieldName"></param>
		public void AddText(string fieldName)
        {
            hField f = new hField(fieldName);
            f.text_ = true;
            this.fields.Add(f);
        }

        /// <summary>
        /// Utilizado para QUALQUER TEXTO, inclusive OBSERVAÇÕES
        /// Padrão: nvarchar(MAX)
        /// </summary>
        /// <param name="fieldName"></param>
        public void AddText(string fieldName, bool criptografado)
        {
            hField f = new hField(fieldName);
            f.text_ = true;
            f.encrypted_ = criptografado;
            this.fields.Add(f);
        }

        /// <summary>
        /// Utilizado para textos muito grandes
        /// Utilizar somente se necessário
        /// Para OBSERVAÇÕES utilizar nvarchar(MAX) = addTexto(campo)
        /// </summary>
        /// <param name="fieldName"></param>
        public void AddNText(string fieldName)
        {
            hField f = new hField(fieldName);
            f.ntext_ = true;
            this.fields.Add(f);
        }

        public void AddLogic(string fieldName)
        {
            hField f = new hField(fieldName);
            f.logic_ = true;
            this.fields.Add(f);
        }

        public void AddDate(string fieldName)
        {
            hField f = new hField(fieldName);
            f.date_ = true;
            this.fields.Add(f);
        }

        public void AddTime(string fieldName)
        {
            hField f = new hField(fieldName);
            f.time_ = true;
            this.fields.Add(f);
        }

        public void AddDateTime(string fieldName)
        {
            hField f = new hField(fieldName);
            f.dateTime_ = true;
            this.fields.Add(f);
        }

        public void AddDateAlteration(string fieldName)
        {
            hField f = new hField(fieldName);
            f.dateTime_ = true;
            f.dateAlteration_ = true;
            this.fields.Add(f);
        }

        public void AddImage(string fieldName)
        {
            hField f = new hField(fieldName);
            f.archive_ = true;
            f.image_ = true;
            f.null_ = false;
            this.fields.Add(f);
        }

        public void AddArchive(string fieldName)
        {
            hField f = new hField(fieldName);
            f.archive_ = true;
            f.image_ = false;
            f.null_ = false;
            this.fields.Add(f);
        }
        #endregion Field ADD
    }
}
