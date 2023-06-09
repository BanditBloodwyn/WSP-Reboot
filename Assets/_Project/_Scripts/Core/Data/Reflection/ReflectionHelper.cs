using System;
using System.Reflection;

namespace Assets._Project._Scripts.Core.Data.Reflection
{
    public static class ReflectionHelper
    {
        public static float GetFieldValueFromStruct(object myClassInstance, string structTypeName, string fieldName)
        {
            Type classType = myClassInstance.GetType();
            FieldInfo structFieldInfo = classType.GetField(structTypeName, BindingFlags.Public | BindingFlags.Instance);
            object structInstance = structFieldInfo.GetValue(myClassInstance);

            Type structType = structInstance.GetType();
            FieldInfo fieldInfo = structType.GetField(fieldName, BindingFlags.Public | BindingFlags.Instance);

            if (fieldInfo != null && fieldInfo.FieldType == typeof(float))
            {
                return (float)fieldInfo.GetValue(structInstance);
            }
            else
            {
                throw new ArgumentException("Invalid struct type or field name.");
            }
        }
    }
}