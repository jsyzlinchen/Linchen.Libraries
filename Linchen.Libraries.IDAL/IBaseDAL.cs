using Linchen.Framework.Model;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Linchen.Libraries.IDAL
{
    public interface IBaseDAL
    {

        T Find<T>(int id) where T : BaseModel;

        List<T> FindAll<T>() where T : BaseModel;

        void Update<T>(T t) where T : BaseModel;

        void Insert<T>(T t) where T : BaseModel;

        void Delete<T>(int id) where T : BaseModel;
       
    }
}
