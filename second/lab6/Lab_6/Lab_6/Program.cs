using System;
using System.IO;

class KnapsackProblem
{
    static int Knapsack(int W, int[] weights, int[] values, int n)
    {
        int[,] dp = new int[n + 1, W + 1];

        for (int i = 0; i <= n; i++)
        {
            for (int w = 0; w <= W; w++)
            {
                if (i == 0 || w == 0)
                    dp[i, w] = 0;
                else if (weights[i - 1] <= w)
                    dp[i, w] = Math.Max(values[i - 1] + dp[i - 1, w - weights[i - 1]], dp[i - 1, w]);
                else
                    dp[i, w] = dp[i - 1, w];
            }
        }

        return dp[n, W];
    }

    static void Main()
    {
        string[] lines = File.ReadAllLines("input.txt");
        int W = int.Parse(lines[0].Split()[0]);
        int n = int.Parse(lines[0].Split()[1]);

        int[] values = new int[n];
        int[] weights = new int[n];

        for (int i = 0; i < n; i++)
        {
            string[] item = lines[i + 1].Split();
            values[i] = int.Parse(item[0]);
            weights[i] = int.Parse(item[1]);
        }

        int result = Knapsack(W, weights, values, n);
        File.WriteAllText("output.txt", result.ToString());
    }
}