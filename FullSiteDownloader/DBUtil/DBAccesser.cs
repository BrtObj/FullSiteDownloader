using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace FullSiteDownloader.DBUtil
{
    class DBAccesser
    {
        private static string mMsSqlConnectionString = ConfigurationManager.ConnectionStrings["MsSql"].ConnectionString;

        private SqlConnection CreateMsSqlConnection()
        {
            return new SqlConnection(mMsSqlConnectionString);
        }

        public DataTable ExecSql(string sqlText)
        {
            DataTable _dt = new DataTable();
            SqlConnection _connection = null;
            try
            {
                _connection = this.CreateMsSqlConnection();
                _connection.Open();
                SqlDataAdapter _adapter = new SqlDataAdapter(sqlText, _connection);
                _adapter.Fill(_dt);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _connection.Close();
            }

            return _dt;
        }

        public int ExecNonQuery(string sqlText)
        {
            int _result = -1;
            SqlConnection _connection = null;
            try
            {
                _connection = _connection = this.CreateMsSqlConnection();
                _connection.Open();
                SqlCommand _command = new SqlCommand(sqlText, _connection);
                _result = _command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _connection.Close();
            }
            return _result;
        }

        public object ExecScalar(string sqlText)
        {
            object _result = null;
            SqlConnection _connection = null;
            try
            {
                _connection = _connection = this.CreateMsSqlConnection();
                _connection.Open();
                SqlCommand _command = new SqlCommand(sqlText, _connection);
                _result = _command.ExecuteScalar();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _connection.Close();
            }
            return _result;
        }
    }
}
