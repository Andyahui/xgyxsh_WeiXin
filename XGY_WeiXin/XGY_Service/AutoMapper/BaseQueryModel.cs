using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using System.Web.Routing;

namespace XGY_Service.AutoMapper
{
    /// <summary>
    /// 关于分页的类
    /// </summary>
    public abstract class BaseQueryModel
    {
        public BaseQueryModel()
        {
            PageIndex = 1;
            PageSize = 20;
        }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }

        //public RouteValueDictionary ToParms()
        //{
        //    RouteValueDictionary dic = new RouteValueDictionary();
        //    this.GetType().GetProperties().Where(x => x.PropertyType.IsPrimitive
        //         || x.PropertyType.IsValueType
        //         || (Nullable.GetUnderlyingType(x.PropertyType) != null && (Nullable.GetUnderlyingType(x.PropertyType).IsValueType || Nullable.GetUnderlyingType(x.PropertyType).IsPrimitive))
        //         || x.PropertyType == typeof(string)).ToList().ForEach(x => dic.Add(x.Name, x.GetValue(this)));
        //    return dic;
        //}
    }

    //public static class Extension
    //{
    //    private static IEnumerable<KeyValuePair<string, object>> GetKeyValueObject(object obj)
    //    {
    //        return obj.GetType().GetProperties().Where(x => x.PropertyType.IsPrimitive
    //            || x.PropertyType.IsValueType
    //            || (Nullable.GetUnderlyingType(x.PropertyType) != null && (Nullable.GetUnderlyingType(x.PropertyType).IsValueType || Nullable.GetUnderlyingType(x.PropertyType).IsPrimitive))
    //            || x.PropertyType == typeof(string)).Select(x => new KeyValuePair<string, object>(x.Name, x.GetValue(obj)));
    //    }

    //    static bool IsNullable<T>(T obj)
    //    {
    //        if (obj == null) return true; // obvious
    //        Type type = typeof(T);
    //        if (!type.IsValueType) return true; // ref-type
    //        if (Nullable.GetUnderlyingType(type) != null) return true; // Nullable<T>
    //        return false; // value-type
    //    }
    //}
}