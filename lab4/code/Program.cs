using System;
using System.Reflection;

namespace Demonstration
{
    class DemonstrationClass
    {
        public int _int;
        private string _string;
        protected bool _bool;
        internal double _double;
        public float _float;

        public void Method1()
        {
            Console.WriteLine("Method1 was called.");
        }

        private int Method2(int x, int y)
        {
            return x + y;
        }

        protected string Method3(string str)
        {
            return str.ToUpper();
        }

        internal void Method4()
        {
            Console.WriteLine("Method4 was called.");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            DemonstrationClass obj = new DemonstrationClass();
            obj._int = 10;
            obj._double = 3.14;
            obj._float = 1.1F;

            Type _type = typeof(DemonstrationClass);
            TypeInfo _typeInfo = _type.GetTypeInfo();

            Console.WriteLine("Type: " + _type.Name);
            Console.WriteLine("IsClass: " + _typeInfo.IsClass);
            Console.WriteLine("IsAbstract: " + _typeInfo.IsAbstract);
            Console.WriteLine("IsPublic: " + _typeInfo.IsPublic);

            MemberInfo[] members = _type.GetMembers();
            Console.WriteLine("\nMembers:");
            foreach (MemberInfo member in members)
            {
                Console.WriteLine(member.Name);
            }

            FieldInfo[] fields = _type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            Console.WriteLine("\nFields:");
            foreach (FieldInfo field in fields)
            {
                Console.WriteLine(field.Name + ": " + field.FieldType);
            }

            // Робота з MethodInfo
            MethodInfo _method = _type.GetMethod("Method1");
            Console.WriteLine("\nMethod: " + _method.Name);
            _method.Invoke(obj, null);

            object[] parameters = new object[] { 2, 3 };
            _method = _type.GetMethod("Method2", BindingFlags.NonPublic | BindingFlags.Instance);
            Console.WriteLine("\nMethod: " + _method.Name);
            Console.WriteLine("Result: " + _method.Invoke(obj, parameters));

            parameters = new object[] { "hello" };
            _method = _type.GetMethod("Method3", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            Console.WriteLine("\nMethod: " + _method.Name);
            Console.WriteLine("Result: " + _method.Invoke(obj, parameters));

            _method = _type.GetMethod("Method4", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);
            Console.WriteLine("\nMethod: " + _method.Name);
            _method.Invoke(obj, null);
        }
    }
}
