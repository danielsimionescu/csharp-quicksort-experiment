using System;
using System.Diagnostics;

namespace Quicksort
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Press any key to start...");
            Console.ReadKey();

            var numbers = RandomNumbers(2000000, 0, 500);
            var copy = new int[2000000];
            Array.Copy(numbers, copy, 2000000);

            PrintExecutionTime(() => SortArray(numbers));
            PrintExecutionTime(() => Array.Sort(copy));

            Console.WriteLine("\nPress any key to stop execution.");
            Console.ReadKey();
        }

        #region Helper methods

        private static void PrintExecutionTime(Action action)
        {
            var watch = Stopwatch.StartNew();
            action.Invoke();
            watch.Stop();

            var time = watch.ElapsedMilliseconds;
            Console.WriteLine($"\nTime with SortArray: {time}ms");
        }

        private static int[] RandomNumbers(int size, int min, int max)
        {
            var array = new int[size];
            var random = new Random();

            for (int i = 0; i < size; i++)
                array[i] = random.Next(min, max);

            return array;
        }

        private static void SortArray<T>(T[] array) where T : IComparable<T>
        {
            Quicksort(array, 0, array.Length - 1);
        }

        private static void Quicksort<T>(T[] array, int left, int right) where T : IComparable<T>
        {
            int i = left;
            int j = right;

            var pivot = array[left + (right - left) / 2];

            while (i <= j)
            {
                while (array[i].CompareTo(pivot) < 0)
                    i++;

                while (array[j].CompareTo(pivot) > 0)
                    j--;

                if (i <= j)
                {
                    var tmp = array[i];
                    array[i] = array[j];
                    array[j] = tmp;

                    i++;
                    j--;
                }
            }

            if (left < j)
                Quicksort(array, left, j);

            if (i < right)
                Quicksort(array, i, right);
        }
        #endregion
    }
}
