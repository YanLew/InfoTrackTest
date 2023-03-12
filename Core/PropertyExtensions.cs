using System.Reflection;

namespace Core
{
    public static class PropertyExtensions
    {
        public static void SetPropertyValue(this object obj, string propName, object value)
        {
            var property = obj.GetType().GetProperty(propName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            property.SetValue(obj, value, null);
        }
    }
}
