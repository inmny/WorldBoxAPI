using System;
using System.Linq;
using System.Reflection;

namespace WorldBoxAPI.Extensions {
    public static class Reflection {
        public static readonly BindingFlags AccessFlags = BindingFlags.Public | BindingFlags.NonPublic;

        /// <summary>
        /// Finds a class in an assembly by name.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="className"></param>
        /// <returns>The class as a type.</returns>
        public static Type GetClass(this Assembly a, string className) {
            return a.GetTypes().FirstOrDefault(x => x.FullName.Contains(className));
        }

        /// <summary>
        /// Calls a method by name.
        /// </summary>
        /// <param name="o"></param>
        /// <param name="methodName"></param>
        /// <param name="args"></param>
        /// <returns>The return value of the original method.</returns>
        public static object CallMethod(this object o, string methodName, params object[] args) {
            MethodInfo methodInfo = o.GetType().GetMethod(methodName, AccessFlags | BindingFlags.InvokeMethod | BindingFlags.Instance);
            return methodInfo == null ? null : methodInfo.Invoke(o, args);
        }

        /// <summary>
        /// Calls a static method by name.
        /// </summary>
        /// <param name="t"></param>
        /// <param name="methodName"></param>
        /// <param name="args"></param>
        /// <returns>The return value of the original method.</returns>
        public static object CallMethod(this Type t, string methodName, params object[] args) {
            MethodInfo methodInfo = t.GetMethod(methodName, AccessFlags | BindingFlags.InvokeMethod | BindingFlags.Static);
            return methodInfo == null ? null : methodInfo.Invoke(null, args);
        }

        /// <summary>
        /// Finds the value of a property by name.
        /// </summary>
        /// <param name="o"></param>
        /// <param name="propertyName"></param>
        /// <returns>The value of the property.</returns>
        public static T GetPropertyValue<T>(this object o, string propertyName) {
            PropertyInfo propertyInfo = o.GetType().GetProperty(propertyName, AccessFlags | BindingFlags.GetProperty | BindingFlags.Instance);
            return propertyInfo == null ? default : (T) propertyInfo.GetValue(o);
        }

        /// <summary>
        /// Finds the value of a static property by name.
        /// </summary>
        /// <param name="t"></param>
        /// <param name="propertyName"></param>
        /// <returns>The value of the property.</returns>
        public static T GetPropertyValue<T>(this Type t, string propertyName) {
            PropertyInfo propertyInfo = t.GetProperty(propertyName, AccessFlags | BindingFlags.GetProperty | BindingFlags.Static);
            return propertyInfo == null ? default : (T) propertyInfo.GetValue(null);
        }

        /// <summary>
        /// Sets the value of a property by name.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="o"></param>
        /// <param name="propertyName"></param>
        /// <param name="newValue"></param>
        public static void SetPropertyValue<T>(this object o, string propertyName, T newValue) {
            PropertyInfo propertyInfo = o.GetType().GetProperty(propertyName, AccessFlags | BindingFlags.SetProperty | BindingFlags.Instance);

            if (propertyInfo != null) {
                propertyInfo.SetValue(o, newValue);
            }
        }

        /// <summary>
        /// Sets the value of a static property by name.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="propertyName"></param>
        /// <param name="newValue"></param>
        public static void SetPropertyValue<T>(this Type t, string propertyName, T newValue) {
            PropertyInfo propertyInfo = t.GetProperty(propertyName, AccessFlags | BindingFlags.SetProperty | BindingFlags.Static);

            if (propertyInfo != null) {
                propertyInfo.SetValue(null, newValue);
            }
        }

        /// <summary>
        /// Finds the value of a field by name.
        /// </summary>
        /// <param name="o"></param>
        /// <param name="fieldName"></param>
        /// <returns>The value of the field.</returns>
        public static T GetFieldValue<T>(this object o, string fieldName) {
            FieldInfo fieldInfo = o.GetType().GetField(fieldName, AccessFlags | BindingFlags.GetField | BindingFlags.Instance);
            return fieldInfo == null ? default : (T) fieldInfo.GetValue(o);
        }

        /// <summary>
        /// Finds the value of a static field by name.
        /// </summary>
        /// <param name="t"></param>
        /// <param name="fieldName"></param>
        /// <returns>The value of the field.</returns>
        public static T GetFieldValue<T>(this Type t, string fieldName) {
            FieldInfo fieldInfo = t.GetField(fieldName, AccessFlags | BindingFlags.GetField | BindingFlags.Static);
            return fieldInfo == null ? default : (T) fieldInfo.GetValue(null);
        }

        /// <summary>
        /// Sets the value of a field by name.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="o"></param>
        /// <param name="fieldName"></param>
        /// <param name="newValue"></param>
        public static void SetFieldValue<T>(this object o, string fieldName, T newValue) {
            FieldInfo fieldInfo = o.GetType().GetField(fieldName, AccessFlags | BindingFlags.SetField | BindingFlags.Instance);

            if (fieldInfo != null) {
                fieldInfo.SetValue(o, newValue);
            }
        }

        /// <summary>
        /// Sets the value of a static field by name.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="fieldName"></param>
        /// <param name="newValue"></param>
        public static void SetFieldValue<T>(this Type t, string fieldName, T newValue) {
            FieldInfo fieldInfo = t.GetField(fieldName, AccessFlags | BindingFlags.SetField | BindingFlags.Static);

            if (fieldInfo != null) {
                fieldInfo.SetValue(null, newValue);
            }
        }
    }
}