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

        public bool Save(hRegisters registro)
        {
            foreach (Campo c in this.Chave)
            {
                if (c.valor.Equals(""))
                {//Se a chave estiver vazia, então é para usar o auto-incremento
                    return this.Inserir(false, registro);
                }
            }
            return this.Atualizar(registro);
        }

        /// <summary>
        /// Retorna a string para Inserir ou Atualizar um registro conforme a chave primária. 
        /// Caso não exista valor na chave primária, irá inserir (carrega na persistencia o novo id gerado automaticamente), senão irá atualizar.
        /// </summary>
        /// <returns></returns>
        public string StringGravar()
        {
            List<DbParameter> parametros = new List<DbParameter>();
            return this.StringGravar(null, ref parametros, false);
        }

        public string StringGravar(bool retornarIdentidade)
        {
            List<DbParameter> parametros = new List<DbParameter>();
            return this.StringGravar(null, ref parametros, retornarIdentidade);
        }

        public string StringGravar(ref List<DbParameter> parametros)
        {
            return this.StringGravar(null, ref parametros, false);
        }

        public string StringGravar(hRegisters register, ref List<DbParameter> parameters)
        {
            return this.StringGravar(register, ref parameters, false);
        }

        public string StringGravar(hRegisters register, ref List<DbParameter> parameters, bool returnIdentity)
        {
            foreach (hField f in this.key)
            {
                if (f.value.Equals(""))
                {//Se a chave estiver vazia, então é para usar o auto-incremento
                    return this.StringInserir(false, register, ref parameters, returnIdentity);
                }
            }
            return this.StringAtualizar(register, ref parameters, returnIdentity);
        }

        /// <summary>
        /// Grava varios dados e retorna as identidades geradas
        /// </summary>
        /// <param name="persistencias">Vetor de persistencias a serem gravadas</param>
        /// <returns></returns>
        public string[] MultipleSave(hPersistence[] persistencias)
        {
            List<DbParameter> parametros = new List<DbParameter>();
            return this.MultipleSave(persistencias, ref parametros);
        }

        /// <summary>
        /// Grava varios dados e retorna as identidades geradas
        /// </summary>
        /// <param name="persistencias">Vetor de persistencias a serem gravadas</param>
        public string[] MultipleSave(Persistencia[] persistencias, ref List<DbParameter> parametros)
        {
            if (persistencias.Length > 0)
            {
                string[] sqls = new string[persistencias.Length];
                string[] retornos = new string[persistencias.Length];
                for (int i = 0; i < persistencias.Length; i++)
                {
                    if (persistencias[i] != null)
                    {
                        sqls[i] = persistencias[i].StringGravar(null, ref parametros, true);
                    }
                    else
                    {
                        sqls[i] = "";
                    }
                }
                DataTable[] dts = this.GetConexao.ConsultarVarios(sqls, parametros);
                for (int i = 0; i < persistencias.Length; i++)
                {
                    if (dts[i] != null && dts[i].Rows.Count > 0)
                    {
                        if (persistencias[i].Identidade != null)
                        {
                            persistencias[i].Identidade.valor = Sistema.ToString(dts[i].Rows[0]["id"]);
                        }
                        retornos[i] = Sistema.ToString(dts[i].Rows[0]["id"]);
                    }
                    else
                    {
                        retornos[i] = "";
                    }
                }
                return retornos;
            }
            return new string[] { };
        }
        #endregion GRAVAR

        #region INSERIR
        /// <summary>
		/// Insere um registro. Não carrega na persistencia o novo id gerado automaticamente
		/// </summary>
		/// <returns></returns>
		public bool Inserir()
        {
            return this.Inserir(true, null);
        }

        /// <summary>
        /// Insere um registro. Não carrega na persistencia o novo id gerado automaticamente
        /// </summary>
        /// <returns></returns>
        public bool Inserir(Registros registro)
        {
            return this.Inserir(true, registro);
        }

        /// <summary>
        /// Insere um registro.
        /// </summary>
        /// <param name="inserirChave">true se for para carregar na persistencia o novo id gerado automaticamente</param>
        /// <returns></returns>
        public bool Inserir(bool inserirChave)
        {
            return this.Inserir(inserirChave, null);
        }

        /// <summary>
        /// Insere um registro.
        /// </summary>
        /// <param name="inserirChave">true se for para carregar na persistencia o novo id gerado automaticamente</param>
        /// <returns></returns>
        public bool Inserir(bool inserirChave, Registros registro)
        {
            bool resultado = false;
            try
            {
                List<DbParameter> parametros = null;
                DataTable dt = new DataTable();
                string sql = this.StringInserir(inserirChave, registro, ref parametros, true);
                resultado = this.GetConexao.Consultar(sql, parametros, dt);
                if (!inserirChave && resultado)
                {
                    if (this.Identidade != null)
                    {
                        this.Identidade.valor = dt.Rows[0]["id"].ToString();
                        resultado = true;
                    }
                }
                if (resultado && registro != null)
                {//Insere o registro de usuário caso tenha dado certo a inserção
                    registro.tabela = this.tabela;
                    registro.registro = this.tabela;
                    registro.operacao = Registros.INCLUSAO.ToString();
                    registro.conteudo = sql;
                    registro.Inserir();
                }
            }
            catch (Excecao ex)
            {
                ex.HabilitarDetalhes();
            }
            return resultado;
        }

        /// <summary>
        /// Insere um registro. Não carrega na persistencia o novo id gerado automaticamente
        /// </summary>
        /// <returns></returns>
        public string StringInserir()
        {
            List<DbParameter> parametros = new List<DbParameter>();
            return this.StringInserir(true, null, ref parametros, false);
        }

        /// <summary>
        /// Insere um registro. Não carrega na persistencia o novo id gerado automaticamente
        /// </summary>
        /// <returns></returns>
        public string StringInserir(Registros registro)
        {
            List<DbParameter> parametros = new List<DbParameter>();
            return this.StringInserir(true, registro, ref parametros, false);
        }

        /// <summary>
        /// Insere um registro.
        /// </summary>
        /// <param name="inserirChave">true se for para carregar na persistencia o novo id gerado automaticamente</param>
        /// <returns></returns>
        public string StringInserir(bool inserirChave)
        {
            List<DbParameter> parametros = new List<DbParameter>();
            return this.StringInserir(inserirChave, null, ref parametros, false);
        }

        public string StringInserir(bool inserirChave, Registros registro, ref List<DbParameter> parametros)
        {
            return StringInserir(inserirChave, registro, ref parametros, false);
        }

        /// <summary>
        /// Insere um registro.
        /// </summary>
        /// <param name="inserirChave">true se for para carregar na persistencia o novo id gerado automaticamente</param>
        /// <returns></returns>
        private string StringInserir(bool inserirChave, Registros registro, ref List<DbParameter> parametros, bool retornarIdentidade)
        {
            try
            {
                string p0 = "";
                string p1 = "";
                StringBuilder p2 = new StringBuilder("INSERT INTO ");
                p2.Append(this.tabela);
                p2.Append(" (");
                StringBuilder p3 = new StringBuilder("VALUES (");
                foreach (Campo c in this.campos)
                {
                    if (inserirChave)
                    {
                        if (c.identidade)
                        {//Se o campo identidade é null ou 0, não colocá-lo na lista de campos
                            if (!c.valorBanco.Equals("NULL") && !c.valorBanco.Equals("0"))
                            {
                                if (this.GetConexao.IsMSSQL())
                                {//Se existe valor para o campo identidade, estas instruções devem ser executadas
                                    p0 = "SET IDENTITY_INSERT " + this.tabela + " ON; ";
                                    p1 = "SET IDENTITY_INSERT " + this.tabela + " OFF; ";
                                }
                                p2.Append(c.nome);
                                p2.Append(", ");
                                p3.Append(c.valorBanco);
                                p3.Append(", ");
                            }
                        }
                        else
                        {
                            if (c.arquivo)
                            {
                                if (parametros == null)
                                {
                                    parametros = new List<DbParameter>();
                                }
                                int idparametro = 0;
                                for (int i = 0; i < parametros.Count; i++)
                                {
                                    if (parametros[i].ParameterName == c.valorBanco + idparametro)
                                    {
                                        idparametro++;
                                        i = -1;
                                    }
                                }
                                string nome = c.nome;
                                c.nome = c.nome + idparametro;
                                if (this.GetConexao.IsMSSQL())
                                {
                                    ConexaoMSSQL.AdicionarParametro(ref parametros, c);
                                }
                                else if (this.GetConexao.IsPostgreSQL())
                                {
                                    ConexaoPostgreSQL.AdicionarParametro(ref parametros, c);
                                }
                                else if (this.GetConexao.IsOracle())
                                {
                                    ConexaoOracle.AdicionarParametro(ref parametros, c);
                                }
                                else if (this.GetConexao.IsMySQL())
                                {
                                    ConexaoMySQL.AdicionarParametro(ref parametros, c);
                                }
                                p3.Append(c.valorBanco);
                                c.nome = nome;
                            }
                            else
                            {
                                p3.Append(c.valorBanco);
                            }
                            p2.Append(c.nome);
                            p2.Append(", ");
                            p3.Append(", ");
                        }
                    }
                    else if (!c.chave)
                    {
                        if (c.arquivo)
                        {
                            if (parametros == null)
                            {
                                parametros = new List<DbParameter>();
                            }
                            int idparametro = 0;
                            for (int i = 0; i < parametros.Count; i++)
                            {
                                if (parametros[i].ParameterName == c.valorBanco + idparametro)
                                {
                                    idparametro++;
                                    i = -1;
                                }
                            }
                            string nome = c.nome;
                            c.nome = c.nome + idparametro;
                            if (this.GetConexao.IsMSSQL())
                            {
                                ConexaoMSSQL.AdicionarParametro(ref parametros, c);
                            }
                            else if (this.GetConexao.IsPostgreSQL())
                            {
                                ConexaoPostgreSQL.AdicionarParametro(ref parametros, c);
                            }
                            else if (this.GetConexao.IsOracle())
                            {
                                ConexaoOracle.AdicionarParametro(ref parametros, c);
                            }
                            else if (this.GetConexao.IsMySQL())
                            {
                                ConexaoMySQL.AdicionarParametro(ref parametros, c);
                            }
                            p3.Append(c.valorBanco);
                            c.nome = nome;
                        }
                        else
                        {

                            p3.Append(c.valorBanco);
                        }
                        p2.Append(c.nome);
                        p2.Append(", ");
                        p3.Append(", ");
                    }
                }
                if (p2.ToString().EndsWith(", "))
                {
                    p2.Remove(p2.Length - 2, 2);
                }
                if (p3.ToString().EndsWith(", "))
                {
                    p3.Remove(p3.Length - 2, 2);
                }
                p2.Append(") ");
                p3.Append(") ");
                p2.Append(p3);
                DataTable dt = new DataTable();
                if (this.GetConexao.IsMSSQL())
                {
                    return p0 + p2.ToString() + ";" + (retornarIdentidade ? " SELECT @@IDENTITY AS id;" : "") + p1;
                }
                else if (this.GetConexao.IsPostgreSQL())
                {
                    string x = "";
                    if (this.Identidade != null)
                    {
                        if (this.Identidade.valor.Length == 0)
                        {
                            x = ";" + (retornarIdentidade ? " SELECT currval(pg_get_serial_sequence('" + this.tabela + "', '" + this.Identidade.nome + "')) AS id;" : "");
                        }
                        else
                        {
                            string tab = this.tabela;
                            string ident = this.Identidade.nome;
                            if (tab.Length > 29)
                            {
                                tab = tab.Substring(0, 29);
                            }
                            if (ident.Length > 29)
                            {
                                ident = ident.Substring(0, 29);
                            }
                            x = "; SELECT setval('" + tab + "_" + ident + "_seq', (SELECT MAX(" + this.Identidade.nome + ") + 1 FROM " + this.tabela + "), false);" + (retornarIdentidade ? " SELECT " + this.Identidade.valor + " AS id;" : "");
                        }
                    }
                    else
                    {
                        foreach (Campo c in this.Chave)
                        {
                            x = ";";
                            break;
                        }
                    }
                    return p2.ToString() + x;
                }
                else if (this.GetConexao.IsOracle())
                {
                    string id = "";
                    if (this.Identidade != null)
                    {
                        id = ";" + (retornarIdentidade ? " SELECT " + this.tabela + "_SEQ.currval AS id  FROM " + this.tabela + " WHERE ROWNUM = 1;" : "");
                    }
                    else
                    {
                        foreach (Campo c in this.Chave)
                        {
                            id = ";";
                            break;
                        }
                    }
                    return p2.ToString() + id;
                }
                else if (this.GetConexao.IsMySQL())
                {
                    return p2.ToString() + ";" + (retornarIdentidade ? " SELECT LAST_INSERT_ID() AS id;" : "");
                }
            }
            catch (Excecao ex)
            {
                ex.HabilitarDetalhes();
            }
            return "";
        }
        #endregion INSERIR

        #region ATUALIZAR
        public bool Atualizar()
        {
            return this.Atualizar(null);
        }

        public bool Atualizar(Registros registro)
        {
            bool resultado = false;
            try
            {
                StringBuilder p1 = new StringBuilder(" WHERE ");
                List<DbParameter> parametros = null;
                int qtdNaoChave = 0;
                foreach (Campo c in this.campos)
                {
                    if (c.chave)
                    {
                        p1.Append(c.nome);
                        p1.Append(" = ");
                        p1.Append(c.valorBanco);
                        p1.Append(" AND ");
                    }
                    else
                    {
                        qtdNaoChave++;
                    }
                }
                p1.Remove(p1.Length - 5, 5);
                if (!this.GetConexao.Consultar("SELECT 1 FROM " + this.tabela + p1.ToString()))
                {//Se o registro nao existir, chama o método inserir
                    return this.Inserir(true, registro);
                }
                else
                {
                    string sql = this.StringAtualizar(registro, ref parametros);
                    if (qtdNaoChave > 0)
                    {
                        resultado = (this.GetConexao.Atualizar(sql, parametros, true) > 0);
                        if (resultado && registro != null)
                        {//Insere o registro de usuário caso tenha dado certo a inserção
                            registro.tabela = this.tabela;
                            registro.registro = this.tabela;
                            registro.operacao = Registros.ALTERACAO.ToString();
                            registro.conteudo = sql;
                            registro.Inserir();
                        }
                    }
                    else//CASO A TABELA SO TENHA CAMPOS CHAVE
                    {
                        resultado = true;
                    }
                }
            }
            catch (Excecao ex)
            {
                ex.HabilitarDetalhes();
            }
            return resultado;
        }

        public string StringAtualizar()
        {
            List<DbParameter> parametros = new List<DbParameter>();
            return this.StringAtualizar(null, ref parametros, false);
        }

        public string StringAtualizar(Registros registro, ref List<DbParameter> parametros)
        {
            return this.StringAtualizar(registro, ref parametros, false);
        }

        public string StringAtualizar(Registros registro, ref List<DbParameter> parametros, bool retornarIdentidade)
        {
            try
            {
                StringBuilder p1 = new StringBuilder("UPDATE ");
                StringBuilder p2 = new StringBuilder(" WHERE ");
                p1.Append(this.tabela);
                p1.Append(" SET ");
                List<DbParameter> param = null;
                int qtdNaoChave = 0;
                foreach (Campo c in this.campos)
                {
                    if (c.chave)
                    {
                        p2.Append(c.nome);
                        p2.Append(" = ");
                        p2.Append(c.valorBanco);
                        p2.Append(" AND ");
                    }
                    else
                    {
                        qtdNaoChave++;
                        p1.Append(c.nome);
                        p1.Append(" = ");
                        if (c.arquivo)
                        {
                            if (parametros == null)
                            {
                                parametros = new List<DbParameter>();
                            }
                            if (param == null)
                            {
                                param = new List<DbParameter>();
                            }
                            int idparametro = 0;
                            for (int i = 0; i < parametros.Count; i++)
                            {
                                if (parametros[i].ParameterName == c.valorBanco + idparametro)
                                {
                                    idparametro++;
                                    i = -1;
                                }
                            }
                            for (int i = 0; i < param.Count; i++)
                            {
                                if (param[i].ParameterName == c.valorBanco + idparametro)
                                {
                                    idparametro++;
                                    i = -1;
                                }
                            }
                            string nome = c.nome;
                            c.nome = c.nome + idparametro;
                            if (this.GetConexao.IsMSSQL())
                            {
                                ConexaoMSSQL.AdicionarParametro(ref param, c);
                            }
                            else if (this.GetConexao.IsPostgreSQL())
                            {
                                ConexaoPostgreSQL.AdicionarParametro(ref param, c);
                            }
                            else if (this.GetConexao.IsOracle())
                            {
                                ConexaoOracle.AdicionarParametro(ref param, c);
                            }
                            else if (this.GetConexao.IsMySQL())
                            {
                                ConexaoMySQL.AdicionarParametro(ref param, c);
                            }
                            p1.Append(c.valorBanco);
                            c.nome = nome;
                        }
                        else
                        {
                            p1.Append(c.valorBanco);
                        }
                        p1.Append(", ");
                    }
                }
                if (p1.ToString().EndsWith(", "))
                {
                    p1.Remove(p1.Length - 2, 2);
                }
                p2.Remove(p2.Length - 5, 5);
                p1.Append(p2);
                if (!this.GetConexao.Consultar("SELECT 1 FROM " + this.tabela + p2.ToString()))
                {//Se o registro nao existir, chama o método inserir
                    return this.StringInserir(true, registro, ref parametros, retornarIdentidade);
                }
                else
                {
                    string id = (retornarIdentidade && this.Identidade != null ? " SELECT " + this.Identidade.valor + " AS id" : "");
                    if (qtdNaoChave > 0)
                    {
                        if (param != null && parametros != null)
                        {
                            foreach (DbParameter p in param)
                            {
                                parametros.Add(p);
                            }
                        }
                        return p1.ToString() + ";" + id;
                    }
                    else//CASO A TABELA SO TENHA CAMPOS CHAVE
                    {
                        return id;
                    }
                }
            }
            catch (Excecao ex)
            {
                ex.HabilitarDetalhes();
            }
            return "";
        }
        #endregion ATUALIZAR

        #region EXCLUIR
        public bool Excluir()
        {
            return this.Excluir(null);
        }

        public bool Excluir(Registros registro)
        {
            bool resultado = false;
            try
            {
                StringBuilder p1 = new StringBuilder(" DELETE FROM ");
                p1.Append(this.tabela);
                p1.Append(" WHERE ");
                foreach (Campo c in this.Chave)
                {
                    p1.Append(c.nome);
                    p1.Append(" = ");
                    p1.Append(c.valorBanco);
                    p1.Append(" AND ");
                }
                p1.Remove(p1.Length - 5, 5);
                resultado = (this.GetConexao.Atualizar(p1.ToString()) > 0);
                if (resultado && registro != null)
                {//Insere o registro de usuário caso tenha dado certo a inserção
                    registro.tabela = this.tabela;
                    registro.registro = this.tabela;
                    registro.operacao = Registros.EXCLUSAO.ToString();
                    registro.conteudo = p1.ToString();
                    registro.Inserir();
                }
            }
            catch (Excecao ex)
            {
                ex.HabilitarDetalhes();
            }
            return resultado;
        }

        public bool ExcluirTodos()
        {
            bool resultado = false;
            try
            {
                StringBuilder p1 = new StringBuilder(" DELETE FROM ");
                p1.Append(this.tabela);
                p1.Append(" WHERE ");
                foreach (Campo c in this.Chave)
                {
                    p1.Append(c.nome);
                    p1.Append(" = ");
                    p1.Append(c.valorBanco);
                    break;
                }
                resultado = (this.GetConexao.Atualizar(p1.ToString()) > 0);
            }
            catch (Excecao ex)
            {
                ex.HabilitarDetalhes();
            }
            return resultado;
        }

        public bool ExcluirTodos(string[] ids)
        {
            bool resultado = false;
            try
            {
                string campoId = "";
                foreach (Campo c in this.campos)
                {
                    if (c.identidade)
                    {
                        campoId = c.nome;
                        break;
                    }
                }
                if (campoId.Length > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (string s in ids)
                    {
                        sb.Append(campoId + " = " + Sistema.ToInt(s) + " OR ");
                    }
                    if (sb.Length > 0)
                    {
                        sb.Remove(sb.Length - 4, 4);
                        sb.Insert(0, " WHERE ");
                    }
                    else
                    {
                        sb.Append(" WHERE " + campoId + " = 0 ");
                    }
                    sb.Insert(0, " DELETE FROM " + this.tabela);
                    resultado = (this.GetConexao.Atualizar(sb.ToString()) > 0);
                }
            }
            catch (Excecao ex)
            {
                ex.HabilitarDetalhes();
            }
            return resultado;
        }

        /// <summary>
        /// Executa o comando sql e apaga todos os registros que tenham o valor do(s) campo(s) cabecalho
        /// </summary>
        public bool ExcluirDetalhes()
        {
            bool resultado = false;
            try
            {
                StringBuilder p1 = new StringBuilder(" DELETE FROM ");
                p1.Append(this.tabela);
                p1.Append(" WHERE ");
                foreach (Campo c in this.campos)
                {
                    if (c.cabecalho)
                    {
                        p1.Append(c.nome);
                        p1.Append(" = ");
                        p1.Append(c.valorBanco);
                        p1.Append(" AND ");
                    }
                }
                p1.Remove(p1.Length - 5, 5);
                resultado = (this.GetConexao.Atualizar(p1.ToString()) > 0);
            }
            catch (Excecao ex)
            {
                ex.HabilitarDetalhes();
            }
            return resultado;
        }

        public bool ExcluirTodosSQL(string condicao)
        {
            bool resultado = false;
            try
            {
                StringBuilder p1 = new StringBuilder(" DELETE FROM ");
                p1.Append(this.tabela);
                p1.Append(" ");
                p1.Append(condicao);
                resultado = (this.GetConexao.Atualizar(p1.ToString()) > 0);
            }
            catch (Excecao ex)
            {
                ex.HabilitarDetalhes();
            }
            return resultado;
        }
        #endregion EXCLUIR

        #region EXISTE
        /// <summary>
		/// Este método verifica se o registro existe na base de dados
		/// </summary>
		/// <returns></returns>
		public bool Existe()
        {
            bool resultado = false;
            try
            {
                StringBuilder p1 = new StringBuilder(" SELECT 1 FROM ");
                p1.Append(this.tabela);
                p1.Append(" WHERE ");
                foreach (Campo c in this.Chave)
                {
                    p1.Append(c.nome);
                    p1.Append(" = ");
                    p1.Append(c.valorBanco);
                    p1.Append(" AND ");
                }
                p1.Remove(p1.Length - 5, 5);
                resultado = this.GetConexao.Consultar(p1.ToString());
            }
            catch (Excecao ex)
            {
                ex.HabilitarDetalhes();
            }
            return resultado;
        }

        /// <summary>
        /// Este método verifica se o registro existe na base de dados conforme condição
        /// </summary>
        /// <returns></returns>
        public bool Existe(string condicao)
        {
            bool resultado = false;
            try
            {
                resultado = this.GetConexao.Consultar("SELECT 1 FROM " + this.tabela + " " + condicao);
            }
            catch (Excecao ex)
            {
                ex.HabilitarDetalhes();
            }
            return resultado;
        }
        #endregion EXISTE

        #region NAVEGAÇÃO
        public void Primeiro()
        {
            this.Primeiro("");
        }

        public void Primeiro(string condicao)
        {
            try
            {
                StringBuilder p1 = new StringBuilder();
                StringBuilder p2 = new StringBuilder();
                p1 = new StringBuilder(" SELECT * FROM ");
                p1.Append(this.tabela);
                if (condicao.Length == 0)
                {
                    p1.Append(" WHERE 1 = 1 ");
                }
                else
                {
                    p1.Append(condicao);
                }
                foreach (Campo c in this.Chave)
                {
                    p1.Append(" AND ");
                    p1.Append(c.nome);
                    p1.Append(" = (SELECT MIN(");
                    p1.Append(c.nome);
                    p1.Append(") FROM ");
                    p1.Append(this.tabela);
                    p1.Append(condicao);
                    p1.Append(")");
                }
                DataTable dt = new DataTable();
                this.GetConexao.Consultar(p1.ToString(), dt);
                this.Popula(dt);
            }
            catch (Excecao ex)
            {
                ex.HabilitarDetalhes();
            }
        }

        public void Anterior()
        {
            this.Anterior("");
        }

        public void Anterior(string condicao)
        {
            try
            {
                StringBuilder p1 = new StringBuilder();
                StringBuilder p2 = new StringBuilder();
                p1 = new StringBuilder(" SELECT * FROM ");
                p1.Append(this.tabela);
                if (condicao.Length == 0)
                {
                    p1.Append(" WHERE 1 = 1 ");
                    condicao = " WHERE 1 = 1 ";
                }
                else
                {
                    p1.Append(condicao);
                }
                foreach (Campo c in this.Chave)
                {
                    p1.Append(" AND ");
                    p1.Append(c.nome);
                    p1.Append(" = (SELECT MAX(");
                    p1.Append(c.nome);
                    p1.Append(") FROM ");
                    p1.Append(this.tabela);
                    p1.Append(condicao);
                    p1.Append(" AND ");
                    p1.Append(c.nome);
                    p1.Append(" < ");
                    p1.Append(c.valorBanco);
                    p1.Append(")");
                }
                DataTable dt = new DataTable();
                this.GetConexao.Consultar(p1.ToString(), dt);
                if (dt.Rows.Count > 0)
                {
                    this.Popula(dt);
                }
            }
            catch (Excecao ex)
            {
                ex.HabilitarDetalhes();
            }
        }

        public void Proximo()
        {
            this.Proximo("");
        }

        public void Proximo(string condicao)
        {
            try
            {
                StringBuilder p1 = new StringBuilder();
                StringBuilder p2 = new StringBuilder();
                p1 = new StringBuilder(" SELECT * FROM ");
                p1.Append(this.tabela);
                if (condicao.Length == 0)
                {
                    p1.Append(" WHERE 1 = 1 ");
                    condicao = " WHERE 1 = 1 ";
                }
                else
                {
                    p1.Append(condicao);
                }
                foreach (Campo c in this.Chave)
                {
                    p1.Append(" AND ");
                    p1.Append(c.nome);
                    p1.Append(" = (SELECT MIN(");
                    p1.Append(c.nome);
                    p1.Append(") FROM ");
                    p1.Append(this.tabela);
                    p1.Append(condicao);
                    p1.Append(" AND ");
                    p1.Append(c.nome);
                    p1.Append(" > ");
                    p1.Append(c.valorBanco);
                    p1.Append(")");
                }
                DataTable dt = new DataTable();
                this.GetConexao.Consultar(p1.ToString(), dt);
                if (dt.Rows.Count > 0)
                {
                    this.Popula(dt);
                }
            }
            catch (Excecao ex)
            {
                ex.HabilitarDetalhes();
            }
        }

        public void Ultimo()
        {
            this.Ultimo("");
        }

        public void Ultimo(string condicao)
        {
            try
            {
                StringBuilder p1 = new StringBuilder();
                StringBuilder p2 = new StringBuilder();
                p1 = new StringBuilder(" SELECT * FROM ");
                p1.Append(this.tabela);
                if (condicao.Length == 0)
                {
                    p1.Append(" WHERE 1 = 1 ");
                }
                else
                {
                    p1.Append(condicao);
                }
                foreach (Campo c in this.Chave)
                {
                    p1.Append(" AND ");
                    p1.Append(c.nome);
                    p1.Append(" = (SELECT MAX(");
                    p1.Append(c.nome);
                    p1.Append(") FROM ");
                    p1.Append(this.tabela);
                    p1.Append(condicao);
                    p1.Append(")");
                }
                DataTable dt = new DataTable();
                this.GetConexao.Consultar(p1.ToString(), dt);
                this.Popula(dt);
            }
            catch (Excecao ex)
            {
                ex.HabilitarDetalhes();
            }
        }
        #endregion NAVEGAÇÃO

        #region CONSULTAR
        /// <summary>
		/// Executa o comando sql com a condição da chave primária.
		/// Portanto, passar os valores das chaves antes de chamar esse método.
		/// </summary>
		public void Consultar()
        {
            try
            {
                DataTable dt = new DataTable();
                this.GetConexao.Consultar(this.StringConsultar(), dt);
                this.Popula(dt);
            }
            catch (Excecao ex)
            {
                ex.HabilitarDetalhes();
            }
        }

        /// <summary>
        /// Executa o comando sql com a condição passada no parâmetro
        /// </summary>
        /// <param name="condicao">condição com a palavra WHERE</param>
        public void Consultar(string condicao)
        {
            try
            {
                DataTable dt = new DataTable();
                this.GetConexao.Consultar(this.StringConsultar(condicao), dt);
                this.Popula(dt);
            }
            catch (Excecao ex)
            {
                ex.HabilitarDetalhes();
            }
        }

        /// <summary>
        /// Retorna a sql de consulta
        /// </summary>
        /// <returns></returns>
        public string StringConsultar()
        {
            StringBuilder p1 = new StringBuilder(" SELECT TOP(1) * FROM ");
            p1.Append(this.tabela);
            p1.Append(" WHERE ");
            foreach (Campo c in this.Chave)
            {
                p1.Append(c.nome);
                p1.Append(" = ");
                p1.Append(c.valorBanco);
                p1.Append(" AND ");
            }
            p1.Remove(p1.Length - 5, 5);
            return p1.ToString();
        }

        /// <summary>
        /// Retorna a sql de consulta com a condição passada no parâmetro
        /// </summary>
        /// <param name="condicao">condição com a palavra WHERE</param>
        /// <returns></returns>
        public string StringConsultar(string condicao)
        {
            StringBuilder p1 = new StringBuilder(" SELECT TOP(1) * FROM ");
            p1.Append(this.tabela);
            p1.Append(condicao);
            return p1.ToString();
        }
        #endregion CONSULTAR

        #region CONSULTAR TODOS
        /// <summary>
		/// Executa o comando sql e retorna todos os registros
		/// </summary>
		public DataTable ConsultarTodos()
        {
            return this.ConsultarTodos("");
        }

        /// <summary>
        /// Executa o comando sql consultando pelo campos identidade da tabela e os valores passados por parametro
        /// </summary>
        /// <param name="condicao">condição com a palavra WHERE</param>
        public DataTable ConsultarTodos(string[] ids)
        {
            DataTable dt = new DataTable();
            try
            {
                this.GetConexao.Consultar(this.StringConsultarTodos(ids), dt);
            }
            catch (Excecao ex)
            {
                ex.HabilitarDetalhes();
            }
            return dt;
        }

        /// <summary>
        /// Executa o comando sql com a condição passada no parâmetro e retorna vários registros
        /// </summary>
        /// <param name="condicao">condição com a palavra WHERE</param>
        public DataTable ConsultarTodos(string condicao)
        {
            DataTable dt = new DataTable();
            try
            {
                this.GetConexao.Consultar(this.StringConsultarTodos(condicao), dt);
            }
            catch (Excecao ex)
            {
                ex.HabilitarDetalhes();
            }
            return dt;
        }

        /// <summary>
        /// Retorna a sql para retorna vários registros
        /// </summary>
        /// <returns></returns>
        public string StringConsultarTodos()
        {
            return this.StringConsultarTodos("");
        }

        /// <summary>
        /// Retorna a sql para retorna vários registros
        /// </summary>
        /// <returns></returns>
        public string StringConsultarTodos(string[] ids)
        {
            string campoId = "";
            foreach (Campo c in this.campos)
            {
                if (c.identidade)
                {
                    campoId = c.nome;
                    break;
                }
            }
            if (campoId.Length > 0)
            {
                StringBuilder sb = new StringBuilder();
                foreach (string s in ids)
                {
                    sb.Append(campoId + " = " + Sistema.ToInt(s) + " OR ");
                }
                if (sb.Length > 0)
                {
                    sb.Remove(sb.Length - 4, 4);
                    sb.Insert(0, " WHERE ");
                }
                else
                {
                    sb.Append(" WHERE " + campoId + " = 0 ");
                }
                return this.StringConsultarTodos(sb.ToString());
            }
            return "";
        }

        /// <summary>
        /// Retorna a sql com a condição passada no parâmetro para retorna vários registros
        /// </summary>
        /// <param name="condicao">condição com a palavra WHERE</param>
        /// <returns></returns>
        public string StringConsultarTodos(string condicao)
        {
            StringBuilder p1 = new StringBuilder(" SELECT * FROM ");
            p1.Append(this.tabela);
            p1.Append(condicao);
            return p1.ToString();
        }
        #endregion CONSULTAR TODOS

        /// <summary>
		/// Executa o comando sql e retorna todos os registros que tenham o valor do primeiro campo chave
		/// </summary>
		public DataTable ConsultarTodosChave()
        {
            DataTable dt = new DataTable();
            try
            {
                StringBuilder p1 = new StringBuilder(" SELECT * FROM ");
                p1.Append(this.tabela);
                p1.Append(" WHERE ");
                foreach (Campo c in this.Chave)
                {
                    p1.Append(c.nome);
                    p1.Append(" = ");
                    p1.Append(c.valorBanco);
                    break;
                }
                this.GetConexao.Consultar(p1.ToString(), dt);
            }
            catch (Excecao ex)
            {
                ex.HabilitarDetalhes();
            }
            return dt;
        }

        #region CONSULTAR DETALHES
        /// <summary>
        /// Executa o comando sql e retorna todos os registros que tenham o valor do(s) campo(s) cabecalho
        /// </summary>
        public string StringConsultarDetalhes()
        {
            StringBuilder p1 = new StringBuilder(" SELECT * FROM ");
            p1.Append(this.tabela);
            p1.Append(" WHERE ");
            foreach (Campo c in this.campos)
            {
                if (c.cabecalho)
                {
                    p1.Append(c.nome);
                    p1.Append(" = ");
                    p1.Append(c.valorBanco);
                    p1.Append(" AND ");
                }
                else
                {
                    break;
                }
            }
            p1.Remove(p1.Length - 5, 5);
            return p1.ToString() + "; ";
        }

        /// <summary>
        /// Executa o comando sql e retorna todos os registros que tenham o valor do(s) campo(s) cabecalho
        /// </summary>
        public DataTable ConsultarDetalhes()
        {
            DataTable dt = new DataTable();
            try
            {
                StringBuilder p1 = new StringBuilder(" SELECT * FROM ");
                p1.Append(this.tabela);
                p1.Append(" WHERE ");
                foreach (Campo c in this.campos)
                {
                    if (c.cabecalho)
                    {
                        p1.Append(c.nome);
                        p1.Append(" = ");
                        p1.Append(c.valorBanco);
                        p1.Append(" AND ");
                    }
                    else
                    {
                        break;
                    }
                }
                p1.Remove(p1.Length - 5, 5);
                this.GetConexao.Consultar(p1.ToString(), dt);
            }
            catch (Excecao ex)
            {
                ex.HabilitarDetalhes();
            }
            return dt;
        }
        #endregion CONSULTAR DETALHES

        #region CONSULTAR TODOS SQL
        /// <summary>
		/// Executa o comando sql e retorna os registros
		/// </summary>
		/// <param name="sql">Comando SQL</param>
		/// <returns></returns>
		public DataTable ConsultarTodosSQL(string sql)
        {
            DataTable dt = new DataTable();
            try
            {
                this.GetConexao.Consultar(sql, dt);
            }
            catch (Excecao ex)
            {
                ex.HabilitarDetalhes();
            }
            return dt;
        }

        public DataTable ConsultarTodosSQL(string sql, int tempotimeout)
        {
            DataTable dt = new DataTable();
            try
            {
                this.GetConexao.Consultar(sql, dt, tempotimeout);
            }
            catch (Excecao ex)
            {
                ex.HabilitarDetalhes();
            }
            return dt;
        }

        /// <summary>
        /// Executa o comando sql e retorna os registros
        /// </summary>
        /// <param name="sql">Comando SQL</param>
        /// <returns></returns>
        public DataTable[] ConsultarTodosSQL(string[] sqls)
        {
            try
            {
                return this.GetConexao.ConsultarVarios(sqls);
            }
            catch (Excecao ex)
            {
                ex.HabilitarDetalhes();
            }
            return new DataTable[sqls.Length];
        }
        #endregion CONSULTAR TODOS SQL

        #region CARREGAR
        #region CARREGAR ComboBox
        /// <summary>
		/// Carrega o combobox com todos os registros: 
		/// Value é o primeiro campo da tabela
		/// Display é o segundo campo da tabela
		/// </summary>
		/// <param name="combo"></param>
		public void Carregar(ComboBox combo)
        {
            this.Carregar(combo, this.campos[0], this.campos[1], "", false, true);
        }

        public void Carregar(ComboBox combo, bool adicionarvazia)
        {
            this.Carregar(combo, this.campos[0], this.campos[1], "", adicionarvazia, true);
        }

        public void Carregar(ComboBox combo, string condicao)
        {
            this.Carregar(combo, this.campos[0], this.campos[1], condicao, false, true);
        }

        public void Carregar(ComboBox combo, string condicao, bool adicionarvazia, bool ajustarDrop)
        {
            this.Carregar(combo, this.campos[0], this.campos[1], condicao, adicionarvazia, ajustarDrop);
        }

        public void Carregar(ComboBox combo, string condicao, bool adicionarvazia)
        {
            this.Carregar(combo, this.campos[0], this.campos[1], condicao, adicionarvazia, true);
        }

        public void Carregar(ComboBox combo, Campo value, Campo display)
        {
            this.Carregar(combo, value, display, "", false, true);
        }

        public void Carregar(ComboBox combo, Campo value, Campo display, string condicao)
        {
            this.Carregar(combo, value, display, condicao, false, true);
        }

        public void Carregar(ComboBox combo, Campo value, Campo display, string condicao, bool adicionarvazia)
        {
            this.Carregar(combo, value, display, condicao, adicionarvazia, true);
        }

        public void Carregar(ComboBox combo, Campo value, Campo display, string condicao, bool adicionarvazia, bool ajustarDrop)
        {
            DataTable dt = new DataTable();
            this.GetConexao.Consultar(this.StringCarregarCombo(value, display, condicao), dt);
            Sistema.CarregarCombo(combo, dt, adicionarvazia, value.nome, display.nome, ajustarDrop);
        }
        #endregion CARREGAR ComboBox

        #region CARREGAR DataGridViewComboBoxColumn
        /// <summary>
        /// Carrega o combobox com todos os registros: 
        /// Value é o primeiro campo da tabela
        /// Display é o segundo campo da tabela
        /// </summary>
        /// <param name="combo"></param>
        public void Carregar(DataGridViewComboBoxColumn combo)
        {
            this.Carregar(combo, this.campos[0], this.campos[1], "", false, true);
        }

        public void Carregar(DataGridViewComboBoxColumn combo, bool adicionarvazia)
        {
            this.Carregar(combo, this.campos[0], this.campos[1], "", adicionarvazia, true);
        }

        public void Carregar(DataGridViewComboBoxColumn combo, string condicao)
        {
            this.Carregar(combo, this.campos[0], this.campos[1], condicao, false, true);
        }

        public void Carregar(DataGridViewComboBoxColumn combo, Campo value, Campo display)
        {
            this.Carregar(combo, value, display, "", false, true);
        }

        public void Carregar(DataGridViewComboBoxColumn combo, Campo value, Campo display, string condicao)
        {
            this.Carregar(combo, value, display, condicao, false, true);
        }

        public void Carregar(DataGridViewComboBoxColumn combo, Campo value, Campo display, string condicao, bool adicionarvazia)
        {
            this.Carregar(combo, value, display, condicao, adicionarvazia, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="combo"></param>
        /// <param name="value"></param>
        /// <param name="display"></param>
        /// <param name="condicao">Ex: WHERE campo = 1</param>
        public void Carregar(DataGridViewComboBoxColumn combo, Campo value, Campo display, string condicao, bool adicionarvazia, bool ajustarDrop)
        {
            DataTable dt = new DataTable();
            this.GetConexao.Consultar(this.StringCarregarCombo(value, display, condicao), dt);
            Sistema.CarregarCombo(combo, dt, adicionarvazia, value.nome, display.nome, ajustarDrop);
        }
        #endregion CARREGAR DataGridViewComboBoxColumn

        #region CARREGAR DataGridViewComboBoxCell
        public void Carregar(DataGridViewComboBoxCell combo)
        {
            this.Carregar(combo, this.campos[0], this.campos[1], "", false, true);
        }

        public void Carregar(DataGridViewComboBoxCell combo, bool adicionarvazia)
        {
            this.Carregar(combo, this.campos[0], this.campos[1], "", adicionarvazia, true);
        }

        public void Carregar(DataGridViewComboBoxCell combo, string condicao)
        {
            this.Carregar(combo, this.campos[0], this.campos[1], condicao, false, true);
        }

        public void Carregar(DataGridViewComboBoxCell combo, Campo value, Campo display)
        {
            this.Carregar(combo, value, display, "", false);
        }

        public void Carregar(DataGridViewComboBoxCell combo, Campo value, Campo display, string condicao)
        {
            this.Carregar(combo, value, display, condicao, false, true);
        }

        public void Carregar(DataGridViewComboBoxCell combo, Campo value, Campo display, string condicao, bool adicionarvazia)
        {
            this.Carregar(combo, value, display, condicao, false, true);
        }

        public void Carregar(DataGridViewComboBoxCell combo, Campo value, Campo display, string condicao, bool adicionarvazia, bool ajustarDrop)
        {
            DataTable dt = new DataTable();
            this.GetConexao.Consultar(this.StringCarregarCombo(value, display, condicao), dt);
            Sistema.CarregarCombo(combo, dt, adicionarvazia, value.nome, display.nome, ajustarDrop);
        }
        #endregion CARREGAR DataGridViewComboBoxCell

        /// <summary>
        /// Retorna a string sql para carregar ComboBox, DataGridViewComboBoxColumn ou DataGridViewComboBoxCell
        /// </summary>
        /// <returns></returns>
        public string StringCarregarCombo()
        {
            return this.StringCarregarCombo(this.campos[0], this.campos[1], "");
        }

        /// <summary>
        /// Retorna a string sql para carregar ComboBox, DataGridViewComboBoxColumn ou DataGridViewComboBoxCell
        /// </summary>
        /// <returns></returns>
        public string StringCarregarCombo(string condicao)
        {
            return this.StringCarregarCombo(this.campos[0], this.campos[1], condicao);
        }

        /// <summary>
        /// Retorna a string sql para carregar ComboBox, DataGridViewComboBoxColumn ou DataGridViewComboBoxCell
        /// </summary>
        /// <returns></returns>
        public string StringCarregarCombo(Campo value, Campo display, string condicao)
        {
            StringBuilder p1 = new StringBuilder("SELECT ");
            p1.Append(value.nome);
            p1.Append(", ");
            p1.Append(display.nome);
            p1.Append(" FROM ");
            p1.Append(this.tabela);
            p1.Append(condicao);
            p1.Append(" ORDER BY " + display.nome);
            return p1.ToString();
        }
        #endregion CARREGAR

        #region CARREGAR GridRow
        public void CarregarGridRow(DataGridViewRow row)
        {
            CarregarGridRow(row, null, "");
        }

        public void CarregarGridRow(DataGridViewRow row, string id_cabecalho)
        {
            this.CarregarGridRow(row, null, id_cabecalho);
        }

        public void CarregarGridRow(DataGridViewRow row, Persistencia cabecalho)
        {
            this.CarregarGridRow(row, cabecalho, "");
        }

        private void CarregarGridRow(DataGridViewRow row, Persistencia cabecalho, string id_cabecalho)
        {
            foreach (Campo c in this.campos)
            {
                if (c.cabecalho)
                {
                    if (cabecalho != null)
                    {
                        c.valor = cabecalho.campo(c.nome).valor;
                    }
                    else if (id_cabecalho.Length > 0)
                    {
                        c.valor = id_cabecalho;
                    }
                }
                else
                {
                    c.valor = "";
                    foreach (DataGridViewColumn o in row.DataGridView.Columns)
                    {
                        if (c.nome.Equals(o.DataPropertyName))
                        {
                            if ((c.inteiro || c.numero) && !c.identidade)
                            {
                                c.valor = Sistema.ToString(row.Cells[o.Index].Value).Replace(".", "");
                            }
                            else
                            {
                                c.valor = Sistema.ToString(row.Cells[o.Index].Value);
                            }
                            break;
                        }
                    }
                }
            }
        }
        #endregion CARREGAR GridRow

        #region GRAVAR Grade
        public void GravarGrade(DataGridView grid)
        {
            this.GravarGrade(grid, null, "", null);
        }

        public void GravarGrade(DataGridView grid, string id_cabecalho)
        {
            this.GravarGrade(grid, null, id_cabecalho, null);
        }

        public void GravarGrade(DataGridView grid, string id_cabecalho, DataTable dtComparacao)
        {
            this.GravarGrade(grid, null, id_cabecalho, dtComparacao);
        }

        public void GravarGrade(DataGridView grid, Persistencia cabecalho)
        {
            this.GravarGrade(grid, cabecalho, "", null);
        }

        public void GravarGrade(DataGridView grid, Persistencia cabecalho, DataTable dtComparacao)
        {
            this.GravarGrade(grid, cabecalho, "", dtComparacao);
        }

        private void GravarGrade(DataGridView grid, Persistencia cabecalho, string id_cabecalho, DataTable dtComparacao)
        {
            StringBuilder sbExcluir = new StringBuilder();
            List<int> linhasAlteradas = null;
            if (dtComparacao != null)
            {
                linhasAlteradas = Sistema.LinhasAlteradas(dtComparacao, grid);
            }
            List<Persistencia> lista = new List<Persistencia>();
            foreach (DataGridViewRow r in grid.Rows)
            {
                if (!r.IsNewRow)
                {
                    Persistencia p = this.Copy();
                    if (cabecalho != null)
                    {
                        p.CarregarGridRow(r, cabecalho);
                    }
                    else
                    {
                        p.CarregarGridRow(r, id_cabecalho);
                    }
                    if (linhasAlteradas == null || linhasAlteradas.Contains(r.Index))
                    {
                        lista.Add(p);
                    }
                    else
                    {
                        foreach (Campo c in p.campos)
                        {
                            if (c.identidade || (c.chave && !c.cabecalho))
                            {
                                sbExcluir.Append(" AND ");
                                sbExcluir.Append(c.nome);
                                sbExcluir.Append(" <> ");
                                sbExcluir.Append(c.valorBanco);
                                //AQUI DEVE-SE CARREGAR O IDENTIDADE OU CHAVE NAO CABECALHO NA COLUNA DA GRADE CORRESPONDENTE  
                                // !CUIDADO NO CASO DE DAR ERRO AO GRAVAR (DEVE-SE POPULAR AS COLUNAS CHAVE E CABECALHO APOS TERMINAR TODAS AS GRAVACOES )
                                break;
                            }
                        }
                    }
                }
            }
            string[] ids = this.GravarVarios(lista.ToArray());
            foreach (string s in ids)
            {
                foreach (Campo c in this.campos)
                {
                    if (c.identidade || (c.chave && !c.cabecalho))
                    {
                        sbExcluir.Append(" AND ");
                        sbExcluir.Append(c.nome);
                        sbExcluir.Append(" <> ");
                        sbExcluir.Append(Sistema.ToInt(s));
                        //AQUI DEVE-SE CARREGAR O IDENTIDADE OU CHAVE NAO CABECALHO NA COLUNA DA GRADE CORRESPONDENTE  
                        // !CUIDADO NO CASO DE DAR ERRO AO GRAVAR (DEVE-SE POPULAR AS COLUNAS CHAVE E CABECALHO APOS TERMINAR TODAS AS GRAVACOES )
                        break;
                    }
                }
            }
            foreach (Campo c in this.campos)
            {
                if (c.cabecalho)
                {
                    sbExcluir.Append(" AND ");
                    sbExcluir.Append(c.nome);
                    sbExcluir.Append(" = ");
                    if (cabecalho != null)
                    {
                        sbExcluir.Append(cabecalho.campo(c.nome).valorBanco);
                    }
                    else
                    {
                        sbExcluir.Append(id_cabecalho);
                    }
                }
                else
                {
                    break;
                }
            }
            if (sbExcluir.Length > 0)
            {
                this.ExcluirTodosSQL(sbExcluir.Remove(0, 5).Insert(0, " WHERE ").ToString());
            }
        }
        #endregion GRAVAR Grade

        #region CARREGAR DataTableRow
        public void CarregarDataTableRow(DataRow row)
        {
            CarregarDataTableRow(row, null);
        }

        public void CarregarDataTableRow(DataRow row, Persistencia cabecalho)
        {
            foreach (Campo c in this.campos)
            {
                if (c.cabecalho && cabecalho != null)
                {
                    c.valor = cabecalho.campo(c.nome).valor;
                }
                else
                {
                    c.valor = "";
                    foreach (DataColumn o in row.Table.Columns)
                    {
                        if (c.nome.Equals(o.ColumnName))
                        {
                            if (c.inteiro && !c.identidade && !c.chaveestrangeira)
                            {
                                c.valor = Sistema.ToString(Sistema.ToInt(Sistema.ToString(row[o.ColumnName]).Replace(".", "")));
                            }
                            else if (c.numero && !c.identidade && !c.chaveestrangeira)
                            {
                                c.valor = Sistema.ToString(Sistema.ToDecimal(Sistema.ToString(row[o.ColumnName]).Replace(".", "")));
                            }
                            else
                            {
                                c.valor = Sistema.ToString(row[o.ColumnName]);
                            }
                            break;
                        }
                    }
                }
            }
        }
        #endregion CARREGAR DataTableRow

        public void PopularGridRow(DataGridViewRow row)
        {
            foreach (DataGridViewColumn col in row.DataGridView.Columns)
            {
                Campo ca = this.campo(col.DataPropertyName.ToLower());
                if (ca != null)
                {
                    if (col is DataGridViewTextBoxColumn)
                    {
                        if (ca.numero)
                        {
                            row.Cells[col.Name].Value = Sistema.ToDecimal(ca.valor);
                        }
                        else
                        {
                            row.Cells[col.Name].Value = ca.valor;
                        }
                    }
                    else if (col is DataGridViewComboBoxColumn)
                    {
                        if (((DataGridViewComboBoxCell)row.Cells[col.Name]).ValueMember.Length > 0)
                        {
                            if (ca.valor.Length > 0)
                            {
                                row.Cells[col.Name].Value = Sistema.ToInt(ca.valor);
                            }
                            else
                            {
                                row.Cells[col.Name].Value = null;
                            }
                        }
                        else
                        {
                            row.Cells[col.Name].Value = Sistema.ToString(ca.valor);
                        }
                    }
                    else if (col is DataGridViewCheckBoxColumn)
                    {
                        row.Cells[col.Name].Value = Sistema.ToBool(ca.valor);
                    }
                }
            }
        }

        public Campo campo(string nome)
        {
            foreach (Campo c in this.campos)
            {
                if (c.nome.Equals(nome))
                {
                    return c;
                }
            }
            return null;
        }

        #region POPULA
        public void Popula(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                this.Popula(dt.Rows[0]);
            }
            else
            {
                this.Limpar();
            }
        }

        public void Popula(DataRow dr)
        {
            this.Popula(dr, false);
        }

        public void Popula(DataRow dr, bool existentes)
        {
            foreach (Campo c in this.campos)
            {
                if (!existentes || dr.Table.Columns.Contains(c.nome))
                {
                    if (c.arquivo)
                    {
                        c.bytes = new byte[1];
                        if (this.GetConexao.IsPostgreSQL())
                        {
                            try
                            {
                                c.bytes = Sistema.ToBytesPostgres(Sistema.ToString(dr[c.nome]));
                            }
                            catch
                            {
                                c.bytes = new byte[1];
                            }
                        }
                        else
                        {
                            try
                            {
                                c.bytes = (byte[])dr[c.nome];
                            }
                            catch
                            {
                                c.bytes = new byte[1];
                            }
                        }
                    }
                    else
                    {
                        try
                        {
                            c.valorBanco = dr[c.nome].ToString();
                        }
                        catch
                        {
                            throw new Exception("O Campo " + c.nome + " não pertence a tabela");
                        }
                    }
                }
            }
        }
        #endregion POPULA

        public void Limpar()
        {
            foreach (Campo c in this.campos)
            {
                c.SetPadrao();
            }
        }

        public Persistencia Copy()
        {
            Persistencia p = (Persistencia)Activator.CreateInstance(this.GetType());
            p.campos.Clear();
            p.Chave.Clear();
            foreach (Campo c in this.campos)
            {
                Campo novo = c.Copy();
                p.campos.Add(novo);
                if (novo.identidade)
                {
                    p.Identidade = novo;
                    p.Chave.Add(novo);
                }
                else if (novo.chave)
                {
                    p.Chave.Add(novo);
                }
            }
            p.tabela = (string)this.tabela.Clone();
            p.ConexaoPadrao = this.ConexaoPadrao;
            if (this.Conexao != null)
            {
                p.Conexao = this.Conexao.Copy();
            }
            return p;
        }

        public override bool Equals(object obj)
        {
            if (obj is Persistencia)
            {
                Persistencia p = (Persistencia)obj;
                if (p.tabela != this.tabela)
                {
                    return false;
                }
                if (p.campos.Count != this.campos.Count)
                {
                    return false;
                }
                for (int i = 0; i < this.campos.Count; i++)
                {
                    if (!p.campos[i].Equals(this.campos[i]))
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }

        #region ADD CAMPO
        /// <summary>
        /// Adiciona um campo Chave não nulo que é uma identidade (Auto-Incremento)
        /// </summary>
        /// <param name="nomeCampo"></param>
        public void AddIdentidade(string nomeCampo)
        {
            Campo c = new Campo(nomeCampo);
            c.inteiro = true;
            c.chave = true;
            c.identidade = true;
            c.nulo = false;
            this.campos.Add(c);
            this.Identidade = c;
            this.Chave.Add(c);
        }

        /// <summary>
        /// Adiciona um campo chave do tipo inteiro não nulo
        /// </summary>
        /// <param name="nomeCampo"></param>
        public void AddChave(string nomeCampo)
        {
            Campo c = new Campo(nomeCampo);
            c.inteiro = true;
            c.chave = true;
            c.nulo = false;
            this.campos.Add(c);
            this.Chave.Add(c);
        }

        /// <summary>
        /// Adiciona um campo chave do tipo inteiro não nulo.
        /// </summary>
        /// <param name="nomeCampo"></param>
        /// <param name="tabelafk">A tabela FK. O campo FK é o nomeCampo. On Delete CASCADE e ON Update CASCADE</param>
        public void AddChave(string nomeCampo, string tabelafk)
        {
            Campo c = new Campo(nomeCampo);
            c.inteiro = true;
            c.chave = true;
            c.nulo = false;
            c.chaveestrangeira = true;
            c.tabelafk = tabelafk;
            c.aoapagar = DeleteUpdate.CASCADE;
            c.aoatualizar = DeleteUpdate.CASCADE;
            this.campos.Add(c);
            this.Chave.Add(c);
        }

        /// <summary>
        /// Adiciona um campo chave do tipo inteiro não nulo.
        /// </summary>
        /// <param name="nomeCampo"></param>
        /// <param name="tabelafk"></param>
        /// <param name="campofk"></param>
        /// <param name="aoapagar"></param>
        /// <param name="aoatualizar"></param>
        public void AddChave(string nomeCampo, string tabelafk, DeleteUpdate aoapagar, DeleteUpdate aoatualizar)
        {
            Campo c = new Campo(nomeCampo);
            c.inteiro = true;
            c.chave = true;
            c.nulo = false;
            c.chaveestrangeira = true;
            c.tabelafk = tabelafk;
            c.aoapagar = aoapagar;
            c.aoatualizar = aoatualizar;
            this.campos.Add(c);
            this.Chave.Add(c);
        }

        /// <summary>
        /// Adiciona um campo chave do tipo inteiro não nulo.
        /// </summary>
        /// <param name="nomeCampo"></param>
        /// <param name="tabelafk"></param>
        /// <param name="campofk"></param>
        /// <param name="aoapagar"></param>
        /// <param name="aoatualizar"></param>
        public void AddChave(string nomeCampo, string tabelafk, DeleteUpdate aoapagar, DeleteUpdate aoatualizar, string campofk2)
        {
            Campo c = new Campo(nomeCampo);
            c.inteiro = true;
            c.chave = true;
            c.nulo = false;
            c.chaveestrangeira = true;
            c.tabelafk = tabelafk;
            c.campofk2 = campofk2;
            c.aoapagar = aoapagar;
            c.aoatualizar = aoatualizar;
            this.campos.Add(c);
            this.Chave.Add(c);
        }

        /// <summary>
        /// Adiciona um campo não chave do tipo inteiro não nulo que representa o id da tabela Cabecalho
        /// </summary>
        /// <param name="nomeCampo"></param>
        /// <param name="tabelafk">A tabela FK. O campo FK é o nomeCampo. On Delete CASCADE e ON Update CASCADE</param>
        public void AddCabecalho(string nomeCampo, string tabelafk)
        {
            Campo c = new Campo(nomeCampo);
            c.inteiro = true;
            c.cabecalho = true;
            c.nulo = false;
            c.chaveestrangeira = true;
            c.tabelafk = tabelafk;
            c.aoapagar = DeleteUpdate.CASCADE;
            c.aoatualizar = DeleteUpdate.CASCADE;
            this.campos.Add(c);
        }

        /// <summary>
        /// Adiciona um campo chave do tipo inteiro não nulo que representa o id da tabela Cabecalho
        /// </summary>
        /// <param name="nomeCampo"></param>
        /// <param name="tabelafk">A tabela FK. O campo FK é o nomeCampo. On Delete CASCADE e ON Update CASCADE</param>
        public void AddChaveCabecalho(string nomeCampo, string tabelafk)
        {
            Campo c = new Campo(nomeCampo);
            c.inteiro = true;
            c.chave = true;
            c.cabecalho = true;
            c.nulo = false;
            c.chaveestrangeira = true;
            c.tabelafk = tabelafk;
            c.aoapagar = DeleteUpdate.CASCADE;
            c.aoatualizar = DeleteUpdate.CASCADE;
            this.campos.Add(c);
            this.Chave.Add(c);
        }

        public void AddChaveTexto(string nomeCampo)
        {
            Campo c = new Campo(nomeCampo);
            c.texto = true;
            c.chave = true;
            c.nulo = false;
            this.campos.Add(c);
            this.Chave.Add(c);
        }

        public void AddChaveData(string nomeCampo)
        {
            Campo c = new Campo(nomeCampo);
            c.data = true;
            c.chave = true;
            c.nulo = false;
            this.campos.Add(c);
            this.Chave.Add(c);
        }

        public void AddInteiro(string nomeCampo)
        {
            Campo c = new Campo(nomeCampo);
            c.inteiro = true;
            this.campos.Add(c);
        }

        /// <summary>
        /// Adiciona um campo chave do tipo inteiro não nulo.
        /// </summary>
        /// <param name="nomeCampo"></param>
        /// <param name="tabelafk">A tabela FK. O campo FK é o nomeCampo. On Delete NOACTION e ON Update NOACTION</param>
        public void AddInteiro(string nomeCampo, string tabelafk)
        {
            Campo c = new Campo(nomeCampo);
            c.inteiro = true;
            c.chaveestrangeira = true;
            c.tabelafk = tabelafk;
            c.aoapagar = DeleteUpdate.NOACTION;
            c.aoatualizar = DeleteUpdate.NOACTION;
            this.campos.Add(c);
        }

        /// <summary>
        /// Adiciona um campo chave do tipo inteiro não nulo.
        /// </summary>
        /// <param name="nomeCampo"></param>
        /// <param name="tabelafk"></param>
        /// <param name="campofk"></param>
        /// <param name="aoapagar"></param>
        /// <param name="aoatualizar"></param>
        public void AddInteiro(string nomeCampo, string tabelafk, DeleteUpdate aoapagar, DeleteUpdate aoatualizar)
        {
            Campo c = new Campo(nomeCampo);
            c.inteiro = true;
            c.chaveestrangeira = true;
            c.tabelafk = tabelafk;
            c.aoapagar = aoapagar;
            c.aoatualizar = aoatualizar;
            this.campos.Add(c);
        }

        /// <summary>
        /// Adiciona um campo chave do tipo inteiro não nulo.
        /// </summary>
        /// <param name="nomeCampo"></param>
        /// <param name="tabelafk"></param>
        /// <param name="campofk"></param>
        /// <param name="aoapagar"></param>
        /// <param name="aoatualizar"></param>
        public void AddInteiro(string nomeCampo, string tabelafk, DeleteUpdate aoapagar, DeleteUpdate aoatualizar, string campofk2)
        {
            Campo c = new Campo(nomeCampo);
            c.inteiro = true;
            c.chaveestrangeira = true;
            c.tabelafk = tabelafk;
            c.campofk2 = campofk2;
            c.aoapagar = aoapagar;
            c.aoatualizar = aoatualizar;
            this.campos.Add(c);
        }

        public void AddFlutuante(string nomeCampo)
        {
            Campo c = new Campo(nomeCampo);
            c.flutuante = true;
            this.campos.Add(c);
        }

        /// <summary>
        /// Tipo NUMERO
        /// Utilizado para VALORES com padrão: (19,2) - (tamanho total, tamanho decimal) - ((17)00000000000000000.00(2))
        /// </summary>
        public void AddMoeda(string nomeCampo)
        {
            Campo c = new Campo(nomeCampo);
            c.numero = true;
            c.tamanho = 19;
            c.tamanhoDecimal = 2;
            this.campos.Add(c);
        }

        /// <summary>
        /// Utilizado para QUANTIDADES e VALORES (19, 9) - ((10)0000000000.000000000(9))
        /// Padrão: 9 decimais
        /// </summary>
        public void AddNumero(string nomeCampo)
        {
            Campo c = new Campo(nomeCampo);
            c.numero = true;
            c.tamanho = 19;
            c.tamanhoDecimal = 9;
            this.campos.Add(c);
        }

        /// <summary>
        /// Utilizado para QUANTIDADES e VALORES (19, tamanho decimal) - ((10)0000000000.000000000(9))
        /// Padrão: Utilizar preferencialmente 9 decimais
        /// </summary>
		public void AddNumero(string nomeCampo, int tamanhoDecimalCampo)
        {
            Campo c = new Campo(nomeCampo);
            c.numero = true;
            c.tamanho = 19;
            c.tamanhoDecimal = tamanhoDecimalCampo;
            this.campos.Add(c);
        }

        public void AddNumero(string nomeCampo, int tamanhoDecimalCampo, int tamanho)
        {
            Campo c = new Campo(nomeCampo);
            c.numero = true;
            c.tamanho = tamanho;
            c.tamanhoDecimal = tamanhoDecimalCampo;
            this.campos.Add(c);
        }

        /// <summary>
        /// Utilizado para QUALQUER TEXTO, inclusive OBSERVAÇÕES
        /// Padrão: nvarchar(MAX)
        /// </summary>
        /// <param name="nomeCampo"></param>
		public void AddTexto(string nomeCampo)
        {
            Campo c = new Campo(nomeCampo);
            c.texto = true;
            this.campos.Add(c);
        }

        /// <summary>
        /// Utilizado para QUALQUER TEXTO, inclusive OBSERVAÇÕES
        /// Padrão: nvarchar(MAX)
        /// </summary>
        /// <param name="nomeCampo"></param>
        public void AddTexto(string nomeCampo, bool criptografado)
        {
            Campo c = new Campo(nomeCampo);
            c.texto = true;
            c.criptografado = criptografado;
            this.campos.Add(c);
        }

        /// <summary>
        /// Utilizado para textos muito grandes
        /// Utilizar somente se necessário
        /// Para OBSERVAÇÕES utilizar nvarchar(MAX) = addTexto(campo)
        /// </summary>
        /// <param name="nomeCampo"></param>
        public void AddNTexto(string nomeCampo)
        {
            Campo c = new Campo(nomeCampo);
            c.ntexto = true;
            this.campos.Add(c);
        }

        public void AddLogico(string nomeCampo)
        {
            Campo c = new Campo(nomeCampo);
            c.logico = true;
            this.campos.Add(c);
        }

        public void AddData(string nomeCampo)
        {
            Campo c = new Campo(nomeCampo);
            c.data = true;
            this.campos.Add(c);
        }

        public void AddTempo(string nomeCampo)
        {
            Campo c = new Campo(nomeCampo);
            c.tempo = true;
            this.campos.Add(c);
        }

        public void AddDataTempo(string nomeCampo)
        {
            Campo c = new Campo(nomeCampo);
            c.dataTempo = true;
            this.campos.Add(c);
        }

        public void AddDataAlteracao(string nomeCampo)
        {
            Campo c = new Campo(nomeCampo);
            c.dataTempo = true;
            c.dataAlteracao = true;
            this.campos.Add(c);
        }

        public void AddImagem(string nomeCampo)
        {
            Campo c = new Campo(nomeCampo);
            c.arquivo = true;
            c.imagem = true;
            c.nulo = false;
            this.campos.Add(c);
        }

        public void AddArquivo(string nomeCampo)
        {
            Campo c = new Campo(nomeCampo);
            c.arquivo = true;
            c.imagem = false;
            c.nulo = false;
            this.campos.Add(c);
        }
        #endregion ADD CAMPO
    }
}
