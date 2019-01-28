using Linchen.Framework;
using Linchen.Framework.AttributeExtend;
using Linchen.Framework.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linchen.Libraries.DAL
{
    public class BaseDAL
    {
        
        // 约束是为了正常的调用
        // 能保证传的T 是BaseModel的子类
        //1,
        public T Find<T>(int id) where T : BaseModel
        {
            Type type = typeof(T);
            //string sql = $"select {0} from {1} where Id={id}";
            // 把当前类型的所有属性，把每一个属性的名称 都转换成加一个[] 然后这个集合用","连接
            string columnString = string.Join(",", type.GetProperties().Select(p => $"[{p.GetColumnName()}]"));
            string sql = $"select {columnString} from [{type.Name}] where Id={id}";
            T t = (T)Activator.CreateInstance(type);
            using (SqlConnection conn = new SqlConnection(StaticConstant.SqlServerConnString))
            {
                SqlCommand command = new SqlCommand(sql, conn);
                conn.Open();
                SqlDataReader reader =  command.ExecuteReader();
                List<T> list = this.ReadToList<T>(reader);
                t = list.FirstOrDefault();
                //if (reader.Read())//表示有数据 开始读取
                //{
                //    foreach (var prop in type.GetProperties())
                //    {
                //        prop.SetValue(t, reader[prop.Name] is DBNull? null: reader[prop.Name]);
                //    }
                //}
            }
            return t;
        }

        //2.
        public List<T> FindAll<T>() where T : BaseModel
        {
            Type type = typeof(T);
            //string sql = $"select {0} from {1} where Id={id}";
            // 把当前类型的所有属性，把每一个属性的名称 都转换成加一个[] 然后这个集合用","连接
            //string columnString = string.Join(",", type.GetProperties().Select(p => $"[{p.Name}]"));
            //string sql = $"select {columnString} from [{type.Name}]";
            string columnString = string.Join(",", type.GetProperties().Select(p => $"[{p.GetColumnName()}]"));
            string sql = $"select {columnString} from [{type.Name}]";
            List<T> list = new List<T>();
            using (SqlConnection conn = new SqlConnection(StaticConstant.SqlServerConnString))
            {
                SqlCommand command = new SqlCommand(sql, conn);
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                list = this.ReadToList<T>(reader);
            }
            return list;
        }

        //优化封装
        //3.
        private List<T> ReadToList<T>(SqlDataReader reader)
        {
            Type type = typeof(T);
            List<T> list = new List<T>();
            while (reader.Read())//表示有数据 开始读取
            {

                T t = (T)Activator.CreateInstance(type);
                foreach (var prop in type.GetProperties())
                {
                    object oValue = reader[prop.GetColumnName()];
                    if (oValue is DBNull)
                        oValue = null;
                    prop.SetValue(t, oValue);
                }
                list.Add(t);
            }
            return list;
        }

    }
}
