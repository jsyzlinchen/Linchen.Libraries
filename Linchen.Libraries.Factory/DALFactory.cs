using Linchen.Framework;
using Linchen.Libraries.IDAL;
using System;
using System.Reflection;

namespace Linchen.Libraries.Factory
{
    public class DALFactory
    {
        static DALFactory()
        {
            Assembly assembly = Assembly.Load(StaticConstant.DALDllName);
            DALType = assembly.GetType(StaticConstant.DALTypeName);
        }
        private static Type DALType = null;
        public static IBaseDAL CreateInstance()
        {
            return (IBaseDAL)Activator.CreateInstance(DALType);
        }
    }
}
