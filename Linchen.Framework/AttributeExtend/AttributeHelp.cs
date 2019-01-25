using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Linchen.Framework.AttributeExtend
{
    public static class AttributeHelp
    {
        /// <summary>
        /// 如果有特性就返回特性名字 如果没有就直接返回属性名字
        /// </summary>
        /// <param name="prop"></param>
        /// <returns></returns>
        public static string GetColumnName(this PropertyInfo prop)
        {
            if (prop.IsDefined(typeof(ColumnAttribute), true))
            {
                ColumnAttribute attribute = (ColumnAttribute)prop.GetCustomAttribute(typeof(ColumnAttribute), true);
                return attribute.GetColumnName();
            }
            else
            {
                return prop.Name;
            }
        }
    }
}
