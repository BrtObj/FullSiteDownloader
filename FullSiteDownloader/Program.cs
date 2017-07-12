using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FullSiteDownloader.DBUtil;
using System.Data;

//todo:
// url解析并逐级生成文件夹
// 数据库封装，支持mssql mysql oracle
// downloadUtil 。。。
// 网站快速遍历，下载更新

namespace FullSiteDownloader
{
    class Program
    {
        static void Main(string[] args)
        {
            DBAccess _dbAccess = new DBAccess();
            string _result = _dbAccess.test();
            Console.WriteLine(_result);
            DataTable _dbs = _dbAccess.ExecSql("select name from SysDatabases");
            Console.Write("库: ");
            foreach(DataRow _row in _dbs.Rows)
            {
                Console.Write(_row.ItemArray[0] + " ");
            }
            Console.ReadKey();
        }
    }
}
