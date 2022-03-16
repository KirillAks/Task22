using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task22
{
    class Program
    {
        /*Сформировать массив случайных целых чисел (размер задается пользователем).
          Вычислить сумму чисел массива и максимальное число в массиве.
          Реализовать решение задачи с использованием механизма задач продолжения.*/
        static void Main(string[] args)
        {
            Console.WriteLine("Введите размер массива:");
            int n = Convert.ToInt32(Console.ReadLine());
            Func<object, int[]> func = new Func<object, int[]>(GetArray);
            Task<int[]> task1 = new Task<int[]>(func, n);
            Action<Task<int[]>> action1 = new Action<Task<int[]>>(SumArray);
            Task task2 = task1.ContinueWith(action1);
            Action<Task<int[]>> action2 = new Action<Task<int[]>>(MaxArray);
            Task task3 = task1.ContinueWith(action2);
            task1.Start();
            Console.ReadKey();
        }
        static int[] GetArray(object a)
        {
            int n = (int)a; 
            int[] array = new int[n];
            Random random = new Random();
            Console.WriteLine($"Массив случайных целых чисел: ");
            for (int i = 0; i < n; i++)
            {
                array[i] = random.Next(0, 50);
                Console.Write($"{array[i]} ");
            }
            Console.WriteLine();
            return array;
        }
        static void SumArray(Task<int[]> task)
        {
            int[] array = task.Result;
            int sum = 0;
            for (int i = 0; i < array.Count(); i++)
            {
                sum += array[i];                
            }
            Console.WriteLine($"Сумма чисел массива:{sum}");
        }
        static void MaxArray(Task<int[]> task)
        {
            int[] array = task.Result;
            int max = array[0];
            foreach (int a in array)
            {
                if (a > max)
                    max = a;                
            }
            Console.WriteLine($"Максимальное число в массиве:{max}");
        }
    }
}
