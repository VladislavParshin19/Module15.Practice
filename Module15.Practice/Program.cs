using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Module15.Practice
{
    class Program
    {
        static void Main()
        {
            // Исследование Типа
            Type myClassType = typeof(MyClass);

            // Имя класса
            Console.WriteLine($"Имя класса: {myClassType.Name}");

            // Список конструкторов
            ConstructorInfo[] constructors = myClassType.GetConstructors();
            Console.WriteLine("Конструкторы:");
            foreach (var constructor in constructors)
            {
                Console.WriteLine($"{constructor.Name}, Модификатор доступа: {constructor.Attributes}");
            }

            // Список полей и свойств
            FieldInfo[] fields = myClassType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            PropertyInfo[] properties = myClassType.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

            Console.WriteLine("\nПоля и свойства:");
            foreach (var field in fields)
            {
                Console.WriteLine($"{field.Name}, Тип: {field.FieldType.Name}, Модификатор доступа: {field.Attributes}");
            }
            foreach (var property in properties)
            {
                Console.WriteLine($"{property.Name}, Тип: {property.PropertyType.Name}, Модификатор доступа: {property.Attributes}");
            }

            // Список методов
            MethodInfo[] methods = myClassType.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            Console.WriteLine("\nМетоды:");
            foreach (var method in methods)
            {
                Console.WriteLine($"{method.Name}, Возвращаемый тип: {method.ReturnType.Name}, Модификатор доступа: {method.Attributes}");
            }

            // Создание экземпляра с использованием рефлексии
            object myObj = Activator.CreateInstance(myClassType);

            // Манипулирование объектом
            PropertyInfo publicProperty = myClassType.GetProperty("PublicProperty");
            publicProperty.SetValue(myObj, 42);

            Console.WriteLine($"Значение PublicProperty после изменения: {publicProperty.GetValue(myObj)}");

            MethodInfo publicMethod = myClassType.GetMethod("PublicMethod");
            publicMethod.Invoke(myObj, null);

            // Расширенное исследование: вызов приватного метода
            MethodInfo privateMethod = myClassType.GetMethod("PrivateMethod", BindingFlags.Instance | BindingFlags.NonPublic);
            privateMethod.Invoke(myObj, null);

            Console.ReadLine();
        }
    }

    class MyClass
    {
        //Поля
        private int privateField;

        //Доступы
        public int PublicProperty { get; set; }
        private string PrivateProperty { get; set; }

        //Конструкторы
        public MyClass()
        {
            Console.WriteLine("Public default constructor");
        }

        private MyClass(int value)
        {
            Console.WriteLine($"Private constructor with value: {value}");
        }

        //Методы
        public void PublicMethod()
        {
            Console.WriteLine("Public method");
        }

        private void PrivateMethod()
        {
            Console.WriteLine("Private method");
        }
    }
}
