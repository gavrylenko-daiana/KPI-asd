using System.Text;

namespace Lab_3;

public class Program
{
    static void Main(string[] args)
    {
        int[] nums = GetConsoleInput();
        FormulaTree tree = new FormulaTree('/');
        object[] values = { nums[3], '*', nums[0], '+', nums[2], nums[1] };
        foreach (object value in values)
        {
            tree.Insert(value);
        }
        Console.WriteLine($"\nTree:\n{tree.LevelTraverse()}");
        Console.ReadLine();
    }

    static int[] GetConsoleInput()
    {
        Console.WriteLine("Enter 4 numbers in formula (a*(b+c))/d:");
        int[] nums = new int[4];
        for (int i = 0; i < nums.Length; i++)
        {
            string? input = Console.ReadLine();
            while (!int.TryParse(input, out _))
            {
                Console.WriteLine("Wrong input format");
                input = Console.ReadLine();
            }
            nums[i] = int.Parse(input);
        }
        return nums;
    }
}
