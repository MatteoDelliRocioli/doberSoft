using System;
using System.Collections.Generic;

namespace doberSoft.Util.Creational
{
    // see: https://codereview.stackexchange.com/questions/8307/implementing-factory-design-pattern-with-generics
    /// <summary>
    /// Implementing factory design pattern with generics
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Factory<T>
    {
        private Factory() { }

        static readonly Dictionary<int, Type> _dict = new Dictionary<int, Type>();

        public static T Create(int id, params object[] args)
        {
            Type type = null;
            if (_dict.TryGetValue(id, out type))
                return (T)Activator.CreateInstance(type, args);

            throw new ArgumentException("No type registered for this id");
        }

        public static void Register<Tderived>(int id) where Tderived : T
        {
            var type = typeof(Tderived);

            if (type.IsInterface || type.IsAbstract)
                throw new ArgumentException("...");

            _dict.Add(id, type);
        }
    }
    /*
        Usage:

        Factory<IAnimal>.Register<Dog>(1);
        Factory<IAnimal>.Register<Cat>(2);

        // this is the "suspicious" part.
        // some instances may require different parameters?
        Factory<IAnimal>.Create(2, "Garfield");     
     */
}
