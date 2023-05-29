using System;
using System.Collections.Generic;
using System.Diagnostics;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Enter 'a' to use an array or 'l' to use a doubly-linked list: ");
        char choice = Convert.ToChar(Console.ReadLine());

        Console.Write("Enter the size of the structure: ");
        int n = Convert.ToInt32(Console.ReadLine());

        Random random = new Random();
        int[] array = null;
        LinkedList<int> list = null;
        if (choice == 'a')
        {
            array = new int[n];
            for (int i = 0; i < n; i++)
            {
                array[i] = random.Next(100);
            }
            Array.Sort(array);
        }
        else if (choice == 'l')
        {
            list = new LinkedList<int>();
            for (int i = 0; i < n; i++)
            {
                list.AddLast(random.Next(100));
            }
            List<int> sortedList = new List<int>(list);
            sortedList.Sort();
            list = new LinkedList<int>(sortedList);
        }

        int key = 50;
        int comparisons = 0;
        int arrayAccesses = 0;
        int listAccesses = 0;
        Stopwatch stopwatch = new Stopwatch();
        
        stopwatch.Start();
        int index;
        if (choice == 'a')
        {
            index = FibonacciSearch.Search(array, key, out comparisons, out arrayAccesses);
            Console.WriteLine($"Number of comparisons in array: {comparisons}");
            Console.WriteLine($"Number of array accesses: {arrayAccesses}");
        }
        else
        {
            index = FibonacciSearch.Search(list, key, out comparisons, out listAccesses);
            Console.WriteLine($"Number of comparisons in list: {comparisons}");
            Console.WriteLine($"Number of list accesses: {listAccesses}");
        }
        stopwatch.Stop();
        
        Console.WriteLine($"Index of {key}: {index}");
        Console.WriteLine($"Time elapsed: {stopwatch.Elapsed.TotalMilliseconds} milliseconds");
    }
}

class FibonacciSearch
{
    public static int Search(int[] array, int key, out int comparisons, out int arrayAccesses)
    {
        comparisons = 0;
        arrayAccesses = 0;

        int fib2 = 0;
        int fib1 = 1;
        int fib = fib2 + fib1;
        
        while (fib < array.Length)
        {
            fib2 = fib1;
            fib1 = fib;
            fib = fib2 + fib1;
        }

        int offset = -1;

        while (fib > 1)
        {
            int i = Math.Min(offset + fib2, array.Length - 1);

            if (array[i] < key)
            {
                comparisons++;
                arrayAccesses++;
                fib = fib1;
                fib1 = fib2;
                fib2 = fib - fib1;
                offset = i;
            }
            else if (array[i] > key)
            {
                comparisons++;
                arrayAccesses++;
                fib = fib2;
                fib1 = fib1 - fib2;
                fib2 = fib - fib1;
            }
            else
            {
                comparisons++;
                arrayAccesses++;
                return i;
            }
        }
        
        if (fib1 == 1 && array[offset + 1] == key)
        {
            comparisons++;
            arrayAccesses++;
            return offset + 1;
        }

        return -1;
    }

    public static int Search(LinkedList<int> list, int key, out int comparisons, out int listAccesses)
    {
        comparisons = 0;
        listAccesses = 0;

        int fib2 = 0;
        int fib1 = 1;
        int fib = fib2 + fib1;

        while (fib < list.Count)
        {
            fib2 = fib1;
            fib1 = fib;
            fib = fib2 + fib1;
        }

        int offset = -1;

        while (fib > 1)
        {
            int i = Math.Min(offset + fib2, list.Count - 1);

            int value = GetValueAt(list, i, out int myAccesses);
            listAccesses += myAccesses;
            
            if (value < key)
            {
                comparisons++;
                fib = fib1;
                fib1 = fib2;
                fib2 = fib - fib1;
                offset = i;
            }
            else if (value > key)
            {
                comparisons++;
                fib = fib2;
                fib1 = fib1 - fib2;
                fib2 = fib - fib1;
            }
            else
            {
                comparisons++;
                return i;
            }
        }

        if (fib1 == 1 && GetValueAt(list, offset + 1, out int accesses) == key)
        {
            comparisons++;
            listAccesses += accesses;
            return offset + 1;
        }

        return -1;
    }

    private static int GetValueAt(LinkedList<int> list, int index, out int accesses)
    {
        accesses = 0;
        LinkedListNode<int> node = list.First;
        for (int i = 0; i < index; i++)
        {
            node = node.Next;
            accesses++;
        }
        accesses++;
        return node.Value;
    }
}