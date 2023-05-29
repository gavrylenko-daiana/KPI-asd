using System;

namespace Lab_2;

using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Enter the dimension of the graph: ");
        int dimension = Convert.ToInt32(Console.ReadLine());

        int[,] adjacencyMatrix = new int[dimension, dimension];

        Console.Write("Enter 'm' for manual data entry or 'r' for random data entry: ");
        char dataEntry = Convert.ToChar(Console.ReadLine());

        if (dataEntry == 'm')
        {
            Console.WriteLine("Enter the adjacency matrix row by row:");
            for (int i = 0; i < dimension; i++)
            {
                string[] row = Console.ReadLine().Split(' ');
                for (int j = 0; j < dimension; j++)
                {
                    adjacencyMatrix[i, j] = Convert.ToInt32(row[j]);
                }
            }
        }
        else if (dataEntry == 'r')
        {
            Random random = new Random();
            for (int i = 0; i < dimension; i++)
            {
                for (int j = 0; j < dimension; j++)
                {
                    if (i == j)
                    {
                        adjacencyMatrix[i, j] = 0;
                    }
                    else
                    {
                        adjacencyMatrix[i, j] = random.Next(2);
                        adjacencyMatrix[j, i] = adjacencyMatrix[i, j];
                    }
                }
            }
        }

        Console.WriteLine("Adjacency matrix:");
        for (int i = 0; i < dimension; i++)
        {
            for (int j = 0; j < dimension; j++)
            {
                Console.Write(adjacencyMatrix[i, j] + " ");
            }

            Console.WriteLine();
        }

        List<int> route = FindRoute(adjacencyMatrix);

        Console.Write("Route: ");
        foreach (int vertex in route)
        {
            Console.Write(vertex + " ");
        }

        Console.WriteLine();
    }

    static List<int> FindRoute(int[,] adjacencyMatrix)
    {
        int dimension = adjacencyMatrix.GetLength(0);
        bool[] visited = new bool[dimension];

        List<int> route = new List<int>();
        Stack<int> stack = new Stack<int>();

        // Start with the first vertex
        stack.Push(0);
        visited[0] = true;

        while (stack.Count > 0)
        {
            int currentVertex = stack.Pop();
            route.Add(currentVertex);

            for (int i = 0; i < dimension; i++)
            {
                if (adjacencyMatrix[currentVertex, i] == 1 && !visited[i])
                {
                    stack.Push(i);
                    visited[i] = true;
                }
            }
        }

        return route;
    }
}