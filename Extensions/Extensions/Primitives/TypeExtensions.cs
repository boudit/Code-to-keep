namespace Extensions.Primitives
{
    using System;
    using System.Linq;

    public static class TypeExtensions
    {
        public static bool IsSubclassOfGenericType(this Type toCheck, Type generic, params Type[] genericArguments)
        {
            while (toCheck != null && toCheck != typeof(object))
            {
                var current = toCheck.IsGenericType ? toCheck.GetGenericTypeDefinition() : toCheck;
                if (generic == current)
                {
                    if (genericArguments == null || !genericArguments.Any() || current.GenericTypeArguments.SequenceEqual(genericArguments))
                    {
                        return true;
                    }
                }

                toCheck = toCheck.BaseType;
            }

            return false;
        }
    }
}
