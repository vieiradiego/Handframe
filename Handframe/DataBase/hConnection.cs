using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Data;
using System.Data.Common;
using Handframe.Persistence;

namespace Handframe.DataBase
{
    public interface hConnection
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        bool Create(string server, string port, string dataBase, string user, string password);

        [MethodImpl(MethodImplOptions.Synchronized)]
        bool Connect(string server, string port, string dataBase, string user, string password);

        [MethodImpl(MethodImplOptions.Synchronized)]
        void Disconnect();

        [MethodImpl(MethodImplOptions.Synchronized)]
        bool Consult(string sql);

        [MethodImpl(MethodImplOptions.Synchronized)]
        bool Consult(string sql, DataTable dataTable);

        [MethodImpl(MethodImplOptions.Synchronized)]
        bool Consult(string sql, DataTable dataTable, int timeOut);

        [MethodImpl(MethodImplOptions.Synchronized)]
        bool Consult(string sql, List<DbParameter> parameters, DataTable dataTable);

        [MethodImpl(MethodImplOptions.Synchronized)]
        DataTable[] MultipleConsult(string[] sql);

        [MethodImpl(MethodImplOptions.Synchronized)]
        DataTable[] MultipleConsult(string[] sql, List<DbParameter> parameters);

        [MethodImpl(MethodImplOptions.Synchronized)]
        int Update(string sql);

        [MethodImpl(MethodImplOptions.Synchronized)]
        int Update(string sql, bool tratarExcecao);

        [MethodImpl(MethodImplOptions.Synchronized)]
        int Update(string sql, List<DbParameter> parameters, bool exceptionCare);

        [MethodImpl(MethodImplOptions.Synchronized)]
        void Start();

        [MethodImpl(MethodImplOptions.Synchronized)]
        void End();

        [MethodImpl(MethodImplOptions.Synchronized)]
        void End(bool rollback);

        [MethodImpl(MethodImplOptions.Synchronized)]
        void Execute(bool resultado, string mensagem);

        [MethodImpl(MethodImplOptions.Synchronized)]
        string ToData(string valor);

        [MethodImpl(MethodImplOptions.Synchronized)]
        string ToData(DateTime data);

        [MethodImpl(MethodImplOptions.Synchronized)]
        string ToTime(string valor);

        [MethodImpl(MethodImplOptions.Synchronized)]
        string ToTime(DateTime data);

        [MethodImpl(MethodImplOptions.Synchronized)]
        int ToInt(string valor);

        [MethodImpl(MethodImplOptions.Synchronized)]
        string Server();

        [MethodImpl(MethodImplOptions.Synchronized)]
        string Port();

        [MethodImpl(MethodImplOptions.Synchronized)]
        string DataBase();

        [MethodImpl(MethodImplOptions.Synchronized)]
        string User();

        [MethodImpl(MethodImplOptions.Synchronized)]
        string Password();

        [MethodImpl(MethodImplOptions.Synchronized)]
        int CountLines(string table);

        [MethodImpl(MethodImplOptions.Synchronized)]
        int CountLines(string table, string condition);

        [MethodImpl(MethodImplOptions.Synchronized)]
        int CountConnections();

        [MethodImpl(MethodImplOptions.Synchronized)]
        DateTime DateServer();

        [MethodImpl(MethodImplOptions.Synchronized)]
        bool ExistColumn(hPersistence h, string column);

        [MethodImpl(MethodImplOptions.Synchronized)]
        DataTable ConsultarCampos(hPersistence h);

        [MethodImpl(MethodImplOptions.Synchronized)]
        void UpdateTable(hPersistence h);

        [MethodImpl(MethodImplOptions.Synchronized)]
        bool ExistTable(hPersistence h);

        [MethodImpl(MethodImplOptions.Synchronized)]
        bool ExistTable(string table);

        [MethodImpl(MethodImplOptions.Synchronized)]
        bool ExistDataBase(string server, string port, string user, string password, string dataBase);

        [MethodImpl(MethodImplOptions.Synchronized)]
        hConnection Copy();

        bool IsMySQL();

        bool IsPostgreSQL();

        bool IsMSSQL();

        bool IsOracle();
    }
}
