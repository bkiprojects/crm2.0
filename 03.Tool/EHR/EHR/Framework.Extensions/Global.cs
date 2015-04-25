using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Extensions
{
    public class Global
    {
        public static T GetGlobalContextData<T>(string name)
        {
            if (((HttpContext.Current != null) && (HttpContext.Current.Items[name] != null)
                && (HttpContext.Current.Items[name].GetType() == typeof(T))))
            {
                return (T)HttpContext.Current.Items[name];
            }
            if (System.Threading.Thread.GetNamedDataSlot(name) != null)
            {
                object data = null;
                System.LocalDataStoreSlot namedDataSlot = System.Threading.Thread.GetNamedDataSlot(name);
                if (namedDataSlot != null)
                {
                    data = System.Threading.Thread.GetData(namedDataSlot);
                }
                if ((data != null) && (data.GetType() == typeof(T)))
                {
                    return (T)data;
                }
            }
            return default(T);
        }
        public static void SetGlobalContextData<T>(string name, T value)
        {
            if (HttpContext.Current != null)
            {
                HttpContext.Current.Items[name] = value;
            }
            else
            {
                System.LocalDataStoreSlot namedDataSlot = System.Threading.Thread.GetNamedDataSlot(name);
                if (namedDataSlot == null)
                {
                    namedDataSlot = System.Threading.Thread.AllocateNamedDataSlot(name);
                }
                if (namedDataSlot != null)
                {
                    System.Threading.Thread.SetData(namedDataSlot, value);
                }
            }
        }

    }
}
