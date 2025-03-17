using System.Reflection;
using System.IO;
using System;
// Эта программа загрузит внешнюю библиотеку,
// и создаст объект с помощью поздней привязки.
public class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("***** Функция с поздним связыванием *****");
        // Попытка загрузить локальную копию CarLibrary.
        Assembly a = null;
        try
        {
            a = Assembly.Load("CarLibrary");
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine(ex.Message);
            return;
        }
        if (a != null)
        {
            CreateUsingLateBinding(a);
            InvokeMethodWithArgsUsingLateBinding(a);
        }
        Console.ReadLine();
    }

    static void CreateUsingLateBinding(Assembly asm)
    {
        try
        {
            // Получить метаданные для типа Minivan.
            Type miniVan = asm.GetType("CarLibrary.MiniVan");

            // Создание экземпляра типа Minivan динамически.
            object obj = Activator.CreateInstance(miniVan);
            Console.WriteLine("Created a {0} using late binding!", obj);
            // Получение информации о TurboBoost.
            MethodInfo mi = miniVan.GetMethod("TurboBoost");

            // Вызвать метод ('null' означает, что параметров нет).
            mi.Invoke(obj, null);

            // Получить метаданные для типа Minivan.
            Type sportsCar = asm.GetType("CarLibrary.SportsCar");

            // Создание экземпляра типа Minivan динамически.
            object obj1 = Activator.CreateInstance(sportsCar);
            Console.WriteLine("Created a {0} using late binding!", obj1);
            // Получение информации о TurboBoost.
            MethodInfo sc = sportsCar.GetMethod("TurboBoost");

            // Вызвать метод ('null' означает, что параметров нет).
            sc.Invoke(obj1, null);

            /*// Получить метаданные для типа Minivan.
            Type miniVan = asm.GetType("CarLibrary.MiniVan");

            // Создание экземпляра типа Minivan динамически.
            object obj = Activator.CreateInstance(miniVan);
            Console.WriteLine("Created a {0} using late binding!", obj);
            // Получение информации о TurboBoost.
            MethodInfo mi = miniVan.GetMethod("TurboBoost");

            // Вызвать метод ('null' означает, что параметров нет).
            mi.Invoke(obj, null);*/
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    static void InvokeMethodWithArgsUsingLateBinding(Assembly asm)
    {
        try
        {
            // Сначала получим метаданные для типа SportsCar.
            Type sport = asm.GetType("CarLibrary.SportsCar");

            // Затем создадим экземпляр типа SportsCar динамически.
            object obj2 = Activator.CreateInstance(sport);
            // Вызов метода TurnOnRadio() с аргументами.
            MethodInfo sc = sport.GetMethod("TurnOnRadio");
           sc.Invoke(obj2, new object[] { true, 2 });
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

}

