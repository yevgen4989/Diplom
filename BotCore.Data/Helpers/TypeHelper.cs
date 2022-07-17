using System;

namespace BotCore.Data.Helpers;

public class TypeHelper
{
    public static bool IsTypeDerivedFromGenericType(Type typeToCheck, Type genericType)
    {
        if (typeToCheck == typeof(object) || typeToCheck == null)
            return false;
        if (typeToCheck.IsGenericType && typeToCheck.GetGenericTypeDefinition() == genericType) return true;

        return IsTypeDerivedFromGenericType(typeToCheck.BaseType, genericType);
    }
}