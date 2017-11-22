using StackExchange.Redis;
using System;
using System.Linq;
using System.Reflection;

namespace Earning.Demo.Shared.Extensions
{
    public static class RedisExtension
    {
        public static HashEntry[] ToHashEntries(this object obj)
        {
            var properties = obj.GetType().GetProperties().Where(p => p.GetValue(obj) != null);
            return properties.Select(property => new HashEntry(property.Name, property.GetValue(obj).ToString())).ToArray();
        }

        public static T ConvertFromRedis<T>(this HashEntry[] hashEntries)
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            var obj = Activator.CreateInstance(typeof(T));
            foreach (var property in properties)
            {
                HashEntry entry = hashEntries.FirstOrDefault(g => g.Name.ToString().Equals(property.Name));
                if (entry.Equals(new HashEntry())) continue;
                property.SetValue(obj, Convert.ChangeType(entry.Value.ToString(), property.PropertyType));
            }
            return (T)obj;
        }
    }
}
