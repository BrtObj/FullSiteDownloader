using System;
using FullSiteDownloader.DBUtil;
//todo:
// url解析并逐级生成文件夹
// 数据库封装，支持mssql mysql oracle
// 数据库连接成功后，如果不存在schema，从头生成
// downloadUtil 。。。支持各种协议，各种proxy
// 网站快速遍历，下载更新

namespace FullSiteDownloader
{
    class Program
    {
        static void Main(string[] args)
        {
            DBChecker _checker = new DBChecker();
            _checker.CheckDB();
            Console.ReadKey();
        }
    }
}
