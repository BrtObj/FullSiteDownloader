using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullSiteDownloader.DBUtil
{
    class DBAccess
    {
        static string MsSqlConnectionString = ConfigurationManager.ConnectionStrings["MsSql"].ConnectionString;

        private SqlConnection CreateMsSqlConnection()
        {
            return new SqlConnection(MsSqlConnectionString);
        }

        public string test()
        {
            string _result = "";
            SqlConnection _connection = null;
            try
            {
                _connection = this.CreateMsSqlConnection();
                _connection.Open();
                _result = string.Format("数据库版本: {0}\n" +
                    "状态: {1}", _connection.ServerVersion, _connection.State);
            }
            catch (Exception ex)
            {
                _result = ex.Message;
            }
            finally
            {
                if (_connection != null)
                    _connection.Close();
            }
            return _result;
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
                _connection.Open();
                SqlCommand _command = new SqlCommand(sqlText, _connection);
                _result = _command.ExecuteNonQuery();
            }
            catch (Exception) {
                throw;
            }
            finally {
                _connection.Close();
            }
            return _result;
        }
    }
}
