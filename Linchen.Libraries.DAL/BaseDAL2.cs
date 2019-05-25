using Linchen.Framework;
using Linchen.Framework.AttributeExtend;
using Linchen.Framework.Model;
using Linchen.Libraries.IDAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linchen.Libraries.DAL
{
    public class BaseDAL2:IBaseDAL
    {

        // 约束是为了正常的调用
        // 能保证传的T 是BaseModel的子类
        //1,            
        //GetColumnName 字段名  Properties特性   列名columnString
        public T Find<T>(int id) where T : BaseModel
        {
            Type type = typeof(T);
            //string sql = $"select {0} from {1} where Id={id}";
            // 把当前类型的所有属性，把每一个属性的名称 都转换成加一个[] 然后这个集合用","连接
            //string columnString = string.Join(",", type.Name().Select(p => $"[{p.Name()}]"));
            //string sql = $"select {columnString} from [{type.Name}] where Id={id}";
            string columnString = string.Join(",", type.GetProperties().Select(p => $"[{p.GetColumnName()}]"));
            string sql = $"select {columnString} from [{type.Name}] where Id={id}";
            T t = null;
            Func<SqlCommand, T> func = new Func<SqlCommand, T>(command =>
            {
                SqlDataReader reader = command.ExecuteReader();
                List<T> list = this.ReadToList<T>(reader);
                T tResult = list.FirstOrDefault();
                return tResult;
            });
            t = this.ExcuteSql<T>(sql, func);
            //using (SqlConnection conn = new SqlConnection(StaticConstant.SqlServerConnString))
            //{
            //    SqlCommand command = new SqlCommand(sql, conn);
            //    conn.Open();
            //    SqlDataReader reader = command.ExecuteReader();
            //    List<T> list = this.ReadToList<T>(reader);
            //    t = list.FirstOrDefault();
            //    //if (reader.Read())//表示有数据 开始读取
            //    //{
            //    //    foreach (var prop in type.GetProperties())
            //    //    {
            //    //        prop.SetValue(t, reader[prop.Name] is DBNull? null: reader[prop.Name]);
            //    //    }
            //    //}
            //}
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
            Func<SqlCommand, List<T>> func = command =>
             {
                 SqlDataReader reader = command.ExecuteReader();
                 List<T> listResult = this.ReadToList<T>(reader);
                 return listResult;
 
             };
            list = this.ExcuteSql<List<T>>(sql, func);
            //using (SqlConnection conn = new SqlConnection(StaticConstant.SqlServerConnString))
            //{
            //    SqlCommand command = new SqlCommand(sql, conn);
            //    conn.Open();
            //    SqlDataReader reader = command.ExecuteReader();
            //    list = this.ReadToList<T>(reader);
            //}
            return list;
        }



        public void Update<T>(T t) where T : BaseModel
        {
            Type type = typeof(T);
            var propArray = type.GetProperties().Where(p => !p.Name.Equals("Id"));//得到type 所有公共属性
            string columnString = string.Join(",", propArray.Select(p => $"[{p.GetColumnName()}]=@[{p.GetColumnName()}]"));
            //必须参数化  不然值里面有引号

            var parameters = propArray.Select(p => new SqlParameter($"@{p.GetColumnName()}", p.GetValue(t) ?? DBNull.Value)).ToArray();
            string sql = $"UPDATE {type.Name} SER{columnString} WHERE ID={t.Id}";

            Func<SqlCommand, int> func = command =>
             {
                 command.Parameters.AddRange(parameters);
                 int iResult = command.ExecuteNonQuery();
                 return iResult;
             };

            //using (SqlConnection conn = new SqlConnection(StaticConstant.SqlServerConnString))
            //{
            //    using (SqlCommand command = new SqlCommand(sql, conn))
            //    {
            //        command.Parameters.AddRange(parameters);
            //        conn.Open();
            //        int iResult = command.ExecuteNonQuery();
            //        if (iResult == 0)
            //            throw new Exception("Update数据不存在");
            //    }
            //}
            int i = this.ExcuteSql<int>(sql, func);
            if (i == 0)
                throw new Exception("updata数据不存在");

        }

        //多个方法里面重复对数据库访问   通过委托解耦  去掉重复代码
        private T ExcuteSql<T>(string sql,Func<SqlCommand,T> func)
        {
            using (SqlConnection conn = new SqlConnection(StaticConstant.SqlServerConnString)) //数据库连接
            {
                using (SqlCommand command = new SqlCommand(sql, conn))//数据库命令
                {
                    conn.Open(); //开启连接
                    SqlTransaction sqlTransaction = conn.BeginTransaction(); //事务
                    try
                    {
                        command.Transaction = sqlTransaction;
                        T tResult = func.Invoke(command);//核心
                        sqlTransaction.Commit();//提交事务
                        return tResult;
                    }
                    catch (Exception ex)
                    {
                        sqlTransaction.Rollback();  //回退事务
                        throw;
                    }
                }
            }
        }


        public void Insert<T>(T t) where T : BaseModel
        {

        }

        public void Delete<T>(int id) where T : BaseModel
        {

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
