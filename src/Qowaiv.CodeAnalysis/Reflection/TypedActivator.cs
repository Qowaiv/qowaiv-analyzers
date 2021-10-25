using System;
using System.Linq;

namespace Qowaiv.CodeAnalysis.Reflection
{
    internal static class TypedActivator
    {
        public static T Create<T>(Func<Type, bool> predicate)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(assembly => assembly.FullName.Contains("Qowaiv"));
            var types = assemblies
                .SelectMany(assembly => assembly.GetTypes())
                .Where(tp 
                    => !tp.IsAbstract 
                    && tp.BaseType == typeof(T));
                    
            var type = types.FirstOrDefault(predicate);

            return type is null
                ? throw new InvalidOperationException($"{typeof(T).Name} could not be resolved.")
                : (T)Activator.CreateInstance(type);
        }
    }
}
