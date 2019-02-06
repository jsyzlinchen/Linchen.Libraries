using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linchen.Framework
{
    public class StaticConstant //静态常量类
    {
        //字符串连接    SqlServerConnString 这个就是拿的 App.config 里面ConnectionStrings 下面的连接
        // 以后可以直接 通过StaticConstant.SqlServerConnString

        public static string SqlServerConnString = ConfigurationManager.ConnectionStrings["Customers"].ConnectionString;

        private static string DALTypeDll = ConfigurationManager.AppSettings["DALTypeDll"];
        public static string DALDllName = DALTypeDll.Split(',')[1];
        public static string DALTypeName = DALTypeDll.Split(',')[0];
    }
}
