using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text;

namespace WLib.Core.Bll.DataAccess.SQLManagement
{
    public class DirectSQL : IDisposable
    {
        private const string Delimiter = ";";
        private bool _created;

        private readonly object _lock = new object();
        private IDirectSQLConnection _connection;

        public DirectSQL(IDirectSQLConnection connection)
        {
            if (!_created)
            {
                SetConnection(connection);
            }
        }

        private void SetConnection(IDirectSQLConnection connection)
        {
            lock (_lock)
            {
                _connection = connection;
                _connection.Open();
                _created = true;
            }
        }
        public int? ExecuteScalar(string sql)
        {
            IDataReader reader = ExecuteReader(sql);
            if (reader.Read() == false)
                return null;
            if (reader.GetValue(0).Equals(DBNull.Value))
                return null;
            return reader.GetInt32(0);
        }

        public decimal? ExecuteDecimal(string sql)
        {

            IDataReader reader = ExecuteReader(sql);
            if (reader.Read() == false)
                return null;
            if (reader.GetValue(0).Equals(DBNull.Value))
                return null;
            return reader.GetDecimal(0);
        }

        public string ExecuteString(string sql)
        {

            IDataReader reader = ExecuteReader(sql);
            if (reader.Read() == false)
                return null;
            if (reader.GetValue(0).Equals(DBNull.Value))
                return null;
            return reader.GetString(0);
        }

        /// <summary>
        /// takes a single parameter, which is a collection of sql
        /// statements, separated by enter symbols
        /// </summary>
        public void ExecuteStatements(string statements, bool swallowExceptions = false)
        {

            foreach (string s in statements.Split('\n'))
            {
                if (string.IsNullOrEmpty(s.Trim()) == true)
                    continue;
                try
                {
                    Execute(s);
                }
                catch (Exception e)
                {
                    Debug.WriteLine("swallowed exception: " + e.Message);
                    if (swallowExceptions == false)
                        throw;

                }
            }
        }

        /// <summary>
        /// Returns number of rows affected
        /// </summary>
        /// <param name="sqlStatement"></param>
        /// <returns></returns>
        public int Execute(string sqlStatement)
        {
            lock (_connection)
            {
                return Execute(sqlStatement, _connection.DbConnection);
            }
        }

        /// <summary>
        /// Returns results as IDataReader. Use <see cref="DirectSQLScope"></see> to
        /// enclose statements with connection
        /// </summary>
        /// <param name="sqlStatement">SQL query</param>
        /// <returns>Reulst of the SQL query as IDataReader</returns>
        public IDataReader ExecuteReader(string sqlStatement)
        {
            lock (_connection)
            {
                try
                {
                    return ExecuteReader(_connection.DbConnection.CreateCommand(), sqlStatement);
                }
                catch (Exception ex)
                {
                    throw new Exception(sqlStatement, ex);
                }

            }
        }

        private int Execute(string sqlStatement, IDbConnection connection)
        {

            if (connection == null)
            {
                throw new ArgumentNullException(nameof(connection), "When export is set to true, you need to pass a non null connection");
            }
            var statement = connection.CreateCommand();
            try
            {
                return Execute(statement, sqlStatement);
            }
            finally
            {
                try
                {
                    statement.Dispose();
                }
                catch (Exception)
                {
                    // ignored
                }
            }
        }


        private int Execute(IDbCommand statement, string sql)
        {
            statement.CommandText = sql;
            statement.CommandType = CommandType.Text;
            return statement.ExecuteNonQuery();
        }

        private IDataReader ExecuteReader(IDbCommand statement, string sql)
        {
            sql += Delimiter;

            statement.CommandText = sql;
            statement.CommandType = CommandType.Text;
            return statement.ExecuteReader();


        }

        public void Dispose()
        {
            if (_connection == null)
                return;

            lock (_connection)
            {
                if (_created == false)
                {
                    return;
                }
                _connection.Close();
                _connection = null;
                _created = false;
            }
        }
    }
}

