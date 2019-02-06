using Linchen.Framework.Model;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Linchen.Libraries.IDAL
{
    public interface IBaseDAL
    {

        // 约束是为了正常的调用
        // 能保证传的T 是BaseModel的子类
        //1,            
        //GetColumnName 字段名  Properties特性   列名columnString
        T Find<T>(int id) where T : BaseModel;


        //2.
        List<T> FindAll<T>() where T : BaseModel;


        //优化封装
        //3.
        //List<T> ReadToList<T>(SqlDataReader reader);


        void Update<T>(T t) where T : BaseModel;


        void Insert<T>(T t) where T : BaseModel;


        void Delete<T>(int id) where T : BaseModel;
       
    }
}
