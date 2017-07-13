using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Security.AccessControl;
using System.Text;
namespace FullSiteDownloader.DBUtil
{
    class DBChecker
    {
        private static string mCheckConenctionString = ConfigurationManager.ConnectionStrings["MsSqlNoInitial"].ConnectionString;

        public void CheckDB()
        {
            SqlConnection _connection;

            Console.WriteLine("开始数据库检查");

            try
            {
                _connection = new SqlConnection(mCheckConenctionString);
                _connection.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine("数据库连接失败，请检查连接字符串: " + ex.Message);
                return;
            }

            try
            {
                string _countSql = "select count(*) from SysDatabases where name = 'SiteDownLoader'";
                SqlCommand _command = new SqlCommand(_countSql, _connection);
                int _dbCount = (int)_command.ExecuteScalar();

                if (_dbCount > 0)
                {
                    Console.WriteLine("目标库: SiteDownLoader 已存在");
                }
                else
                {
                    Console.WriteLine("目标库: SiteDownLoader 不存在");
                    Console.WriteLine("正在创建目标库");

                    StringBuilder _creationSql = new StringBuilder();
                    string _targetDirectory = Directory.GetCurrentDirectory() + "\\DBUtil\\";
                    FileInfo fileInfo = new FileInfo(_targetDirectory);
                    FileSecurity fileSecurity = fileInfo.GetAccessControl();
                    fileSecurity.AddAccessRule(new FileSystemAccessRule("Everyone", FileSystemRights.FullControl, AccessControlType.Allow));
                    fileInfo.SetAccessControl(fileSecurity);
                    string _mdfSavePath = _targetDirectory + "\\SiteDownLoader.mdf";
                    string _ldfSavePath = _targetDirectory + "\\SiteDownLoader_log.ldf";

                    using (StreamReader _reader = new StreamReader(".\\DBUtil\\MsSql.sql"))
                    {
                        while (!_reader.EndOfStream)
                        {
                            string _line = _reader.ReadLine();
                            if (_line.Contains("mdfSavePath"))
                                _line = _line.Replace("mdfSavePath", _mdfSavePath);
                            if (_line.Contains("ldfSavePath"))
                                _line = _line.Replace("ldfSavePath", _ldfSavePath);
                            if (_line.ToUpper().Equals("GO"))
                            {
                                _command.CommandText = _creationSql.ToString();
                                _command.ExecuteNonQuery();
                                _creationSql.Clear();
                            }
                            else
                            {
                                _creationSql.AppendLine(_line);
                            }
                        }
                    }

                    Console.WriteLine("目标库创建成功");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("数据库创建失败: " + ex.Message);
                return;
            }

            Console.WriteLine("数据库检查完成");
        }
    }
}
